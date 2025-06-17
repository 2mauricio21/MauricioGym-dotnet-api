using System;

namespace MauricioGym.Entities
{
    public class MensalidadeEntity
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public int PlanoId { get; set; }
        public decimal ValorPago { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime PeriodoInicio { get; set; }
        public DateTime PeriodoFim { get; set; }
    }
}
