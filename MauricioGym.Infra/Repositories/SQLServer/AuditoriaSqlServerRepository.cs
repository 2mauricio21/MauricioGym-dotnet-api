using System.Threading.Tasks;
using MauricioGym.Infra.Entities;
using MauricioGym.Infra.Repositories.SqlServer.Interfaces;
using MauricioGym.Infra.Repositories.SqlServer.Queries;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;

namespace MauricioGym.Infra.Repositories.SqlServer
{
    public class AuditoriaSqlServerRepository : SqlServerRepository, IAuditoriaSqlServerRepository
    {
        public AuditoriaSqlServerRepository(SQLServerDbContext context) : base(context)
        {
        }

        public async Task<AuditoriaEntity> CriarAuditoriaAsync(AuditoriaEntity auditoria)
        {
            var parameters = new
            {
                IdUsuario = auditoria.IdUsuario,
                Descricao = auditoria.Descricao,
                Data = auditoria.Data
            };

            var id = await QueryFirstAsync<int>(AuditoriaSqlServerQuery.INSERT, parameters);
            auditoria.IdAuditoria = id;
            return auditoria;
        }

        public async Task<AuditoriaEntity> ConsultarAuditoriaAsync(int idAuditoria)
        {
            var parameters = new { IdAuditoria = idAuditoria };
            return await QueryFirstOrDefaultAsync<AuditoriaEntity>(AuditoriaSqlServerQuery.SELECT_BY_ID, parameters);
        }
    }
}