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
    public class PessoaControllerTest
    {
        private readonly Mock<IPessoaService> _pessoaServiceMock;
        private readonly PessoaController _controller;

        public PessoaControllerTest()
        {
            _pessoaServiceMock = new Mock<IPessoaService>();
            _controller = new PessoaController(_pessoaServiceMock.Object);
        }

        [Fact]
        public async Task Criar_Deve_Criar_Pessoa_Retorna_Created()
        {
            var pessoa = new PessoaEntity { Id = 1, Nome = "João" };
            _pessoaServiceMock.Setup(s => s.CriarAsync(It.IsAny<PessoaEntity>())).ReturnsAsync(1);

            var result = await _controller.Criar(pessoa);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(201, created.StatusCode);
        }

        [Fact]
        public async Task ObterPorId_Deve_Retornar_Pessoa_Por_Id()
        {
            var pessoa = new PessoaEntity { Id = 1, Nome = "João" };
            _pessoaServiceMock.Setup(s => s.ObterPorIdAsync(1)).ReturnsAsync(pessoa);

            var result = await _controller.ObterPorId(1);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var retorno = Assert.IsType<PessoaEntity>(ok.Value);
            Assert.Equal("João", retorno.Nome);
        }

        [Fact]
        public async Task ObterPorId_Deve_Retornar_NotFound_Se_Nao_Encontrar()
        {
            _pessoaServiceMock.Setup(s => s.ObterPorIdAsync(1)).ReturnsAsync((PessoaEntity)null);

            var result = await _controller.ObterPorId(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Remover_Deve_Remover_Pessoa_Retorna_NoContent()
        {
            _pessoaServiceMock.Setup(s => s.RemoverLogicamenteAsync(1)).ReturnsAsync(true);

            var result = await _controller.Remover(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
