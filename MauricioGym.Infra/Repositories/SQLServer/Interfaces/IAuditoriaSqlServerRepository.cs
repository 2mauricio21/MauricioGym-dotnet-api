using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Infra.Entities;
using MauricioGym.Infra.Repositories.Interfaces;

namespace MauricioGym.Infra.Repositories.SqlServer.Interfaces
{
    public interface IAuditoriaSqlServerRepository : ISqlServerRepository
    {
        Task<AuditoriaEntity> ConsultarAuditoriaAsync(int idAuditoria);
        Task<AuditoriaEntity> CriarAuditoriaAsync(AuditoriaEntity auditoria);
    }
}