using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Pagamento.Entities
{
    public class FormaPagamentoEntity : IEntity
    {
        public int IdFormaPagamento { get; set; }
        public int IdAcademia { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public bool AceitaParcelamento { get; set; } = false;
        public int MaximoParcelas { get; set; } = 1;
        public decimal? TaxaProcessamento { get; set; }
        public bool Ativo { get; set; } = true;
    }
}