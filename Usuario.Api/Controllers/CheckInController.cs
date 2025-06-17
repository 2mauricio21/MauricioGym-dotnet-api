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
    public class CheckInController : ControllerBase
    {
        private readonly ICheckInService _checkInService;

        public CheckInController(ICheckInService checkInService)
        {
            _checkInService = checkInService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CheckInEntity>> ObterPorId(int id)
        {
            var checkIn = await _checkInService.ObterPorIdAsync(id);
            if (checkIn == null) return NotFound();
            return Ok(checkIn);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<CheckInEntity>>> ListarPorUsuario(int usuarioId)
        {
            var checkIns = await _checkInService.ListarPorUsuarioAsync(usuarioId);
            return Ok(checkIns);
        }

        [HttpPost]
        public async Task<ActionResult<int>> RealizarCheckIn([FromBody] CheckInEntity checkIn)
        {
            try
            {
                checkIn.DataHora = DateTime.Now;
                var id = await _checkInService.CriarAsync(checkIn);
                return CreatedAtAction(nameof(ObterPorId), new { id }, id);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("usuario/{usuarioId}/mes/{ano}/{mes}")]
        public async Task<ActionResult<int>> ContarCheckInsPorMes(int usuarioId, int ano, int mes)
        {
            var count = await _checkInService.ContarCheckInsPorUsuarioMesAsync(usuarioId, ano, mes);
            return Ok(count);
        }

        [HttpGet("usuario/{usuarioId}/pode-checkin")]
        public async Task<ActionResult<bool>> PodeRealizarCheckIn(int usuarioId)
        {
            var pode = await _checkInService.PodeRealizarCheckInAsync(usuarioId);
            return Ok(pode);
        }
    }
}
