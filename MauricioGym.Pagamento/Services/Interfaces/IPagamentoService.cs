using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Pagamento.Entities;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Pagamento.Services.Validators;
using MauricioGym.Infra.Services.Interfaces;

namespace MauricioGym.Pagamento.Services.Interfaces
{
    public interface IPagamentoService : IService<PagamentoValidator>
    {
        Task<IResultadoValidacao<PagamentoEntity>> IncluirPagamentoAsync(PagamentoEntity pagamento, int idUsuario);
        Task<IResultadoValidacao<PagamentoEntity>> ConsultarPagamentoAsync(int idPagamento);
        Task<IResultadoValidacao<PagamentoEntity>> ConsultarPagamentoPorTransacaoAsync(string transacaoId);
        Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ConsultarPagamentosPorUsuarioAsync(int idUsuario);
        Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ConsultarPagamentosPorUsuarioPlanoAsync(int idUsuarioPlano);
        Task<IResultadoValidacao> AlterarPagamentoAsync(PagamentoEntity pagamento, int idUsuario);
        Task<IResultadoValidacao> CancelarPagamentoAsync(int idPagamento, int idUsuario);
        Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ListarPagamentosAsync();
        Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ListarPagamentosPendentesAsync();
    }
}