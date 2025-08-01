using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Infra.Repositories.Interfaces;
using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Repositories.SqlServer.Interfaces
{
    public interface IRecursoSqlServerRepository : ISqlServerRepository
    {
        Task<RecursoEntity> IncluirRecursoAsync(RecursoEntity recurso);
        Task<RecursoEntity> ConsultarRecursoAsync(int idRecurso);
        Task<RecursoEntity> ConsultarRecursoPorCodigoAsync(string codigo);
        Task AlterarRecursoAsync(RecursoEntity recurso);
        Task ExcluirRecursoAsync(int idRecurso);
        Task<IEnumerable<RecursoEntity>> ListarRecursosAsync();
        Task<IEnumerable<RecursoEntity>> ListarRecursosAtivosAsync();
    }
}