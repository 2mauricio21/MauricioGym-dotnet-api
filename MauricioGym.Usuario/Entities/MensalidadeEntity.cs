namespace MauricioGym.Usuario.Entities
{    public class MensalidadeEntity
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int PlanoId { get; set; }
        public int UsuarioPlanoId { get; set; }
        public int MesReferencia { get; set; }
        public int AnoReferencia { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public string Status { get; set; } = "Pendente"; // Paga, Pendente, Atrasada
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
