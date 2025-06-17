using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;
using MauricioGym.Repositories.SqlServer.Interfaces;
using MauricioGym.Services;
using Moq;
using Xunit;

namespace MauricioGym.Testes
{
    public class PlanoServiceTest
    {
        private readonly Mock<IPlanoSqlServerRepository> _planoRepositoryMock;
        private readonly PlanoService _planoService;

        public PlanoServiceTest()
        {
            _planoRepositoryMock = new Mock<IPlanoSqlServerRepository>();
            _planoService = new PlanoService(_planoRepositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Criar_Plano_Com_Sucesso()
        {
            var plano = new PlanoEntity { Id = 1, Nome = "Mensal", Valor = 100 };
            _planoRepositoryMock.Setup(r => r.CriarAsync(It.IsAny<PlanoEntity>())).ReturnsAsync(1);

            var id = await _planoService.CriarAsync(plano);

            Assert.Equal(1, id);
        }

        [Fact]
        public async Task Deve_Listar_Planos()
        {
            var planos = new List<PlanoEntity> { new PlanoEntity { Id = 1, Nome = "Mensal" } };
            _planoRepositoryMock.Setup(r => r.ListarAsync()).ReturnsAsync(planos);

            var result = await _planoService.ListarAsync();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task Deve_Obter_Plano_Por_Id()
        {
            var plano = new PlanoEntity { Id = 1, Nome = "Mensal" };
            _planoRepositoryMock.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(plano);

            var result = await _planoService.ObterPorIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Mensal", result.Nome);
        }

        [Fact]
        public async Task Deve_Atualizar_Plano()
        {
            var plano = new PlanoEntity { Id = 1, Nome = "Mensal" };
            _planoRepositoryMock.Setup(r => r.AtualizarAsync(plano)).ReturnsAsync(true);

            var result = await _planoService.AtualizarAsync(plano);

            Assert.True(result);
        }

        [Fact]
        public async Task Deve_Remover_Plano_Logicamente()
        {
            _planoRepositoryMock.Setup(r => r.RemoverLogicamenteAsync(1)).ReturnsAsync(true);

            var result = await _planoService.RemoverLogicamenteAsync(1);

            Assert.True(result);
        }
    }
}
