using MauricioGym.Acesso.Entities;
using MauricioGym.Acesso.Services.Interfaces;
using MauricioGym.Infra.Controller;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Acesso.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcessoController : ApiController
    {
        private readonly IAcessoService acessoService;

        public AcessoController(IAcessoService acessoService)
        {
            this.acessoService = acessoService;
        }

        [HttpPost]
        public async Task<IActionResult> IncluirAsync(AcessoEntity acesso)
        {
            var incluir = await acessoService.IncluirAcessoAsync(acesso, IdUsuario);
            if (incluir.OcorreuErro)
                return BadRequest(incluir.MensagemErro);

            return Ok(incluir.Retorno);
        }

        [HttpPut]
        public async Task<IActionResult> AlterarAsync(AcessoEntity acesso)
        {
            var alterar = await acessoService.AlterarAcessoAsync(acesso, IdUsuario);
            if (alterar.OcorreuErro)
                return BadRequest(alterar.MensagemErro);

            return Ok();
        }

        [HttpDelete("{idAcesso}")]
        public async Task<IActionResult> ExcluirAsync([FromRoute] int idAcesso)
        {
            var excluir = await acessoService.ExcluirAcessoAsync(idAcesso, IdUsuario);
            if (excluir.OcorreuErro)
                return BadRequest(excluir.MensagemErro);

            return Ok();
        }

        [HttpGet("{idAcesso}")]
        public async Task<IActionResult> ConsultarAsync([FromRoute] int idAcesso)
        {
            var consultar = await acessoService.ConsultarAcessoAsync(idAcesso);
            if (consultar.OcorreuErro)
                return BadRequest(consultar.MensagemErro);

            return Ok(consultar.Retorno);
        }

        [HttpGet]
        public async Task<IActionResult> ListarAsync()
        {
            var listar = await acessoService.ListarAcessoAsync();
            if (listar.OcorreuErro)
                return BadRequest(listar.MensagemErro);

            return Ok(listar.Retorno);
        }
    }
}