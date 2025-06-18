using MauricioGym.Infra.Controller;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Usuario.Api.Controllers
{
    [Route("api/[controller]")]
    public class CheckInController : ApiController
    {
        private readonly ICheckInService _checkInService;

        public CheckInController(ICheckInService checkInService)
        {
            _checkInService = checkInService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var resultado = await _checkInService.ObterPorIdAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> ListarPorUsuario(int usuarioId)
        {
            var resultado = await _checkInService.ObterPorUsuarioAsync(usuarioId);
            return ProcessarResultado(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> RealizarCheckIn([FromBody] CheckInEntity checkIn)
        {
            checkIn.DataHora = DateTime.Now;
            var resultado = await _checkInService.CriarAsync(checkIn);
            return ProcessarResultado(resultado);
        }

        [HttpGet("usuario/{usuarioId}/mes/{ano}/{mes}")]
        public async Task<IActionResult> ContarCheckInsPorMes(int usuarioId, int ano, int mes)
        {
            var resultado = await _checkInService.ContarCheckInsPorUsuarioMesAsync(usuarioId, ano, mes);
            return ProcessarResultado(resultado);
        }

        [HttpGet("usuario/{usuarioId}/pode-checkin")]
        public async Task<IActionResult> PodeRealizarCheckIn(int usuarioId)
        {
            var resultado = await _checkInService.PodeRealizarCheckInAsync(usuarioId);
            return ProcessarResultado(resultado);
        }
    }
}
