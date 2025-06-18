using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Validators;

namespace MauricioGym.Usuario.Services.Interfaces
{
    public interface ICheckInService : IService<CheckInValidator>
    {
        Task<IResultadoValidacao<IEnumerable<CheckInEntity>>> ObterTodosAsync();
        Task<IResultadoValidacao<CheckInEntity?>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<IEnumerable<CheckInEntity>>> ObterPorUsuarioAsync(int usuarioId);
        Task<IResultadoValidacao<IEnumerable<CheckInEntity>>> ObterPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
        Task<IResultadoValidacao<int>> CriarAsync(CheckInEntity checkIn);
        Task<IResultadoValidacao<bool>> AtualizarAsync(CheckInEntity checkIn);
        Task<IResultadoValidacao<bool>> ExcluirAsync(int id);
        Task<IResultadoValidacao<bool>> ExisteAsync(int id);
        Task<IResultadoValidacao<int>> ContarCheckInsPorUsuarioMesAsync(int usuarioId, int ano, int mes);
        Task<IResultadoValidacao<bool>> PodeRealizarCheckInAsync(int usuarioId);
    }
}
