using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Repositories.Interfaces;

namespace MauricioGym.Administrador.Repositories.SqlServer.Interfaces
{
    public interface IUsuarioPapelSqlServerRepository : ISqlServerRepository
    {
        Task<UsuarioPapelEntity?> ObterPorIdAsync(int id);
        Task<UsuarioPapelEntity?> ObterPorUsuarioPapelAsync(int usuarioId, int papelId);
        Task<IEnumerable<UsuarioPapelEntity>> ListarPorUsuarioAsync(int usuarioId);
        Task<IEnumerable<UsuarioPapelEntity>> ListarPorPapelAsync(int papelId);
        Task<IEnumerable<PapelEntity>> ListarPapeisDoUsuarioAsync(int usuarioId);
        Task<int> CriarAsync(UsuarioPapelEntity usuarioPapel);
        Task<bool> RemoverAsync(int id);
        Task<bool> ExisteAsync(int usuarioId, int papelId);
    }
}