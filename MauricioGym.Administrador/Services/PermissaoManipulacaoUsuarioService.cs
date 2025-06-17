using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Services.Interfaces;

namespace MauricioGym.Administrador.Services
{
    public class PermissaoManipulacaoUsuarioService : IPermissaoManipulacaoUsuarioService
    {
        private readonly IPermissaoManipulacaoUsuarioSqlServerRepository _permissaoRepository;

        public PermissaoManipulacaoUsuarioService(IPermissaoManipulacaoUsuarioSqlServerRepository permissaoRepository)
        {
            _permissaoRepository = permissaoRepository;
        }

        public Task<PermissaoManipulacaoUsuarioEntity> ObterPorIdAsync(int id) => _permissaoRepository.ObterPorIdAsync(id);

        public Task<IEnumerable<PermissaoManipulacaoUsuarioEntity>> ListarPorUsuarioAsync(int usuarioId) => _permissaoRepository.ListarPorUsuarioAsync(usuarioId);

        public Task<int> CriarAsync(PermissaoManipulacaoUsuarioEntity permissao) => _permissaoRepository.CriarAsync(permissao);

        public Task<bool> AtualizarAsync(PermissaoManipulacaoUsuarioEntity permissao) => _permissaoRepository.AtualizarAsync(permissao);

        public Task<bool> RemoverAsync(int id) => _permissaoRepository.RemoverAsync(id);
    }
}
