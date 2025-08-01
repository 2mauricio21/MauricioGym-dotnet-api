using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Infra.Repositories.Interfaces;
using MauricioGym.Plano.Entities;

namespace MauricioGym.Plano.Repositories.SqlServer.Interfaces
{
    public interface IPlanoSqlServerRepository : ISqlServerRepository
    {
        Task<PlanoEntity> IncluirPlanoAsync(PlanoEntity plano);
        Task<PlanoEntity> ConsultarPlanoAsync(int idPlano);
        Task<PlanoEntity> ConsultarPlanoPorNomeAsync(string nome, int idAcademia);
        Task AlterarPlanoAsync(PlanoEntity plano);
        Task ExcluirPlanoAsync(int idPlano);
        Task<IEnumerable<PlanoEntity>> ListarPlanosAsync();
        Task<IEnumerable<PlanoEntity>> ListarPlanosPorAcademiaAsync(int idAcademia);
        Task<IEnumerable<PlanoEntity>> ListarPlanosAtivosAsync();
    }
}