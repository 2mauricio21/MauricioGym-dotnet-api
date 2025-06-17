using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Administrador.Api.Controllers
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

        /// <summary>
        /// Obtém uma permissão específica por ID
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PermissaoManipulacaoUsuarioEntity>> ObterPorId([FromRoute] int id)
        {
            try
            {
                var permissao = await _permissaoService.ObterPorIdAsync(id);
                if (permissao == null) return NotFound($"Permissão com ID {id} não encontrada");
                return Ok(permissao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter permissão: {ex.Message}");
            }
        }
        /// <summary>
        /// Lista permissões por usuário
        /// </summary>
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<PermissaoManipulacaoUsuarioEntity>>> ListarPorUsuario(int usuarioId)
        {
            var permissoes = await _permissaoService.ListarPorUsuarioAsync(usuarioId);
            return Ok(permissoes);
        }

        /// <summary>
        /// Cria uma nova permissão de manipulação de usuário
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<int>> Criar([FromBody] PermissaoManipulacaoUsuarioEntity permissao)
        {
            var id = await _permissaoService.CriarAsync(permissao);
            return CreatedAtAction(nameof(ObterPorId), new { id }, id);
        }

        /// <summary>
        /// Atualiza uma permissão existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] PermissaoManipulacaoUsuarioEntity permissao)
        {
            if (id != permissao.Id) return BadRequest();
            var atualizado = await _permissaoService.AtualizarAsync(permissao);
            if (!atualizado) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Remove uma permissão (remoção lógica)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            var removido = await _permissaoService.RemoverAsync(id);
            if (!removido) return NotFound();
            return NoContent();
        }
    }
}
