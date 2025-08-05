using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Validators;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Usuario.Services.Interfaces
{
    public interface IRecursoService : IService<RecursoValidator>
    {
        Task<IResultadoValidacao<RecursoEntity>> IncluirRecursoAsync(RecursoEntity recurso, int idUsuario);
        Task<IResultadoValidacao<RecursoEntity>> ConsultarRecursoAsync(int idRecurso);
        Task<IResultadoValidacao<RecursoEntity>> ConsultarRecursoPorCodigoAsync(string codigo);
        Task<IResultadoValidacao> AlterarRecursoAsync(RecursoEntity recurso, int idUsuario);
        Task<IResultadoValidacao> ExcluirRecursoAsync(int idRecurso, int idUsuario);
        Task<IResultadoValidacao<IEnumerable<RecursoEntity>>> ListarRecursosAsync();
        Task<IResultadoValidacao<IEnumerable<RecursoEntity>>> ListarRecursosAtivosAsync();
    }
}