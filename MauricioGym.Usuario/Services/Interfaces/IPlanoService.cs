using MauricioGym.Usuario.Entities;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Usuario.Services.Interfaces
{
    public interface IPlanoService
    {
        Task<IResultadoValidacao<IEnumerable<PlanoEntity>>> ObterTodosAsync();
        Task<IResultadoValidacao<PlanoEntity?>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<bool>> ExisteAsync(int id);
    }
}
