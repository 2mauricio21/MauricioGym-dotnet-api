using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Acesso.Entities;
using MauricioGym.Acesso.Repositories.SqlServer.Interfaces;
using MauricioGym.Acesso.Repositories.SqlServer.Queries;

namespace MauricioGym.Acesso.Repositories.SqlServer
{
    public class BloqueioAcessoSqlServerRepository : SqlServerRepository, IBloqueioAcessoSqlServerRepository
    {
        public BloqueioAcessoSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<BloqueioAcessoEntity> IncluirBloqueioAcessoAsync(BloqueioAcessoEntity bloqueioAcesso)
        {
            bloqueioAcesso.IdBloqueioAcesso = (await QueryAsync<int>(BloqueioAcessoSqlServerQuery.IncluirBloqueioAcesso, bloqueioAcesso)).Single();
            return bloqueioAcesso;
        }

        public async Task<BloqueioAcessoEntity> ConsultarBloqueioAcessoAsync(int idBloqueioAcesso)
        {
            var p = new DynamicParameters();
            p.Add("@IdBloqueioAcesso", idBloqueioAcesso);
            return (await QueryAsync<BloqueioAcessoEntity>(BloqueioAcessoSqlServerQuery.ConsultarBloqueioAcesso, p)).FirstOrDefault();
        }

        public async Task<IEnumerable<BloqueioAcessoEntity>> ConsultarBloqueiosPorUsuarioAsync(int idUsuario)
        {
            var p = new DynamicParameters();
            p.Add("@IdUsuario", idUsuario);
            return await QueryAsync<BloqueioAcessoEntity>(BloqueioAcessoSqlServerQuery.ConsultarBloqueiosPorUsuario, p);
        }

        public async Task<IEnumerable<BloqueioAcessoEntity>> ConsultarBloqueiosPorUsuarioAcademiaAsync(int idUsuario, int idAcademia)
        {
            var p = new DynamicParameters();
            p.Add("@IdUsuario", idUsuario);
            p.Add("@IdAcademia", idAcademia);
            return await QueryAsync<BloqueioAcessoEntity>(BloqueioAcessoSqlServerQuery.ConsultarBloqueiosPorUsuarioAcademia, p);
        }

        public async Task<BloqueioAcessoEntity> ConsultarBloqueioAtivoPorUsuarioAcademiaAsync(int idUsuario, int idAcademia)
        {
            var p = new DynamicParameters();
            p.Add("@IdUsuario", idUsuario);
            p.Add("@IdAcademia", idAcademia);
            return (await QueryAsync<BloqueioAcessoEntity>(BloqueioAcessoSqlServerQuery.ConsultarBloqueioAtivoPorUsuarioAcademia, p)).FirstOrDefault();
        }

        public async Task<bool> AlterarBloqueioAcessoAsync(BloqueioAcessoEntity bloqueioAcesso)
        {
            var affectedRows = await ExecuteNonQueryAsync(BloqueioAcessoSqlServerQuery.AlterarBloqueioAcesso, bloqueioAcesso);
            return affectedRows > 0;
        }

        public async Task<bool> CancelarBloqueioAcessoAsync(int idBloqueioAcesso)
        {
            var p = new DynamicParameters();
            p.Add("@IdBloqueioAcesso", idBloqueioAcesso);
            var affectedRows = await ExecuteNonQueryAsync(BloqueioAcessoSqlServerQuery.CancelarBloqueioAcesso, p);
            return affectedRows > 0;
        }

        public async Task<IEnumerable<BloqueioAcessoEntity>> ListarBloqueiosAcessoAsync()
        {
            return await QueryAsync<BloqueioAcessoEntity>(BloqueioAcessoSqlServerQuery.ListarBloqueiosAcesso);
        }

        public async Task<IEnumerable<BloqueioAcessoEntity>> ListarBloqueiosAtivosAsync()
        {
            return await QueryAsync<BloqueioAcessoEntity>(BloqueioAcessoSqlServerQuery.ListarBloqueiosAtivos);
        }
    }
}