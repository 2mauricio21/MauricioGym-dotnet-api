using Dapper;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer.Queries;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;

namespace MauricioGym.Administrador.Repositories.SqlServer
{
    public class PapelPermissaoSqlServerRepository : SqlServerRepository, IPapelPermissaoSqlServerRepository
    {
        public PapelPermissaoSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<PapelPermissaoEntity?> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entidade = await QueryAsync<PapelPermissaoEntity>(PapelPermissaoSqlServerQuery.ListarPorId, p);
            return entidade.FirstOrDefault();
        }

        public async Task<PapelPermissaoEntity?> ObterPorPapelPermissaoAsync(int papelId, int permissaoId)
        {
            var p = new DynamicParameters();
            p.Add("@PapelId", papelId);
            p.Add("@PermissaoId", permissaoId);

            var entidade = await QueryAsync<PapelPermissaoEntity>(PapelPermissaoSqlServerQuery.ListarPorPapelPermissao, p);
            return entidade.FirstOrDefault();
        }

        public async Task<IEnumerable<PapelPermissaoEntity>> ListarPorPapelAsync(int papelId)
        {
            var p = new DynamicParameters();
            p.Add("@PapelId", papelId);

            return await QueryAsync<PapelPermissaoEntity>(PapelPermissaoSqlServerQuery.ListarPorPapelId, p);
        }

        public async Task<IEnumerable<PapelPermissaoEntity>> ListarPorPermissaoAsync(int permissaoId)
        {
            var p = new DynamicParameters();
            p.Add("@PermissaoId", permissaoId);

            return await QueryAsync<PapelPermissaoEntity>(PapelPermissaoSqlServerQuery.ListarPorPermissaoId, p);
        }

        public async Task<IEnumerable<PapelPermissaoEntity>> ObterPorPapelIdAsync(int idPapel)
        {
            var p = new DynamicParameters();
            p.Add("@PapelId", idPapel);
            return await QueryAsync<PapelPermissaoEntity>(PapelPermissaoSqlServerQuery.ListarPorPapelId, p);
        }

        public async Task<IEnumerable<PapelPermissaoEntity>> ObterPorPermissaoIdAsync(int idPermissao)
        {
            var p = new DynamicParameters();
            p.Add("@PermissaoId", idPermissao);
            return await QueryAsync<PapelPermissaoEntity>(PapelPermissaoSqlServerQuery.ListarPorPermissaoId, p);
        }

        public async Task<IEnumerable<PermissaoEntity>> ListarPermissoesDoPapelAsync(int papelId)
        {
            var p = new DynamicParameters();
            p.Add("@PapelId", papelId);

            return await QueryAsync<PermissaoEntity>(PapelPermissaoSqlServerQuery.ListarPermissoesDoPapel, p);
        }

        public async Task<IEnumerable<PapelEntity>> ListarPapeisComPermissaoAsync(int permissaoId)
        {
            var p = new DynamicParameters();
            p.Add("@PermissaoId", permissaoId);

            return await QueryAsync<PapelEntity>(PapelPermissaoSqlServerQuery.ListarPapeisComPermissao, p);
        }

        public async Task<int> CriarAsync(PapelPermissaoEntity papelPermissao)
        {
            var p = new DynamicParameters();
            p.Add("@IdPapel", papelPermissao.IdPapel);
            p.Add("@IdPermissao", papelPermissao.IdPermissao);
            p.Add("@DataCriacao", papelPermissao.DataCriacao);
            p.Add("@Ativo", papelPermissao.Ativo);

            var entidade = await QueryAsync<int>(PapelPermissaoSqlServerQuery.Criar, p);
            return entidade.FirstOrDefault();
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@DataExclusao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(PapelPermissaoSqlServerQuery.ExcluirLogico, p);
            return rowsAffected > 0;
        }

        public async Task<bool> RemoverAsync(int idPapel, int idPermissao)
        {
            var p = new DynamicParameters();
            p.Add("@PapelId", idPapel);
            p.Add("@PermissaoId", idPermissao);
            p.Add("@DataExclusao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(PapelPermissaoSqlServerQuery.ExcluirLogicoPorPapelEPermissao, p);
            return rowsAffected > 0;
        }

        public async Task<bool> ExisteAsync(int papelId, int permissaoId)
        {
            var p = new DynamicParameters();
            p.Add("@PapelId", papelId);
            p.Add("@PermissaoId", permissaoId);

            var count = await QueryAsync<int>(PapelPermissaoSqlServerQuery.VerificarExistencia, p);
            return count != null;
        }

        public async Task ExcluirPorPapelPermissaoAsync(int idPapel, int idPermissao)
        {
            var p = new DynamicParameters();
            p.Add("@PapelId", idPapel);
            p.Add("@PermissaoId", idPermissao);
            p.Add("@DataExclusao", DateTime.Now);
            await ExecuteNonQueryAsync(
                PapelPermissaoSqlServerQuery.ExcluirLogicoPorPapelEPermissao, p);
        }

    }
}
