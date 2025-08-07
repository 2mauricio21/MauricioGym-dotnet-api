using MauricioGym.Pagamento.Entities;
using MauricioGym.Pagamento.Services.Interfaces;
using MauricioGym.Infra.Controller;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Pagamento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentoController : ApiController
    {
        private readonly IPagamentoService pagamentoService;

        public PagamentoController(IPagamentoService pagamentoService)
        {
            this.pagamentoService = pagamentoService;
        }

        [HttpPost]
        public async Task<IActionResult> IncluirAsync(PagamentoEntity pagamento)
        {
            var incluir = await pagamentoService.IncluirPagamentoAsync(pagamento, IdUsuario);
            if (incluir.OcorreuErro)
                return BadRequest(incluir.MensagemErro);

            return Ok(incluir.Retorno);
        }

        [HttpPut]
        public async Task<IActionResult> AlterarAsync(PagamentoEntity pagamento)
        {
            var alterar = await pagamentoService.AlterarPagamentoAsync(pagamento, IdUsuario);
            if (alterar.OcorreuErro)
                return BadRequest(alterar.MensagemErro);

            return Ok();
        }

        [HttpDelete("{idPagamento}")]
        public async Task<IActionResult> ExcluirAsync([FromRoute] int idPagamento)
        {
            var excluir = await pagamentoService.CancelarPagamentoAsync(idPagamento, IdUsuario);
            if (excluir.OcorreuErro)
                return BadRequest(excluir.MensagemErro);

            return Ok();
        }

        [HttpGet("{idPagamento}")]
        public async Task<IActionResult> ConsultarAsync([FromRoute] int idPagamento)
        {
            var consultar = await pagamentoService.ConsultarPagamentoAsync(idPagamento);
            if (consultar.OcorreuErro)
                return BadRequest(consultar.MensagemErro);

            return Ok(consultar.Retorno);
        }

        [HttpGet]
        public async Task<IActionResult> ListarAsync()
        {
            var listar = await pagamentoService.ListarPagamentosAsync();
            if (listar.OcorreuErro)
                return BadRequest(listar.MensagemErro);

            return Ok(listar.Retorno);
        }
    }
}