using MauricioGym.Infra.Controller;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Usuario.Api.Controllers
{
    [Route("api/[controller]")]
    public class PagamentoController : ApiController
    {
        private readonly IPagamentoService _pagamentoService;

        public PagamentoController(IPagamentoService pagamentoService)
        {
            _pagamentoService = pagamentoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var resultado = await _pagamentoService.ObterTodosAsync();
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var resultado = await _pagamentoService.ObterPorIdAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> ObterPorCliente(int clienteId)
        {
            var resultado = await _pagamentoService.ObterPorClienteIdAsync(clienteId);
            return ProcessarResultado(resultado);
        }

        [HttpGet("academia/{academiaId}")]
        public async Task<IActionResult> ObterPorAcademia(int academiaId)
        {
            var resultado = await _pagamentoService.ObterPorAcademiaIdAsync(academiaId);
            return ProcessarResultado(resultado);
        }

        [HttpGet("planocliente/{planoClienteId}")]
        public async Task<IActionResult> ObterPorPlanoCliente(int planoClienteId)
        {
            var resultado = await _pagamentoService.ObterPorPlanoClienteIdAsync(planoClienteId);
            return ProcessarResultado(resultado);
        }

        [HttpGet("periodo")]
        public async Task<IActionResult> ObterPorPeriodo([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim, [FromQuery] int? academiaId = null)
        {
            var resultado = await _pagamentoService.ObterPorPeriodoAsync(dataInicio, dataFim, academiaId);
            return ProcessarResultado(resultado);
        }

        [HttpGet("pendentes")]
        public async Task<IActionResult> ObterPendentes([FromQuery] int? academiaId = null)
        {
            var resultado = await _pagamentoService.ObterPendentesAsync(academiaId);
            return ProcessarResultado(resultado);
        }

        [HttpGet("totalrecebido")]
        public async Task<IActionResult> ObterTotalRecebidoPorPeriodo([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim, [FromQuery] int? academiaId = null)
        {
            var resultado = await _pagamentoService.ObterTotalRecebidoPorPeriodoAsync(dataInicio, dataFim, academiaId);
            return ProcessarResultado(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] PagamentoEntity pagamento)
        {
            var resultado = await _pagamentoService.CriarAsync(pagamento);
            return ProcessarResultado(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] PagamentoEntity pagamento)
        {
            pagamento.Id = id;
            var resultado = await _pagamentoService.AtualizarAsync(pagamento);
            return ProcessarResultado(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var resultado = await _pagamentoService.ExcluirAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}/existe")]
        public async Task<IActionResult> Existe(int id)
        {
            var resultado = await _pagamentoService.ExisteAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("atraso/usuario/{usuarioId}")]
        public async Task<IActionResult> ObterPagamentosEmAtrasoPorUsuario(int usuarioId)
        {
            var resultado = await _pagamentoService.ObterPagamentosEmAtrasoPorUsuarioAsync(usuarioId);
            return ProcessarResultado(resultado);
        }
    }
}
