using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;
using MauricioGym.Repositories.SqlServer.Interfaces;
using MauricioGym.Services;
using Moq;
using Xunit;

namespace MauricioGym.Testes
{
    public class PessoaPlanoServiceTest
    {
        private readonly Mock<IPessoaPlanoSqlServerRepository> _pessoaPlanoRepositoryMock;
        private readonly PessoaPlanoService _pessoaPlanoService;

        public PessoaPlanoServiceTest()
        {
            _pessoaPlanoRepositoryMock = new Mock<IPessoaPlanoSqlServerRepository>();
            _pessoaPlanoService = new PessoaPlanoService(_pessoaPlanoRepositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Criar_PessoaPlano_Com_Sucesso()
        {
            var pessoaPlano = new PessoaPlanoEntity { Id = 1, PessoaId = 1, PlanoId = 1 };
            _pessoaPlanoRepositoryMock.Setup(r => r.CriarAsync(It.IsAny<PessoaPlanoEntity>())).ReturnsAsync(1);

            var id = await _pessoaPlanoService.CriarAsync(pessoaPlano);

            Assert.Equal(1, id);
        }

        [Fact]
        public async Task Deve_Listar_PessoaPlanos_Por_Pessoa()
        {
            var pessoaPlanos = new List<PessoaPlanoEntity> { new PessoaPlanoEntity { Id = 1, PessoaId = 1, PlanoId = 1 } };
            _pessoaPlanoRepositoryMock.Setup(r => r.ListarPorPessoaAsync(1)).ReturnsAsync(pessoaPlanos);

            var result = await _pessoaPlanoService.ListarPorPessoaAsync(1);

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task Deve_Obter_PessoaPlano_Por_Id()
        {
            var pessoaPlano = new PessoaPlanoEntity { Id = 1, PessoaId = 1, PlanoId = 1 };
            _pessoaPlanoRepositoryMock.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(pessoaPlano);

            var result = await _pessoaPlanoService.ObterPorIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.PessoaId);
        }

        [Fact]
        public async Task Deve_Atualizar_PessoaPlano()
        {
            var pessoaPlano = new PessoaPlanoEntity { Id = 1, PessoaId = 1, PlanoId = 1 };
            _pessoaPlanoRepositoryMock.Setup(r => r.AtualizarAsync(pessoaPlano)).ReturnsAsync(true);

            var result = await _pessoaPlanoService.AtualizarAsync(pessoaPlano);

            Assert.True(result);
        }

        [Fact]
        public async Task Deve_Remover_PessoaPlano_Logicamente()
        {
            _pessoaPlanoRepositoryMock.Setup(r => r.RemoverLogicamenteAsync(1)).ReturnsAsync(true);

            var result = await _pessoaPlanoService.RemoverLogicamenteAsync(1);

            Assert.True(result);
        }
    }
}
