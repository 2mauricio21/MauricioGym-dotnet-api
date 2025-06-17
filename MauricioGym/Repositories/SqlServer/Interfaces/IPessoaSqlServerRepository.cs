using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;

namespace MauricioGym.Repositories.SqlServer.Interfaces
{
    public interface IPessoaSqlServerRepository
    {
        Task<PessoaEntity> ObterPorIdAsync(int id);
        Task<IEnumerable<PessoaEntity>> ListarAsync();
        Task<int> CriarAsync(PessoaEntity pessoa);
        Task<bool> AtualizarAsync(PessoaEntity pessoa);
        Task<bool> RemoverLogicamenteAsync(int id);
    }
}
