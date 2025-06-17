using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;

namespace MauricioGym.Services.Interfaces
{
    public interface IPessoaPlanoService
    {
        Task<PessoaPlanoEntity> ObterPorIdAsync(int id);
        Task<IEnumerable<PessoaPlanoEntity>> ListarPorPessoaAsync(int pessoaId);
        Task<int> CriarAsync(PessoaPlanoEntity pessoaPlano);
        Task<bool> AtualizarAsync(PessoaPlanoEntity pessoaPlano);
        Task<bool> RemoverLogicamenteAsync(int id);
    }
}
