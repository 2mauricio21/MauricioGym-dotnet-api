using MauricioGym.Infra.Services;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Seguranca.Services.Interfaces;
using MauricioGym.Seguranca.Services.Validators;
using MauricioGym.Seguranca.Repositories.SqlServer.Interfaces;
using MauricioGym.Seguranca.Entities;
using MauricioGym.Usuario.Services.Interfaces;
using MauricioGym.Infra.Entities;
using System.Security.Cryptography;

namespace MauricioGym.Seguranca.Services
{
    /// <summary>
    /// Serviço para operações de autenticação
    /// </summary>
    public class AutenticacaoService : ServiceBase<AutenticacaoValidator>, IAutenticacaoService
    {
        private readonly IAutenticacaoSqlServerRepository _autenticacaoRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IJwtService _jwtService;
        private readonly IHashService _hashService;
        private const int MAX_TENTATIVAS_LOGIN = 5;
        private const int TEMPO_BLOQUEIO_MINUTOS = 30;

        public AutenticacaoService(
            IAutenticacaoSqlServerRepository autenticacaoRepository,
            IUsuarioService usuarioService,
            IJwtService jwtService,
            IHashService hashService)
        {
            _autenticacaoRepository = autenticacaoRepository;
            _usuarioService = usuarioService;
            _jwtService = jwtService;
            _hashService = hashService;
        }

        public async Task<IResultadoValidacao<string>> LoginAsync(string email, string senha)
        {
            try
            {
                // Validar entrada
                var validacao = Validator.ValidarLogin(email, senha);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<string>(validacao);

                // Buscar dados de autenticação
                var autenticacaoResult = await _autenticacaoRepository.ConsultarAutenticacaoPorEmailAsync(email);
                if (autenticacaoResult.OcorreuErro)
                    return new ResultadoValidacao<string>(autenticacaoResult.MensagemErro);

                if (autenticacaoResult.Retorno == null)
                    return new ResultadoValidacao<string>("Credenciais inválidas");

                var autenticacao = autenticacaoResult.Retorno;

                // Verificar se a conta está bloqueada
                if (autenticacao.ContaBloqueada)
                {
                    // Verificar se o tempo de bloqueio já passou
                    if (autenticacao.DataBloqueio.HasValue && 
                        autenticacao.DataBloqueio.Value.AddMinutes(TEMPO_BLOQUEIO_MINUTOS) > DateTime.Now)
                    {
                        return new ResultadoValidacao<string>("Conta bloqueada temporariamente. Tente novamente mais tarde.");
                    }
                    else
                    {
                        // Desbloquear conta automaticamente
                        await _autenticacaoRepository.DesbloquearContaAsync(autenticacao.IdUsuario);
                        autenticacao.ContaBloqueada = false;
                        autenticacao.TentativasLogin = 0;
                    }
                }

                // Verificar senha
                var senhaHash = _hashService.GerarHashSHA256(senha);
                if (autenticacao.Senha != senhaHash)
                {
                    // Incrementar tentativas de login
                    autenticacao.TentativasLogin++;
                    await _autenticacaoRepository.AtualizarTentativasLoginAsync(autenticacao.IdUsuario, autenticacao.TentativasLogin);

                    // Bloquear conta se exceder o limite
                    if (autenticacao.TentativasLogin >= MAX_TENTATIVAS_LOGIN)
                    {
                        await _autenticacaoRepository.BloquearContaAsync(autenticacao.IdUsuario);
                        return new ResultadoValidacao<string>("Conta bloqueada devido a muitas tentativas de login inválidas.");
                    }

                    return new ResultadoValidacao<string>("Credenciais inválidas");
                }

                // Buscar dados do usuário
                var usuarioResult = await _usuarioService.ConsultarUsuarioAsync(autenticacao.IdUsuario);
                if (usuarioResult.OcorreuErro || usuarioResult.Retorno == null)
                    return new ResultadoValidacao<string>("Erro ao buscar dados do usuário");

                var usuario = usuarioResult.Retorno;

                // Verificar se usuário está ativo
                if (!usuario.Ativo)
                    return new ResultadoValidacao<string>("Usuário inativo");

                // Gerar tokens
                var usuarioJwt = new MauricioGym.Infra.Entities.Usuario
                {
                    Id = usuario.IdUsuario,
                    Nome = usuario.Nome,
                    Email = usuario.Email
                };

                var token = _jwtService.GenerateToken(usuarioJwt);
                var refreshToken = GerarRefreshToken();
                var expiracaoRefreshToken = DateTime.Now.AddDays(7); // 7 dias

                // Atualizar dados de autenticação
                await _autenticacaoRepository.AtualizarUltimoLoginAsync(autenticacao.IdUsuario);
                await _autenticacaoRepository.AtualizarRefreshTokenAsync(autenticacao.IdUsuario, refreshToken, expiracaoRefreshToken);

                return new ResultadoValidacao<string>(token);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<string>(ex, "Erro ao realizar login");
            }
        }

