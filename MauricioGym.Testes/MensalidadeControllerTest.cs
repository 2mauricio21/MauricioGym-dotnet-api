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
    public class MensalidadeControllerTest
    {
        private readonly Mock<IMensalidadeService> _mensalidadeServiceMock;
        private readonly MensalidadeController _controller;

        public MensalidadeControllerTest()
        {
            _mensalidadeServiceMock = new Mock<IMensalidadeService>();
            _controller = new MensalidadeController(_mensalidadeServiceMock.Object);
        }

        [Fact]
        public async Task RegistrarComDesconto_Deve_Registrar_Mensalidade_Com_Desconto()
        {
            _mensalidadeServiceMock.Setup(s => s.RegistrarMensalidadeComDesconto(1, 1, 3, 100, It.IsAny<System.DateTime>())).ReturnsAsync(1);

            var result = await _controller.RegistrarComDesconto(1, 1, 3, 100, System.DateTime.Now);

            var created = Assert.IsType<CreatedResult>(result.Result);
            Assert.Equal(201, created.StatusCode);
        }

        [Fact]
        public async Task ObterTotalRecebido_Deve_Retornar_Valor()
        {
            _mensalidadeServiceMock.Setup(s => s.ObterTotalRecebidoAsync()).ReturnsAsync(1000m);

            var result = await _controller.ObterTotalRecebido();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(1000m, ok.Value);
        }
    }
}
