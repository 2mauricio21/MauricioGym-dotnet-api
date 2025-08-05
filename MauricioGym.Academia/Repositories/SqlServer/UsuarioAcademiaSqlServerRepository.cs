using Dapper;
using MauricioGym.Academia.Entities;
using MauricioGym.Academia.Repositories.SqlServer.Interfaces;
using MauricioGym.Academia.Repositories.SqlServer.Queries;
using MauricioGym.Infra.Repositories;
using MauricioGym.Infra.SQLServer;

namespace MauricioGym.Academia.Repositories.SqlServer
{
    public class UsuarioAcademiaSqlServerRepository : SqlServerRepository, IUsuarioAcademiaSqlServerRepository
    {
        public UsuarioAcademiaSqlServerRepository(SQLServerDbContext sQLServerDbContext) : base(sQLServerDbContext)
        {
        }

        public async Task<UsuarioAcademiaEntity> IncluirUsuarioAcademiaAsync(UsuarioAcademiaEntity usuarioAcademia)
        {
            usuarioAcademia.IdUsuarioAcademia = (await QueryAsync<int>(UsuarioAcademiaSqlServerQuery.IncluirUsuarioAcademia, usuarioAcademia)).Single();
            return usuarioAcademia;
        }

        public async Task<UsuarioAcademiaEntity> ConsultarUsuarioAcademiaAsync(int idUsuarioAcademia)
        {
            var p = new DynamicParameters();
            p.Add("@IdUsuarioAcademia", idUsuarioAcademia);

            var entity = await QueryAsync<UsuarioAcademiaEntity>(UsuarioAcademiaSqlServerQuery.ConsultarUsuarioAcademia, p);
            return entity.FirstOrDefault();
        }

        public async Task AlterarUsuarioAcademiaAsync(UsuarioAcademiaEntity usuarioAcademia)
        {
            await ExecuteNonQueryAsync(UsuarioAcademiaSqlServerQuery.AlterarUsuarioAcademia, usuarioAcademia);
        }

        public async Task ExcluirUsuarioAcademiaAsync(int idUsuarioAcademia)
        {
            var p = new DynamicParameters();
            p.Add("@IdUsuarioAcademia", idUsuarioAcademia);

            await ExecuteNonQueryAsync(UsuarioAcademiaSqlServerQuery.ExcluirUsuarioAcademia, p);
        }

        public async Task<IEnumerable<UsuarioAcademiaEntity>> ListarUsuarioAcademiaAsync()
        {
            return await QueryAsync<UsuarioAcademiaEntity>(UsuarioAcademiaSqlServerQuery.ListarUsuarioAcademia);
        }

        public async Task<IEnumerable<UsuarioAcademiaEntity>> ListarUsuarioAcademiaPorUsuarioAsync(int idUsuario)
        {
            var p = new DynamicParameters();
            p.Add("@IdUsuario", idUsuario);

            return await QueryAsync<UsuarioAcademiaEntity>(UsuarioAcademiaSqlServerQuery.ListarUsuarioAcademiaPorUsuario, p);
        }

        public async Task<IEnumerable<UsuarioAcademiaEntity>> ListarUsuarioAcademiaPorAcademiaAsync(int idAcademia)
        {
            var p = new DynamicParameters();
            p.Add("@IdAcademia", idAcademia);

            return await QueryAsync<UsuarioAcademiaEntity>(UsuarioAcademiaSqlServerQuery.ListarUsuarioAcademiaPorAcademia, p);
        }
    }
}