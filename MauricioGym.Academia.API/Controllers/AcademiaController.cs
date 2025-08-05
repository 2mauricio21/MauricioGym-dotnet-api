using Microsoft.AspNetCore.Mvc;
using MauricioGym.Academia.Services.Interfaces;
using MauricioGym.Academia.Entities;

namespace MauricioGym.Academia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcademiaController : ControllerBase
    {
        private readonly IAcademiaService _academiaService;

        public AcademiaController(IAcademiaService academiaService)
        {
            _academiaService = academiaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _academiaService.ListarAcademiasAsync();
            if (result.Sucesso)
                return Ok(result.Dados);
            
            return BadRequest(result.Erros);
        }

        [HttpGet("ativas")]
        public async Task<IActionResult> GetAtivas()
        {
            var result = await _academiaService.ListarAcademiasAtivasAsync();
            if (result.Sucesso)
                return Ok(result.Dados);
            
            return BadRequest(result.Erros);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _academiaService.ConsultarAcademiaAsync(id);
            if (result.Sucesso)
                return Ok(result.Dados);
            
            return NotFound(result.Erros);
        }

        [HttpGet("cnpj/{cnpj}")]
        public async Task<IActionResult> GetByCnpj(string cnpj)
        {
            var result = await _academiaService.ConsultarAcademiaPorCNPJAsync(cnpj);
            if (result.Sucesso)
                return Ok(result.Dados);
            
            return NotFound(result.Erros);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AcademiaEntity academia)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _academiaService.IncluirAcademiaAsync(academia);
            if (result.Sucesso)
                return CreatedAtAction(nameof(GetById), new { id = result.Dados.IdAcademia }, result.Dados);
            
            return BadRequest(result.Erros);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AcademiaEntity academia)
        {
            if (id != academia.IdAcademia)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _academiaService.AlterarAcademiaAsync(academia);
            if (result.Sucesso)
                return NoContent();
            
            return BadRequest(result.Erros);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _academiaService.ExcluirAcademiaAsync(id);
            if (result.Sucesso)
                return NoContent();
            
            return BadRequest(result.Erros);
        }
    }
}