using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Repositories.Interfaces
{
    public interface ICheckInSqlServerRepository
    {
        Task<IEnumerable<CheckInEntity>> ObterTodosAsync();
        Task<CheckInEntity?> ObterPorIdAsync(int id);
        Task<IEnumerable<CheckInEntity>> ObterPorUsuarioAsync(int usuarioId);
        Task<IEnumerable<CheckInEntity>> ObterPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
        Task<int> CriarAsync(CheckInEntity checkIn);
        Task<bool> AtualizarAsync(CheckInEntity checkIn);
        Task<bool> RemoverAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<int> ContarCheckInsPorUsuarioMesAsync(int usuarioId, int ano, int mes);
    }
}
