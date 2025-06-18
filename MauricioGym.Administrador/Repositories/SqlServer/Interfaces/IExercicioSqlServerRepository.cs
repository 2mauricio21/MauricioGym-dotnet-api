using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Repositories.Interfaces;

namespace MauricioGym.Administrador.Repositories.SqlServer.Interfaces
{
    public interface IExercicioSqlServerRepository : ISqlServerRepository
    {
        Task<ExercicioEntity?> ObterPorIdAsync(int id);
        Task<ExercicioEntity?> ObterPorNomeAsync(string nome);
        Task<IEnumerable<ExercicioEntity>> ListarAtivosAsync();
        Task<IEnumerable<ExercicioEntity>> ListarPorGrupoMuscularAsync(string grupoMuscular);
        Task<IEnumerable<ExercicioEntity>> ListarPorNivelAsync(string nivel);
        Task<IEnumerable<ExercicioEntity>> ListarPorTipoAsync(string tipo);
        Task<int> CriarAsync(ExercicioEntity exercicio);
        Task<bool> AtualizarAsync(ExercicioEntity exercicio);
        Task<bool> RemoverAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<bool> ExistePorNomeAsync(string nome);
    }
}