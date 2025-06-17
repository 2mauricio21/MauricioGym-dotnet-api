using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;

namespace MauricioGym.Repositories.SqlServer.Interfaces
{
    public interface ICheckInSqlServerRepository
    {
        Task<CheckInEntity> ObterPorIdAsync(int id);
        Task<IEnumerable<CheckInEntity>> ListarPorPessoaAsync(int pessoaId);
        Task<int> CriarAsync(CheckInEntity checkIn);
        Task<int> ContarCheckInsPorPessoaMesAsync(int pessoaId, int ano, int mes);
    }
}
