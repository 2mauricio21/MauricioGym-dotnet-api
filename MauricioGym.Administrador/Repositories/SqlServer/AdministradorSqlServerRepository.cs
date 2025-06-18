using Dapper;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer.Queries;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using System.Data;

namespace MauricioGym.Administrador.Repositories.SqlServer
{
    public class AdministradorSqlServerRepository : SqlServerRepository, IAdministradorSqlServerRepository
    {
        public AdministradorSqlServerRepository(SQLServerDbContext sqlServerDbContext)
            : base(sqlServerDbContext)
        {
        }

        public async Task<AdministradorEntity?> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id, DbType.Int32);
            var entidade = await QueryAsync<AdministradorEntity>(AdministradorSqlServerQuery.ObterPorId, p);
            return entidade.FirstOrDefault();
        }

        public async Task<AdministradorEntity?> ObterPorEmailAsync(string email)
        {
            var p = new DynamicParameters();
            p.Add("@Email", email, DbType.String);
            var entidade = await QueryAsync<AdministradorEntity>(
                AdministradorSqlServerQuery.ObterPorEmail,
                p);
            return entidade.FirstOrDefault();
        }

        public async Task<AdministradorEntity?> ObterPorCpfAsync(string cpf)
        {
            var p = new DynamicParameters();
            p.Add("@Cpf", cpf, DbType.String);
            p.Add("@Email", cpf, DbType.String);
            var entidade = await QueryAsync<AdministradorEntity>(
                AdministradorSqlServerQuery.ObterPorCpf,
                p);
            return entidade.FirstOrDefault();
        }

        public async Task<IEnumerable<AdministradorEntity>> ObterTodosAsync()
        {
            return await QueryAsync<AdministradorEntity>(
                AdministradorSqlServerQuery.ObterTodos);
        }

        public async Task<IEnumerable<AdministradorEntity>> ObterAtivosAsync()
        {
            return await QueryAsync<AdministradorEntity>(
                AdministradorSqlServerQuery.ObterAtivos);
        }

        public async Task<int> CriarAsync(AdministradorEntity administrador)
        {

            var entidade = await QueryAsync<int>(
                AdministradorSqlServerQuery.Criar,
                administrador);
            return entidade.FirstOrDefault();
        }

        public async Task<bool> AtualizarAsync(AdministradorEntity administrador)
        {
            var linhasAfetadas = await ExecuteNonQueryAsync(
                AdministradorSqlServerQuery.Atualizar,
                administrador);

            return linhasAfetadas > 0;
        }

        public async Task<bool> ExcluirAsync(int id, int usuarioId)
        {
            var entidade = new AdministradorEntity
            {
                Id = id,
                DataExclusao = DateTime.Now,
                UsuarioAlteracao = usuarioId
            };

            var linhasAfetadas = await ExecuteNonQueryAsync(
                AdministradorSqlServerQuery.ExcluirLogico,
                entidade);

            return linhasAfetadas > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            var entidade = new AdministradorEntity { Id = id };
            var count = await QueryAsync<int>(
                AdministradorSqlServerQuery.VerificarExistencia,
                entidade);

            return count != null;
        }

        public async Task<bool> ExistePorEmailAsync(string email, int? idExcluir = null)
        {
            var entidade = new AdministradorEntity
            {
                Email = email,
                Id = idExcluir ?? 0
            };

            var count = await QueryAsync<int>(
                AdministradorSqlServerQuery.VerificarExistenciaPorEmail,
                entidade);

            return count != null;
        }

        public async Task<bool> ExistePorCpfAsync(string cpf, int? idExcluir = null)
        {
            var entidade = new AdministradorEntity
            {
                Cpf = cpf,
                Id = idExcluir ?? 0
            };

            var count = await QueryAsync<int>(
                AdministradorSqlServerQuery.VerificarExistenciaPorCpf,
                entidade);

            return count != null;
        }
    }
}