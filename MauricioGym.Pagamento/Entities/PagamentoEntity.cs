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
        public DateTime DataPagamento { get; set; } = DateTime.Now;
        public DateTime? DataVencimento { get; set; }
        public string FormaPagamento { get; set; } = string.Empty; // Dinheiro, Cartão, PIX, etc.
        public string StatusPagamento { get; set; } = "Pendente"; // Pendente, Aprovado, Rejeitado, Cancelado
        public string TransacaoId { get; set; } = string.Empty; // ID do gateway de pagamento
        public string ObservacaoPagamento { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}