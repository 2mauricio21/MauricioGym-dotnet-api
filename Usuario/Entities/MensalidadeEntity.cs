namespace MauricioGym.Usuario.Entities
{
    public class MensalidadeEntity
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int PlanoId { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal Valor { get; set; }
        public decimal? Desconto { get; set; }
        public bool Pago { get; set; } = false;
        public bool Removido { get; set; } = false;
    }
}
