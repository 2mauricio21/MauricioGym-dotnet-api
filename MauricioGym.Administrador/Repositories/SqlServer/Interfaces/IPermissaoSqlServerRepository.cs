using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Repositories.Interfaces;

namespace MauricioGym.Administrador.Repositories.SqlServer.Interfaces
{
    public interface IPermissaoSqlServerRepository : ISqlServerRepository
    {
        Task<PermissaoEntity> ObterPorIdAsync(int id);
        Task<PermissaoEntity> ObterPorNomeAsync(string nome);
        Task<PermissaoEntity> ObterPorRecursoAcaoAsync(string recurso, string acao);
        Task<IEnumerable<PermissaoEntity>> ListarAsync();
        Task<IEnumerable<PermissaoEntity>> ListarPorRecursoAsync(string recurso);
        Task<PermissaoEntity> IncluirAsync(PermissaoEntity permissao);
        Task<PermissaoEntity> AlterarAsync(PermissaoEntity permissao);
        Task ExcluirAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<bool> ExistePorNomeAsync(string nome, int? idExcluir = null);
        Task<bool> ExcluirLogicamenteAsync(int id, int usuarioId);

        // Métodos para compatibilidade com padrão Juris
        Task<IEnumerable<PermissaoEntity>> ObterTodosAsync();
        Task<IEnumerable<PermissaoEntity>> ObterPorPapelIdAsync(int papelId);
        Task<IEnumerable<PermissaoEntity>> ObterPorRecursoAsync(string recurso);
        Task<PermissaoEntity> CriarAsync(PermissaoEntity permissao);
        Task<PermissaoEntity> AtualizarAsync(PermissaoEntity permissao);
        Task RemoverLogicamenteAsync(int id);
        Task<bool> ExisteNomeAsync(string nome);
    }
}
