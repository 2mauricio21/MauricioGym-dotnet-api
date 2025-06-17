using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;
using MauricioGym.Repositories.SqlServer.Interfaces;
using MauricioGym.Services.Interfaces;

namespace MauricioGym.Services
{
    public class PessoaPlanoService : IPessoaPlanoService
    {
        private readonly IPessoaPlanoSqlServerRepository _pessoaPlanoRepository;

        public PessoaPlanoService(IPessoaPlanoSqlServerRepository pessoaPlanoRepository)
        {
            _pessoaPlanoRepository = pessoaPlanoRepository;
        }

        public Task<PessoaPlanoEntity> ObterPorIdAsync(int id) => _pessoaPlanoRepository.ObterPorIdAsync(id);

        public Task<IEnumerable<PessoaPlanoEntity>> ListarPorPessoaAsync(int pessoaId) => _pessoaPlanoRepository.ListarPorPessoaAsync(pessoaId);

        public Task<int> CriarAsync(PessoaPlanoEntity pessoaPlano) => _pessoaPlanoRepository.CriarAsync(pessoaPlano);

        public Task<bool> AtualizarAsync(PessoaPlanoEntity pessoaPlano) => _pessoaPlanoRepository.AtualizarAsync(pessoaPlano);

        public Task<bool> RemoverLogicamenteAsync(int id) => _pessoaPlanoRepository.RemoverLogicamenteAsync(id);
    }
}
