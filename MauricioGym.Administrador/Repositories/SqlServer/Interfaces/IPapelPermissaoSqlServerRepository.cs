using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Repositories.Interfaces;

namespace MauricioGym.Administrador.Repositories.SqlServer.Interfaces
{
    public interface IPapelPermissaoSqlServerRepository : ISqlServerRepository
    {
        Task<PapelPermissaoEntity?> ObterPorIdAsync(int id);
        Task<PapelPermissaoEntity?> ObterPorPapelPermissaoAsync(int papelId, int permissaoId);
        Task<IEnumerable<PapelPermissaoEntity>> ListarPorPapelAsync(int papelId);
        Task<IEnumerable<PapelPermissaoEntity>> ListarPorPermissaoAsync(int permissaoId);
        Task<IEnumerable<PermissaoEntity>> ListarPermissoesDoPapelAsync(int papelId);
        Task<IEnumerable<PapelEntity>> ListarPapeisComPermissaoAsync(int permissaoId);
        Task<int> CriarAsync(PapelPermissaoEntity papelPermissao);
        Task<bool> RemoverAsync(int id);
        Task<bool> RemoverAsync(int idPapel, int idPermissao);
        Task<bool> ExisteAsync(int papelId, int permissaoId);
    }
}
