using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;

namespace MauricioGym.Repositories.SqlServer.Interfaces
{
    public interface IAdministradorSqlServerRepository
    {
        Task<AdministradorEntity> ObterPorIdAsync(int id);
        Task<IEnumerable<AdministradorEntity>> ListarAsync();
        Task<int> CriarAsync(AdministradorEntity administrador);
        Task<bool> AtualizarAsync(AdministradorEntity administrador);
        Task<bool> RemoverLogicamenteAsync(int id);
    }
}
