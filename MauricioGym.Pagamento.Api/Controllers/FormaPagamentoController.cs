using MauricioGym.Pagamento.Entities;
using MauricioGym.Pagamento.Services.Interfaces;
using MauricioGym.Infra.Controller;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Pagamento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormaPagamentoController : ApiController
    {
        private readonly IFormaPagamentoService formaPagamentoService;

        public FormaPagamentoController(IFormaPagamentoService formaPagamentoService)
        {
            this.formaPagamentoService = formaPagamentoService;
        }

        [HttpPost]
        public async Task<IActionResult> IncluirAsync(FormaPagamentoEntity formaPagamento)
        {
            var incluir = await formaPagamentoService.IncluirFormaPagamentoAsync(formaPagamento, IdUsuario);
            if (incluir.OcorreuErro)
                return BadRequest(incluir.MensagemErro);

            return Ok(incluir.Retorno);
        }

        [HttpPut]
        public async Task<IActionResult> AlterarAsync(FormaPagamentoEntity formaPagamento)
        {
            var alterar = await formaPagamentoService.AlterarFormaPagamentoAsync(formaPagamento, IdUsuario);
            if (alterar.OcorreuErro)
                return BadRequest(alterar.MensagemErro);

            return Ok();
        }

        [HttpDelete("{idFormaPagamento}")]
        public async Task<IActionResult> ExcluirAsync([FromRoute] int idFormaPagamento)
        {
            var excluir = await formaPagamentoService.ExcluirFormaPagamentoAsync(idFormaPagamento, IdUsuario);
            if (excluir.OcorreuErro)
                return BadRequest(excluir.MensagemErro);

            return Ok();
        }

        [HttpGet("{idFormaPagamento}")]
        public async Task<IActionResult> ConsultarAsync([FromRoute] int idFormaPagamento)
        {
            var consultar = await formaPagamentoService.ConsultarFormaPagamentoAsync(idFormaPagamento);
            if (consultar.OcorreuErro)
                return BadRequest(consultar.MensagemErro);

            return Ok(consultar.Retorno);
        }

        [HttpGet]
        public async Task<IActionResult> ListarAsync()
        {
            var listar = await formaPagamentoService.ListarFormasPagamentoAsync();
            if (listar.OcorreuErro)
                return BadRequest(listar.MensagemErro);

            return Ok(listar.Retorno);
        }
    }
}