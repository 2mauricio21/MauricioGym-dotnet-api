using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.Interfaces;
using MauricioGym.Usuario.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MauricioGym.Usuario.Testes
{
    public class MensalidadeServiceTest
    {
        private readonly Mock<IMensalidadeSqlServerRepository> _mensalidadeRepositoryMock;
        private readonly Mock<ILogger<MensalidadeService>> _loggerMock;
        private readonly MensalidadeService _mensalidadeService;

        public MensalidadeServiceTest()
        {
            _mensalidadeRepositoryMock = new Mock<IMensalidadeSqlServerRepository>();
            _loggerMock = new Mock<ILogger<MensalidadeService>>();
            _mensalidadeService = new MensalidadeService(_mensalidadeRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Deve_Registrar_Pagamento_Mensalidade_Com_Sucesso()
        {
            _mensalidadeRepositoryMock.Setup(r => r.CriarAsync(It.IsAny<MensalidadeEntity>())).ReturnsAsync(1);

            var id = await _mensalidadeService.RegistrarPagamentoMensalidadeAsync(1, 1, 3, 100, DateTime.Now);

            Assert.Equal(1, id);
        }        [Fact]
        public void Deve_Calcular_Desconto_Trimestral()
        {
            var valorBase = 100m;
            var meses = 3;

            var valorComDesconto = _mensalidadeService.CalcularValorComDesconto(meses, valorBase);

            Assert.Equal(285m, valorComDesconto); // 300 - 5% = 285
        }

        [Fact]
        public void Deve_Calcular_Desconto_Semestral()
        {
            var valorBase = 100m;
            var meses = 6;

            var valorComDesconto = _mensalidadeService.CalcularValorComDesconto(meses, valorBase);

            Assert.Equal(540m, valorComDesconto); // 600 - 10% = 540
        }

        [Fact]
        public void Deve_Calcular_Desconto_Anual()
        {
            var valorBase = 100m;
            var meses = 12;

            var valorComDesconto = _mensalidadeService.CalcularValorComDesconto(meses, valorBase);

            Assert.Equal(1020m, valorComDesconto); // 1200 - 15% = 1020
        }

        [Fact]
        public void Deve_Calcular_Sem_Desconto_Para_Periodo_Mensal()
        {
            var valorBase = 100m;
            var meses = 1;

            var valorComDesconto = _mensalidadeService.CalcularValorComDesconto(meses, valorBase);

            Assert.Equal(100m, valorComDesconto); // Sem desconto
        }        [Fact]
        public async Task Deve_Registrar_Pagamento_Com_Desconto()
        {
            _mensalidadeRepositoryMock.Setup(r => r.CriarAsync(It.IsAny<MensalidadeEntity>())).ReturnsAsync(1);

            var id = await _mensalidadeService.RegistrarPagamentoMensalidadeAsync(1, 1, 3, 100m, DateTime.Now);

            Assert.Equal(1, id);
            _mensalidadeRepositoryMock.Verify(r => r.CriarAsync(It.Is<MensalidadeEntity>(m => m.Valor == 285m)), Times.Once);
        }

        [Fact]
        public async Task Deve_Verificar_Mensalidade_Em_Dia()
        {
            _mensalidadeRepositoryMock.Setup(r => r.EstaEmDiaAsync(1)).ReturnsAsync(true);

            var result = await _mensalidadeService.VerificarMensalidadeEmDiaAsync(1);

            Assert.True(result);
        }

        [Fact]
        public async Task Deve_Verificar_Mensalidade_Vencida()
        {
            _mensalidadeRepositoryMock.Setup(r => r.EstaEmDiaAsync(1)).ReturnsAsync(false);

            var result = await _mensalidadeService.VerificarMensalidadeEmDiaAsync(1);            Assert.False(result);
        }
    }
}
