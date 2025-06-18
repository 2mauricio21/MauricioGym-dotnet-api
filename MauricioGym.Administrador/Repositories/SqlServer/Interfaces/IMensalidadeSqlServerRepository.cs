using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Repositories.Interfaces;

namespace MauricioGym.Administrador.Repositories.SqlServer.Interfaces
{
    public interface IMensalidadeSqlServerRepository : ISqlServerRepository
    {
        Task<MensalidadeEntity?> ObterPorIdAsync(int id);
        Task<IEnumerable<MensalidadeEntity>> ListarPorAlunoAsync(int alunoId);
        Task<IEnumerable<MensalidadeEntity>> ListarPorStatusAsync(string status);
        Task<IEnumerable<MensalidadeEntity>> ListarVencidasAsync();
        Task<IEnumerable<MensalidadeEntity>> ListarPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
        Task<int> CriarAsync(MensalidadeEntity mensalidade);
        Task<bool> AtualizarAsync(MensalidadeEntity mensalidade);
        Task<bool> RemoverAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}