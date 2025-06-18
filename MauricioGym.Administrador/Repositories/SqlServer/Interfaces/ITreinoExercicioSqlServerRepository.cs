using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Repositories.Interfaces;

namespace MauricioGym.Administrador.Repositories.SqlServer.Interfaces
{
    public interface ITreinoExercicioSqlServerRepository : ISqlServerRepository
    {
        Task<TreinoExercicioEntity?> ObterPorIdAsync(int id);
        Task<TreinoExercicioEntity?> ObterPorTreinoExercicioAsync(int treinoId, int exercicioId);
        Task<IEnumerable<TreinoExercicioEntity>> ListarPorTreinoAsync(int treinoId);
        Task<IEnumerable<TreinoExercicioEntity>> ListarPorExercicioAsync(int exercicioId);
        Task<IEnumerable<ExercicioEntity>> ListarExerciciosDoTreinoAsync(int treinoId);
        Task<IEnumerable<TreinoEntity>> ListarTreinosComExercicioAsync(int exercicioId);
        Task<int> CriarAsync(TreinoExercicioEntity treinoExercicio);
        Task<bool> AtualizarAsync(TreinoExercicioEntity treinoExercicio);
        Task<bool> RemoverAsync(int id);
        Task<bool> ExisteAsync(int treinoId, int exercicioId);
    }
}