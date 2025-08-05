using MauricioGym.Plano.Entities;
using MauricioGym.Plano.Services.Interfaces;
using MauricioGym.Infra.Controller;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Plano.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanoController : ApiController
    {
        private readonly IPlanoService planoService;

        public PlanoController(IPlanoService planoService)
        {
            this.planoService = planoService;
        }

        [HttpPost]
        public async Task<IActionResult> IncluirAsync(PlanoEntity plano)
        {
            var incluir = await planoService.IncluirPlanoAsync(plano, IdUsuario);
            if (incluir.OcorreuErro)
                return BadRequest(incluir.MensagemErro);

            return Ok(incluir.Retorno);
        }

        [HttpPut]
        public async Task<IActionResult> AlterarAsync(PlanoEntity plano)
        {
            var alterar = await planoService.AlterarPlanoAsync(plano);
            if (alterar.OcorreuErro)
                return BadRequest(alterar.MensagemErro);

            return Ok();
        }

        [HttpDelete("{idPlano}")]
        public async Task<IActionResult> ExcluirAsync([FromRoute] int idPlano)
        {
            var excluir = await planoService.ExcluirPlanoAsync(idPlano);
            if (excluir.OcorreuErro)
                return BadRequest(excluir.MensagemErro);

            return Ok();
        }

        [HttpGet("{idPlano}")]
        public async Task<IActionResult> ConsultarAsync([FromRoute] int idPlano)
        {
            var consultar = await planoService.ConsultarPlanoAsync(idPlano);
            if (consultar.OcorreuErro)
                return BadRequest(consultar.MensagemErro);

            return Ok(consultar.Retorno);
        }

        [HttpGet]
        public async Task<IActionResult> ListarAsync()
        {
            var listar = await planoService.ListarPlanoAsync();
            if (listar.OcorreuErro)
                return BadRequest(listar.MensagemErro);

            return Ok(listar.Retorno);
        }
    }
}