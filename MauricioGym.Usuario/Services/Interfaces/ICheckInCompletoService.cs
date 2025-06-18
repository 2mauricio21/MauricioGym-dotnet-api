using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Validators;

namespace MauricioGym.Usuario.Services.Interfaces
{
    public interface ICheckInCompletoService : IService<CheckInValidator>
    {
        Task<IEnumerable<CheckInEntity>> ObterTodosAsync();
        Task<CheckInEntity?> ObterPorIdAsync(int id);
        Task<IEnumerable<CheckInEntity>> ObterPorClienteIdAsync(int clienteId);
        Task<IEnumerable<CheckInEntity>> ObterPorAcademiaIdAsync(int academiaId);
        Task<IEnumerable<CheckInEntity>> ObterPorPeriodoAsync(DateTime dataInicio, DateTime dataFim, int? academiaId = null);
        Task<int> CriarAsync(CheckInEntity checkIn);
        Task<bool> AtualizarAsync(CheckInEntity checkIn);
        Task<bool> ExcluirAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<bool> ClienteJaFezCheckInHojeAsync(int clienteId, int academiaId);
        Task<int> ContarCheckInsPorUsuarioMesAsync(int usuarioId, int ano, int mes);
    }
}
