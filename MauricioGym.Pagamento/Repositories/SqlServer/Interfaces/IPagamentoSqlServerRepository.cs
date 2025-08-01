using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Infra.Repositories.Interfaces;
using MauricioGym.Pagamento.Entities;

namespace MauricioGym.Pagamento.Repositories.SqlServer.Interfaces
{
    public interface IPagamentoSqlServerRepository : ISqlServerRepository
    {
        Task<PagamentoEntity> IncluirPagamentoAsync(PagamentoEntity pagamento);
        Task<PagamentoEntity> ConsultarPagamentoAsync(int idPagamento);
        Task<PagamentoEntity> ConsultarPagamentoPorTransacaoAsync(string transacaoId);
        Task AlterarPagamentoAsync(PagamentoEntity pagamento);
        Task<IEnumerable<PagamentoEntity>> ListarPagamentosAsync();
        Task<IEnumerable<PagamentoEntity>> ListarPagamentosPorUsuarioAsync(int idUsuario);
        Task<IEnumerable<PagamentoEntity>> ListarPagamentosPorStatusAsync(string status);
    }
}