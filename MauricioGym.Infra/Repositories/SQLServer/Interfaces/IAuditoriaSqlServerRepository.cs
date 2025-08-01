using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Infra.Entities;
using MauricioGym.Infra.Repositories.Interfaces;

namespace MauricioGym.Infra.Repositories.SqlServer.Interfaces
{
    public interface IAuditoriaSqlServerRepository : ISqlServerRepository
    {
        Task<AuditoriaEntity> IncluirAuditoriaAsync(AuditoriaEntity auditoria);
        Task<IEnumerable<AuditoriaEntity>> ListarAuditoriasAsync();
        Task<IEnumerable<AuditoriaEntity>> ListarAuditoriasPorUsuarioAsync(int idUsuario);
    }
}