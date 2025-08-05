using System;
using System.Threading.Tasks;
using MauricioGym.Infra.Entities;
using MauricioGym.Infra.Repositories.SqlServer.Interfaces;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Infra.Services
{
    public class AuditoriaService : ServiceBase<AuditoriaValidator>, IAuditoriaService
    {
        private readonly IAuditoriaSqlServerRepository auditoriaSqlServerRepository;
        public AuditoriaService(IAuditoriaSqlServerRepository _auditoriaSqlServerRepository)
        {
            auditoriaSqlServerRepository = _auditoriaSqlServerRepository;
        }

        public async Task<IResultadoValidacao<AuditoriaEntity>> IncluirAuditoriaAsync(int idUsuario, string descricao, object entity = null)
        {
            try
            {
                var auditoria = new AuditoriaEntity
                {
                    IdUsuario = idUsuario,
                    Descricao = descricao,
                    Data = DateTime.Now
                };

                var result = await auditoriaSqlServerRepository.CriarAuditoriaAsync(auditoria);
                return new ResultadoValidacao<AuditoriaEntity>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<AuditoriaEntity>(ex, "[AuditoriaService]-Ocorreu erro ao tentar incluir a auditoria.");
            }
        }

        public async Task<IResultadoValidacao<AuditoriaEntity>> ConsultarAuditoriaPorIdAsync(int idAuditoria)
        {
            try
            {
                var result = await auditoriaSqlServerRepository.ConsultarAuditoriaAsync(idAuditoria);
                return new ResultadoValidacao<AuditoriaEntity>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<AuditoriaEntity>(ex, "[AuditoriaService]-Ocorreu erro ao tentar consultar a auditoria.");
            }
        }

  }
}