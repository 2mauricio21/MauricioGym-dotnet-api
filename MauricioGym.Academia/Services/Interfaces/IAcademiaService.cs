using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Academia.Entities;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Academia.Services.Interfaces
{
    public interface IAcademiaService
    {
        Task<IResultadoValidacao<AcademiaEntity>> IncluirAcademiaAsync(AcademiaEntity academia, int idUsuario);
        Task<IResultadoValidacao<AcademiaEntity>> ConsultarAcademiaAsync(int idAcademia);
        Task<IResultadoValidacao<AcademiaEntity>> ConsultarAcademiaPorCNPJAsync(string cnpj);
        Task<IResultadoValidacao<bool>> AlterarAcademiaAsync(AcademiaEntity academia, int idUsuario);
        Task<IResultadoValidacao<bool>> ExcluirAcademiaAsync(int idAcademia, int idUsuario);
        Task<IResultadoValidacao<IEnumerable<AcademiaEntity>>> ListarAcademiasAsync();
        Task<IResultadoValidacao<IEnumerable<AcademiaEntity>>> ListarAcademiasAtivasAsync();
    }
}