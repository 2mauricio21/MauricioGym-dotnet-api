using System.Threading.Tasks;
using MauricioGym.Infra.Entities;
using MauricioGym.Infra.Repositories.SqlServer.Interfaces;
using MauricioGym.Infra.Services.Interfaces;

namespace MauricioGym.Infra.Services
{
    public class AuditoriaService : IAuditoriaService
    {
        private readonly IAuditoriaSqlServerRepository _auditoriaRepository;

        public AuditoriaService(IAuditoriaSqlServerRepository auditoriaRepository)
        {
            _auditoriaRepository = auditoriaRepository;
        }

        public async Task RegistrarAuditoriaAsync(int idUsuario, string descricao)
        {
            var auditoria = new AuditoriaEntity
            {
                IdUsuario = idUsuario,
                Descricao = descricao
            };

            await _auditoriaRepository.IncluirAuditoriaAsync(auditoria);
        }
    }
}