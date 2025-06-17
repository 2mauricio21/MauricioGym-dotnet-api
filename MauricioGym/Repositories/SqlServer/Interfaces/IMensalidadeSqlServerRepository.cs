using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;

namespace MauricioGym.Repositories.SqlServer.Interfaces
{
    public interface IMensalidadeSqlServerRepository
    {
        Task<MensalidadeEntity> ObterPorIdAsync(int id);
        Task<IEnumerable<MensalidadeEntity>> ListarPorPessoaAsync(int pessoaId);
        Task<int> CriarAsync(MensalidadeEntity mensalidade);
        Task<decimal> ObterTotalRecebidoAsync();
    }
}
