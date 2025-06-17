using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Administrador.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanoController : ControllerBase
    {
        private readonly IPlanoService _planoService;

        public PlanoController(IPlanoService planoService)
        {
            _planoService = planoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanoEntity>>> Listar()
        {
            var planos = await _planoService.ListarAsync();
            return Ok(planos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlanoEntity>> ObterPorId(int id)
        {
            var plano = await _planoService.ObterPorIdAsync(id);
            if (plano == null) return NotFound();
            return Ok(plano);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Criar([FromBody] PlanoEntity plano)
        {
            var id = await _planoService.CriarAsync(plano);
            return CreatedAtAction(nameof(ObterPorId), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] PlanoEntity plano)
        {
            if (id != plano.Id) return BadRequest();
            var atualizado = await _planoService.AtualizarAsync(plano);
            if (!atualizado) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            var removido = await _planoService.RemoverLogicamenteAsync(id);
            if (!removido) return NotFound();
            return NoContent();
        }
    }
}
