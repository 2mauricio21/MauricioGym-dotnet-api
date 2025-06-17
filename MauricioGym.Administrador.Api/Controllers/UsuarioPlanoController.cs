using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Interfaces;
using MauricioGym.Infra.Controller;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Administrador.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioPlanoController : ApiController
    {
        #region [ Campos ]

        private readonly IUsuarioPlanoService _usuarioPlanoService;

        #endregion

        #region [ Construtor ]

        public UsuarioPlanoController(IUsuarioPlanoService usuarioPlanoService)
        {
            _usuarioPlanoService = usuarioPlanoService;
        }

        #endregion

        #region [ Endpoints ]

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioPlanoEntity>> ObterPorId(int id)
        {
            var resultado = await _usuarioPlanoService.ObterPorIdAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<UsuarioPlanoEntity>>> ListarPorUsuario(int usuarioId)
        {
            var resultado = await _usuarioPlanoService.ListarPorUsuarioAsync(usuarioId);
            return ProcessarResultado(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Criar([FromBody] UsuarioPlanoEntity usuarioPlano)
        {
            var resultado = await _usuarioPlanoService.CriarAsync(usuarioPlano);
            return ProcessarResultado(resultado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] UsuarioPlanoEntity usuarioPlano)
        {
            if (id != usuarioPlano.Id) 
                return BadRequest("O ID da URL deve corresponder ao ID do usuário-plano.");

            var resultado = await _usuarioPlanoService.AtualizarAsync(usuarioPlano);
            return ProcessarResultado(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            var resultado = await _usuarioPlanoService.RemoverLogicamenteAsync(id);
            return ProcessarResultado(resultado);
        }

        #endregion
    }
}
