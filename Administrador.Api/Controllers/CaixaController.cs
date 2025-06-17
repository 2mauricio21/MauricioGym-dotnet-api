using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Administrador.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaixaController : ControllerBase
    {
        private readonly ICaixaService _caixaService;

        public CaixaController(ICaixaService caixaService)
        {
            _caixaService = caixaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaixaEntity>>> Listar()
        {
            var caixas = await _caixaService.ListarAsync();
            return Ok(caixas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CaixaEntity>> ObterPorId(int id)
        {
            var caixa = await _caixaService.ObterPorIdAsync(id);
            if (caixa == null) return NotFound();
            return Ok(caixa);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Criar([FromBody] CaixaEntity caixa)
        {
            var id = await _caixaService.CriarAsync(caixa);
            return CreatedAtAction(nameof(ObterPorId), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] CaixaEntity caixa)
        {
            if (id != caixa.Id) return BadRequest();
            var atualizado = await _caixaService.AtualizarAsync(caixa);
            if (!atualizado) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            var removido = await _caixaService.RemoverAsync(id);
            if (!removido) return NotFound();
            return NoContent();
        }
    }
}