        public async Task<IResultadoValidacao<bool>> ValidarTokenAsync(string token)
        {
            try
            {
                // Validar entrada
                var validacao = Validator.ValidarToken(token);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                // Validar token usando o JwtService
                var principal = _jwtService.ValidateToken(token);
                if (principal == null)
                    return new ResultadoValidacao<bool>("Token inválido ou expirado");

                // Extrair email do token
                var email = principal.FindFirst("email")?.Value;
                if (string.IsNullOrEmpty(email))
                    return new ResultadoValidacao<bool>("Token não contém informações válidas do usuário");

                // Buscar dados do usuário
                var usuarioResult = await _usuarioService.ConsultarUsuarioPorEmailAsync(email);
                if (usuarioResult.OcorreuErro || usuarioResult.Retorno == null)
                    return new ResultadoValidacao<bool>("Usuário não encontrado");

                var usuario = usuarioResult.Retorno;

                // Verificar se usuário ainda está ativo
                if (!usuario.Ativo)
                    return new ResultadoValidacao<bool>("Usuário inativo");

                return new ResultadoValidacao<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "Erro ao validar token");
            }
        }

        public async Task<IResultadoValidacao<string>> RenovarTokenAsync(string refreshToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(refreshToken))
                    return new ResultadoValidacao<string>("Refresh token é obrigatório");

                // Buscar autenticação pelo refresh token
                var autenticacaoResult = await _autenticacaoRepository.ConsultarPorRefreshTokenAsync(refreshToken);
                if (autenticacaoResult.OcorreuErro)
                    return new ResultadoValidacao<string>(autenticacaoResult.MensagemErro);

                if (autenticacaoResult.Retorno == null)
                    return new ResultadoValidacao<string>("Refresh token inválido ou expirado");

                var autenticacao = autenticacaoResult.Retorno;

                // Buscar dados do usuário
                var usuarioResult = await _usuarioService.ConsultarUsuarioAsync(autenticacao.IdUsuario);
                if (usuarioResult.OcorreuErro || usuarioResult.Retorno == null)
                    return new ResultadoValidacao<string>("Usuário não encontrado");

                var usuario = usuarioResult.Retorno;

                // Verificar se usuário está ativo
                if (!usuario.Ativo)
                    return new ResultadoValidacao<string>("Usuário inativo");

                // Gerar novo token
                var usuarioJwt = new MauricioGym.Infra.Entities.Usuario
                {
                    Id = usuario.IdUsuario,
                    Nome = usuario.Nome,
                    Email = usuario.Email
                };

                var novoToken = _jwtService.GenerateToken(usuarioJwt);
                var novoRefreshToken = GerarRefreshToken();
                var expiracaoRefreshToken = DateTime.Now.AddDays(7);

                // Atualizar refresh token
                await _autenticacaoRepository.AtualizarRefreshTokenAsync(autenticacao.IdUsuario, novoRefreshToken, expiracaoRefreshToken);

                return new ResultadoValidacao<string>(novoToken);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<string>(ex, "Erro ao renovar token");
            }
        }

        public async Task<IResultadoValidacao> LogoutAsync(string token)
        {
            try
            {
                // Validar entrada
                var validacao = Validator.ValidarToken(token);
                if (validacao.OcorreuErro)
                    return validacao;

                // Validar token e extrair email
                var principal = _jwtService.ValidateToken(token);
                if (principal == null)
                    return new ResultadoValidacao("Token inválido ou expirado");

                var email = principal.FindFirst("email")?.Value;
                if (string.IsNullOrEmpty(email))
                    return new ResultadoValidacao("Token não contém informações válidas do usuário");

                // Buscar dados do usuário
                var usuarioResult = await _usuarioService.ConsultarUsuarioPorEmailAsync(email);
                if (usuarioResult.OcorreuErro || usuarioResult.Retorno == null)
                    return new ResultadoValidacao("Usuário não encontrado");

                var usuario = usuarioResult.Retorno;

                // Remover refresh token
                var resultado = await _autenticacaoRepository.RemoverRefreshTokenAsync(usuario.IdUsuario);
                if (resultado.OcorreuErro)
                    return new ResultadoValidacao(resultado.MensagemErro);

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "Erro ao realizar logout");
            }
        }

        public async Task<IResultadoValidacao> AlterarSenhaAsync(int idUsuario, string senhaAtual, string novaSenha)
        {
            try
            {
                // Validar entrada
                var validacao = Validator.ValidarAlteracaoSenha(senhaAtual, novaSenha);
                if (validacao.OcorreuErro)
                    return validacao;

                var validacaoId = Validator.ValidarId(idUsuario);
                if (validacaoId.OcorreuErro)
                    return validacaoId;

                // Buscar autenticação do usuário
                var autenticacaoResult = await _autenticacaoRepository.ConsultarAutenticacaoPorUsuarioAsync(idUsuario);
                if (autenticacaoResult.OcorreuErro)
                    return new ResultadoValidacao(autenticacaoResult.MensagemErro);

                if (autenticacaoResult.Retorno == null)
                    return new ResultadoValidacao("Dados de autenticação não encontrados");

                var autenticacao = autenticacaoResult.Retorno;

                // Verificar senha atual
                var senhaAtualHash = _hashService.GerarHashSHA256(senhaAtual);
                if (autenticacao.Senha != senhaAtualHash)
                    return new ResultadoValidacao("Senha atual incorreta");

                // Alterar senha
                var novaSenhaHash = _hashService.GerarHashSHA256(novaSenha);
                var resultado = await _autenticacaoRepository.AlterarSenhaAsync(idUsuario, novaSenhaHash);
                if (resultado.OcorreuErro)
                    return new ResultadoValidacao(resultado.MensagemErro);

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "Erro ao alterar senha");
            }
        }



        private static string GerarRefreshToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}