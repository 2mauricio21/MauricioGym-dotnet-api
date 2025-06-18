using MauricioGym.Infra.Repositories.Interfaces;
using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Repositories.SqlServer.Interfaces
{
    public interface ICheckInSqlServerRepository : ISqlServerRepository
    {
        Task<CheckInEntity?> ObterPorIdAsync(int id);
        Task<IEnumerable<CheckInEntity>> ObterPorClienteIdAsync(int clienteId);
        Task<IEnumerable<CheckInEntity>> ObterPorAcademiaIdAsync(int academiaId);
        Task<IEnumerable<CheckInEntity>> ObterPorPeriodoAsync(DateTime dataInicio, DateTime dataFim, int? academiaId = null);
        Task<IEnumerable<CheckInEntity>> ObterPorClienteEPeriodoAsync(int clienteId, DateTime dataInicio, DateTime dataFim);
        Task<CheckInEntity?> ObterUltimoCheckInClienteAsync(int clienteId, int academiaId);
        Task<int> ContarCheckInsPorPeriodoAsync(int clienteId, DateTime dataInicio, DateTime dataFim);
        Task<int> CriarAsync(CheckInEntity checkIn);
        Task<bool> AtualizarAsync(CheckInEntity checkIn);
        Task<bool> ExcluirAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<IEnumerable<CheckInEntity>> ObterTodosAsync();
        Task<IEnumerable<CheckInEntity>> ObterPorUsuarioAsync(int usuarioId);
        Task<bool> ClienteJaFezCheckInHojeAsync(int clienteId, int academiaId);
        Task<int> ContarCheckInsPorUsuarioMesAsync(int usuarioId, int ano, int mes);
    }
}
