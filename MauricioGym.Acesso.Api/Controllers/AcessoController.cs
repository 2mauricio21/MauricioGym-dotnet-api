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

        [HttpPut("{idAcesso}/saida")]
        public async Task<IActionResult> RegistrarSaidaAsync([FromRoute] int idAcesso)
        {
            var registrarSaida = await acessoService.RegistrarSaidaAsync(idAcesso, IdUsuario);
            if (registrarSaida.OcorreuErro)
                return BadRequest(registrarSaida.MensagemErro);

            return Ok();
        }

        [HttpGet("{idAcesso}")]
        public async Task<IActionResult> ConsultarAsync([FromRoute] int idAcesso, [FromQuery] int idAcademia)
        {
            var consultar = await acessoService.ConsultarAcessoAsync(idAcesso, idAcademia);
            if (consultar.OcorreuErro)
                return BadRequest(consultar.MensagemErro);

            return Ok(consultar.Retorno);
        }

        [HttpGet("academia/{idAcademia}")]
        public async Task<IActionResult> ListarAsync([FromRoute] int idAcademia)
        {
            var listar = await acessoService.ConsultarAcessosPorAcademiaAsync(idAcademia);
            if (listar.OcorreuErro)
                return BadRequest(listar.MensagemErro);

            return Ok(listar.Retorno);
        }
    }
}