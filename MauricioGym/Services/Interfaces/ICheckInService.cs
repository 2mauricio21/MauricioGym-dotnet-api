using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;

namespace MauricioGym.Services.Interfaces
{
    public interface ICheckInService
    {
        Task<CheckInEntity> ObterPorIdAsync(int id);
        Task<IEnumerable<CheckInEntity>> ListarPorPessoaAsync(int pessoaId);
        Task<int> CriarAsync(CheckInEntity checkIn);
        Task<int> ContarCheckInsPorPessoaMesAsync(int pessoaId, int ano, int mes);
    }
}
