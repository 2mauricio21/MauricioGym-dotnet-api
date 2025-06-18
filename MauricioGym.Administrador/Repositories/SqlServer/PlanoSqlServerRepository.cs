using Dapper;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer.Queries;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;

namespace MauricioGym.Administrador.Repositories.SqlServer
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

            var entidade = await QueryAsync<PlanoEntity>(PlanoSqlServerQuery.ListarPorId, p);
            return entidade.FirstOrDefault();
        }

        public async Task<PlanoEntity> ObterPorNomeAsync(string nome)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", nome);

            var entidade = await QueryAsync<PlanoEntity>(PlanoSqlServerQuery.ListarPorNome, p);
            return entidade.FirstOrDefault();
        }

        public async Task<IEnumerable<PlanoEntity>> ListarAtivosAsync()
        {
            return await QueryAsync<PlanoEntity>(PlanoSqlServerQuery.ListarAtivos);
        }

        public async Task<IEnumerable<PlanoEntity>> ListarPorTipoAsync(string tipo)
        {
            var p = new DynamicParameters();
            p.Add("@Tipo", tipo);

            return await QueryAsync<PlanoEntity>(PlanoSqlServerQuery.ListarPorTipo, p);
        }

        public async Task<IEnumerable<PlanoEntity>> ListarPorFaixaPrecoAsync(decimal precoMinimo, decimal precoMaximo)
        {
            var p = new DynamicParameters();
            p.Add("@PrecoMinimo", precoMinimo);
            p.Add("@PrecoMaximo", precoMaximo);

            return await QueryAsync<PlanoEntity>(PlanoSqlServerQuery.ListarPorFaixaPreco, p);
        }

        public async Task<int> CriarAsync(PlanoEntity plano)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", plano.Nome);
            p.Add("@Descricao", plano.Descricao);
            p.Add("@Tipo", plano.Tipo);
            p.Add("@Valor", plano.Valor);
            p.Add("@DuracaoDias", plano.DuracaoDias);
            p.Add("@DataInclusao", plano.DataInclusao);
            p.Add("@Ativo", plano.Ativo);

            var entidade = await QueryAsync<int>(PlanoSqlServerQuery.Criar, p);
            return entidade.FirstOrDefault();
        }

        public async Task<bool> AtualizarAsync(PlanoEntity plano)
        {
            var p = new DynamicParameters();
            p.Add("@Id", plano.Id);
            p.Add("@Nome", plano.Nome);
            p.Add("@Descricao", plano.Descricao);
            p.Add("@Tipo", plano.Tipo);
            p.Add("@Valor", plano.Valor);
            p.Add("@DuracaoDias", plano.DuracaoDias);
            p.Add("@DataAtualizacao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(PlanoSqlServerQuery.Atualizar, p);
            return rowsAffected > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@DataExclusao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(PlanoSqlServerQuery.ExcluirLogico, p);
            return rowsAffected > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var count = await QueryAsync<int>(PlanoSqlServerQuery.VerificarExistencia, p);
            return count != null;
        }

        public async Task<bool> ExistePorNomeAsync(string nome)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", nome);

            var count = await QueryAsync<int>(PlanoSqlServerQuery.VerificarExistenciaPorNome, p);
            return count != null;
        }
    }
}