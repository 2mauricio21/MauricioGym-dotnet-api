using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Repositories.Interfaces;

namespace MauricioGym.Administrador.Repositories.SqlServer.Interfaces
{
    public interface IFrequenciaSqlServerRepository : ISqlServerRepository
    {
        Task<FrequenciaEntity?> ObterPorIdAsync(int id);
        Task<FrequenciaEntity?> ObterPorAlunoDataAsync(int alunoId, DateTime data);
        Task<IEnumerable<FrequenciaEntity>> ListarPorAlunoAsync(int alunoId);
        Task<IEnumerable<FrequenciaEntity>> ListarPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
        Task<IEnumerable<FrequenciaEntity>> ListarPorAlunoPeriodoAsync(int alunoId, DateTime dataInicio, DateTime dataFim);
        Task<int> CriarAsync(FrequenciaEntity frequencia);
        Task<bool> AtualizarAsync(FrequenciaEntity frequencia);
        Task<bool> RemoverAsync(int id);
        Task<bool> ExisteAsync(int alunoId, DateTime data);
    }
}