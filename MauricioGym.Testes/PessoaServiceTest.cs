using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;
using MauricioGym.Repositories.SqlServer.Interfaces;
using MauricioGym.Services;
using Moq;
using Xunit;

namespace MauricioGym.Testes
{
    public class PessoaServiceTest
    {
        private readonly Mock<IPessoaSqlServerRepository> _pessoaRepositoryMock;
        private readonly PessoaService _pessoaService;

        public PessoaServiceTest()
        {
            _pessoaRepositoryMock = new Mock<IPessoaSqlServerRepository>();
            _pessoaService = new PessoaService(_pessoaRepositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Criar_Pessoa_Com_Sucesso()
        {
            var pessoa = new PessoaEntity { Id = 1, Nome = "João", Email = "joao@email.com", Ativo = true };
            _pessoaRepositoryMock.Setup(r => r.CriarAsync(It.IsAny<PessoaEntity>())).ReturnsAsync(1);

            var id = await _pessoaService.CriarAsync(pessoa);

            Assert.Equal(1, id);
        }

        [Fact]
        public async Task Deve_Listar_Pessoas()
        {
            var pessoas = new List<PessoaEntity> { new PessoaEntity { Id = 1, Nome = "João" } };
            _pessoaRepositoryMock.Setup(r => r.ListarAsync()).ReturnsAsync(pessoas);

            var result = await _pessoaService.ListarAsync();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task Deve_Obter_Pessoa_Por_Id()
        {
            var pessoa = new PessoaEntity { Id = 1, Nome = "João" };
            _pessoaRepositoryMock.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(pessoa);

            var result = await _pessoaService.ObterPorIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("João", result.Nome);
        }

        [Fact]
        public async Task Deve_Atualizar_Pessoa()
        {
            var pessoa = new PessoaEntity { Id = 1, Nome = "João" };
            _pessoaRepositoryMock.Setup(r => r.AtualizarAsync(pessoa)).ReturnsAsync(true);

            var result = await _pessoaService.AtualizarAsync(pessoa);

            Assert.True(result);
        }

        [Fact]
        public async Task Deve_Remover_Pessoa_Logicamente()
        {
            _pessoaRepositoryMock.Setup(r => r.RemoverLogicamenteAsync(1)).ReturnsAsync(true);

            var result = await _pessoaService.RemoverLogicamenteAsync(1);

            Assert.True(result);
        }
    }
}
