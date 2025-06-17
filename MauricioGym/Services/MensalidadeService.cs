using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;
using MauricioGym.Repositories.SqlServer.Interfaces;
using MauricioGym.Services.Interfaces;

namespace MauricioGym.Services
{
    public class MensalidadeService : IMensalidadeService
    {
        private readonly IMensalidadeSqlServerRepository _mensalidadeRepository;

        public MensalidadeService(IMensalidadeSqlServerRepository mensalidadeRepository)
        {
            _mensalidadeRepository = mensalidadeRepository;
        }

        public Task<MensalidadeEntity> ObterPorIdAsync(int id) => _mensalidadeRepository.ObterPorIdAsync(id);

        public Task<IEnumerable<MensalidadeEntity>> ListarPorPessoaAsync(int pessoaId) => _mensalidadeRepository.ListarPorPessoaAsync(pessoaId);

        public Task<int> CriarAsync(MensalidadeEntity mensalidade) => _mensalidadeRepository.CriarAsync(mensalidade);

        public Task<decimal> ObterTotalRecebidoAsync() => _mensalidadeRepository.ObterTotalRecebidoAsync();

        public decimal CalcularValorComDesconto(int meses, decimal valorBase)
        {
            decimal desconto = 0;
            if (meses == 3) desconto = 0.10m; // 10% para trimestral
            if (meses == 12) desconto = 0.20m; // 20% para anual
            var valorTotal = valorBase * meses;
            return valorTotal - (valorTotal * desconto);
        }

        public async Task<int> RegistrarMensalidadeComDesconto(int pessoaId, int planoId, int meses, decimal valorBase, DateTime inicio)
        {
            var valorComDesconto = CalcularValorComDesconto(meses, valorBase);
            var mensalidade = new MensalidadeEntity
            {
                PessoaId = pessoaId,
                PlanoId = planoId,
                ValorPago = valorComDesconto,
                DataPagamento = DateTime.Now,
                PeriodoInicio = inicio,
                PeriodoFim = inicio.AddMonths(meses)
            };
            return await CriarAsync(mensalidade);
        }
    }
}
