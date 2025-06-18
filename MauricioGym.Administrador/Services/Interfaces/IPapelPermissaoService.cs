using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Validators;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Interfaces
{
    public interface IPapelPermissaoService : IService<PapelPermissaoValidator>
    {
        Task<IResultadoValidacao<PapelPermissaoEntity>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<PapelPermissaoEntity>> ObterPorPapelPermissaoAsync(int idPapel, int idPermissao);
        Task<IResultadoValidacao<IEnumerable<PapelPermissaoEntity>>> ListarPorPapelAsync(int idPapel);
        Task<IResultadoValidacao<IEnumerable<PapelPermissaoEntity>>> ListarPorPermissaoAsync(int idPermissao);
        Task<IResultadoValidacao<IEnumerable<PermissaoEntity>>> ListarPermissoesDoPapelAsync(int idPapel);
        Task<IResultadoValidacao<IEnumerable<PapelEntity>>> ListarPapeisComPermissaoAsync(int idPermissao);
        Task<IResultadoValidacao<int>> CriarAsync(PapelPermissaoEntity papelPermissao);
        Task<IResultadoValidacao> RemoverAsync(int id);
        Task<IResultadoValidacao> RemoverPorPapelPermissaoAsync(int idPapel, int idPermissao);
        Task<IResultadoValidacao> AtribuirPermissaoAoPapelAsync(int idPapel, int idPermissao);
        Task<IResultadoValidacao> RemoverPermissaoDoPapelAsync(int idPapel, int idPermissao);
    }
}
