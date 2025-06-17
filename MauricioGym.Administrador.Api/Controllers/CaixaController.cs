using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Interfaces;
using MauricioGym.Infra.Controller;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Administrador.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaixaController : ApiController
    {
        private readonly ICaixaService _caixaService;

        public CaixaController(ICaixaService caixaService)
        {
            _caixaService = caixaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaixaEntity>>> Listar()
        {
            var resultado = await _caixaService.ListarAsync();
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CaixaEntity>> ObterPorId(int id)
        {
            var resultado = await _caixaService.ObterPorIdAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Criar([FromBody] CaixaEntity caixa)        {
            var resultado = await _caixaService.CriarAsync(caixa);
            if (resultado.OcorreuErro)
                return ProcessarResultado(resultado);

            return CreatedAtAction(nameof(ObterPorId), new { id = resultado.Retorno }, resultado.Retorno);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] CaixaEntity caixa)
        {
            if (id != caixa.Id) 
                return BadRequest("ID do parâmetro não confere com o ID do objeto");

            var resultado = await _caixaService.AtualizarAsync(caixa);
            return ProcessarResultado(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            var resultado = await _caixaService.RemoverAsync(id);
            return ProcessarResultado(resultado);
        }
    }
}
