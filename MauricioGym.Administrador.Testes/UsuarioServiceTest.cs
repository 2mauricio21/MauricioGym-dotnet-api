using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Services;
using Moq;
using Xunit;

namespace MauricioGym.Administrador.Testes
{
    public class UsuarioServiceTest
    {
        private readonly Mock<IUsuarioSqlServerRepository> _usuarioRepositoryMock;
        private readonly UsuarioService _usuarioService;

        public UsuarioServiceTest()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioSqlServerRepository>();
            _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Criar_Usuario_Com_Sucesso()
        {
            var usuario = new UsuarioEntity { Id = 1, Nome = "João", Email = "joao@email.com", Ativo = true };
            _usuarioRepositoryMock.Setup(r => r.CriarAsync(It.IsAny<UsuarioEntity>())).ReturnsAsync(1);

            var id = await _usuarioService.CriarAsync(usuario);

            Assert.Equal(1, id);
        }

        [Fact]
        public async Task Deve_Listar_Usuarios()
        {
            var usuarios = new List<UsuarioEntity> { new UsuarioEntity { Id = 1, Nome = "João" } };
            _usuarioRepositoryMock.Setup(r => r.ListarAsync()).ReturnsAsync(usuarios);

            var result = await _usuarioService.ListarAsync();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task Deve_Obter_Usuario_Por_Id()
        {
            var usuario = new UsuarioEntity { Id = 1, Nome = "João" };
            _usuarioRepositoryMock.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(usuario);

            var result = await _usuarioService.ObterPorIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task Deve_Atualizar_Usuario()
        {
            var usuario = new UsuarioEntity { Id = 1, Nome = "João Atualizado" };
            _usuarioRepositoryMock.Setup(r => r.AtualizarAsync(It.IsAny<UsuarioEntity>())).ReturnsAsync(true);

            var result = await _usuarioService.AtualizarAsync(usuario);

            Assert.True(result);
        }

        [Fact]
        public async Task Deve_Remover_Usuario_Logicamente()
        {
            _usuarioRepositoryMock.Setup(r => r.RemoverLogicamenteAsync(1)).ReturnsAsync(true);

            var result = await _usuarioService.RemoverLogicamenteAsync(1);

            Assert.True(result);
        }
    }
}
