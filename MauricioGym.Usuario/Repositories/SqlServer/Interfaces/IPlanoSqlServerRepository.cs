using MauricioGym.Infra.Repositories.Interfaces;
using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Repositories.SqlServer.Interfaces
{
    public interface IPlanoSqlServerRepository : ISqlServerRepository
    {
        Task<PlanoEntity> ObterPorIdAsync(int id);
        Task<IEnumerable<PlanoEntity>> ObterTodosAsync();
        Task<IEnumerable<PlanoEntity>> ObterPorAcademiaIdAsync(int academiaId);
        Task<IEnumerable<PlanoEntity>> ListarAtivosAsync();
        Task<IEnumerable<PlanoEntity>> ListarPorModalidadeAsync(int modalidadeId);
        Task<int> CriarAsync(PlanoEntity plano);
        Task<bool> AtualizarAsync(PlanoEntity plano);
        Task<bool> ExcluirAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<bool> ExisteNomeAsync(string nome, int? academiaId);
    }
}
