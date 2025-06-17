using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;

namespace MauricioGym.Services.Interfaces
{
    public interface IMensalidadeService
    {
        Task<MensalidadeEntity> ObterPorIdAsync(int id);
        Task<IEnumerable<MensalidadeEntity>> ListarPorPessoaAsync(int pessoaId);
        Task<int> CriarAsync(MensalidadeEntity mensalidade);
        Task<decimal> ObterTotalRecebidoAsync();
        Task<int> RegistrarMensalidadeComDesconto(int pessoaId, int planoId, int meses, decimal valorBase, DateTime inicio);
    }
}
