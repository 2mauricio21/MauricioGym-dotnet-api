using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;
using MauricioGym.Repositories.SqlServer.Interfaces;
using MauricioGym.Services;
using Moq;
using Xunit;

namespace MauricioGym.Testes
{
    public class CheckInServiceTest
    {
        private readonly Mock<ICheckInSqlServerRepository> _checkInRepositoryMock;
        private readonly CheckInService _checkInService;

        public CheckInServiceTest()
        {
            _checkInRepositoryMock = new Mock<ICheckInSqlServerRepository>();
            _checkInService = new CheckInService(_checkInRepositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Criar_CheckIn_Com_Sucesso()
        {
            var checkIn = new CheckInEntity { Id = 1, PessoaId = 1 };
            _checkInRepositoryMock.Setup(r => r.CriarAsync(It.IsAny<CheckInEntity>())).ReturnsAsync(1);

            var id = await _checkInService.CriarAsync(checkIn);

            Assert.Equal(1, id);
        }

        [Fact]
        public async Task Deve_Listar_CheckIns_Por_Pessoa()
        {
            var checkIns = new List<CheckInEntity> { new CheckInEntity { Id = 1, PessoaId = 1 } };
            _checkInRepositoryMock.Setup(r => r.ListarPorPessoaAsync(1)).ReturnsAsync(checkIns);

            var result = await _checkInService.ListarPorPessoaAsync(1);

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task Deve_Obter_CheckIn_Por_Id()
        {
            var checkIn = new CheckInEntity { Id = 1, PessoaId = 1 };
            _checkInRepositoryMock.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(checkIn);

            var result = await _checkInService.ObterPorIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.PessoaId);
        }

        [Fact]
        public async Task Deve_Contar_CheckIns_Por_Pessoa_Mes()
        {
            _checkInRepositoryMock.Setup(r => r.ContarCheckInsPorPessoaMesAsync(1, 2025, 6)).ReturnsAsync(5);

            var result = await _checkInService.ContarCheckInsPorPessoaMesAsync(1, 2025, 6);

            Assert.Equal(5, result);
        }
    }
}
