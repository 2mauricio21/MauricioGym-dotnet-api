using MauricioGym.Usuario.Entities;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Usuario.Services.Interfaces
{
    public interface ICheckInService
    {
        Task<IResultadoValidacao<IEnumerable<CheckInEntity>>> ObterTodosAsync();
        Task<IResultadoValidacao<CheckInEntity?>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<IEnumerable<CheckInEntity>>> ObterPorUsuarioAsync(int usuarioId);
        Task<IResultadoValidacao<IEnumerable<CheckInEntity>>> ObterPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
        Task<IResultadoValidacao<int>> RealizarCheckInAsync(int usuarioId);
        Task<IResultadoValidacao<bool>> RemoverAsync(int id);
        Task<IResultadoValidacao<bool>> ExisteAsync(int id);
        Task<IResultadoValidacao<int>> CriarAsync(CheckInEntity checkIn);
        Task<IResultadoValidacao<IEnumerable<CheckInEntity>>> ListarPorUsuarioAsync(int usuarioId);
        Task<IResultadoValidacao<int>> ContarCheckInsPorUsuarioMesAsync(int usuarioId, int ano, int mes);
        Task<IResultadoValidacao<bool>> PodeRealizarCheckInAsync(int usuarioId);
    }
}
