using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Interfaces;
using MauricioGym.Infra.Controller;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Administrador.Api.Controllers
{
    [Route("api/[controller]")]
    public class AdministradorPapelController : ApiController
    {
        private readonly IAdministradorPapelService _administradorPapelService;

        public AdministradorPapelController(IAdministradorPapelService administradorPapelService)
        {
            _administradorPapelService = administradorPapelService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var resultado = await _administradorPapelService.ObterPorIdAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("administrador/{administradorId}")]
        public async Task<IActionResult> ObterPorAdministrador(int administradorId)
        {
            var resultado = await _administradorPapelService.ObterPorAdministradorIdAsync(administradorId);
            return ProcessarResultado(resultado);
        }

        [HttpGet("papel/{papelId}")]
        public async Task<IActionResult> ObterPorPapel(int papelId)
        {
            var resultado = await _administradorPapelService.ObterPorPapelIdAsync(papelId);
            return ProcessarResultado(resultado);
        }

        [HttpGet("administrador/{administradorId}/papel/{papelId}")]
        public async Task<IActionResult> ObterPorAdministradorEPapel(int administradorId, int papelId)
        {
            var resultado = await _administradorPapelService.ObterPorAdministradorEPapelAsync(administradorId, papelId);
            return ProcessarResultado(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] AdministradorPapelEntity administradorPapel)
        {
            var resultado = await _administradorPapelService.IncluirAdministradorPapelAsync(administradorPapel, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var resultado = await _administradorPapelService.ExcluirAdministradorPapelAsync(id, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpDelete("administrador/{administradorId}/papel/{papelId}")]
        public async Task<IActionResult> RemoverPapelDoAdministrador(int administradorId, int papelId)
        {
            var resultado = await _administradorPapelService.RemoverPapelDoAdministradorAsync(administradorId, papelId, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpGet("administrador/{administradorId}/papel/{papelId}/verifica")]
        public async Task<IActionResult> VerificarAdministradorPossuiPapel(int administradorId, int papelId)
        {
            var resultado = await _administradorPapelService.AdministradorPossuiPapelAsync(administradorId, papelId);
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}/existe")]
        public async Task<IActionResult> VerificarExistencia(int id)
        {
            var resultado = await _administradorPapelService.ExisteAsync(id);
            return ProcessarResultado(resultado);
        }
    }
}
