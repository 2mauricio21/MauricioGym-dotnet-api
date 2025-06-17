using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Administrador.Entities;

namespace MauricioGym.Administrador.Services.Interfaces
{
    public interface IPlanoService
    {
        Task<PlanoEntity> ObterPorIdAsync(int id);
        Task<IEnumerable<PlanoEntity>> ListarAsync();
        Task<int> CriarAsync(PlanoEntity plano);
        Task<bool> AtualizarAsync(PlanoEntity plano);
        Task<bool> RemoverLogicamenteAsync(int id);
    }
}
