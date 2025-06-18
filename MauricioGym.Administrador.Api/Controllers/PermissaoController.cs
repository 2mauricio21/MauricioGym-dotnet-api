using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Interfaces;
using MauricioGym.Infra.Controller;
using MauricioGym.Infra.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Administrador.Api.Controllers
{
    [Route("api/[controller]")]
    public class PermissaoController : ApiController
    {
        private readonly IPermissaoService _permissaoService;

        public PermissaoController(IPermissaoService permissaoService)
        {
            _permissaoService = permissaoService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var resultado = await _permissaoService.ObterTodosAsync();
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var resultado = await _permissaoService.ObterPorIdAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("nome/{nome}")]
        public async Task<IActionResult> ObterPorNome(string nome)
        {
            var resultado = await _permissaoService.ObterPorNomeAsync(nome);
            return ProcessarResultado(resultado);
        }

        [HttpGet("papel/{papelId}")]
        public async Task<IActionResult> ObterPorPapel(int papelId)
        {
            var resultado = await _permissaoService.ObterPorPapelIdAsync(papelId);
            return ProcessarResultado(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] PermissaoEntity permissao)
        {
            var resultado = await _permissaoService.IncluirPermissaoAsync(permissao, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Alterar(int id, [FromBody] PermissaoEntity permissao)
        {
            permissao.Id = id;
            var resultado = await _permissaoService.AlterarPermissaoAsync(permissao, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var resultado = await _permissaoService.ExcluirPermissaoAsync(id, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}/existe")]
        public async Task<IActionResult> VerificarExistencia(int id)
        {
            var resultado = await _permissaoService.ExisteAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("nome/{nome}/existe")]
        public async Task<IActionResult> VerificarExistenciaNome(string nome, [FromQuery] int? idExcluir = null)
        {
            var resultado = await _permissaoService.ExisteNomeAsync(nome, idExcluir);
            return ProcessarResultado(resultado);
        }
    }
} 