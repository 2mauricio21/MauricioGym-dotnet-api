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
    public class AdministradorControllerTest
    {
        private readonly Mock<IAdministradorService> _adminServiceMock;
        private readonly AdministradorController _controller;

        public AdministradorControllerTest()
        {
            _adminServiceMock = new Mock<IAdministradorService>();
            _controller = new AdministradorController(_adminServiceMock.Object);
        }

        [Fact]
        public async Task Criar_Deve_Criar_Administrador()
        {
            var admin = new AdministradorEntity { Id = 1, Nome = "Admin" };
            _adminServiceMock.Setup(s => s.CriarAsync(It.IsAny<AdministradorEntity>())).ReturnsAsync(1);

            var result = await _controller.Criar(admin);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(201, created.StatusCode);
        }

        [Fact]
        public async Task Remover_Deve_Remover_Administrador()
        {
            _adminServiceMock.Setup(s => s.RemoverLogicamenteAsync(1)).ReturnsAsync(true);

            var result = await _controller.Remover(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
