using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Plano.Entities;
using MauricioGym.Plano.Repositories.SqlServer.Interfaces;
using MauricioGym.Plano.Repositories.SqlServer.Queries;

namespace MauricioGym.Plano.Repositories.SqlServer
{
    public class UsuarioPlanoSqlServerRepository : SqlServerRepository, IUsuarioPlanoSqlServerRepository
    {
        public UsuarioPlanoSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<UsuarioPlanoEntity> IncluirAsync(UsuarioPlanoEntity usuarioPlano)
        {
            usuarioPlano.IdUsuarioPlano = (await QueryAsync<int>(UsuarioPlanoSqlServerQuery.IncluirUsuarioPlano, usuarioPlano)).Single();
            return usuarioPlano;
        }

        public async Task<UsuarioPlanoEntity> ObterPorIdAsync(int idUsuarioPlano)
        {
            var p = new DynamicParameters();
            p.Add("@IdUsuarioPlano", idUsuarioPlano);
            return (await QueryAsync<UsuarioPlanoEntity>(UsuarioPlanoSqlServerQuery.ConsultarUsuarioPlano, p)).FirstOrDefault();
        }

        public async Task<UsuarioPlanoEntity> ObterAtivoPorUsuarioAsync(int idUsuario)
        {
            var p = new DynamicParameters();
            p.Add("@IdUsuario", idUsuario);
            return (await QueryAsync<UsuarioPlanoEntity>(UsuarioPlanoSqlServerQuery.ConsultarUsuarioPlanoAtivoPorUsuario, p)).FirstOrDefault();
        }

        public async Task<IEnumerable<UsuarioPlanoEntity>> ObterPorUsuarioAsync(int idUsuario)
        {
            var p = new DynamicParameters();
            p.Add("@IdUsuario", idUsuario);
            return await QueryAsync<UsuarioPlanoEntity>(UsuarioPlanoSqlServerQuery.ConsultarUsuarioPlanosPorUsuario, p);
        }

        public async Task<bool> AtualizarAsync(UsuarioPlanoEntity usuarioPlano)
        {
            var affectedRows = await ExecuteNonQueryAsync(UsuarioPlanoSqlServerQuery.AlterarUsuarioPlano, usuarioPlano);
            return affectedRows > 0;
        }

        public async Task<bool> CancelarAsync(int idUsuarioPlano)
        {
            var p = new DynamicParameters();
            p.Add("@IdUsuarioPlano", idUsuarioPlano);
            var affectedRows = await ExecuteNonQueryAsync(UsuarioPlanoSqlServerQuery.CancelarUsuarioPlano, p);
            return affectedRows > 0;
        }

        public async Task<IEnumerable<UsuarioPlanoEntity>> ListarTodosAsync()
        {
            return await QueryAsync<UsuarioPlanoEntity>(UsuarioPlanoSqlServerQuery.ListarUsuarioPlanos);
        }

        public async Task<IEnumerable<UsuarioPlanoEntity>> ListarAtivosAsync()
        {
            return await QueryAsync<UsuarioPlanoEntity>(UsuarioPlanoSqlServerQuery.ListarUsuarioPlanosAtivos);
        }

        public async Task<IEnumerable<UsuarioPlanoEntity>> ListarPorStatusAsync(string status)
        {
            var p = new DynamicParameters();
            p.Add("@StatusPlano", status);
            return await QueryAsync<UsuarioPlanoEntity>(UsuarioPlanoSqlServerQuery.ListarUsuarioPlanosPorStatus, p);
        }
    }
}