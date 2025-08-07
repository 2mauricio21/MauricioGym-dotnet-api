using MauricioGym.Plano.Entities;
using MauricioGym.Plano.Services.Interfaces;
using MauricioGym.Infra.Controller;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Plano.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioPlanoController : ApiController
    {
        private readonly IUsuarioPlanoService usuarioPlanoService;

        public UsuarioPlanoController(IUsuarioPlanoService usuarioPlanoService)
        {
            this.usuarioPlanoService = usuarioPlanoService;
        }

        [HttpPost]
        public async Task<IActionResult> IncluirAsync(UsuarioPlanoEntity usuarioPlano)
        {
            var incluir = await usuarioPlanoService.IncluirUsuarioPlanoAsync(usuarioPlano, IdUsuario);
            if (incluir.OcorreuErro)
                return BadRequest(incluir.MensagemErro);

            return Ok(incluir.Retorno);
        }

        [HttpPut]
        public async Task<IActionResult> AlterarAsync(UsuarioPlanoEntity usuarioPlano)
        {
            var alterar = await usuarioPlanoService.AlterarUsuarioPlanoAsync(usuarioPlano, IdUsuario);
            if (alterar.OcorreuErro)
                return BadRequest(alterar.MensagemErro);

            return Ok();
        }

        [HttpDelete("{idUsuarioPlano}")]
        public async Task<IActionResult> ExcluirAsync([FromRoute] int idUsuarioPlano)
        {
            var excluir = await usuarioPlanoService.CancelarUsuarioPlanoAsync(idUsuarioPlano, IdUsuario);
            if (excluir.OcorreuErro)
                return BadRequest(excluir.MensagemErro);

            return Ok();
        }

        [HttpGet("{idUsuarioPlano}")]
        public async Task<IActionResult> ConsultarAsync([FromRoute] int idUsuarioPlano)
        {
            var consultar = await usuarioPlanoService.ConsultarUsuarioPlanoAsync(idUsuarioPlano);
            if (consultar.OcorreuErro)
                return BadRequest(consultar.MensagemErro);

            return Ok(consultar.Retorno);
        }

        [HttpGet]
        public async Task<IActionResult> ListarAsync()
        {
            var listar = await usuarioPlanoService.ListarUsuariosPlanosAsync();
            if (listar.OcorreuErro)
                return BadRequest(listar.MensagemErro);

            return Ok(listar.Retorno);
        }
    }
}