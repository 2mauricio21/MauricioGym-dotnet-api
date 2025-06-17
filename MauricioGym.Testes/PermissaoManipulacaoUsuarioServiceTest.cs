using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;
using MauricioGym.Repositories.SqlServer.Interfaces;
using MauricioGym.Services;
using Moq;
using Xunit;

namespace MauricioGym.Testes
{
    public class PermissaoManipulacaoUsuarioServiceTest
    {
        private readonly Mock<IPermissaoManipulacaoUsuarioSqlServerRepository> _permissaoRepositoryMock;
        private readonly PermissaoManipulacaoUsuarioService _permissaoService;

        public PermissaoManipulacaoUsuarioServiceTest()
        {
            _permissaoRepositoryMock = new Mock<IPermissaoManipulacaoUsuarioSqlServerRepository>();
            _permissaoService = new PermissaoManipulacaoUsuarioService(_permissaoRepositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Criar_Permissao_Com_Sucesso()
        {
            var permissao = new PermissaoManipulacaoUsuarioEntity { Id = 1, PessoaId = 1, AdministradorId = 1 };
            _permissaoRepositoryMock.Setup(r => r.CriarAsync(It.IsAny<PermissaoManipulacaoUsuarioEntity>())).ReturnsAsync(1);

            var id = await _permissaoService.CriarAsync(permissao);

            Assert.Equal(1, id);
        }

        [Fact]
        public async Task Deve_Listar_Permissoes_Por_Pessoa()
        {
            var permissoes = new List<PermissaoManipulacaoUsuarioEntity> { new PermissaoManipulacaoUsuarioEntity { Id = 1, PessoaId = 1 } };
            _permissaoRepositoryMock.Setup(r => r.ListarPorPessoaAsync(1)).ReturnsAsync(permissoes);

            var result = await _permissaoService.ListarPorPessoaAsync(1);

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task Deve_Obter_Permissao_Por_Id()
        {
            var permissao = new PermissaoManipulacaoUsuarioEntity { Id = 1, PessoaId = 1 };
            _permissaoRepositoryMock.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(permissao);

            var result = await _permissaoService.ObterPorIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.PessoaId);
        }

        [Fact]
        public async Task Deve_Atualizar_Permissao()
        {
            var permissao = new PermissaoManipulacaoUsuarioEntity { Id = 1, PessoaId = 1 };
            _permissaoRepositoryMock.Setup(r => r.AtualizarAsync(permissao)).ReturnsAsync(true);

            var result = await _permissaoService.AtualizarAsync(permissao);

            Assert.True(result);
        }

        [Fact]
        public async Task Deve_Remover_Permissao()
        {
            _permissaoRepositoryMock.Setup(r => r.RemoverAsync(1)).ReturnsAsync(true);

            var result = await _permissaoService.RemoverAsync(1);

            Assert.True(result);
        }
    }
}
