using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;
using MauricioGym.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaPlanoController : ControllerBase
    {
        private readonly IPessoaPlanoService _pessoaPlanoService;

        public PessoaPlanoController(IPessoaPlanoService pessoaPlanoService)
        {
            _pessoaPlanoService = pessoaPlanoService;
        }

        [HttpGet("pessoa/{pessoaId}")]
        public async Task<ActionResult<IEnumerable<PessoaPlanoEntity>>> ListarPorPessoa(int pessoaId)
        {
            var planos = await _pessoaPlanoService.ListarPorPessoaAsync(pessoaId);
            return Ok(planos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaPlanoEntity>> ObterPorId(int id)
        {
            var pessoaPlano = await _pessoaPlanoService.ObterPorIdAsync(id);
            if (pessoaPlano == null) return NotFound();
            return Ok(pessoaPlano);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Criar([FromBody] PessoaPlanoEntity pessoaPlano)
        {
            var id = await _pessoaPlanoService.CriarAsync(pessoaPlano);
            return CreatedAtAction(nameof(ObterPorId), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] PessoaPlanoEntity pessoaPlano)
        {
            if (id != pessoaPlano.Id) return BadRequest();
            var atualizado = await _pessoaPlanoService.AtualizarAsync(pessoaPlano);
            if (!atualizado) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            var removido = await _pessoaPlanoService.RemoverLogicamenteAsync(id);
            if (!removido) return NotFound();
            return NoContent();
        }
    }
}
