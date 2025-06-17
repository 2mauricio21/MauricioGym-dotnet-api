using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Administrador.Entities;

namespace MauricioGym.Administrador.Services.Interfaces
{
    public interface IPermissaoManipulacaoUsuarioService
    {
        Task<PermissaoManipulacaoUsuarioEntity> ObterPorIdAsync(int id);
        Task<IEnumerable<PermissaoManipulacaoUsuarioEntity>> ListarPorUsuarioAsync(int usuarioId);
        Task<int> CriarAsync(PermissaoManipulacaoUsuarioEntity permissao);
        Task<bool> AtualizarAsync(PermissaoManipulacaoUsuarioEntity permissao);
        Task<bool> RemoverAsync(int id);
    }
}
