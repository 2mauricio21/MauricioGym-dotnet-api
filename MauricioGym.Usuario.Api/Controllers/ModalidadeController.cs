using MauricioGym.Infra.Controller;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Usuario.Api.Controllers
{
    [Route("api/[controller]")]
    public class ModalidadeController : ApiController
    {
        private readonly IModalidadeService _modalidadeService;

        public ModalidadeController(IModalidadeService modalidadeService)
        {
            _modalidadeService = modalidadeService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var resultado = await _modalidadeService.ObterTodosAsync();
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var resultado = await _modalidadeService.ObterPorIdAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("nome/{nome}")]
        public async Task<IActionResult> ObterPorNome(string nome)
        {
            var resultado = await _modalidadeService.ObterPorNomeAsync(nome);
            return ProcessarResultado(resultado);
        }

        [HttpGet("academia/{academiaId}")]
        public async Task<IActionResult> ListarPorAcademia(int academiaId)
        {
            var resultado = await _modalidadeService.ObterPorAcademiaIdAsync(academiaId);
            return ProcessarResultado(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] ModalidadeEntity modalidade)
        {
            var resultado = await _modalidadeService.IncluirModalidadeAsync(modalidade, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Alterar(int id, [FromBody] ModalidadeEntity modalidade)
        {
            modalidade.Id = id;
            var resultado = await _modalidadeService.AlterarModalidadeAsync(modalidade, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var resultado = await _modalidadeService.ExcluirModalidadeAsync(id, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}/existe")]
        public async Task<IActionResult> VerificarExistencia(int id)
        {
            var resultado = await _modalidadeService.ExisteAsync(id);
            return ProcessarResultado(resultado);
        }

    }
}
