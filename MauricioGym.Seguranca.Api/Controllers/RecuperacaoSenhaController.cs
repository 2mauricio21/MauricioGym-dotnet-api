using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MauricioGym.Seguranca.Services.Interfaces;
using MauricioGym.Infra.Controller;

namespace MauricioGym.Seguranca.Api.Controllers
{
    /// <summary>
    /// Controller responsável pelas operações de recuperação de senha
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RecuperacaoSenhaController : ApiController
    {
        private readonly IRecuperacaoSenhaService _recuperacaoSenhaService;

        public RecuperacaoSenhaController(IRecuperacaoSenhaService recuperacaoSenhaService)
        {
            _recuperacaoSenhaService = recuperacaoSenhaService;
        }

        /// <summary>
        /// Solicita a recuperação de senha para um email
        /// </summary>
        /// <param name="request">Email para recuperação</param>
        /// <returns>Confirmação da solicitação</returns>
        [HttpPost("solicitar")]
        public async Task<IActionResult> SolicitarRecuperacao([FromBody] SolicitarRecuperacaoRequestDto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    return BadRequest("Email é obrigatório");
                }

                // Validar formato do email
                if (!IsValidEmail(request.Email))
                {
                    return BadRequest("Formato de email inválido");
                }

                var enderecoIP = HttpContext.Connection.RemoteIpAddress?.ToString();
                var userAgent = Request.Headers.UserAgent.ToString();

                var resultado = await _recuperacaoSenhaService.SolicitarRecuperacaoAsync(request.Email, enderecoIP, userAgent);

                if (resultado.OcorreuErro)
                    return BadRequest(resultado.MensagemErro);

                return Ok("Se o email estiver cadastrado, você receberá as instruções para recuperação de senha");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro interno do servidor");
            }
        }

        /// <summary>
        /// Valida um token de recuperação de senha
        /// </summary>
        /// <param name="request">Token a ser validado</param>
        /// <returns>Confirmação se o token é válido</returns>
        [HttpPost("validar-token")]
        public async Task<IActionResult> ValidarToken([FromBody] ValidarTokenRecuperacaoRequestDto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Token))
                {
                    return BadRequest("Token é obrigatório");
                }

                var resultado = await _recuperacaoSenhaService.ValidarTokenRecuperacaoAsync(request.Token);
                if (resultado.OcorreuErro)
                    return BadRequest(resultado.MensagemErro);

                return Ok(resultado.Retorno);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro interno do servidor");
            }
        }

        /// <summary>
        /// Redefine a senha usando um token de recuperação
        /// </summary>
        /// <param name="request">Token e nova senha</param>
        /// <returns>Confirmação da redefinição</returns>
        [HttpPost("redefinir")]
        public async Task<IActionResult> RedefinirSenha([FromBody] RedefinirSenhaRequestDto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Token) || string.IsNullOrWhiteSpace(request.NovaSenha))
                {
                    return BadRequest("Token e nova senha são obrigatórios");
                }

                // Validar força da senha
                if (request.NovaSenha.Length < 6)
                {
                    return BadRequest("A senha deve ter pelo menos 6 caracteres");
                }

                var resultado = await _recuperacaoSenhaService.RedefinirSenhaAsync(request.Token, request.NovaSenha);
                if (resultado.OcorreuErro)
                    return BadRequest(resultado.MensagemErro);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Erro interno do servidor");
            }
        }

        /// <summary>
        /// Lista as solicitações de recuperação de senha do usuário autenticado
        /// </summary>
        /// <returns>Lista de solicitações</returns>
        [HttpGet("minhas-solicitacoes")]
        [Authorize]
        public async Task<IActionResult> MinhasSolicitacoes()
        {
            try
            {
                if (IdUsuario == 0)
                {
                    return BadRequest("Token inválido");
                }

                var resultado = await _recuperacaoSenhaService.ListarSolicitacoesPorUsuarioAsync(IdUsuario);
                if (resultado.OcorreuErro)
                    return BadRequest(resultado.MensagemErro);

                var solicitacoes = resultado.Retorno?.Select(s => new
                {
                    s.IdRecuperacao,
                    s.Email,
                    s.DataSolicitacao,
                    s.DataExpiracao,
                    s.Utilizado,
                    s.DataUtilizacao,
                    s.EnderecoIP,
                    Expirado = s.DataExpiracao < DateTime.Now
                });

                return Ok(solicitacoes);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro interno do servidor");
            }
        }

        /// <summary>
        /// Limpa solicitações de recuperação expiradas (endpoint administrativo)
        /// </summary>
        /// <returns>Confirmação da limpeza</returns>
        [HttpPost("limpar-expiradas")]
        [Authorize] // Em produção, adicionar role de administrador
        public async Task<IActionResult> LimparSolicitacoesExpiradas()
        {
            try
            {
                var resultado = await _recuperacaoSenhaService.LimparSolicitacoesExpiradasAsync();
                if (resultado.OcorreuErro)
                    return BadRequest(resultado.MensagemErro);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Erro interno do servidor");
            }
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

    // DTOs para as requisições
    public class SolicitarRecuperacaoRequestDto
    {
        public string Email { get; set; } = string.Empty;
    }

    public class ValidarTokenRecuperacaoRequestDto
    {
        public string Token { get; set; } = string.Empty;
    }

    public class RedefinirSenhaRequestDto
    {
        public string Token { get; set; } = string.Empty;
        public string NovaSenha { get; set; } = string.Empty;
    }
}