using MauricioGym.Acesso.Entities;
using MauricioGym.Acesso.Services.Interfaces;
using MauricioGym.Infra.Controller;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Acesso.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BloqueioAcessoController : ApiController
    {
        private readonly IBloqueioAcessoService bloqueioAcessoService;

        public BloqueioAcessoController(IBloqueioAcessoService bloqueioAcessoService)
        {
            this.bloqueioAcessoService = bloqueioAcessoService;
        }

        [HttpPost]
        public async Task<IActionResult> IncluirAsync(BloqueioAcessoEntity bloqueioAcesso)
        {
            var incluir = await bloqueioAcessoService.IncluirBloqueioAcessoAsync(bloqueioAcesso, IdUsuario);
            if (incluir.OcorreuErro)
                return BadRequest(incluir.MensagemErro);

            return Ok(incluir.Retorno);
        }

        [HttpPut]
        public async Task<IActionResult> AlterarAsync(BloqueioAcessoEntity bloqueioAcesso)
        {
            var alterar = await bloqueioAcessoService.AlterarBloqueioAcessoAsync(bloqueioAcesso);
            if (alterar.OcorreuErro)
                return BadRequest(alterar.MensagemErro);

            return Ok();
        }

        [HttpDelete("{idBloqueioAcesso}")]
        public async Task<IActionResult> ExcluirAsync([FromRoute] int idBloqueioAcesso)
        {
            var excluir = await bloqueioAcessoService.CancelarBloqueioAcessoAsync(idBloqueioAcesso);
            if (excluir.OcorreuErro)
                return BadRequest(excluir.MensagemErro);

            return Ok();
        }

        [HttpGet("{idBloqueioAcesso}")]
        public async Task<IActionResult> ConsultarAsync([FromRoute] int idBloqueioAcesso)
        {
            var consultar = await bloqueioAcessoService.ConsultarBloqueioAcessoAsync(idBloqueioAcesso);
            if (consultar.OcorreuErro)
                return BadRequest(consultar.MensagemErro);

            return Ok(consultar.Retorno);
        }

        [HttpGet]
        public async Task<IActionResult> ListarAsync()
        {
            var listar = await bloqueioAcessoService.ListarBloqueioAcessoAsync();
            if (listar.OcorreuErro)
                return BadRequest(listar.MensagemErro);

            return Ok(listar.Retorno);
        }
    }
}