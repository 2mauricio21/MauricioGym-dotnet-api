using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Repositories.Interfaces;

namespace MauricioGym.Administrador.Repositories.SqlServer.Interfaces
{
    public interface IPlanoSqlServerRepository : ISqlServerRepository
    {
        Task<PlanoEntity?> ObterPorIdAsync(int id);
        Task<PlanoEntity?> ObterPorNomeAsync(string nome);
        Task<IEnumerable<PlanoEntity>> ListarAtivosAsync();
        Task<IEnumerable<PlanoEntity>> ListarPorTipoAsync(string tipo);
        Task<IEnumerable<PlanoEntity>> ListarPorFaixaPrecoAsync(decimal precoMinimo, decimal precoMaximo);
        Task<int> CriarAsync(PlanoEntity plano);
        Task<bool> AtualizarAsync(PlanoEntity plano);
        Task<bool> RemoverAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<bool> ExistePorNomeAsync(string nome);
    }
}