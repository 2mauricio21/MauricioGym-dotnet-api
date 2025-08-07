using Microsoft.AspNetCore.Mvc;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Interfaces;

namespace MauricioGym.Usuario.API.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de usuários
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Inclui um novo usuário no sistema
        /// </summary>
        /// <param name="usuario">Dados do usuário a ser incluído</param>
        /// <returns>ID do usuário criado</returns>
        /// <response code="201">Usuário criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(typeof(object), 400)]
        public async Task<IActionResult> IncluirUsuario([FromBody] UsuarioEntity usuario)
        {
            var resultado = await _usuarioService.IncluirUsuarioAsync(usuario);
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            return CreatedAtAction(nameof(ConsultarUsuario), new { id = resultado.Retorno }, new { id = resultado.Retorno });
        }

        /// <summary>
        /// Consulta um usuário pelo ID
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>Dados do usuário</returns>
        /// <response code="200">Usuário encontrado</response>
        /// <response code="404">Usuário não encontrado</response>
        /// <response code="400">Erro na consulta</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsuarioEntity), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(object), 400)]
        public async Task<IActionResult> ConsultarUsuario(int id)
        {
            var resultado = await _usuarioService.ConsultarUsuarioAsync(id);
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            if (resultado.Retorno == null)
                return NotFound();

            return Ok(resultado.Retorno);
        }

        /// <summary>
        /// Consulta um usuário pelo email
        /// </summary>
        /// <param name="email">Email do usuário</param>
        /// <returns>Dados do usuário</returns>
        /// <response code="200">Usuário encontrado</response>
        /// <response code="404">Usuário não encontrado</response>
        /// <response code="400">Erro na consulta</response>
        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(UsuarioEntity), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(object), 400)]
        public async Task<IActionResult> ConsultarUsuarioPorEmail(string email)
        {
            var resultado = await _usuarioService.ConsultarUsuarioPorEmailAsync(email);
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            if (resultado.Retorno == null)
                return NotFound();

            return Ok(resultado.Retorno);
        }

        /// <summary>
        /// Consulta um usuário pelo CPF
        /// </summary>
        /// <param name="cpf">CPF do usuário</param>
        /// <returns>Dados do usuário</returns>
        /// <response code="200">Usuário encontrado</response>
        /// <response code="404">Usuário não encontrado</response>
        /// <response code="400">Erro na consulta</response>
        [HttpGet("cpf/{cpf}")]
        [ProducesResponseType(typeof(UsuarioEntity), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(object), 400)]
        public async Task<IActionResult> ConsultarUsuarioPorCPF(string cpf)
        {
            var resultado = await _usuarioService.ConsultarUsuarioPorCPFAsync(cpf);
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            if (resultado.Retorno == null)
                return NotFound();

            return Ok(resultado.Retorno);
        }

        /// <summary>
        /// Altera os dados de um usuário
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <param name="usuario">Novos dados do usuário</param>
        /// <returns>Confirmação da alteração</returns>
        /// <response code="204">Usuário alterado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(object), 400)]
        public async Task<IActionResult> AlterarUsuario(int id, [FromBody] UsuarioEntity usuario)
        {
            if (id != usuario.IdUsuario)
                return BadRequest("ID do usuário não corresponde");

            var resultado = await _usuarioService.AlterarUsuarioAsync(usuario);
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            return NoContent();
        }

        /// <summary>
        /// Exclui um usuário do sistema
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>Confirmação da exclusão</returns>
        /// <response code="204">Usuário excluído com sucesso</response>
        /// <response code="400">Erro na exclusão</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(object), 400)]
        public async Task<IActionResult> ExcluirUsuario(int id)
        {
            var resultado = await _usuarioService.ExcluirUsuarioAsync(id);
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            return NoContent();
        }

        /// <summary>
        /// Lista todos os usuários do sistema
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="200">Lista de usuários</response>
        /// <response code="400">Erro na consulta</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UsuarioEntity>), 200)]
        [ProducesResponseType(typeof(object), 400)]
        public async Task<IActionResult> ListarUsuarios()
        {
            var resultado = await _usuarioService.ListarUsuariosAsync();
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            return Ok(resultado.Retorno);
        }

        /// <summary>
        /// Lista todos os usuários ativos do sistema
        /// </summary>
        /// <returns>Lista de usuários ativos</returns>
        /// <response code="200">Lista de usuários ativos</response>
        /// <response code="400">Erro na consulta</response>
        [HttpGet("ativos")]
        [ProducesResponseType(typeof(IEnumerable<UsuarioEntity>), 200)]
        [ProducesResponseType(typeof(object), 400)]
        public async Task<IActionResult> ListarUsuariosAtivos()
        {
            var resultado = await _usuarioService.ListarUsuariosAtivosAsync();
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            return Ok(resultado.Retorno);
        }

        /// <summary>
        /// Realiza login do usuário
        /// </summary>
        /// <param name="request">Dados de login (email e senha)</param>
        /// <returns>Dados do usuário autenticado</returns>
        /// <response code="200">Login realizado com sucesso</response>
        /// <response code="401">Credenciais inválidas</response>
        /// <response code="400">Erro na validação</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(UsuarioEntity), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(object), 400)]
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

    /// <summary>
    /// Dados para requisição de login
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Email do usuário
        /// </summary>
        public string Email { get; set; } = string.Empty;
        
        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Senha { get; set; } = string.Empty;
    }
}