using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MauricioGym.Seguranca.Services.Interfaces;
using MauricioGym.Seguranca.Entities.Request;
using MauricioGym.Seguranca.Entities.Response;
using MauricioGym.Infra.Controller;

namespace MauricioGym.Seguranca.Api.Controllers
{
    /// <summary>
    /// Controller responsável pelas operações de autenticação
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AutenticacaoController : ApiController
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        /// <summary>
        /// Realiza o login do usuário
        /// </summary>
        /// <param name="request">Dados de login (email e senha)</param>
        /// <returns>Token JWT</returns>
        /// <response code="200">Login realizado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Credenciais inválidas</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(typeof(object), 401)]
        public async Task<IActionResult> Login([FromBody] LoginRequestEntity request)
        {
            var resultado = await _autenticacaoService.LoginAsync(request.Email, request.Senha);
            if (resultado.OcorreuErro)
                return BadRequest(resultado.MensagemErro);

            return Ok(resultado.Retorno);
        }

        /// <summary>
        /// Valida um token JWT
        /// </summary>
        /// <param name="request">Token a ser validado</param>
        /// <returns>Resultado da validação</returns>
        /// <response code="200">Token válido</response>
        /// <response code="400">Token inválido</response>
        [HttpPost("validate-token")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(object), 400)]
        public async Task<IActionResult> ValidateToken([FromBody] ValidateTokenRequestEntity request)
        {
            var resultado = await _autenticacaoService.ValidarTokenAsync(request.Token);
            if (resultado.OcorreuErro)
                return BadRequest(resultado.MensagemErro);

            return Ok(resultado.Retorno);
        }

        /// <summary>
        /// Renova um token JWT usando o refresh token
        /// </summary>
        /// <param name="request">Refresh token</param>
        /// <returns>Novo token JWT</returns>
        /// <response code="200">Token renovado com sucesso</response>
        /// <response code="400">Refresh token inválido</response>
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(object), 400)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestEntity request)
        {
            var resultado = await _autenticacaoService.RenovarTokenAsync(request.RefreshToken);
            if (resultado.OcorreuErro)
                return BadRequest(resultado.MensagemErro);

            return Ok(resultado.Retorno);
        }

        /// <summary>
        /// Realiza o logout do usuário
        /// </summary>
        /// <returns>Confirmação de logout</returns>
        /// <response code="200">Logout realizado com sucesso</response>
        /// <response code="400">Erro no logout</response>
        /// <response code="401">Token inválido</response>
        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(typeof(object), 401)]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
                return BadRequest("Token não encontrado");

            var resultado = await _autenticacaoService.LogoutAsync(token);
            if (resultado.OcorreuErro)
                return BadRequest(resultado.MensagemErro);

            return Ok();
        }

        /// <summary>
        /// Altera a senha do usuário autenticado
        /// </summary>
        /// <param name="request">Senha atual e nova senha</param>
        /// <returns>Confirmação da alteração</returns>
        /// <response code="200">Senha alterada com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Token inválido</response>
        [HttpPost("alterar-senha")]
        [Authorize]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(typeof(object), 401)]
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaRequestEntity request)
        {
            if (IdUsuario == 0)
                return BadRequest("Token inválido");

            var resultado = await _autenticacaoService.AlterarSenhaAsync(IdUsuario, request.SenhaAtual, request.NovaSenha);
            if (resultado.OcorreuErro)
                return BadRequest(resultado.MensagemErro);

            return Ok();
        }
    }
}