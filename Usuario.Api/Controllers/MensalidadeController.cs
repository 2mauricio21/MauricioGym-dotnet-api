using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Usuario.Api.Controllers
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

        [HttpGet("{id}")]
        public async Task<ActionResult<MensalidadeEntity>> ObterPorId(int id)
        {
            var mensalidade = await _mensalidadeService.ObterPorIdAsync(id);
            if (mensalidade == null) return NotFound();
            return Ok(mensalidade);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<MensalidadeEntity>>> ListarPorUsuario(int usuarioId)
        {
            var mensalidades = await _mensalidadeService.ListarPorUsuarioAsync(usuarioId);
            return Ok(mensalidades);
        }

        [HttpGet("usuario/{usuarioId}/atual")]
        public async Task<ActionResult<MensalidadeEntity>> ObterMensalidadeAtual(int usuarioId)
        {
            var mensalidade = await _mensalidadeService.ObterMensalidadeAtualAsync(usuarioId);
            if (mensalidade == null) return NotFound();
            return Ok(mensalidade);
        }

        [HttpPost("pagamento")]
        public async Task<ActionResult<int>> PagarMensalidade([FromBody] PagamentoMensalidadeRequest request)
        {
            var id = await _mensalidadeService.RegistrarPagamentoMensalidadeAsync(
                request.UsuarioId, 
                request.PlanoId, 
                request.Meses, 
                request.ValorBase, 
                request.DataInicio);
            
            return CreatedAtAction(nameof(ObterPorId), new { id }, id);
        }

        [HttpGet("usuario/{usuarioId}/status")]
        public async Task<ActionResult<bool>> VerificarMensalidadeEmDia(int usuarioId)
        {
            var emDia = await _mensalidadeService.VerificarMensalidadeEmDiaAsync(usuarioId);
            return Ok(emDia);
        }

        [HttpPost("calcular-desconto")]
        public ActionResult<decimal> CalcularValorComDesconto([FromBody] CalculoDescontoRequest request)
        {
            var valorComDesconto = _mensalidadeService.CalcularValorComDesconto(request.Meses, request.ValorBase);
            return Ok(valorComDesconto);
        }
    }

    public class PagamentoMensalidadeRequest
    {
        public int UsuarioId { get; set; }
        public int PlanoId { get; set; }
        public int Meses { get; set; }
        public decimal ValorBase { get; set; }
        public DateTime DataInicio { get; set; }
    }

    public class CalculoDescontoRequest
    {
        public int Meses { get; set; }
        public decimal ValorBase { get; set; }
    }
}
