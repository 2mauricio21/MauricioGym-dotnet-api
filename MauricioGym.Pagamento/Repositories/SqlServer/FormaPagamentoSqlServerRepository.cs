using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Pagamento.Entities;
using MauricioGym.Pagamento.Repositories.SqlServer.Interfaces;
using MauricioGym.Pagamento.Repositories.SqlServer.Queries;

namespace MauricioGym.Pagamento.Repositories.SqlServer
{
    public class FormaPagamentoSqlServerRepository : SqlServerRepository, IFormaPagamentoSqlServerRepository
    {
        public FormaPagamentoSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<FormaPagamentoEntity> IncluirAsync(FormaPagamentoEntity formaPagamento)
        {
            formaPagamento.IdFormaPagamento = (await QueryAsync<int>(FormaPagamentoSqlServerQuery.IncluirFormaPagamento, formaPagamento)).Single();
            return formaPagamento;
        }

        public async Task<FormaPagamentoEntity> ObterPorIdAsync(int idFormaPagamento)
        {
            var p = new DynamicParameters();
            p.Add("@IdFormaPagamento", idFormaPagamento);
            return (await QueryAsync<FormaPagamentoEntity>(FormaPagamentoSqlServerQuery.ConsultarFormaPagamento, p)).FirstOrDefault();
        }

        public async Task<FormaPagamentoEntity> ObterPorNomeAsync(string nome, int idAcademia)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", nome);
            p.Add("@IdAcademia", idAcademia);
            return (await QueryAsync<FormaPagamentoEntity>(FormaPagamentoSqlServerQuery.ConsultarFormaPagamentoPorNome, p)).FirstOrDefault();
        }

        public async Task<bool> AtualizarAsync(FormaPagamentoEntity formaPagamento)
        {
            var affectedRows = await ExecuteNonQueryAsync(FormaPagamentoSqlServerQuery.AlterarFormaPagamento, formaPagamento);
            return affectedRows > 0;
        }

        public async Task<bool> ExcluirAsync(int idFormaPagamento)
        {
            var p = new DynamicParameters();
            p.Add("@IdFormaPagamento", idFormaPagamento);
            var affectedRows = await ExecuteNonQueryAsync(FormaPagamentoSqlServerQuery.ExcluirFormaPagamento, p);
            return affectedRows > 0;
        }

        public async Task<IEnumerable<FormaPagamentoEntity>> ListarTodosAsync()
        {
            return await QueryAsync<FormaPagamentoEntity>(FormaPagamentoSqlServerQuery.ListarFormasPagamento);
        }

        public async Task<IEnumerable<FormaPagamentoEntity>> ListarPorAcademiaAsync(int idAcademia)
        {
            var p = new DynamicParameters();
            p.Add("@IdAcademia", idAcademia);
            return await QueryAsync<FormaPagamentoEntity>(FormaPagamentoSqlServerQuery.ListarFormasPagamentoPorAcademia, p);
        }

        public async Task<IEnumerable<FormaPagamentoEntity>> ListarAtivosAsync()
        {
            return await QueryAsync<FormaPagamentoEntity>(FormaPagamentoSqlServerQuery.ListarFormasPagamentoAtivas);
        }
    }
}