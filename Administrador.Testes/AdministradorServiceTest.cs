using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Services;
using Moq;
using Xunit;

namespace MauricioGym.Administrador.Testes
{
    public class AdministradorServiceTest
    {
        private readonly Mock<IAdministradorSqlServerRepository> _adminRepositoryMock;
        private readonly AdministradorService _adminService;

        public AdministradorServiceTest()
        {
            _adminRepositoryMock = new Mock<IAdministradorSqlServerRepository>();
            _adminService = new AdministradorService(_adminRepositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Criar_Administrador_Com_Sucesso()
        {
            var admin = new AdministradorEntity { Id = 1, Nome = "Admin", Email = "admin@email.com", Ativo = true };
            _adminRepositoryMock.Setup(r => r.CriarAsync(It.IsAny<AdministradorEntity>())).ReturnsAsync(1);

            var id = await _adminService.CriarAsync(admin);

            Assert.Equal(1, id);
        }

        [Fact]
        public async Task Deve_Listar_Administradores()
        {
            var admins = new List<AdministradorEntity> { new AdministradorEntity { Id = 1, Nome = "Admin" } };
            _adminRepositoryMock.Setup(r => r.ListarAsync()).ReturnsAsync(admins);

            var result = await _adminService.ListarAsync();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task Deve_Obter_Administrador_Por_Id()
        {
            var admin = new AdministradorEntity { Id = 1, Nome = "Admin" };
            _adminRepositoryMock.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(admin);

            var result = await _adminService.ObterPorIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task Deve_Atualizar_Administrador()
        {
            var admin = new AdministradorEntity { Id = 1, Nome = "Admin Atualizado" };
            _adminRepositoryMock.Setup(r => r.AtualizarAsync(It.IsAny<AdministradorEntity>())).ReturnsAsync(true);

            var result = await _adminService.AtualizarAsync(admin);

            Assert.True(result);
        }

        [Fact]
        public async Task Deve_Remover_Administrador_Logicamente()
        {
            _adminRepositoryMock.Setup(r => r.RemoverLogicamenteAsync(1)).ReturnsAsync(true);

            var result = await _adminService.RemoverLogicamenteAsync(1);

            Assert.True(result);
        }
    }
}
