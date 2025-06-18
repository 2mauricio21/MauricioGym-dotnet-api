using MauricioGym.Infra.Controller;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Usuario.Api.Controllers
{
    [Route("api/[controller]")]
    public class CheckInCompletoController : ApiController
    {
        private readonly ICheckInCompletoService _checkInCompletoService;

        public CheckInCompletoController(ICheckInCompletoService checkInCompletoService)
        {
            _checkInCompletoService = checkInCompletoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var resultado = await _checkInCompletoService.ObterTodosAsync();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var resultado = await _checkInCompletoService.ObterPorIdAsync(id);
            return Ok(resultado);
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> ObterPorCliente(int clienteId)
        {
            var resultado = await _checkInCompletoService.ObterPorClienteIdAsync(clienteId);
            return Ok(resultado);
        }

        [HttpGet("academia/{academiaId}")]
        public async Task<IActionResult> ObterPorAcademia(int academiaId)
        {
            var resultado = await _checkInCompletoService.ObterPorAcademiaIdAsync(academiaId);
            return Ok(resultado);
        }

        [HttpGet("periodo")]
        public async Task<IActionResult> ObterPorPeriodo([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim, [FromQuery] int? academiaId = null)
        {
            var resultado = await _checkInCompletoService.ObterPorPeriodoAsync(dataInicio, dataFim, academiaId);
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CheckInEntity checkIn)
        {
            var resultado = await _checkInCompletoService.CriarAsync(checkIn);
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] CheckInEntity checkIn)
        {
            checkIn.Id = id;
            var resultado = await _checkInCompletoService.AtualizarAsync(checkIn);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var resultado = await _checkInCompletoService.ExcluirAsync(id);
            return Ok(resultado);
        }

        [HttpGet("{id}/existe")]
        public async Task<IActionResult> Existe(int id)
        {
            var resultado = await _checkInCompletoService.ExisteAsync(id);
            return Ok(resultado);
        }

        [HttpGet("cliente/{clienteId}/academia/{academiaId}/hoje")]
        public async Task<IActionResult> ClienteJaFezCheckInHoje(int clienteId, int academiaId)
        {
            var resultado = await _checkInCompletoService.ClienteJaFezCheckInHojeAsync(clienteId, academiaId);
            return Ok(resultado);
        }

        [HttpGet("cliente/{clienteId}/mes/{ano}/{mes}/contagem")]
        public async Task<IActionResult> ContarCheckInsPorUsuarioMes(int clienteId, int ano, int mes)
        {
            var resultado = await _checkInCompletoService.ContarCheckInsPorUsuarioMesAsync(clienteId, ano, mes);
            return Ok(resultado);
        }
    }
}
