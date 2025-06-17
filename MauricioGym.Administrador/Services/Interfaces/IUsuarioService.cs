using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Administrador.Entities;

namespace MauricioGym.Administrador.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioEntity> ObterPorIdAsync(int id);
        Task<IEnumerable<UsuarioEntity>> ListarAsync();
        Task<int> CriarAsync(UsuarioEntity usuario);
        Task<bool> AtualizarAsync(UsuarioEntity usuario);
        Task<bool> RemoverLogicamenteAsync(int id);
    }
}
