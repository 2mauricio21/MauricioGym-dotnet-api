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
        public AuditoriaSqlServerRepository(SQLServerDbContext sQLServerDbContext) : base(sQLServerDbContext)
        {
        }

        public async Task<AuditoriaEntity> IncluirAuditoriaAsync(AuditoriaEntity auditoria)
        {
            var p = new DynamicParameters();
            p.Add("@IdUsuario", auditoria.IdUsuario);
            p.Add("@Descricao", auditoria.Descricao);
            p.Add("@Data", auditoria.Data);
            p.Add("@IdAuditoria", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await QueryAsync<AuditoriaEntity>(AuditoriaSqlServerQuery.Criar, p);
            
            auditoria.IdAuditoria = p.Get<int>("@IdAuditoria");
            return auditoria;
        }

        public async Task<IEnumerable<AuditoriaEntity>> ListarAuditoriasAsync()
        {
            return await QueryAsync<AuditoriaEntity>(AuditoriaSqlServerQuery.ObterTodos);
        }

        public async Task<IEnumerable<AuditoriaEntity>> ListarAuditoriasPorUsuarioAsync(int idUsuario)
        {
            var p = new DynamicParameters();
            p.Add("@IdUsuario", idUsuario);
            
            return await QueryAsync<AuditoriaEntity>(AuditoriaSqlServerQuery.ObterPorUsuario, p);
        }
    }
}