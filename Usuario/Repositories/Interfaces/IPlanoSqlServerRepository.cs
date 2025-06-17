using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Repositories.Interfaces
{
    public interface IPlanoSqlServerRepository
    {
        Task<IEnumerable<PlanoEntity>> ObterTodosAsync();
        Task<PlanoEntity?> ObterPorIdAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}
