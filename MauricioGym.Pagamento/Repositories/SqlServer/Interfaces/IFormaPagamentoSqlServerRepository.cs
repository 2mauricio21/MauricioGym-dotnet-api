using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Infra.Repositories.Interfaces;
using MauricioGym.Pagamento.Entities;

namespace MauricioGym.Pagamento.Repositories.SqlServer.Interfaces
{
    public interface IFormaPagamentoSqlServerRepository : ISqlServerRepository
    {
        Task<FormaPagamentoEntity> IncluirAsync(FormaPagamentoEntity formaPagamento);
        Task<FormaPagamentoEntity> ObterPorIdAsync(int idFormaPagamento);
        Task<FormaPagamentoEntity> ObterPorNomeAsync(string nome, int idAcademia);
        Task<bool> AtualizarAsync(FormaPagamentoEntity formaPagamento);
        Task<bool> ExcluirAsync(int idFormaPagamento);
        Task<IEnumerable<FormaPagamentoEntity>> ListarTodosAsync();
        Task<IEnumerable<FormaPagamentoEntity>> ListarPorAcademiaAsync(int idAcademia);
        Task<IEnumerable<FormaPagamentoEntity>> ListarAtivosAsync();
    }
}