using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Administrador.Api.Controllers
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioEntity>>> Listar()
        {
            var usuarios = await _usuarioService.ListarAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioEntity>> ObterPorId(int id)
        {
            var usuario = await _usuarioService.ObterPorIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Criar([FromBody] UsuarioEntity usuario)
        {
            var id = await _usuarioService.CriarAsync(usuario);
            return CreatedAtAction(nameof(ObterPorId), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] UsuarioEntity usuario)
        {
            if (id != usuario.Id) return BadRequest();
            var atualizado = await _usuarioService.AtualizarAsync(usuario);
            if (!atualizado) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            var removido = await _usuarioService.RemoverLogicamenteAsync(id);
            if (!removido) return NotFound();
            return NoContent();
        }
    }
}
