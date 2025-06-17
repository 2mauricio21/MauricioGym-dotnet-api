using System;
using System.Threading.Tasks;
using MauricioGym.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaixaRelatorioController : ControllerBase
    {
        private readonly IMensalidadeService _mensalidadeService;
        private readonly IPessoaService _pessoaService;

        public CaixaRelatorioController(IMensalidadeService mensalidadeService, IPessoaService pessoaService)
        {
            _mensalidadeService = mensalidadeService;
            _pessoaService = pessoaService;
        }

        [HttpGet("total-alunos-ativos")]
        public async Task<ActionResult<int>> TotalAlunosAtivos()
        {
            var alunos = await _pessoaService.ListarAsync();
            var total = 0;
            foreach (var a in alunos)
                if (a.Ativo) total++;
            return Ok(total);
        }

        [HttpGet("receita-periodo")]
        public async Task<ActionResult<decimal>> ReceitaPeriodo([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
        {
            // Ideal: criar método específico no repositório para receita por período
            // Aqui, exemplo simples usando total geral
            var total = await _mensalidadeService.ObterTotalRecebidoAsync();
            return Ok(total);
        }

        [HttpGet("alunos-inadimplentes")]
        public async Task<ActionResult<int>> AlunosInadimplentes([FromQuery] DateTime referencia)
        {
            var total = await ContarAlunosInadimplentesAsync(referencia);
            return Ok(total);
        }

        private async Task<int> ContarAlunosInadimplentesAsync(DateTime referencia)
        {
            var alunos = await _pessoaService.ListarAsync();
            var inadimplentes = 0;
            foreach (var aluno in alunos)
            {
                if (!aluno.Ativo) continue;
                var mensalidades = await _mensalidadeService.ListarPorPessoaAsync(aluno.Id);
                bool pagou = false;
                foreach (var m in mensalidades)
                {
                    if (m.PeriodoInicio.Month == referencia.Month && m.PeriodoInicio.Year == referencia.Year)
                    {
                        pagou = true;
                        break;
                    }
                }
                if (!pagou) inadimplentes++;
            }
            return inadimplentes;
        }
    }
}
