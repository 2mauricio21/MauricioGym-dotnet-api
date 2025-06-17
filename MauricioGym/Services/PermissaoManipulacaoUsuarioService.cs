using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;
using MauricioGym.Repositories.SqlServer.Interfaces;
using MauricioGym.Services.Interfaces;

namespace MauricioGym.Services
{
    public class PermissaoManipulacaoUsuarioService : IPermissaoManipulacaoUsuarioService
    {
        private readonly IPermissaoManipulacaoUsuarioSqlServerRepository _permissaoRepository;

        public PermissaoManipulacaoUsuarioService(IPermissaoManipulacaoUsuarioSqlServerRepository permissaoRepository)
        {
            _permissaoRepository = permissaoRepository;
        }

        public Task<PermissaoManipulacaoUsuarioEntity> ObterPorIdAsync(int id) => _permissaoRepository.ObterPorIdAsync(id);

        public Task<IEnumerable<PermissaoManipulacaoUsuarioEntity>> ListarPorPessoaAsync(int pessoaId) => _permissaoRepository.ListarPorPessoaAsync(pessoaId);

        public Task<int> CriarAsync(PermissaoManipulacaoUsuarioEntity permissao) => _permissaoRepository.CriarAsync(permissao);

        public Task<bool> AtualizarAsync(PermissaoManipulacaoUsuarioEntity permissao) => _permissaoRepository.AtualizarAsync(permissao);

        public Task<bool> RemoverAsync(int id) => _permissaoRepository.RemoverAsync(id);
    }
}
