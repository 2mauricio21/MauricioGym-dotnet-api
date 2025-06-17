using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;
using MauricioGym.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissaoManipulacaoUsuarioController : ControllerBase
    {
        private readonly IPermissaoManipulacaoUsuarioService _permissaoService;

        public PermissaoManipulacaoUsuarioController(IPermissaoManipulacaoUsuarioService permissaoService)
        {
            _permissaoService = permissaoService;
        }

        [HttpGet("pessoa/{pessoaId}")]
        public async Task<ActionResult<IEnumerable<PermissaoManipulacaoUsuarioEntity>>> ListarPorPessoa(int pessoaId)
        {
            var permissoes = await _permissaoService.ListarPorPessoaAsync(pessoaId);
            return Ok(permissoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PermissaoManipulacaoUsuarioEntity>> ObterPorId(int id)
        {
            var permissao = await _permissaoService.ObterPorIdAsync(id);
            if (permissao == null) return NotFound();
            return Ok(permissao);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Criar([FromBody] PermissaoManipulacaoUsuarioEntity permissao)
        {
            var id = await _permissaoService.CriarAsync(permissao);
            return CreatedAtAction(nameof(ObterPorId), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] PermissaoManipulacaoUsuarioEntity permissao)
        {
            if (id != permissao.Id) return BadRequest();
            var atualizado = await _permissaoService.AtualizarAsync(permissao);
            if (!atualizado) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            var removido = await _permissaoService.RemoverAsync(id);
            if (!removido) return NotFound();
            return NoContent();
        }
    }
}
