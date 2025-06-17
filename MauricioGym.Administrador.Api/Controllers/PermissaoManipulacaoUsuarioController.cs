using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Interfaces;
using MauricioGym.Infra.Controller;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Administrador.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissaoManipulacaoUsuarioController : ApiController
    {
        #region [ Campos ]

        private readonly IPermissaoManipulacaoUsuarioService _permissaoService;

        #endregion

        #region [ Construtor ]

        public PermissaoManipulacaoUsuarioController(IPermissaoManipulacaoUsuarioService permissaoService)
        {
            _permissaoService = permissaoService;
        }

        #endregion

        #region [ Endpoints ]

        /// <summary>
        /// Obtém uma permissão específica por ID
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PermissaoManipulacaoUsuarioEntity>> ObterPorId([FromRoute] int id)
        {
            var resultado = await _permissaoService.ObterPorIdAsync(id);
            return ProcessarResultado(resultado);
        }

        /// <summary>
        /// Lista permissões por usuário
        /// </summary>
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<PermissaoManipulacaoUsuarioEntity>>> ListarPorUsuario(int usuarioId)
        {
            var resultado = await _permissaoService.ListarPorUsuarioAsync(usuarioId);
            return ProcessarResultado(resultado);
        }

        /// <summary>
        /// Cria uma nova permissão de manipulação de usuário
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<int>> Criar([FromBody] PermissaoManipulacaoUsuarioEntity permissao)
        {
            var resultado = await _permissaoService.CriarAsync(permissao);
            return ProcessarResultado(resultado);
        }

        /// <summary>
        /// Atualiza uma permissão existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] PermissaoManipulacaoUsuarioEntity permissao)
        {
            if (id != permissao.Id) 
                return BadRequest("O ID da URL deve corresponder ao ID da permissão.");

            var resultado = await _permissaoService.AtualizarAsync(permissao);
            return ProcessarResultado(resultado);
        }

        /// <summary>
        /// Remove uma permissão
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            var resultado = await _permissaoService.RemoverAsync(id);
            return ProcessarResultado(resultado);
        }

        #endregion
    }
}
