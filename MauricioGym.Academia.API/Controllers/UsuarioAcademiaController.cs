using MauricioGym.Academia.Entities;
using MauricioGym.Academia.Services.Interfaces;
using MauricioGym.Infra.Controller;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Academia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioAcademiaController : ApiController
    {
        private readonly IUsuarioAcademiaService usuarioAcademiaService;

        public UsuarioAcademiaController(IUsuarioAcademiaService usuarioAcademiaService)
        {
            this.usuarioAcademiaService = usuarioAcademiaService;
        }

        [HttpPost]
        public async Task<IActionResult> IncluirAsync(UsuarioAcademiaEntity usuarioAcademia)
        {
            var incluir = await usuarioAcademiaService.IncluirUsuarioAcademiaAsync(usuarioAcademia, IdUsuario);
            if (incluir.OcorreuErro)
                return BadRequest(incluir.MensagemErro);

            return Ok(incluir.Retorno);
        }

        [HttpPut]
        public async Task<IActionResult> AlterarAsync(UsuarioAcademiaEntity usuarioAcademia)
        {
            var alterar = await usuarioAcademiaService.AlterarUsuarioAcademiaAsync(usuarioAcademia, IdUsuario);
            if (alterar.OcorreuErro)
                return BadRequest(alterar.MensagemErro);

            return Ok();
        }

        [HttpDelete("{idUsuarioAcademia}")]
        public async Task<IActionResult> ExcluirAsync([FromRoute] int idUsuarioAcademia)
        {
            var excluir = await usuarioAcademiaService.ExcluirUsuarioAcademiaAsync(idUsuarioAcademia, IdUsuario);
            if (excluir.OcorreuErro)
                return BadRequest(excluir.MensagemErro);

            return Ok();
        }

        [HttpGet("{idUsuarioAcademia}")]
        public async Task<IActionResult> ConsultarAsync([FromRoute] int idUsuarioAcademia)
        {
            var consultar = await usuarioAcademiaService.ConsultarUsuarioAcademiaAsync(idUsuarioAcademia);
            if (consultar.OcorreuErro)
                return BadRequest(consultar.MensagemErro);

            return Ok(consultar.Retorno);
        }

        [HttpGet]
        public async Task<IActionResult> ListarAsync()
        {
            var listar = await usuarioAcademiaService.ListarUsuarioAcademiaAsync();
            if (listar.OcorreuErro)
                return BadRequest(listar.MensagemErro);

            return Ok(listar.Retorno);
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> ListarPorUsuarioAsync([FromRoute] int idUsuario)
        {
            var listar = await usuarioAcademiaService.ListarUsuarioAcademiaPorUsuarioAsync(idUsuario);
            if (listar.OcorreuErro)
                return BadRequest(listar.MensagemErro);

            return Ok(listar.Retorno);
        }

        [HttpGet("academia/{idAcademia}")]
        public async Task<IActionResult> ListarPorAcademiaAsync([FromRoute] int idAcademia)
        {
            var listar = await usuarioAcademiaService.ListarUsuarioAcademiaPorAcademiaAsync(idAcademia);
            if (listar.OcorreuErro)
                return BadRequest(listar.MensagemErro);

            return Ok(listar.Retorno);
        }
    }
}