using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Interfaces;
using MauricioGym.Infra.Controller;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Administrador.Api.Controllers
{
    [Route("api/[controller]")]
    public class AdministradorController : ApiController
    {
        private readonly IAdministradorService _administradorService;

        public AdministradorController(IAdministradorService administradorService)
        {
            _administradorService = administradorService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var resultado = await _administradorService.ObterTodosAsync();
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var resultado = await _administradorService.ObterPorIdAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> ObterPorEmail(string email)
        {
            var resultado = await _administradorService.ObterPorEmailAsync(email);
            return ProcessarResultado(resultado);
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<IActionResult> ObterPorCpf(string cpf)
        {
            var resultado = await _administradorService.ObterPorCpfAsync(cpf);
            return ProcessarResultado(resultado);
        }

        [HttpGet("ativos")]
        public async Task<IActionResult> ListarAtivos()
        {
            var resultado = await _administradorService.ObterAtivosAsync();
            return ProcessarResultado(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] AdministradorEntity administrador)
        {
            var resultado = await _administradorService.IncluirAdministradorAsync(administrador, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Alterar(int id, [FromBody] AdministradorEntity administrador)
        {
            administrador.Id = id;
            var resultado = await _administradorService.AlterarAdministradorAsync(administrador, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var resultado = await _administradorService.ExcluirAdministradorAsync(id, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}/existe")]
        public async Task<IActionResult> VerificarExistencia(int id)
        {
            var resultado = await _administradorService.ExisteAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("email/{email}/existe")]
        public async Task<IActionResult> VerificarExistenciaEmail(string email, [FromQuery] int? idExcluir = null)
        {
            var resultado = await _administradorService.ExistePorEmailAsync(email, idExcluir);
            return ProcessarResultado(resultado);
        }

        [HttpGet("cpf/{cpf}/existe")]
        public async Task<IActionResult> VerificarExistenciaCpf(string cpf, [FromQuery] int? idExcluir = null)
        {
            var resultado = await _administradorService.ExistePorCpfAsync(cpf, idExcluir);
            return ProcessarResultado(resultado);
        }
    }
}
