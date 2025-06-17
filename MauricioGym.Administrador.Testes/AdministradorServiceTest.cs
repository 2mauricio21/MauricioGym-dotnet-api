using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MauricioGym.Administrador.Testes
{
    public class AdministradorServiceTest
    {
        private readonly Mock<IAdministradorSqlServerRepository> _adminRepositoryMock;
        private readonly Mock<ILogger<AdministradorService>> _loggerMock;
        private readonly AdministradorService _adminService;

        public AdministradorServiceTest()
        {
            _adminRepositoryMock = new Mock<IAdministradorSqlServerRepository>();
            _loggerMock = new Mock<ILogger<AdministradorService>>();
            _adminService = new AdministradorService(_adminRepositoryMock.Object, _loggerMock.Object);
        }        [Fact]
        public async Task Deve_Criar_Administrador_Com_Sucesso()
        {
            var admin = new AdministradorEntity 
            { 
                Id = 1, 
                Nome = "Admin", 
                Email = "admin@email.com", 
                Senha = "123456",
                Ativo = true 
            };
            _adminRepositoryMock.Setup(r => r.CriarAsync(It.IsAny<AdministradorEntity>())).ReturnsAsync(1);

            var resultado = await _adminService.CriarAsync(admin);

            Assert.False(resultado.OcorreuErro);
            Assert.Equal(1, resultado.Retorno);
        }

        [Fact]
        public async Task Deve_Listar_Administradores()
        {
            var admins = new List<AdministradorEntity> { new AdministradorEntity { Id = 1, Nome = "Admin" } };
            _adminRepositoryMock.Setup(r => r.ListarAsync()).ReturnsAsync(admins);

            var resultado = await _adminService.ListarAsync();

            Assert.False(resultado.OcorreuErro);
            Assert.NotNull(resultado.Retorno);
            Assert.Single(resultado.Retorno);
        }

        [Fact]
        public async Task Deve_Obter_Administrador_Por_Id()
        {
            var admin = new AdministradorEntity { Id = 1, Nome = "Admin" };
            _adminRepositoryMock.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(admin);

            var resultado = await _adminService.ObterPorIdAsync(1);

            Assert.False(resultado.OcorreuErro);
            Assert.NotNull(resultado.Retorno);
            Assert.Equal(1, resultado.Retorno.Id);
        }

        [Fact]
        public async Task Deve_Atualizar_Administrador()
        {
            var admin = new AdministradorEntity 
            { 
                Id = 1, 
                Nome = "Admin Atualizado",
                Email = "admin@email.com",
                Senha = "123456"
            };
            _adminRepositoryMock.Setup(r => r.AtualizarAsync(It.IsAny<AdministradorEntity>())).ReturnsAsync(true);

            var resultado = await _adminService.AtualizarAsync(admin);

            Assert.False(resultado.OcorreuErro);
            Assert.True(resultado.Retorno);
        }

        [Fact]
        public async Task Deve_Remover_Administrador_Logicamente()
        {
            _adminRepositoryMock.Setup(r => r.RemoverLogicamenteAsync(1)).ReturnsAsync(true);

            var resultado = await _adminService.RemoverLogicamenteAsync(1);

            Assert.False(resultado.OcorreuErro);
            Assert.True(resultado.Retorno);
        }
    }
}
