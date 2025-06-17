using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Interfaces;
using MauricioGym.Infra.Controller;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Administrador.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministradorController : ApiController
    {
        private readonly IAdministradorService _administradorService;

        public AdministradorController(IAdministradorService administradorService)
        {
            _administradorService = administradorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdministradorEntity>>> Listar()
        {
            var resultado = await _administradorService.ListarAsync();
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdministradorEntity>> ObterPorId(int id)
        {
            var resultado = await _administradorService.ObterPorIdAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Criar([FromBody] AdministradorEntity administrador)        {
            var resultado = await _administradorService.CriarAsync(administrador);
            if (resultado.OcorreuErro)
                return ProcessarResultado(resultado);

            return CreatedAtAction(nameof(ObterPorId), new { id = resultado.Retorno }, resultado.Retorno);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] AdministradorEntity administrador)
        {
            if (id != administrador.Id) 
                return BadRequest("ID do parâmetro não confere com o ID do objeto");

            var resultado = await _administradorService.AtualizarAsync(administrador);
            return ProcessarResultado(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            var resultado = await _administradorService.RemoverLogicamenteAsync(id);
            return ProcessarResultado(resultado);
        }
    }
}
