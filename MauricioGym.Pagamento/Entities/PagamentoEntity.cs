using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Pagamento.Entities
{
    public class PagamentoEntity : IEntity
    {
        public int IdPagamento { get; set; }
        public int IdUsuario { get; set; }
        public int IdUsuarioPlano { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime? DataVencimento { get; set; }
        public string FormaPagamento { get; set; }
        public string StatusPagamento { get; set; }
        public string TransacaoId { get; set; }
        public string ObservacaoPagamento { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}