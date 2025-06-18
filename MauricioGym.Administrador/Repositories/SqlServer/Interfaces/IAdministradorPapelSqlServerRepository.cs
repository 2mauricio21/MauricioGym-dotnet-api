using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Repositories.Interfaces;

namespace MauricioGym.Administrador.Repositories.SqlServer.Interfaces
{
    public interface IAdministradorPapelSqlServerRepository : ISqlServerRepository
    {
        Task<AdministradorPapelEntity> ObterPorIdAsync(int id);
        Task<AdministradorPapelEntity> ObterPorAdministradorPapelAsync(int idAdministrador, int idPapel);
        Task<IEnumerable<AdministradorPapelEntity>> ListarPorAdministradorAsync(int idAdministrador);
        Task<IEnumerable<AdministradorPapelEntity>> ListarPorPapelAsync(int idPapel);
        Task<IEnumerable<PapelEntity>> ListarPapeisDoAdministradorAsync(int idAdministrador);
        Task<IEnumerable<AdministradorEntity>> ListarAdministradoresComPapelAsync(int idPapel);
        Task<AdministradorPapelEntity> IncluirAsync(AdministradorPapelEntity administradorPapel);
        Task ExcluirAsync(int id);
        Task ExcluirPorAdministradorPapelAsync(int idAdministrador, int idPapel);
        Task<bool> ExisteAsync(int idAdministrador, int idPapel);

        // Métodos para compatibilidade com padrão Juris
        Task<AdministradorPapelEntity> ObterPorAdministradorEPapelAsync(int administradorId, int papelId);
        Task<IEnumerable<AdministradorPapelEntity>> ObterPorAdministradorIdAsync(int administradorId);
        Task<IEnumerable<AdministradorPapelEntity>> ObterPorPapelIdAsync(int papelId);
        Task<AdministradorPapelEntity> CriarAsync(AdministradorPapelEntity administradorPapel);
        Task RemoverLogicamenteAsync(int id);
        Task<bool> AdministradorPossuiPapelAsync(int administradorId, int papelId);
    }
}
