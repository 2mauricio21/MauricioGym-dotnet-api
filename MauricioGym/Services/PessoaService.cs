using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;
using MauricioGym.Repositories.SqlServer.Interfaces;
using MauricioGym.Services.Interfaces;

namespace MauricioGym.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaSqlServerRepository _pessoaRepository;

        public PessoaService(IPessoaSqlServerRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public Task<PessoaEntity> ObterPorIdAsync(int id) => _pessoaRepository.ObterPorIdAsync(id);

        public Task<IEnumerable<PessoaEntity>> ListarAsync() => _pessoaRepository.ListarAsync();

        public Task<int> CriarAsync(PessoaEntity pessoa) => _pessoaRepository.CriarAsync(pessoa);

        public Task<bool> AtualizarAsync(PessoaEntity pessoa) => _pessoaRepository.AtualizarAsync(pessoa);

        public Task<bool> RemoverLogicamenteAsync(int id) => _pessoaRepository.RemoverLogicamenteAsync(id);
    }
}
