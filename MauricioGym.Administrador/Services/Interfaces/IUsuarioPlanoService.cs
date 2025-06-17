using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Interfaces
{
    public interface IUsuarioPlanoService
    {
        Task<IResultadoValidacao<UsuarioPlanoEntity?>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>> ListarPorUsuarioAsync(int usuarioId);
        Task<IResultadoValidacao<int>> CriarAsync(UsuarioPlanoEntity usuarioPlano);
        Task<IResultadoValidacao<bool>> AtualizarAsync(UsuarioPlanoEntity usuarioPlano);
        Task<IResultadoValidacao<bool>> RemoverLogicamenteAsync(int id);
    }
}
