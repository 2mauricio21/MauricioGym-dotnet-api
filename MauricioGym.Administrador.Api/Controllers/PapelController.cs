using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Interfaces;
using MauricioGym.Infra.Controller;
using MauricioGym.Infra.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Administrador.Api.Controllers
{
    [Route("api/[controller]")]
    public class PapelController : ApiController
    {
        private readonly IPapelService _papelService;

        public PapelController(IPapelService papelService)
        {
            _papelService = papelService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var resultado = await _papelService.ObterTodosAsync();
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var resultado = await _papelService.ObterPorIdAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("nome/{nome}")]
        public async Task<IActionResult> ObterPorNome(string nome)
        {
            var resultado = await _papelService.ObterPorNomeAsync(nome);
            return ProcessarResultado(resultado);
        }

        [HttpGet("administrador/{administradorId}")]
        public async Task<IActionResult> ObterPorAdministrador(int administradorId)
        {
            var resultado = await _papelService.ObterPorAdministradorIdAsync(administradorId);
            return ProcessarResultado(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] PapelEntity papel)
        {
            var resultado = await _papelService.IncluirPapelAsync(papel, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Alterar(int id, [FromBody] PapelEntity papel)
        {
            papel.Id = id;
            var resultado = await _papelService.AlterarPapelAsync(papel, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var resultado = await _papelService.ExcluirPapelAsync(id, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}/existe")]
        public async Task<IActionResult> VerificarExistencia(int id)
        {
            var resultado = await _papelService.ExisteAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("nome/{nome}/existe")]
        public async Task<IActionResult> VerificarExistenciaNome(string nome, [FromQuery] int? idExcluir = null)
        {
            var resultado = await _papelService.ExisteNomeAsync(nome, idExcluir);
            return ProcessarResultado(resultado);
        }
    }
} 