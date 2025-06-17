using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;
using MauricioGym.Repositories.SqlServer.Interfaces;
using MauricioGym.Services;
using Moq;
using Xunit;

namespace MauricioGym.Testes
{
    public class MensalidadeServiceTest
    {
        private readonly Mock<IMensalidadeSqlServerRepository> _mensalidadeRepositoryMock;
        private readonly MensalidadeService _mensalidadeService;

        public MensalidadeServiceTest()
        {
            _mensalidadeRepositoryMock = new Mock<IMensalidadeSqlServerRepository>();
            _mensalidadeService = new MensalidadeService(_mensalidadeRepositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Criar_Mensalidade_Com_Sucesso()
        {
            var mensalidade = new MensalidadeEntity { Id = 1, PessoaId = 1, PlanoId = 1 };
            _mensalidadeRepositoryMock.Setup(r => r.CriarAsync(It.IsAny<MensalidadeEntity>())).ReturnsAsync(1);

            var id = await _mensalidadeService.CriarAsync(mensalidade);

            Assert.Equal(1, id);
        }

        [Fact]
        public async Task Deve_Listar_Mensalidades_Por_Pessoa()
        {
            var mensalidades = new List<MensalidadeEntity> { new MensalidadeEntity { Id = 1, PessoaId = 1, PlanoId = 1 } };
            _mensalidadeRepositoryMock.Setup(r => r.ListarPorPessoaAsync(1)).ReturnsAsync(mensalidades);

            var result = await _mensalidadeService.ListarPorPessoaAsync(1);

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task Deve_Obter_Mensalidade_Por_Id()
        {
            var mensalidade = new MensalidadeEntity { Id = 1, PessoaId = 1, PlanoId = 1 };
            _mensalidadeRepositoryMock.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(mensalidade);

            var result = await _mensalidadeService.ObterPorIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.PessoaId);
        }

        [Fact]
        public async Task Deve_Obter_Total_Recebido()
        {
            _mensalidadeRepositoryMock.Setup(r => r.ObterTotalRecebidoAsync()).ReturnsAsync(1000m);

            var result = await _mensalidadeService.ObterTotalRecebidoAsync();

            Assert.Equal(1000m, result);
        }
    }
}
