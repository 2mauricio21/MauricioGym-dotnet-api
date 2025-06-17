using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;
using MauricioGym.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministradorController : ControllerBase
    {
        private readonly IAdministradorService _administradorService;

        public AdministradorController(IAdministradorService administradorService)
        {
            _administradorService = administradorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdministradorEntity>>> Listar()
        {
            var administradores = await _administradorService.ListarAsync();
            return Ok(administradores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdministradorEntity>> ObterPorId(int id)
        {
            var administrador = await _administradorService.ObterPorIdAsync(id);
            if (administrador == null) return NotFound();
            return Ok(administrador);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Criar([FromBody] AdministradorEntity administrador)
        {
            var id = await _administradorService.CriarAsync(administrador);
            return CreatedAtAction(nameof(ObterPorId), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] AdministradorEntity administrador)
        {
            if (id != administrador.Id) return BadRequest();
            var atualizado = await _administradorService.AtualizarAsync(administrador);
            if (!atualizado) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            var removido = await _administradorService.RemoverLogicamenteAsync(id);
            if (!removido) return NotFound();
            return NoContent();
        }
    }
}
