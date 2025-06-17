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
    public class CheckInControllerTest
    {
        private readonly Mock<ICheckInService> _checkInServiceMock;
        private readonly CheckInController _controller;

        public CheckInControllerTest()
        {
            _checkInServiceMock = new Mock<ICheckInService>();
            _controller = new CheckInController(_checkInServiceMock.Object);
        }

        [Fact]
        public async Task Criar_Deve_Registrar_CheckIn()
        {
            var checkIn = new CheckInEntity { Id = 1, PessoaId = 1 };
            _checkInServiceMock.Setup(s => s.CriarAsync(It.IsAny<CheckInEntity>())).ReturnsAsync(1);

            var result = await _controller.Criar(checkIn);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(201, created.StatusCode);
        }

        [Fact]
        public async Task ListarPorPessoa_Deve_Listar_CheckIns()
        {
            var checkIns = new List<CheckInEntity> { new CheckInEntity { Id = 1, PessoaId = 1 } };
            _checkInServiceMock.Setup(s => s.ListarPorPessoaAsync(1)).ReturnsAsync(checkIns);

            var result = await _controller.ListarPorPessoa(1);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var retorno = Assert.IsType<List<CheckInEntity>>(ok.Value);
            Assert.Single(retorno);
        }
    }
}
