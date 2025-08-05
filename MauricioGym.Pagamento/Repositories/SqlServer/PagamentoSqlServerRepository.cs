using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Pagamento.Entities;
using MauricioGym.Pagamento.Repositories.SqlServer.Interfaces;
using MauricioGym.Pagamento.Repositories.SqlServer.Queries;

namespace MauricioGym.Pagamento.Repositories.SqlServer
{
    public class PagamentoSqlServerRepository : SqlServerRepository, IPagamentoSqlServerRepository
    {
        public PagamentoSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<PagamentoEntity> IncluirAsync(PagamentoEntity pagamento)
        {
            pagamento.IdPagamento = (await QueryAsync<int>(PagamentoSqlServerQuery.IncluirPagamento, pagamento)).Single();
            return pagamento;
        }

        public async Task<PagamentoEntity> ObterPorIdAsync(int idPagamento)
        {
            var p = new DynamicParameters();
            p.Add("@IdPagamento", idPagamento);
            return (await QueryAsync<PagamentoEntity>(PagamentoSqlServerQuery.ConsultarPagamento, p)).FirstOrDefault();
        }

        public async Task<PagamentoEntity> ObterPorTransacaoAsync(string transacaoId)
        {
            var p = new DynamicParameters();
            p.Add("@TransacaoId", transacaoId);
            return (await QueryAsync<PagamentoEntity>(PagamentoSqlServerQuery.ConsultarPagamentoPorTransacao, p)).FirstOrDefault();
        }

        public async Task AtualizarAsync(PagamentoEntity pagamento)
        {
            await ExecuteNonQueryAsync(PagamentoSqlServerQuery.AlterarPagamento, pagamento);
        }

        public async Task<bool> CancelarAsync(int idPagamento)
        {
            var p = new DynamicParameters();
            p.Add("@IdPagamento", idPagamento);
            var affectedRows = await ExecuteNonQueryAsync(PagamentoSqlServerQuery.CancelarPagamento, p);
            return affectedRows > 0;
        }

        public async Task<IEnumerable<PagamentoEntity>> ListarTodosAsync()
        {
            return await QueryAsync<PagamentoEntity>(PagamentoSqlServerQuery.ListarPagamentos);
        }

        public async Task<IEnumerable<PagamentoEntity>> ListarPorUsuarioAsync(int idUsuario)
        {
            var p = new DynamicParameters();
            p.Add("@IdUsuario", idUsuario);
            return await QueryAsync<PagamentoEntity>(PagamentoSqlServerQuery.ConsultarPagamentosPorUsuario, p);
        }

        public async Task<IEnumerable<PagamentoEntity>> ListarPorUsuarioPlanoAsync(int idUsuarioPlano)
        {
            var p = new DynamicParameters();
            p.Add("@IdUsuarioPlano", idUsuarioPlano);
            return await QueryAsync<PagamentoEntity>(PagamentoSqlServerQuery.ConsultarPagamentosPorUsuarioPlano, p);
        }

        public async Task<IEnumerable<PagamentoEntity>> ListarPorStatusAsync(string status)
        {
            var p = new DynamicParameters();
            p.Add("@Status", status);
            return await QueryAsync<PagamentoEntity>(PagamentoSqlServerQuery.ListarPagamentosPorStatus, p);
        }

        public async Task<IEnumerable<PagamentoEntity>> ListarPendentesAsync()
        {
            return await QueryAsync<PagamentoEntity>(PagamentoSqlServerQuery.ListarPagamentosPendentes);
        }
    }
}