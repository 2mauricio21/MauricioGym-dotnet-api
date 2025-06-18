using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Repositories.Interfaces;

namespace MauricioGym.Administrador.Repositories.SqlServer.Interfaces
{
    public interface IPapelSqlServerRepository : ISqlServerRepository
    {
        Task<PapelEntity> ObterPorIdAsync(int id);
        Task<PapelEntity> ObterPorNomeAsync(string nome);
        Task<IEnumerable<PapelEntity>> ListarAsync();
        Task<IEnumerable<PapelEntity>> ListarPapeisSistemaAsync();
        Task<PapelEntity> IncluirAsync(PapelEntity papel);
        Task<PapelEntity> AlterarAsync(PapelEntity papel);
        Task ExcluirAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<bool> ExistePorNomeAsync(string nome);

        // Métodos para compatibilidade com padrão Juris
        Task<IEnumerable<PapelEntity>> ObterTodosAsync();
        Task<IEnumerable<PapelEntity>> ObterPorAdministradorIdAsync(int administradorId);
        Task<PapelEntity> CriarAsync(PapelEntity papel);
        Task<PapelEntity> AtualizarAsync(PapelEntity papel);
        Task RemoverLogicamenteAsync(int id);
        Task<bool> ExisteNomeAsync(string nome);
    }
}
