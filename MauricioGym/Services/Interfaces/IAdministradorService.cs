using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;

namespace MauricioGym.Services.Interfaces
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
