using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;
using MauricioGym.Repositories.SqlServer.Interfaces;
using MauricioGym.Services.Interfaces;

namespace MauricioGym.Services
{
    public class CheckInService : ICheckInService
    {
        private readonly ICheckInSqlServerRepository _checkInRepository;

        public CheckInService(ICheckInSqlServerRepository checkInRepository)
        {
            _checkInRepository = checkInRepository;
        }

        public Task<CheckInEntity> ObterPorIdAsync(int id) => _checkInRepository.ObterPorIdAsync(id);

        public Task<IEnumerable<CheckInEntity>> ListarPorPessoaAsync(int pessoaId) => _checkInRepository.ListarPorPessoaAsync(pessoaId);

        public Task<int> CriarAsync(CheckInEntity checkIn) => _checkInRepository.CriarAsync(checkIn);

        public Task<int> ContarCheckInsPorPessoaMesAsync(int pessoaId, int ano, int mes) => _checkInRepository.ContarCheckInsPorPessoaMesAsync(pessoaId, ano, mes);
    }
}
