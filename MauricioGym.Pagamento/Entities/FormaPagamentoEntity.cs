using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Pagamento.Entities
{
    public class FormaPagamentoEntity : IEntity
    {
        public int IdFormaPagamento { get; set; }
        public int IdAcademia { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool AceitaParcelamento { get; set; }
        public int MaximoParcelas { get; set; }
        public decimal? TaxaProcessamento { get; set; }
        public bool Ativo { get; set; }
    }
}