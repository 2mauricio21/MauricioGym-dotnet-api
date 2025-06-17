using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;

namespace MauricioGym.Services.Interfaces
{
    public interface IPermissaoManipulacaoUsuarioService
    {
        Task<PermissaoManipulacaoUsuarioEntity> ObterPorIdAsync(int id);
        Task<IEnumerable<PermissaoManipulacaoUsuarioEntity>> ListarPorPessoaAsync(int pessoaId);
        Task<int> CriarAsync(PermissaoManipulacaoUsuarioEntity permissao);
        Task<bool> AtualizarAsync(PermissaoManipulacaoUsuarioEntity permissao);
        Task<bool> RemoverAsync(int id);
    }
}
