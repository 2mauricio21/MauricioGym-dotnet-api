using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Interfaces;
using MauricioGym.Infra.Controller;
using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Administrador.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanoController : ApiController
    {
        #region [ Campos ]

        private readonly IPlanoService _planoService;

        #endregion

        #region [ Construtor ]

        public PlanoController(IPlanoService planoService)
        {
            _planoService = planoService;
        }

        #endregion

        #region [ Endpoints ]

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanoEntity>>> Listar()
        {
            var resultado = await _planoService.ListarAsync();
            return ProcessarResultado(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlanoEntity>> ObterPorId(int id)
        {
            var resultado = await _planoService.ObterPorIdAsync(id);
            return ProcessarResultado(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Criar([FromBody] PlanoEntity plano)
        {
            var resultado = await _planoService.CriarAsync(plano);
            return ProcessarResultado(resultado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] PlanoEntity plano)
        {
            if (id != plano.Id) 
                return BadRequest("O ID da URL deve corresponder ao ID do plano.");

            var resultado = await _planoService.AtualizarAsync(plano);
            return ProcessarResultado(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            var resultado = await _planoService.RemoverLogicamenteAsync(id);
            return ProcessarResultado(resultado);
        }

        #endregion
    }
}
