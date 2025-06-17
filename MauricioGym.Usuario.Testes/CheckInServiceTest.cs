using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.Interfaces;
using MauricioGym.Usuario.Services;
using MauricioGym.Usuario.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Infra.Shared;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MauricioGym.Usuario.Testes
{
    public class CheckInServiceTest
    {
        private readonly Mock<ICheckInSqlServerRepository> _checkInRepositoryMock;
        private readonly Mock<IMensalidadeService> _mensalidadeServiceMock;
        private readonly Mock<ILogger<CheckInService>> _loggerMock;
        private readonly CheckInService _checkInService;

        public CheckInServiceTest()
        {
            _checkInRepositoryMock = new Mock<ICheckInSqlServerRepository>();
            _mensalidadeServiceMock = new Mock<IMensalidadeService>();
            _loggerMock = new Mock<ILogger<CheckInService>>();
            _checkInService = new CheckInService(_checkInRepositoryMock.Object, _mensalidadeServiceMock.Object, _loggerMock.Object);
        }        [Fact]
        public async Task Deve_Criar_CheckIn_Com_Mensalidade_Em_Dia()
        {
            var checkIn = new CheckInEntity { UsuarioId = 1, DataHora = DateTime.Now };
            var resultadoMensalidade = new ResultadoValidacao<bool>(true);
            
            _mensalidadeServiceMock.Setup(s => s.VerificarMensalidadeEmDiaAsync(1)).ReturnsAsync(resultadoMensalidade);
            _checkInRepositoryMock.Setup(r => r.CriarAsync(It.IsAny<CheckInEntity>())).ReturnsAsync(1);

            var resultado = await _checkInService.CriarAsync(checkIn);

            Assert.False(resultado.OcorreuErro);
            Assert.Equal(1, resultado.Retorno);
        }

        [Fact]
        public async Task Deve_Impedir_CheckIn_Com_Mensalidade_Vencida()
        {
            var checkIn = new CheckInEntity { UsuarioId = 1, DataHora = DateTime.Now };
            var resultadoMensalidade = new ResultadoValidacao<bool>(false);
            
            _mensalidadeServiceMock.Setup(s => s.VerificarMensalidadeEmDiaAsync(1)).ReturnsAsync(resultadoMensalidade);

            var resultado = await _checkInService.CriarAsync(checkIn);

            Assert.True(resultado.OcorreuErro);
        }

        [Fact]
        public async Task Deve_Impedir_CheckIn_Sem_Mensalidade()
        {
            var checkIn = new CheckInEntity { UsuarioId = 1, DataHora = DateTime.Now };
            var resultadoMensalidade = new ResultadoValidacao<bool>(false);
            
            _mensalidadeServiceMock.Setup(s => s.VerificarMensalidadeEmDiaAsync(1)).ReturnsAsync(resultadoMensalidade);

            var resultado = await _checkInService.CriarAsync(checkIn);

            Assert.True(resultado.OcorreuErro);
        }

        [Fact]
        public async Task Deve_Listar_CheckIns_Por_Usuario()
        {
            var checkIns = new List<CheckInEntity> { new CheckInEntity { Id = 1, UsuarioId = 1 } };
            _checkInRepositoryMock.Setup(r => r.ObterPorUsuarioAsync(1)).ReturnsAsync(checkIns);

            var resultado = await _checkInService.ListarPorUsuarioAsync(1);

            Assert.False(resultado.OcorreuErro);
            Assert.NotNull(resultado.Retorno);
            Assert.Single(resultado.Retorno);
        }

        [Fact]
        public async Task Deve_Verificar_Se_Pode_Fazer_CheckIn()
        {
            var resultadoMensalidade = new ResultadoValidacao<bool>(true);
            _mensalidadeServiceMock.Setup(s => s.VerificarMensalidadeEmDiaAsync(1)).ReturnsAsync(resultadoMensalidade);

            var resultado = await _checkInService.PodeRealizarCheckInAsync(1);

            Assert.False(resultado.OcorreuErro);
            Assert.True(resultado.Retorno);
        }
    }
}
