using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Interfaces
{
    public interface IPermissaoManipulacaoUsuarioService
    {
        Task<IResultadoValidacao<PermissaoManipulacaoUsuarioEntity?>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<IEnumerable<PermissaoManipulacaoUsuarioEntity>>> ListarPorUsuarioAsync(int usuarioId);
        Task<IResultadoValidacao<int>> CriarAsync(PermissaoManipulacaoUsuarioEntity permissao);
        Task<IResultadoValidacao<bool>> AtualizarAsync(PermissaoManipulacaoUsuarioEntity permissao);
        Task<IResultadoValidacao<bool>> RemoverAsync(int id);
    }
}
