using MauricioGym.Infra.Controller;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Usuario.Api.Controllers
{
    [Route("api/[controller]")]
    public class VinculoClienteAcademiaController : ApiController
    {
        private readonly IVinculoClienteAcademiaService _vinculoClienteAcademiaService;

        public VinculoClienteAcademiaController(IVinculoClienteAcademiaService vinculoClienteAcademiaService)
        {
            _vinculoClienteAcademiaService = vinculoClienteAcademiaService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var resultado = await _vinculoClienteAcademiaService.ObterPorIdAsync(id);
            return Ok(resultado);
        }

        [HttpGet("cliente/{clienteId}/academia/{academiaId}")]
        public async Task<IActionResult> ObterPorClienteEAcademia(int clienteId, int academiaId)
        {
            var resultado = await _vinculoClienteAcademiaService.ObterPorClienteEAcademiaAsync(clienteId, academiaId);
            return Ok(resultado);
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> ObterPorCliente(int clienteId)
        {
            var resultado = await _vinculoClienteAcademiaService.ObterPorClienteIdAsync(clienteId);
            return Ok(resultado);
        }

        [HttpGet("academia/{academiaId}")]
        public async Task<IActionResult> ObterPorAcademia(int academiaId)
        {
            var resultado = await _vinculoClienteAcademiaService.ObterPorAcademiaIdAsync(academiaId);
            return Ok(resultado);
        }

        [HttpGet("cliente/{clienteId}/ativos")]
        public async Task<IActionResult> ObterAtivosPorCliente(int clienteId)
        {
            var resultado = await _vinculoClienteAcademiaService.ObterAtivosPorClienteIdAsync(clienteId);
            return Ok(resultado);
        }

        [HttpGet("academia/{academiaId}/ativos")]
        public async Task<IActionResult> ObterAtivosPorAcademia(int academiaId)
        {
            var resultado = await _vinculoClienteAcademiaService.ObterAtivosPorAcademiaIdAsync(academiaId);
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] VinculoClienteAcademiaEntity vinculo)
        {
            var resultado = await _vinculoClienteAcademiaService.CriarAsync(vinculo);
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] VinculoClienteAcademiaEntity vinculo)
        {
            vinculo.Id = id;
            var resultado = await _vinculoClienteAcademiaService.AtualizarAsync(vinculo);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var resultado = await _vinculoClienteAcademiaService.ExcluirAsync(id);
            return Ok(resultado);
        }

        [HttpGet("{id}/existe")]
        public async Task<IActionResult> Existe(int id)
        {
            var resultado = await _vinculoClienteAcademiaService.ExisteAsync(id);
            return Ok(resultado);
        }

        [HttpGet("cliente/{clienteId}/academia/{academiaId}/existe")]
        public async Task<IActionResult> ExisteVinculo(int clienteId, int academiaId)
        {
            var resultado = await _vinculoClienteAcademiaService.ExisteVinculoAsync(clienteId, academiaId);
            return Ok(resultado);
        }

        [HttpGet("cliente/{clienteId}/academia/{academiaId}/ativo")]
        public async Task<IActionResult> ExisteVinculoAtivo(int clienteId, int academiaId)
        {
            var resultado = await _vinculoClienteAcademiaService.ExisteVinculoAtivoAsync(clienteId, academiaId);
            return Ok(resultado);
        }
    }
}
