using Dapper;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer.Queries;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;

namespace MauricioGym.Administrador.Repositories.SqlServer
{
    public class UsuarioPapelSqlServerRepository : SqlServerRepository, IUsuarioPapelSqlServerRepository
    {
        public UsuarioPapelSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<UsuarioPapelEntity?> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entidade = await QueryAsync<UsuarioPapelEntity>(UsuarioPapelSqlServerQuery.ObterPorId, p);
            return entidade.FirstOrDefault();
        }

        public async Task<UsuarioPapelEntity?> ObterPorUsuarioPapelAsync(int usuarioId, int papelId)
        {
            var p = new DynamicParameters();
            p.Add("@UsuarioId", usuarioId);
            p.Add("@PapelId", papelId);

            var entidade = await QueryAsync<UsuarioPapelEntity>(UsuarioPapelSqlServerQuery.ObterPorUsuarioPapel, p);
            return entidade.FirstOrDefault();
        }

        public async Task<IEnumerable<UsuarioPapelEntity>> ListarPorUsuarioAsync(int usuarioId)
        {
            var p = new DynamicParameters();
            p.Add("@UsuarioId", usuarioId);

            return await QueryAsync<UsuarioPapelEntity>(UsuarioPapelSqlServerQuery.ListarPorUsuario, p);
        }

        public async Task<IEnumerable<UsuarioPapelEntity>> ListarPorPapelAsync(int papelId)
        {
            var p = new DynamicParameters();
            p.Add("@PapelId", papelId);

            return await QueryAsync<UsuarioPapelEntity>(UsuarioPapelSqlServerQuery.ListarPorPapel, p);
        }

        public async Task<IEnumerable<PapelEntity>> ListarPapeisDoUsuarioAsync(int usuarioId)
        {
            var p = new DynamicParameters();
            p.Add("@UsuarioId", usuarioId);

            return await QueryAsync<PapelEntity>(UsuarioPapelSqlServerQuery.ListarPapeisDoUsuario, p);
        }

        public async Task<int> CriarAsync(UsuarioPapelEntity usuarioPapel)
        {
            var p = new DynamicParameters();
            p.Add("@IdUsuario", usuarioPapel.IdUsuario);
            p.Add("@IdPapel", usuarioPapel.IdPapel);
            p.Add("@DataInclusao", usuarioPapel.DataInclusao);
            p.Add("@Ativo", usuarioPapel.Ativo);

            var entidade = await QueryAsync<int>(UsuarioPapelSqlServerQuery.Criar, p);
            return entidade.FirstOrDefault();
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@DataExclusao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(UsuarioPapelSqlServerQuery.ExcluirLogico, p);
            return rowsAffected > 0;
        }

        public async Task<bool> ExisteAsync(int usuarioId, int papelId)
        {
            var p = new DynamicParameters();
            p.Add("@UsuarioId", usuarioId);
            p.Add("@PapelId", papelId);

            var count = await QueryAsync<int>(UsuarioPapelSqlServerQuery.VerificarExistencia, p);
            return count != null;
        }
    }
}