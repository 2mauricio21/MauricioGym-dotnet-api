using MauricioGym.Infra.Repositories.Interfaces;
using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Repositories.SqlServer.Interfaces
{
    public interface IPagamentoSqlServerRepository : ISqlServerRepository
    {
        Task<PagamentoEntity> ObterPorIdAsync(int id);
        Task<IEnumerable<PagamentoEntity>> ObterTodosAsync();
        Task<IEnumerable<PagamentoEntity>> ObterPorClienteIdAsync(int clienteId);
        Task<IEnumerable<PagamentoEntity>> ObterPorAcademiaIdAsync(int academiaId);
        Task<IEnumerable<PagamentoEntity>> ObterPorPlanoClienteIdAsync(int planoClienteId);
        Task<IEnumerable<PagamentoEntity>> ObterPorPeriodoAsync(DateTime dataInicio, DateTime dataFim, int? academiaId = null);
        Task<IEnumerable<PagamentoEntity>> ObterPendentesAsync(int? academiaId = null);
        Task<IEnumerable<PagamentoEntity>> ObterPagamentosEmAtrasoPorUsuarioAsync(int usuarioId);
        Task<decimal> ObterTotalRecebidoPorPeriodoAsync(DateTime dataInicio, DateTime dataFim, int? academiaId = null);
        Task<int> CriarAsync(PagamentoEntity pagamento);
        Task<bool> AtualizarAsync(PagamentoEntity pagamento);
        Task<bool> ExcluirAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}