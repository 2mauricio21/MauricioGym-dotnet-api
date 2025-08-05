using System.Threading.Tasks;
using MauricioGym.Infra.Entities;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Infra.Services.Validators;

namespace MauricioGym.Infra.Services.Interfaces
{
    public interface IAuditoriaService : IService<AuditoriaValidator>
    {
        Task<IResultadoValidacao<AuditoriaEntity>> IncluirAuditoriaAsync(int idUsuario, string descricao, object entity = null);

        Task<IResultadoValidacao<AuditoriaEntity>> ConsultarAuditoriaPorIdAsync(int idAuditoria);
    }
}