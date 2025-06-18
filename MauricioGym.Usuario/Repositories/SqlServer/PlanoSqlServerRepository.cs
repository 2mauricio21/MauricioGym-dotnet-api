using Dapper;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.SqlServer.Interfaces;
using MauricioGym.Usuario.Repositories.SqlServer.Queries;

namespace MauricioGym.Usuario.Repositories.SqlServer
{
    public class PlanoSqlServerRepository : SqlServerRepository, IPlanoSqlServerRepository
    {
        public PlanoSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<PlanoEntity> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entidade = await QueryAsync<PlanoEntity>(PlanoSqlServerQuery.ObterPorId, p);
            return entidade.FirstOrDefault();
        }

        public async Task<IEnumerable<PlanoEntity>> ObterPorAcademiaIdAsync(int academiaId)
        {
            var p = new DynamicParameters();
            p.Add("@AcademiaId", academiaId);

            return await QueryAsync<PlanoEntity>(PlanoSqlServerQuery.ObterPorAcademiaId, p);
        }

        public async Task<IEnumerable<PlanoEntity>> ListarAtivosAsync()
        {
            return await QueryAsync<PlanoEntity>(PlanoSqlServerQuery.ListarAtivos);
        }

        public async Task<IEnumerable<PlanoEntity>> ListarPorModalidadeAsync(int modalidadeId)
        {
            var p = new DynamicParameters();
            p.Add("@ModalidadeId", modalidadeId);

            return await QueryAsync<PlanoEntity>(PlanoSqlServerQuery.ListarPorModalidade, p);
        }

        public async Task<int> CriarAsync(PlanoEntity plano)
        {
            await ExecuteNonQueryAsync(PlanoSqlServerQuery.Criar, plano);
            var ultimoId = await QueryAsync<int>("SELECT SCOPE_IDENTITY()", new DynamicParameters());
            return ultimoId.FirstOrDefault();
        }

        public async Task<bool> AtualizarAsync(PlanoEntity plano)
        {
            var linhasAfetadas = await ExecuteNonQueryAsync(PlanoSqlServerQuery.Atualizar, plano);
            return linhasAfetadas > 0;
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@DataExclusao", DateTime.Now);

            var linhasAfetadas = await ExecuteNonQueryAsync(PlanoSqlServerQuery.ExcluirLogico, p);
            return linhasAfetadas > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entidades = await QueryAsync<int>(PlanoSqlServerQuery.VerificarExistencia, p);
            return entidades.FirstOrDefault() > 0;
        }

        public async Task<bool> ExisteNomeAsync(string nome, int? academiaId)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", nome);
            p.Add("@AcademiaId", academiaId);

            var entidades = await QueryAsync<int>(PlanoSqlServerQuery.VerificarExistenciaNome, p);
            return entidades.FirstOrDefault() > 0;
        }

        public async Task<IEnumerable<PlanoEntity>> ObterTodosAsync()
        {
            return await QueryAsync<PlanoEntity>(PlanoSqlServerQuery.ObterTodos);
        }
    }
}
