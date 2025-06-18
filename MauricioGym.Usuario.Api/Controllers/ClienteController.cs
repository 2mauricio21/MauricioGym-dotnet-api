using MauricioGym.Infra.Controller;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Usuario.Api.Controllers
{
    [Route("api/[controller]")]
    public class ClienteController : ApiController
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var resultado = await _clienteService.ObterTodosAsync();
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var resultado = await _clienteService.ObterPorIdAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<IActionResult> ObterPorCpf(string cpf)
        {
            var resultado = await _clienteService.ObterPorCpfAsync(cpf);
            return ProcessarResultado(resultado);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> ObterPorEmail(string email)
        {
            var resultado = await _clienteService.ObterPorEmailAsync(email);
            return ProcessarResultado(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] ClienteEntity cliente)
        {
            var resultado = await _clienteService.IncluirClienteAsync(cliente, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Alterar(int id, [FromBody] ClienteEntity cliente)
        {
            cliente.Id = id;
            var resultado = await _clienteService.AlterarClienteAsync(cliente, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var resultado = await _clienteService.ExcluirClienteAsync(id, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}/existe")]
        public async Task<IActionResult> VerificarExistencia(int id)
        {
            var resultado = await _clienteService.ExisteAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("cpf/{cpf}/existe")]
        public async Task<IActionResult> VerificarExistenciaCpf(string cpf, [FromQuery] int? idExcluir = null)
        {
            var resultado = await _clienteService.ExisteCpfAsync(cpf, idExcluir);
            return ProcessarResultado(resultado);
        }

        [HttpGet("email/{email}/existe")]
        public async Task<IActionResult> VerificarExistenciaEmail(string email, [FromQuery] int? idExcluir = null)
        {
            var resultado = await _clienteService.ExisteEmailAsync(email, idExcluir);
            return ProcessarResultado(resultado);
        }
    }
}
