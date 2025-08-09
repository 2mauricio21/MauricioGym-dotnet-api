using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Infra.Shared;
using MauricioGym.Seguranca.Services.Interfaces;
using MauricioGym.Seguranca.Repositories.SqlServer.Interfaces;
using MauricioGym.Seguranca.Entities;
using MauricioGym.Usuario.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace MauricioGym.Seguranca.Services
{
    /// <summary>
    /// Serviço para operações de recuperação de senha
    /// </summary>
    public class RecuperacaoSenhaService : IRecuperacaoSenhaService
    {
        private readonly IRecuperacaoSenhaSqlServerRepository _recuperacaoRepository;
        private readonly IAutenticacaoSqlServerRepository _autenticacaoRepository;
        private readonly IUsuarioService _usuarioService;
        private const int TEMPO_EXPIRACAO_HORAS = 2; // 2 horas para usar o token

        public RecuperacaoSenhaService(
            IRecuperacaoSenhaSqlServerRepository recuperacaoRepository,
            IAutenticacaoSqlServerRepository autenticacaoRepository,
            IUsuarioService usuarioService)
        {
            _recuperacaoRepository = recuperacaoRepository;
            _autenticacaoRepository = autenticacaoRepository;
            _usuarioService = usuarioService;
        }

        public async Task<IResultadoValidacao<bool>> SolicitarRecuperacaoAsync(string email, string? enderecoIP = null, string? userAgent = null)
        {
            try
            {
                // Verificar se o usuário existe
                var usuarioResult = await _usuarioService.ConsultarUsuarioPorEmailAsync(email);
                if (usuarioResult.OcorreuErro)
                    return new ResultadoValidacao<bool>(usuarioResult.MensagemErro!);

                if (usuarioResult.Retorno == null)
                {
                    // Por segurança, não informamos se o email existe ou não
                    return new ResultadoValidacao<bool>(true);
                }

                var usuario = usuarioResult.Retorno;

                // Verificar se usuário está ativo
                if (!usuario.Ativo)
                    return new ResultadoValidacao<bool>("Usuário inativo");

                // Invalidar solicitações anteriores
                await _recuperacaoRepository.InvalidarSolicitacoesAnterioresAsync(usuario.IdUsuario);

                // Gerar token de recuperação
                var token = GerarTokenRecuperacao();
                var dataExpiracao = DateTime.Now.AddHours(TEMPO_EXPIRACAO_HORAS);

                var recuperacao = new RecuperacaoSenhaEntity
                {
                    Email = email,
                    Token = token,
                    DataSolicitacao = DateTime.Now,
                    DataExpiracao = dataExpiracao,
                    IdUsuario = usuario.IdUsuario,
                    EnderecoIP = enderecoIP,
                    UserAgent = userAgent,
                    Utilizado = false,
                    Ativo = true
                };

                var resultado = await _recuperacaoRepository.IncluirSolicitacaoAsync(recuperacao);
                if (resultado.OcorreuErro)
                    return new ResultadoValidacao<bool>(resultado.MensagemErro!);

                // TODO: Aqui seria enviado o email com o token
                // Por enquanto, apenas retornamos sucesso
                // Em uma implementação real, você integraria com um serviço de email
                
                return new ResultadoValidacao<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, $"Erro ao solicitar recuperação de senha: {ex.Message}");
            }
        }

        public async Task<IResultadoValidacao<bool>> ValidarTokenRecuperacaoAsync(string token)
        {
            try
            {
                var recuperacaoResult = await _recuperacaoRepository.ConsultarPorTokenAsync(token);
                if (recuperacaoResult.OcorreuErro)
                    return new ResultadoValidacao<bool>(recuperacaoResult.MensagemErro!);

                var isValid = recuperacaoResult.Retorno != null;
                return new ResultadoValidacao<bool>(isValid);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, $"Erro ao validar token de recuperação: {ex.Message}");
            }
        }

        public async Task<IResultadoValidacao<bool>> RedefinirSenhaAsync(string token, string novaSenha)
        {
            try
            {
                // Buscar solicitação pelo token
                var recuperacaoResult = await _recuperacaoRepository.ConsultarPorTokenAsync(token);
                if (recuperacaoResult.OcorreuErro)
                    return new ResultadoValidacao<bool>(recuperacaoResult.MensagemErro!);

                if (recuperacaoResult.Retorno == null)
                    return new ResultadoValidacao<bool>("Token inválido ou expirado");

                var recuperacao = recuperacaoResult.Retorno;

                // Gerar hash da nova senha
                var novaSenhaHash = GerarHashSenha(novaSenha);

                // Alterar senha na autenticação
                var alterarSenhaResult = await _autenticacaoRepository.AlterarSenhaAsync(recuperacao.IdUsuario, novaSenhaHash);
                if (alterarSenhaResult.OcorreuErro)
                    return new ResultadoValidacao<bool>(alterarSenhaResult.MensagemErro!);

                // Marcar token como utilizado
                await _recuperacaoRepository.MarcarComoUtilizadoAsync(recuperacao.IdRecuperacao);

                // Invalidar outras solicitações do usuário
                await _recuperacaoRepository.InvalidarSolicitacoesAnterioresAsync(recuperacao.IdUsuario);

                return new ResultadoValidacao<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, $"Erro ao redefinir senha: {ex.Message}");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<RecuperacaoSenhaEntity>>> ListarSolicitacoesPorUsuarioAsync(int idUsuario)
        {
            try
            {
                var resultado = await _recuperacaoRepository.ListarSolicitacoesPorUsuarioAsync(idUsuario);
                return resultado;
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<RecuperacaoSenhaEntity>>(ex, $"Erro ao listar solicitações: {ex.Message}");
            }
        }

        public async Task<IResultadoValidacao<bool>> LimparSolicitacoesExpiradasAsync()
        {
            try
            {
                var resultado = await _recuperacaoRepository.LimparSolicitacoesExpiradasAsync();
                return resultado;
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, $"Erro ao limpar solicitações expiradas: {ex.Message}");
            }
        }

        private static string GerarTokenRecuperacao()
        {
            // Gerar um token seguro de 32 bytes
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            
            // Converter para string hexadecimal
            return Convert.ToHexString(randomBytes).ToLower();
        }

        private static string GerarHashSenha(string senha)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}