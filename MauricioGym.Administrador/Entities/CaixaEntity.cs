using System;

namespace MauricioGym.Administrador.Entities
{    public class CaixaEntity
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataMovimento { get; set; }
        public int QuantidadeAlunos { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
