using Microsoft.AspNetCore.Mvc;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Interfaces;

namespace MauricioGym.Usuario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> IncluirUsuario([FromBody] UsuarioEntity usuario)
        {
            var resultado = await _usuarioService.IncluirUsuarioAsync(usuario);
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            return CreatedAtAction(nameof(ConsultarUsuario), new { id = resultado.Retorno }, new { id = resultado.Retorno });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ConsultarUsuario(int id)
        {
            var resultado = await _usuarioService.ConsultarUsuarioAsync(id);
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            if (resultado.Retorno == null)
                return NotFound();

            return Ok(resultado.Retorno);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> ConsultarUsuarioPorEmail(string email)
        {
            var resultado = await _usuarioService.ConsultarUsuarioPorEmailAsync(email);
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            if (resultado.Retorno == null)
                return NotFound();

            return Ok(resultado.Retorno);
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<IActionResult> ConsultarUsuarioPorCPF(string cpf)
        {
            var resultado = await _usuarioService.ConsultarUsuarioPorCPFAsync(cpf);
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            if (resultado.Retorno == null)
                return NotFound();

            return Ok(resultado.Retorno);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarUsuario(int id, [FromBody] UsuarioEntity usuario)
        {
            if (id != usuario.IdUsuario)
                return BadRequest("ID do usuário não corresponde");

            var resultado = await _usuarioService.AlterarUsuarioAsync(usuario);
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirUsuario(int id)
        {
            var resultado = await _usuarioService.ExcluirUsuarioAsync(id);
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            var resultado = await _usuarioService.ListarUsuariosAsync();
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            return Ok(resultado.Retorno);
        }

        [HttpGet("ativos")]
        public async Task<IActionResult> ListarUsuariosAtivos()
        {
            var resultado = await _usuarioService.ListarUsuariosAtivosAsync();
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            return Ok(resultado.Retorno);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var resultado = await _usuarioService.ValidarLoginAsync(request.Email, request.Senha);
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            if (!resultado.Retorno)
                return Unauthorized();

            var usuarioResultado = await _usuarioService.ConsultarUsuarioPorEmailAsync(request.Email);
            return Ok(usuarioResultado.Retorno);
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}