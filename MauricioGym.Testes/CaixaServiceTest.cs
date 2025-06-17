using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;
using MauricioGym.Repositories.SqlServer.Interfaces;
using MauricioGym.Services;
using Moq;
using Xunit;

namespace MauricioGym.Testes
{
    public class CaixaServiceTest
    {
        private readonly Mock<ICaixaSqlServerRepository> _caixaRepositoryMock;
        private readonly CaixaService _caixaService;

        public CaixaServiceTest()
        {
            _caixaRepositoryMock = new Mock<ICaixaSqlServerRepository>();
            _caixaService = new CaixaService(_caixaRepositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Criar_Caixa_Com_Sucesso()
        {
            var caixa = new CaixaEntity { Id = 1, QuantidadeAlunos = 10, ValorTotal = 1000 };
            _caixaRepositoryMock.Setup(r => r.CriarAsync(It.IsAny<CaixaEntity>())).ReturnsAsync(1);

            var id = await _caixaService.CriarAsync(caixa);

            Assert.Equal(1, id);
        }

        [Fact]
        public async Task Deve_Listar_Caixas()
        {
            var caixas = new List<CaixaEntity> { new CaixaEntity { Id = 1, QuantidadeAlunos = 10 } };
            _caixaRepositoryMock.Setup(r => r.ListarAsync()).ReturnsAsync(caixas);

            var result = await _caixaService.ListarAsync();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task Deve_Obter_Caixa_Por_Id()
        {
            var caixa = new CaixaEntity { Id = 1, QuantidadeAlunos = 10 };
            _caixaRepositoryMock.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(caixa);

            var result = await _caixaService.ObterPorIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(10, result.QuantidadeAlunos);
        }

        [Fact]
        public async Task Deve_Atualizar_Caixa()
        {
            var caixa = new CaixaEntity { Id = 1, QuantidadeAlunos = 10 };
            _caixaRepositoryMock.Setup(r => r.AtualizarAsync(caixa)).ReturnsAsync(true);

            var result = await _caixaService.AtualizarAsync(caixa);

            Assert.True(result);
        }

        [Fact]
        public async Task Deve_Remover_Caixa()
        {
            _caixaRepositoryMock.Setup(r => r.RemoverAsync(1)).ReturnsAsync(true);

            var result = await _caixaService.RemoverAsync(1);

            Assert.True(result);
        }
    }
}
