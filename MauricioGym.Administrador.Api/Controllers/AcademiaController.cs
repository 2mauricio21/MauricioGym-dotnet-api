using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Interfaces;
using MauricioGym.Infra.Controller;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Administrador.Api.Controllers
{
    [Route("api/[controller]")]
    public class AcademiaController : ApiController
    {
        private readonly IAcademiaService _academiaService;

        public AcademiaController(IAcademiaService academiaService)
        {
            _academiaService = academiaService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var resultado = await _academiaService.ObterTodosAsync();
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var resultado = await _academiaService.ObterPorIdAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("cnpj/{cnpj}")]
        public async Task<IActionResult> ObterPorCnpj(string cnpj)
        {
            var resultado = await _academiaService.ObterPorCnpjAsync(cnpj);
            return ProcessarResultado(resultado);
        }

        [HttpGet("ativas")]
        public async Task<IActionResult> ListarAtivas()
        {
            var resultado = await _academiaService.ObterAtivosAsync();
            return ProcessarResultado(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] AcademiaEntity academia)
        {
            var resultado = await _academiaService.IncluirAcademiaAsync(academia, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Alterar(int id, [FromBody] AcademiaEntity academia)
        {
            academia.Id = id;
            var resultado = await _academiaService.AlterarAcademiaAsync(academia, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var resultado = await _academiaService.ExcluirAcademiaAsync(id, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}/existe")]
        public async Task<IActionResult> VerificarExistencia(int id)
        {
            var resultado = await _academiaService.ExisteAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("cnpj/{cnpj}/existe")]
        public async Task<IActionResult> VerificarExistenciaCnpj(string cnpj, [FromQuery] int? idExcluir = null)
        {
            var resultado = await _academiaService.ExisteCnpjAsync(cnpj, idExcluir);
            return ProcessarResultado(resultado);
        }
    }
}