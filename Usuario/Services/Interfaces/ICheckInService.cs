using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Services.Interfaces
{
    public interface ICheckInService
    {
        Task<IEnumerable<CheckInEntity>> ObterTodosAsync();
        Task<CheckInEntity?> ObterPorIdAsync(int id);
        Task<IEnumerable<CheckInEntity>> ObterPorUsuarioAsync(int usuarioId);
        Task<IEnumerable<CheckInEntity>> ObterPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
        Task<int> RealizarCheckInAsync(int usuarioId);
        Task<bool> RemoverAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<int> CriarAsync(CheckInEntity checkIn);
        Task<IEnumerable<CheckInEntity>> ListarPorUsuarioAsync(int usuarioId);        Task<int> ContarCheckInsPorUsuarioMesAsync(int usuarioId, int ano, int mes);
        Task<bool> PodeRealizarCheckInAsync(int usuarioId);
    }
}
