using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Validators;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Interfaces
{
    public interface IAdministradorPapelService : IService<AdministradorPapelValidator>
    {
        Task<IResultadoValidacao<AdministradorPapelEntity>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<AdministradorPapelEntity>> ObterPorAdministradorEPapelAsync(int administradorId, int papelId);
        Task<IResultadoValidacao<IEnumerable<AdministradorPapelEntity>>> ObterPorAdministradorIdAsync(int administradorId);
        Task<IResultadoValidacao<IEnumerable<AdministradorPapelEntity>>> ObterPorPapelIdAsync(int papelId);
        Task<IResultadoValidacao<AdministradorPapelEntity>> IncluirAdministradorPapelAsync(AdministradorPapelEntity administradorPapel, int idUsuario);
        Task<IResultadoValidacao> ExcluirAdministradorPapelAsync(int id, int idUsuario);
        Task<IResultadoValidacao> RemoverPapelDoAdministradorAsync(int administradorId, int papelId, int idUsuario);
        Task<IResultadoValidacao<bool>> AdministradorPossuiPapelAsync(int administradorId, int papelId);
        Task<IResultadoValidacao<bool>> ExisteAsync(int id);
    }
}
