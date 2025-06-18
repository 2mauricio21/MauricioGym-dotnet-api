using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Interfaces;
using MauricioGym.Infra.Controller;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Administrador.Api.Controllers
{
    [Route("api/[controller]")]
    public class PapelPermissaoController : ApiController
    {
        private readonly IPapelPermissaoService _papelPermissaoService;

        public PapelPermissaoController(IPapelPermissaoService papelPermissaoService)
        {
            _papelPermissaoService = papelPermissaoService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var resultado = await _papelPermissaoService.ObterPorIdAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("papel/{idPapel}/permissao/{idPermissao}")]
        public async Task<IActionResult> ObterPorPapelPermissao(int idPapel, int idPermissao)
        {
            var resultado = await _papelPermissaoService.ObterPorPapelPermissaoAsync(idPapel, idPermissao);
            return ProcessarResultado(resultado);
        }

        [HttpGet("papel/{idPapel}")]
        public async Task<IActionResult> ListarPorPapel(int idPapel)
        {
            var resultado = await _papelPermissaoService.ListarPorPapelAsync(idPapel);
            return ProcessarResultado(resultado);
        }

        [HttpGet("permissao/{idPermissao}")]
        public async Task<IActionResult> ListarPorPermissao(int idPermissao)
        {
            var resultado = await _papelPermissaoService.ListarPorPermissaoAsync(idPermissao);
            return ProcessarResultado(resultado);
        }

        [HttpGet("papel/{idPapel}/permissoes")]
        public async Task<IActionResult> ListarPermissoesDoPapel(int idPapel)
        {
            var resultado = await _papelPermissaoService.ListarPermissoesDoPapelAsync(idPapel);
            return ProcessarResultado(resultado);
        }

        [HttpGet("permissao/{idPermissao}/papeis")]
        public async Task<IActionResult> ListarPapeisComPermissao(int idPermissao)
        {
            var resultado = await _papelPermissaoService.ListarPapeisComPermissaoAsync(idPermissao);
            return ProcessarResultado(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] PapelPermissaoEntity papelPermissao)
        {
            var resultado = await _papelPermissaoService.CriarAsync(papelPermissao);
            return ProcessarResultado(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var resultado = await _papelPermissaoService.RemoverAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpDelete("papel/{idPapel}/permissao/{idPermissao}")]
        public async Task<IActionResult> RemoverPorPapelPermissao(int idPapel, int idPermissao)
        {
            var resultado = await _papelPermissaoService.RemoverPorPapelPermissaoAsync(idPapel, idPermissao);
            return ProcessarResultado(resultado);
        }

        [HttpPost("papel/{idPapel}/permissao/{idPermissao}")]
        public async Task<IActionResult> AtribuirPermissaoAoPapel(int idPapel, int idPermissao)
        {
            var resultado = await _papelPermissaoService.AtribuirPermissaoAoPapelAsync(idPapel, idPermissao);
            return ProcessarResultado(resultado);
        }

        [HttpDelete("papel/{idPapel}/permissao/{idPermissao}/remover")]
        public async Task<IActionResult> RemoverPermissaoDoPapel(int idPapel, int idPermissao)
        {
            var resultado = await _papelPermissaoService.RemoverPermissaoDoPapelAsync(idPapel, idPermissao);
            return ProcessarResultado(resultado);
        }
    }
}
