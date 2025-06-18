using Dapper;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.SqlServer.Interfaces;
using MauricioGym.Usuario.Repositories.SqlServer.Queries;

namespace MauricioGym.Usuario.Repositories.SqlServer
{
    public class PagamentoSqlServerRepository : SqlServerRepository, IPagamentoSqlServerRepository
    {
        public PagamentoSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<PagamentoEntity> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entidade = await QueryAsync<PagamentoEntity>(PagamentoSqlServerQuery.ObterPorId, p);
            return entidade.FirstOrDefault();
        }

        public async Task<IEnumerable<PagamentoEntity>> ObterPorClienteIdAsync(int clienteId)
        {
            var p = new DynamicParameters();
            p.Add("@ClienteId", clienteId);

            return await QueryAsync<PagamentoEntity>(PagamentoSqlServerQuery.ObterPorClienteId, p);
        }

        public async Task<IEnumerable<PagamentoEntity>> ObterPorAcademiaIdAsync(int academiaId)
        {
            var p = new DynamicParameters();
            p.Add("@AcademiaId", academiaId);

            return await QueryAsync<PagamentoEntity>(PagamentoSqlServerQuery.ObterPorAcademiaId, p);
        }

        public async Task<IEnumerable<PagamentoEntity>> ObterPorPlanoClienteIdAsync(int planoClienteId)
        {
            var p = new DynamicParameters();
            p.Add("@PlanoClienteId", planoClienteId);

            return await QueryAsync<PagamentoEntity>(PagamentoSqlServerQuery.ObterPorPlanoClienteId, p);
        }

        public async Task<IEnumerable<PagamentoEntity>> ObterPorPeriodoAsync(DateTime dataInicio, DateTime dataFim, int? academiaId = null)
        {
            var p = new DynamicParameters();
            p.Add("@DataInicio", dataInicio);
            p.Add("@DataFim", dataFim);

            string query;
            if (academiaId.HasValue)
            {
                query = PagamentoSqlServerQuery.ObterPorPeriodoEAcademia;
                p.Add("@AcademiaId", academiaId.Value);
            }
            else
            {
                query = PagamentoSqlServerQuery.ObterPorPeriodo;
            }

            return await QueryAsync<PagamentoEntity>(query, p);
        }

        public async Task<IEnumerable<PagamentoEntity>> ObterPendentesAsync(int? academiaId = null)
        {
            var p = new DynamicParameters();

            string query;
            if (academiaId.HasValue)
            {
                query = PagamentoSqlServerQuery.ObterPendentesPorAcademia;
                p.Add("@AcademiaId", academiaId.Value);
            }
            else
            {
                query = PagamentoSqlServerQuery.ObterPendentes;
            }

            return await QueryAsync<PagamentoEntity>(query, p);
        }

        public async Task<decimal> ObterTotalRecebidoPorPeriodoAsync(DateTime dataInicio, DateTime dataFim, int? academiaId = null)
        {
            var p = new DynamicParameters();
            p.Add("@DataInicio", dataInicio);
            p.Add("@DataFim", dataFim);

            string query;
            if (academiaId.HasValue)
            {
                query = PagamentoSqlServerQuery.ObterTotalRecebidoPorPeriodoEAcademia;
                p.Add("@AcademiaId", academiaId.Value);
            }
            else
            {
                query = PagamentoSqlServerQuery.ObterTotalRecebidoPorPeriodo;
            }

            var entidades = await QueryAsync<decimal?>(query, p);
            return entidades.FirstOrDefault() ?? 0;
        }

        public async Task<int> CriarAsync(PagamentoEntity pagamento)
        {
            await ExecuteNonQueryAsync(PagamentoSqlServerQuery.Criar, pagamento);
            var ultimoId = await QueryAsync<int>("SELECT SCOPE_IDENTITY()", new DynamicParameters());
            return ultimoId.FirstOrDefault();
        }

        public async Task<bool> AtualizarAsync(PagamentoEntity pagamento)
        {
            var linhasAfetadas = await ExecuteNonQueryAsync(PagamentoSqlServerQuery.Atualizar, pagamento);
            return linhasAfetadas > 0;
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@DataExclusao", DateTime.Now);

            var linhasAfetadas = await ExecuteNonQueryAsync(PagamentoSqlServerQuery.ExcluirLogico, p);
            return linhasAfetadas > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entidades = await QueryAsync<int>(PagamentoSqlServerQuery.VerificarExistencia, p);
            return entidades.FirstOrDefault() > 0;
        }

        public async Task<IEnumerable<PagamentoEntity>> ObterTodosAsync()
        {
            return await QueryAsync<PagamentoEntity>(PagamentoSqlServerQuery.ObterTodos, new DynamicParameters());
        }

        public async Task<IEnumerable<PagamentoEntity>> ObterPagamentosEmAtrasoPorUsuarioAsync(int usuarioId)
        {
            var p = new DynamicParameters();
            p.Add("@UsuarioId", usuarioId);
            return await QueryAsync<PagamentoEntity>(PagamentoSqlServerQuery.ObterPagamentosEmAtrasoPorUsuario, p);
        }
    }
}