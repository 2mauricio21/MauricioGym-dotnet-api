using MauricioGym.Infra.Controller;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Usuario.Api.Controllers
{
    [Route("api/[controller]")]
    public class PlanoController : ApiController
    {
        private readonly IPlanoService _planoService;

        public PlanoController(IPlanoService planoService)
        {
            _planoService = planoService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var resultado = await _planoService.ObterTodosAsync();
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var resultado = await _planoService.ObterPorIdAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("academia/{academiaId}")]
        public async Task<IActionResult> ListarPorAcademia(int academiaId)
        {
            var resultado = await _planoService.ObterPorAcademiaIdAsync(academiaId);
            return ProcessarResultado(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] PlanoEntity plano)
        {
            var resultado = await _planoService.IncluirPlanoAsync(plano, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Alterar(int id, [FromBody] PlanoEntity plano)
        {
            plano.Id = id;
            var resultado = await _planoService.AlterarPlanoAsync(plano, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var resultado = await _planoService.ExcluirPlanoAsync(id, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}/existe")]
        public async Task<IActionResult> VerificarExistencia(int id)
        {
            var resultado = await _planoService.ExisteAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("nome/{nome}/existe")]
        public async Task<IActionResult> VerificarExistenciaNome(string nome, [FromQuery] int? idExcluir = null)
        {
            var resultado = await _planoService.ExisteNomeAsync(nome, idExcluir);
            return ProcessarResultado(resultado);
        }
    }
}
