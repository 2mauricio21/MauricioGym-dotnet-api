using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;
using MauricioGym.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MensalidadeController : ControllerBase
    {
        private readonly IMensalidadeService _mensalidadeService;

        public MensalidadeController(IMensalidadeService mensalidadeService)
        {
            _mensalidadeService = mensalidadeService;
        }

        [HttpGet("pessoa/{pessoaId}")]
        public async Task<ActionResult<IEnumerable<MensalidadeEntity>>> ListarPorPessoa(int pessoaId)
        {
            var mensalidades = await _mensalidadeService.ListarPorPessoaAsync(pessoaId);
            return Ok(mensalidades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MensalidadeEntity>> ObterPorId(int id)
        {
            var mensalidade = await _mensalidadeService.ObterPorIdAsync(id);
            if (mensalidade == null) return NotFound();
            return Ok(mensalidade);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Criar([FromBody] MensalidadeEntity mensalidade)
        {
            var id = await _mensalidadeService.CriarAsync(mensalidade);
            return CreatedAtAction(nameof(ObterPorId), new { id }, id);
        }

        [HttpGet("total-recebido")]
        public async Task<ActionResult<decimal>> ObterTotalRecebido()
        {
            var total = await _mensalidadeService.ObterTotalRecebidoAsync();
            return Ok(total);
        }

        [HttpPost("registrar-com-desconto")]
        public async Task<ActionResult<int>> RegistrarComDesconto(int pessoaId, int planoId, int meses, decimal valorBase, DateTime inicio)
        {
            var id = await _mensalidadeService.RegistrarMensalidadeComDesconto(pessoaId, planoId, meses, valorBase, inicio);
            return Created("/api/mensalidade/" + id, id);
        }
    }
}
