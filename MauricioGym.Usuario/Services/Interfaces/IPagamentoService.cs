using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Validators;

namespace MauricioGym.Usuario.Services.Interfaces
{
    public interface IPagamentoService : IService<PagamentoValidator>
    {
        Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ObterTodosAsync();
        Task<IResultadoValidacao<PagamentoEntity?>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ObterPorClienteIdAsync(int clienteId);
        Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ObterPorAcademiaIdAsync(int academiaId);
        Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ObterPorPlanoClienteIdAsync(int planoClienteId);
        Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ObterPorPeriodoAsync(DateTime dataInicio, DateTime dataFim, int? academiaId = null);
        Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ObterPendentesAsync(int? academiaId = null);
        Task<IResultadoValidacao<decimal>> ObterTotalRecebidoPorPeriodoAsync(DateTime dataInicio, DateTime dataFim, int? academiaId = null);
        Task<IResultadoValidacao<int>> CriarAsync(PagamentoEntity pagamento);
        Task<IResultadoValidacao<bool>> AtualizarAsync(PagamentoEntity pagamento);
        Task<IResultadoValidacao<bool>> ExcluirAsync(int id);
        Task<IResultadoValidacao<bool>> ExisteAsync(int id);
        Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ObterPagamentosEmAtrasoPorUsuarioAsync(int usuarioId);
    }
}