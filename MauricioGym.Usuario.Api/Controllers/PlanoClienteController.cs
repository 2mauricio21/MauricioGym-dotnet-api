using MauricioGym.Infra.Controller;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Usuario.Api.Controllers
{
    [Route("api/[controller]")]
    public class PlanoClienteController : ApiController
    {
        private readonly IPlanoClienteService _planoClienteService;

        public PlanoClienteController(IPlanoClienteService planoClienteService)
        {
            _planoClienteService = planoClienteService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var resultado = await _planoClienteService.ObterPorIdAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> ObterPorClienteId(int clienteId)
        {
            var resultado = await _planoClienteService.ObterPorClienteIdAsync(clienteId);
            return ProcessarResultado(resultado);
        }

        [HttpGet("plano/{planoId}")]
        public async Task<IActionResult> ObterPorPlanoId(int planoId)
        {
            var resultado = await _planoClienteService.ObterPorPlanoIdAsync(planoId);
            return ProcessarResultado(resultado);
        }

        [HttpGet("cliente/{clienteId}/ativos")]
        public async Task<IActionResult> ObterAtivosPorClienteId(int clienteId)
        {
            var resultado = await _planoClienteService.ObterAtivosPorClienteIdAsync(clienteId);
            return ProcessarResultado(resultado);
        }

        [HttpGet("cliente/{clienteId}/vencidos")]
        public async Task<IActionResult> ObterVencidosPorClienteId(int clienteId)
        {
            var resultado = await _planoClienteService.ObterVencidosPorClienteIdAsync(clienteId);
            return ProcessarResultado(resultado);
        }

        [HttpGet("vencendo")]
        public async Task<IActionResult> ObterVencendo([FromQuery] int diasAntecedencia = 7)
        {
            var resultado = await _planoClienteService.ObterVencendoAsync(diasAntecedencia);
            return ProcessarResultado(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] PlanoClienteEntity planoCliente)
        {
            var resultado = await _planoClienteService.IncluirPlanoClienteAsync(planoCliente, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Alterar(int id, [FromBody] PlanoClienteEntity planoCliente)
        {
            planoCliente.Id = id;
            var resultado = await _planoClienteService.AlterarPlanoClienteAsync(planoCliente, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var resultado = await _planoClienteService.ExcluirPlanoClienteAsync(id, IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}/existe")]
        public async Task<IActionResult> VerificarExistencia(int id)
        {
            var resultado = await _planoClienteService.ExisteAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("cliente/{clienteId}/plano/{planoId}/ativo")]
        public async Task<IActionResult> VerificarClientePossuiPlanoAtivo(int clienteId, int planoId)
        {
            var resultado = await _planoClienteService.ClientePossuiPlanoAtivoAsync(clienteId, planoId);
            return ProcessarResultado(resultado);
        }

        [HttpGet("cliente/cpf/{cpfCliente}/academia/{idAcademia}")]
        public async Task<IActionResult> ObterPlanosDoCliente(string cpfCliente, int idAcademia)
        {
            var resultado = await _planoClienteService.ObterPlanosDoClienteAsync(cpfCliente, idAcademia);
            return ProcessarResultado(resultado);
        }

        [HttpGet("cliente/cpf/{cpfCliente}/academia/{idAcademia}/ativo")]
        public async Task<IActionResult> ObterPlanoAtivo(string cpfCliente, int idAcademia)
        {
            var resultado = await _planoClienteService.ObterPlanoAtivoAsync(cpfCliente, idAcademia);
            return ProcessarResultado(resultado);
        }

        [HttpPost("contratar")]
        public async Task<IActionResult> ContratarPlano([FromBody] ContratarPlanoRequest request)
        {
            var resultado = await _planoClienteService.ContratarPlanoAsync(
                request.CpfCliente,
                request.IdPlano,
                request.IdAcademia,
                request.Meses,
                request.ValorPago,
                IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpPost("renovar")]
        public async Task<IActionResult> RenovarPlano([FromBody] RenovarPlanoRequest request)
        {
            var resultado = await _planoClienteService.RenovarPlanoAsync(
                request.CpfCliente,
                request.IdAcademia,
                request.MesesAdicionais,
                request.ValorPago,
                IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpPost("cancelar")]
        public async Task<IActionResult> CancelarPlano([FromBody] CancelarPlanoRequest request)
        {
            var resultado = await _planoClienteService.CancelarPlanoAsync(
                request.IdPlanoCliente,
                request.Motivo,
                IdUsuario);
            return ProcessarResultado(resultado);
        }

        [HttpGet("cliente/cpf/{cpfCliente}/academia/{idAcademia}/pode-checkin")]
        public async Task<IActionResult> VerificarPodeRealizarCheckIn(string cpfCliente, int idAcademia)
        {
            var resultado = await _planoClienteService.ClientePodeEfetuarCheckInAsync(cpfCliente, idAcademia);
            return ProcessarResultado(resultado);
        }

        [HttpGet("academia/{idAcademia}/vencendo")]
        public async Task<IActionResult> ObterPlanosVencendo(int idAcademia, [FromQuery] int dias = 7)
        {
            var resultado = await _planoClienteService.ObterPlanosVencendoAsync(idAcademia, dias);
            return ProcessarResultado(resultado);
        }
    }

    public class ContratarPlanoRequest
    {
        public string CpfCliente { get; set; }
        public int IdPlano { get; set; }
        public int IdAcademia { get; set; }
        public int Meses { get; set; }
        public decimal ValorPago { get; set; }
    }

    public class RenovarPlanoRequest
    {
        public string CpfCliente { get; set; }
        public int IdAcademia { get; set; }
        public int MesesAdicionais { get; set; }
        public decimal ValorPago { get; set; }
    }

    public class CancelarPlanoRequest
    {
        public int IdPlanoCliente { get; set; }
        public string Motivo { get; set; }
    }
}
