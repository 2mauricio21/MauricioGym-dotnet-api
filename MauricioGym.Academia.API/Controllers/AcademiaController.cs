using Microsoft.AspNetCore.Mvc;
using MauricioGym.Academia.Services.Interfaces;
using MauricioGym.Academia.Entities;
using MauricioGym.Infra.Controller;

namespace MauricioGym.Academia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcademiaController : ApiController
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
            if (result.OcorreuErro)
                return BadRequest(result.MensagemErro);
            
            return Ok(result.Retorno);
        }

        [HttpGet("ativas")]
        public async Task<IActionResult> GetAtivas()
        {
            var result = await _academiaService.ListarAcademiasAtivasAsync();
            if (result.OcorreuErro)
                return BadRequest(result.MensagemErro);
            
            return Ok(result.Retorno);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _academiaService.ConsultarAcademiaAsync(id);
            if (result.OcorreuErro)
                return NotFound(result.MensagemErro);
            
            return Ok(result.Retorno);
        }

        [HttpGet("cnpj/{cnpj}")]
        public async Task<IActionResult> GetByCnpj(string cnpj)
        {
            var result = await _academiaService.ConsultarAcademiaPorCNPJAsync(cnpj);
            if (result.OcorreuErro)
                return NotFound(result.MensagemErro);
            
            return Ok(result.Retorno);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AcademiaEntity academia)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _academiaService.IncluirAcademiaAsync(academia, IdUsuario);
            if (result.OcorreuErro)
                return BadRequest(result.MensagemErro);
            
            return CreatedAtAction(nameof(GetById), new { id = result.Retorno.IdAcademia }, result.Retorno);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AcademiaEntity academia)
        {
            if (id != academia.IdAcademia)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _academiaService.AlterarAcademiaAsync(academia, IdUsuario);
            if (result.OcorreuErro)
                return BadRequest(result.MensagemErro);
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _academiaService.ExcluirAcademiaAsync(id, IdUsuario);
            if (result.OcorreuErro)
                return BadRequest(result.MensagemErro);
            
            return NoContent();
        }
    }
}