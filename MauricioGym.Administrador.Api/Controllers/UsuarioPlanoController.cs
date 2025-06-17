using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Administrador.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioPlanoController : ControllerBase
    {
        private readonly IUsuarioPlanoService _usuarioPlanoService;

        public UsuarioPlanoController(IUsuarioPlanoService usuarioPlanoService)
        {
            _usuarioPlanoService = usuarioPlanoService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioPlanoEntity>> ObterPorId(int id)
        {
            var usuarioPlano = await _usuarioPlanoService.ObterPorIdAsync(id);
            if (usuarioPlano == null) return NotFound();
            return Ok(usuarioPlano);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<UsuarioPlanoEntity>>> ListarPorUsuario(int usuarioId)
        {
            var usuarioPlanos = await _usuarioPlanoService.ListarPorUsuarioAsync(usuarioId);
            return Ok(usuarioPlanos);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Criar([FromBody] UsuarioPlanoEntity usuarioPlano)
        {
            var id = await _usuarioPlanoService.CriarAsync(usuarioPlano);
            return CreatedAtAction(nameof(ObterPorId), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] UsuarioPlanoEntity usuarioPlano)
        {
            if (id != usuarioPlano.Id) return BadRequest();
            var atualizado = await _usuarioPlanoService.AtualizarAsync(usuarioPlano);
            if (!atualizado) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            var removido = await _usuarioPlanoService.RemoverLogicamenteAsync(id);
            if (!removido) return NotFound();
            return NoContent();
        }
    }
}
