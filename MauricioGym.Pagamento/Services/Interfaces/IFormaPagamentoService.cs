using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Pagamento.Entities;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Pagamento.Services.Validators;
using MauricioGym.Infra.Services.Interfaces;

namespace MauricioGym.Pagamento.Services.Interfaces
{
    public interface IFormaPagamentoService : IService<FormaPagamentoValidator>
    {
        Task<IResultadoValidacao<FormaPagamentoEntity>> IncluirFormaPagamentoAsync(FormaPagamentoEntity formaPagamento, int idUsuario);
        Task<IResultadoValidacao<FormaPagamentoEntity>> ConsultarFormaPagamentoAsync(int idFormaPagamento);
        Task<IResultadoValidacao<FormaPagamentoEntity>> ConsultarFormaPagamentoPorNomeAsync(string nome, int idAcademia);
        Task<IResultadoValidacao> AlterarFormaPagamentoAsync(FormaPagamentoEntity formaPagamento, int idUsuario);
        Task<IResultadoValidacao> ExcluirFormaPagamentoAsync(int idFormaPagamento, int idUsuario);
        Task<IResultadoValidacao<IEnumerable<FormaPagamentoEntity>>> ListarFormasPagamentoAsync();
        Task<IResultadoValidacao<IEnumerable<FormaPagamentoEntity>>> ListarFormasPagamentoPorAcademiaAsync(int idAcademia);
        Task<IResultadoValidacao<IEnumerable<FormaPagamentoEntity>>> ListarFormasPagamentoAtivasAsync();
    }
}