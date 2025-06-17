using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;
using MauricioGym.Repositories.SqlServer.Interfaces;
using MauricioGym.Services.Interfaces;

namespace MauricioGym.Services
{
    public class CaixaService : ICaixaService
    {
        private readonly ICaixaSqlServerRepository _caixaRepository;

        public CaixaService(ICaixaSqlServerRepository caixaRepository)
        {
            _caixaRepository = caixaRepository;
        }

        public Task<CaixaEntity> ObterPorIdAsync(int id) => _caixaRepository.ObterPorIdAsync(id);

        public Task<IEnumerable<CaixaEntity>> ListarAsync() => _caixaRepository.ListarAsync();

        public Task<int> CriarAsync(CaixaEntity caixa) => _caixaRepository.CriarAsync(caixa);

        public Task<bool> AtualizarAsync(CaixaEntity caixa) => _caixaRepository.AtualizarAsync(caixa);

        public Task<bool> RemoverAsync(int id) => _caixaRepository.RemoverAsync(id);
    }
}
