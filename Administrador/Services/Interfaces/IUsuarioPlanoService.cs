using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Administrador.Entities;

namespace MauricioGym.Administrador.Services.Interfaces
{
    public interface IUsuarioPlanoService
    {
        Task<UsuarioPlanoEntity> ObterPorIdAsync(int id);
        Task<IEnumerable<UsuarioPlanoEntity>> ListarPorUsuarioAsync(int usuarioId);
        Task<int> CriarAsync(UsuarioPlanoEntity usuarioPlano);
        Task<bool> AtualizarAsync(UsuarioPlanoEntity usuarioPlano);
        Task<bool> RemoverLogicamenteAsync(int id);
    }
}
