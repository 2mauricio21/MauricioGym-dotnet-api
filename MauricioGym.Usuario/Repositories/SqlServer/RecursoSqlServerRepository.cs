using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.SqlServer.Interfaces;
using MauricioGym.Usuario.Repositories.SqlServer.Queries;

namespace MauricioGym.Usuario.Repositories.SqlServer
{
    public class RecursoSqlServerRepository : SqlServerRepository, IRecursoSqlServerRepository
    {
        public RecursoSqlServerRepository(SQLServerDbContext sQLServerDbContext) : base(sQLServerDbContext)
        {
        }

        public async Task<RecursoEntity> IncluirRecursoAsync(RecursoEntity recurso)
        {
            recurso.IdRecurso = (await QueryAsync<int>(RecursoSqlServerQuery.IncluirRecurso, recurso)).Single();
            return recurso;
        }

        public async Task<RecursoEntity> ConsultarRecursoAsync(int idRecurso)
        {
            var p = new DynamicParameters();
            p.Add("@IdRecurso", idRecurso);
            
            var entity = await QueryAsync<RecursoEntity>(RecursoSqlServerQuery.ConsultarRecurso, p);
            return entity.FirstOrDefault();
        }

        public async Task<RecursoEntity> ConsultarRecursoPorCodigoAsync(string codigo)
        {
            var p = new DynamicParameters();
            p.Add("@Codigo", codigo);
            
            var entity = await QueryAsync<RecursoEntity>(RecursoSqlServerQuery.ConsultarRecursoPorCodigo, p);
            return entity.FirstOrDefault();
        }

        public async Task AlterarRecursoAsync(RecursoEntity recurso)
        {
            await ExecuteNonQueryAsync(RecursoSqlServerQuery.AlterarRecurso, recurso);
        }

        public async Task ExcluirRecursoAsync(int idRecurso)
        {
            var p = new DynamicParameters();
            p.Add("@IdRecurso", idRecurso);
            
            await ExecuteNonQueryAsync(RecursoSqlServerQuery.ExcluirRecurso, p);
        }

        public async Task<IEnumerable<RecursoEntity>> ListarRecursosAsync()
        {
            return await QueryAsync<RecursoEntity>(RecursoSqlServerQuery.ListarRecursos);
        }

        public async Task<IEnumerable<RecursoEntity>> ListarRecursosAtivosAsync()
        {
            return await QueryAsync<RecursoEntity>(RecursoSqlServerQuery.ListarRecursosAtivos);
        }
    }
}