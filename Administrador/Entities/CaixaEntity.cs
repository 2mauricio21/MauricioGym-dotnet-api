using System;

namespace MauricioGym.Administrador.Entities
{
    public class CaixaEntity
    {
        public int Id { get; set; }
        public int QuantidadeAlunos { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
