using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Validators;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Interfaces
{
    public interface IPermissaoService : IService<PermissaoValidator>
    {
        Task<IResultadoValidacao<PermissaoEntity>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<PermissaoEntity>> ObterPorNomeAsync(string nome);
        Task<IResultadoValidacao<IEnumerable<PermissaoEntity>>> ObterTodosAsync();
        Task<IResultadoValidacao<IEnumerable<PermissaoEntity>>> ObterPorPapelIdAsync(int papelId);
        Task<IResultadoValidacao<IEnumerable<PermissaoEntity>>> ObterPorRecursoAsync(string recurso);
        Task<IResultadoValidacao<PermissaoEntity>> IncluirPermissaoAsync(PermissaoEntity permissao, int idUsuario);
        Task<IResultadoValidacao<PermissaoEntity>> AlterarPermissaoAsync(PermissaoEntity permissao, int idUsuario);
        Task<IResultadoValidacao> ExcluirPermissaoAsync(int id, int idUsuario);
        Task<IResultadoValidacao<bool>> ExisteAsync(int id);
        Task<IResultadoValidacao<bool>> ExisteNomeAsync(string nome, int? idExcluir = null);
    }
}
