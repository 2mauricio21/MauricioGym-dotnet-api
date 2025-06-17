using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Interfaces
{
    public interface IAdministradorService
    {
        Task<IResultadoValidacao<AdministradorEntity>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<IEnumerable<AdministradorEntity>>> ListarAsync();
        Task<IResultadoValidacao<int>> CriarAsync(AdministradorEntity administrador);
        Task<IResultadoValidacao<bool>> AtualizarAsync(AdministradorEntity administrador);
        Task<IResultadoValidacao<bool>> RemoverLogicamenteAsync(int id);
    }
}
