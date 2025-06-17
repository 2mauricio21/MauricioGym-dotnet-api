using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;
using MauricioGym.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Api.Controllers
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

        [HttpGet("pessoa/{pessoaId}")]
        public async Task<ActionResult<IEnumerable<CheckInEntity>>> ListarPorPessoa(int pessoaId)
        {
            var checkins = await _checkInService.ListarPorPessoaAsync(pessoaId);
            return Ok(checkins);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CheckInEntity>> ObterPorId(int id)
        {
            var checkin = await _checkInService.ObterPorIdAsync(id);
            if (checkin == null) return NotFound();
            return Ok(checkin);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Criar([FromBody] CheckInEntity checkIn)
        {
            var id = await _checkInService.CriarAsync(checkIn);
            return CreatedAtAction(nameof(ObterPorId), new { id }, id);
        }

        [HttpGet("pessoa/{pessoaId}/mes/{ano}/{mes}")]
        public async Task<ActionResult<int>> ContarCheckInsPorPessoaMes(int pessoaId, int ano, int mes)
        {
            var total = await _checkInService.ContarCheckInsPorPessoaMesAsync(pessoaId, ano, mes);
            return Ok(total);
        }
    }
}
