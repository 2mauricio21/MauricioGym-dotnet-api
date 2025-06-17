using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Administrador.Entities;

namespace MauricioGym.Administrador.Services.Interfaces
{
    public interface IAdministradorService
    {
        Task<AdministradorEntity> ObterPorIdAsync(int id);
        Task<IEnumerable<AdministradorEntity>> ListarAsync();
        Task<int> CriarAsync(AdministradorEntity administrador);
        Task<bool> AtualizarAsync(AdministradorEntity administrador);
        Task<bool> RemoverLogicamenteAsync(int id);
    }
}
