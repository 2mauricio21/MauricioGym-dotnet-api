using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Interfaces
{
    public interface IPlanoService
    {
        Task<IResultadoValidacao<PlanoEntity?>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<IEnumerable<PlanoEntity>>> ListarAsync();
        Task<IResultadoValidacao<int>> CriarAsync(PlanoEntity plano);
        Task<IResultadoValidacao<bool>> AtualizarAsync(PlanoEntity plano);
        Task<IResultadoValidacao<bool>> RemoverLogicamenteAsync(int id);
    }
}
