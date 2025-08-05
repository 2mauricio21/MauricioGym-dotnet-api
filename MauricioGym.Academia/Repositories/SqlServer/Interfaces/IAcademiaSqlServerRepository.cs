using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Infra.Repositories.Interfaces;
using MauricioGym.Academia.Entities;

namespace MauricioGym.Academia.Repositories.SqlServer.Interfaces
{
    public interface IAcademiaSqlServerRepository : ISqlServerRepository
    {
        Task<AcademiaEntity> IncluirAcademiaAsync(AcademiaEntity academia);
        Task<AcademiaEntity> ConsultarAcademiaAsync(int idAcademia);
        Task<AcademiaEntity> ConsultarAcademiaPorCNPJAsync(string cnpj);
        Task<bool> AlterarAcademiaAsync(AcademiaEntity academia);
        Task<bool> ExcluirAcademiaAsync(int idAcademia);
        Task<IEnumerable<AcademiaEntity>> ListarAcademiasAsync();
        Task<IEnumerable<AcademiaEntity>> ListarAcademiasAtivasAsync();
    }
}