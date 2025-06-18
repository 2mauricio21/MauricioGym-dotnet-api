using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Repositories.Interfaces;

namespace MauricioGym.Administrador.Repositories.SqlServer.Interfaces
{
    public interface ITreinoSqlServerRepository : ISqlServerRepository
    {
        Task<TreinoEntity?> ObterPorIdAsync(int id);
        Task<TreinoEntity?> ObterPorNomeAsync(string nome);
        Task<IEnumerable<TreinoEntity>> ListarAtivosAsync();
        Task<IEnumerable<TreinoEntity>> ListarPorTipoAsync(string tipo);
        Task<IEnumerable<TreinoEntity>> ListarPorNivelAsync(string nivel);
        Task<IEnumerable<TreinoEntity>> ListarPorAlunoAsync(int alunoId);
        Task<int> CriarAsync(TreinoEntity treino);
        Task<bool> AtualizarAsync(TreinoEntity treino);
        Task<bool> RemoverAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<bool> ExistePorNomeAsync(string nome);
    }
}