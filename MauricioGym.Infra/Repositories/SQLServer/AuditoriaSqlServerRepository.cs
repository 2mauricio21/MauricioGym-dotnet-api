using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MauricioGym.Infra.Entities;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.Repositories.SqlServer.Interfaces;
using MauricioGym.Infra.Repositories.SqlServer.Queries;
using MauricioGym.Infra.SQLServer;
using System.Data;

namespace MauricioGym.Infra.Repositories.SQLServer
{
  public class AuditoriaSqlServerRepository : SqlServerRepository, IAuditoriaSqlServerRepository
  {
    public AuditoriaSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
    {
    }

    public async Task<AuditoriaEntity> CriarAuditoriaAsync(AuditoriaEntity auditoria)
    {
      var p = new DynamicParameters();
      p.Add("@IdUsuario", auditoria.IdUsuario);
      p.Add("@Descricao", auditoria.Descricao);
      p.Add("@Data", auditoria.Data);

      var data = await ExecuteNonQueryAsync(AuditoriaSqlServerQuery.CriarAuditoria, p);
      auditoria.IdAuditoria = data;
      return auditoria;
    }

    public async Task<AuditoriaEntity> ConsultarAuditoriaAsync(int idAuditoria)
    {
      var p = new DynamicParameters();
      p.Add("@IdAuditoria", idAuditoria);
      var consulta = await QueryAsync<AuditoriaEntity>(AuditoriaSqlServerQuery.ConsultarIdAuditoria, p);

      return consulta.FirstOrDefault();
    }

  }

}