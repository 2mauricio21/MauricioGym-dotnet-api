using MauricioGym.Infra.Controller;
using MauricioGym.Infra.Shared;
using MauricioGym.Seguranca.Entities;
using MauricioGym.Seguranca.Entities.Request;
using MauricioGym.Seguranca.Repositories.SqlServer.Interfaces;
using MauricioGym.Seguranca.Services.Interfaces;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Seguranca.Api.Controllers
{
    /// <summary>
    /// Controller responsável pelo cadastro de novos usuários
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CadastroController : ApiController
    {
        private readonly IAutenticacaoService _autenticacaoService;
        private readonly IAutenticacaoSqlServerRepository _autenticacaoRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IHashService _hashService;

        public CadastroController(
            IAutenticacaoService autenticacaoService,
            IAutenticacaoSqlServerRepository autenticacaoRepository,
            IUsuarioService usuarioService,
            IHashService hashService)
        {
            _autenticacaoService = autenticacaoService;
            _autenticacaoRepository = autenticacaoRepository;
            _usuarioService = usuarioService;
            _hashService = hashService;
        }

        /// <summary>
        /// Cadastra um novo usuário no sistema
        /// </summary>
        /// <param name="request">Dados do usuário a ser cadastrado</param>
        /// <returns>Dados do usuário cadastrado e token de acesso</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CadastrarUsuario([FromBody] CadastroUsuarioRequestEntity request)
        {
            try
            {
                // Validações básicas
                var validationResult = ValidarDadosCadastro(request);
                if (!string.IsNullOrEmpty(validationResult))
                {
                    return BadRequest(validationResult);
                }

                // Verificar se email já existe
                var usuarioExistente = await _usuarioService.ConsultarUsuarioPorEmailAsync(request.Email);
                if (!usuarioExistente.OcorreuErro && usuarioExistente.Retorno != null)
                {
                    return BadRequest("Email já cadastrado no sistema");
                }

                // Verificar se CPF já existe
                if (!string.IsNullOrWhiteSpace(request.CPF))
                {
                    var usuarioCpfExistente = await _usuarioService.ConsultarUsuarioPorCPFAsync(request.CPF);
                    if (!usuarioCpfExistente.OcorreuErro && usuarioCpfExistente.Retorno != null)
                    {
                        return BadRequest("CPF já cadastrado no sistema");
                    }
                }

                // Criar entidade do usuário
                var novoUsuario = new UsuarioEntity
                {
                    Nome = request.Nome.Trim(),
                    Email = request.Email.Trim().ToLower(),
                    CPF = request.CPF?.Trim(),
                    Telefone = request.Telefone?.Trim(),
                    DataNascimento = request.DataNascimento,
                    Endereco = request.Endereco?.Trim(),
                    Cidade = request.Cidade?.Trim(),
                    Estado = request.Estado?.Trim(),
                    CEP = request.CEP?.Trim(),
                    Ativo = true,
                    DataCadastro = DateTime.Now
                };

                // Criar usuário
                var resultadoUsuario = await _usuarioService.IncluirUsuarioAsync(novoUsuario);
                if (resultadoUsuario.OcorreuErro)
                    return BadRequest(resultadoUsuario.MensagemErro);

                var idUsuarioCriado = resultadoUsuario.Retorno;

                // Buscar o usuário criado
                var usuarioCriadoResult = await _usuarioService.ConsultarUsuarioAsync(idUsuarioCriado);
                if (usuarioCriadoResult.OcorreuErro || usuarioCriadoResult.Retorno == null)
                    return BadRequest("Erro ao buscar usuário criado");

                var usuarioCriado = usuarioCriadoResult.Retorno;

                // Criar conta de autenticação
                var senhaHash = _hashService.GerarHashSHA256(request.Senha);
                var novaAutenticacao = new AutenticacaoEntity
                {
                    IdUsuario = idUsuarioCriado,
                    Email = request.Email.Trim().ToLower(),
                    Senha = senhaHash,
                    TentativasLogin = 0,
                    ContaBloqueada = false,
                    DataCriacao = DateTime.Now
                };

                var resultadoConta = await _autenticacaoRepository.IncluirAutenticacaoAsync(novaAutenticacao);
                if (resultadoConta.OcorreuErro)
                {
                    // Se falhar ao criar a conta, remover o usuário criado
                    await _usuarioService.ExcluirUsuarioAsync(idUsuarioCriado);
                    return BadRequest(resultadoConta.MensagemErro);
                }

                // Fazer login automático após cadastro
                var resultadoLogin = await _autenticacaoService.LoginAsync(request.Email, request.Senha);
                if (resultadoLogin.OcorreuErro)
                {
                    return Ok(new
                    {
                        message = "Usuário cadastrado com sucesso",
                        usuario = new
                        {
                            usuarioCriado.IdUsuario,
                            usuarioCriado.Nome,
                            usuarioCriado.Email,
                            usuarioCriado.CPF,
                            usuarioCriado.Telefone,
                            usuarioCriado.DataNascimento,
                            usuarioCriado.DataCadastro
                        },
                        loginAutomatico = false
                    });
                }

                return Ok(new
                {
                    message = "Usuário cadastrado com sucesso",
                    usuario = new
                    {
                        usuarioCriado.IdUsuario,
                        usuarioCriado.Nome,
                        usuarioCriado.Email,
                        usuarioCriado.CPF,
                        usuarioCriado.Telefone,
                        usuarioCriado.DataNascimento,
                        usuarioCriado.DataCadastro
                    },
                    autenticacao = resultadoLogin.Retorno,
                    loginAutomatico = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no cadastro: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return BadRequest("Erro interno do servidor");
            }
        }

        /// <summary>
        /// Verifica se um email já está cadastrado
        /// </summary>
        /// <param name="email">Email a ser verificado</param>
        /// <returns>Informação se o email existe</returns>
        [HttpGet("verificar-email/{email}")]
        [AllowAnonymous]
        public async Task<IActionResult> VerificarEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return BadRequest("Email é obrigatório");
                }

                if (!IsValidEmail(email))
                {
                    return BadRequest("Formato de email inválido");
                }

                var resultado = await _usuarioService.ConsultarUsuarioPorEmailAsync(email.Trim().ToLower());
                var emailExiste = !resultado.OcorreuErro && resultado.Retorno != null;

                return Ok(new { emailExiste });
            }
            catch (Exception ex)
            {
                return BadRequest("Erro interno do servidor");
            }
        }

        /// <summary>
        /// Verifica se um CPF já está cadastrado
        /// </summary>
        /// <param name="cpf">CPF a ser verificado</param>
        /// <returns>Informação se o CPF existe</returns>
        [HttpGet("verificar-cpf/{cpf}")]
        [AllowAnonymous]
        public async Task<IActionResult> VerificarCpf(string cpf)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cpf))
                {
                    return BadRequest("CPF é obrigatório");
                }

                var resultado = await _usuarioService.ConsultarUsuarioPorCPFAsync(cpf.Trim());
                var cpfExiste = !resultado.OcorreuErro && resultado.Retorno != null;

                return Ok(new { cpfExiste });
            }
            catch (Exception ex)
            {
                return BadRequest("Erro interno do servidor");
            }
        }

        private static string ValidarDadosCadastro(CadastroUsuarioRequestEntity request)
        {
            if (string.IsNullOrWhiteSpace(request.Nome))
                return "Nome é obrigatório";

            if (request.Nome.Trim().Length < 2)
                return "Nome deve ter pelo menos 2 caracteres";

            if (string.IsNullOrWhiteSpace(request.Email))
                return "Email é obrigatório";

            if (!IsValidEmail(request.Email))
                return "Formato de email inválido";

            if (string.IsNullOrWhiteSpace(request.Senha))
                return "Senha é obrigatória";

            if (request.Senha.Length < 6)
                return "Senha deve ter pelo menos 6 caracteres";

            if (string.IsNullOrWhiteSpace(request.CPF))
                return "O CPF é obrigatório.";

            if (!Common.IsValidCpf(request.CPF))
                return "CPF inválido.";

            if (request.DataNascimento.HasValue)
            {
                var idade = DateTime.Now.Year - request.DataNascimento.Value.Year;
                if (request.DataNascimento.Value.Date > DateTime.Now.AddYears(-idade)) idade--;

                if (idade < 16)
                    return "Usuário deve ter pelo menos 16 anos";

                if (idade > 120)
                    return "Data de nascimento inválida";
            }

            return string.Empty;
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


    }
}