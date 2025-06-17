using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Api.Controllers;
using MauricioGym.Entities;
using MauricioGym.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace MauricioGym.Testes
{
    public class PlanoControllerTest
    {
        private readonly Mock<IPlanoService> _planoServiceMock;
        private readonly PlanoController _controller;

        public PlanoControllerTest()
        {
            _planoServiceMock = new Mock<IPlanoService>();
            _controller = new PlanoController(_planoServiceMock.Object);
        }

        [Fact]
        public async Task Criar_Deve_Criar_Plano_Retorna_Created()
        {
            var plano = new PlanoEntity { Id = 1, Nome = "Mensal" };
            _planoServiceMock.Setup(s => s.CriarAsync(It.IsAny<PlanoEntity>())).ReturnsAsync(1);

            var result = await _controller.Criar(plano);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(201, created.StatusCode);
        }

        [Fact]
        public async Task Listar_Deve_Listar_Planos()
        {
            var planos = new List<PlanoEntity> { new PlanoEntity { Id = 1, Nome = "Mensal" } };
            _planoServiceMock.Setup(s => s.ListarAsync()).ReturnsAsync(planos);

            var result = await _controller.Listar();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var retorno = Assert.IsType<List<PlanoEntity>>(ok.Value);
            Assert.Single(retorno);
        }

        [Fact]
        public async Task Remover_Deve_Remover_Plano_Retorna_NoContent()
        {
            _planoServiceMock.Setup(s => s.RemoverLogicamenteAsync(1)).ReturnsAsync(true);

            var result = await _controller.Remover(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
