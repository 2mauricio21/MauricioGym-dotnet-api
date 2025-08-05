using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Infra.Repositories.Interfaces;
using MauricioGym.Pagamento.Entities;

namespace MauricioGym.Pagamento.Repositories.SqlServer.Interfaces
{
    public interface IPagamentoSqlServerRepository : ISqlServerRepository
    {
        Task<PagamentoEntity> IncluirAsync(PagamentoEntity pagamento);
        Task<PagamentoEntity> ObterPorIdAsync(int idPagamento);
        Task<PagamentoEntity> ObterPorTransacaoAsync(string transacaoId);
        Task AtualizarAsync(PagamentoEntity pagamento);
        Task<bool> CancelarAsync(int idPagamento);
        Task<IEnumerable<PagamentoEntity>> ListarTodosAsync();
        Task<IEnumerable<PagamentoEntity>> ListarPorUsuarioAsync(int idUsuario);
        Task<IEnumerable<PagamentoEntity>> ListarPorUsuarioPlanoAsync(int idUsuarioPlano);
        Task<IEnumerable<PagamentoEntity>> ListarPorStatusAsync(string status);
        Task<IEnumerable<PagamentoEntity>> ListarPendentesAsync();
    }
}