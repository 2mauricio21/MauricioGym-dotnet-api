using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Plano.Entities;
using MauricioGym.Plano.Services.Validators;

namespace MauricioGym.Plano.Services.Interfaces
{
    public interface IPlanoService : IService<PlanoValidator>
    {
        Task<IResultadoValidacao<PlanoEntity>> IncluirPlanoAsync(PlanoEntity plano, int idUsuario);
        Task<IResultadoValidacao<PlanoEntity>> ConsultarPlanoAsync(int idPlano);
        Task<IResultadoValidacao<PlanoEntity>> ConsultarPlanoPorNomeAsync(string nome, int idAcademia);
        Task<IResultadoValidacao> AlterarPlanoAsync(PlanoEntity plano, int idUsuario);
        Task<IResultadoValidacao> ExcluirPlanoAsync(int idPlano, int idUsuario);
        Task<IResultadoValidacao<IEnumerable<PlanoEntity>>> ListarPlanosAsync();
        Task<IResultadoValidacao<IEnumerable<PlanoEntity>>> ListarPlanosPorAcademiaAsync(int idAcademia);
        Task<IResultadoValidacao<IEnumerable<PlanoEntity>>> ListarPlanosAtivosAsync();
    }
}