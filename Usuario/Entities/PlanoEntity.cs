namespace MauricioGym.Usuario.Entities
{
    public class PlanoEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int DuracaoMeses { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
