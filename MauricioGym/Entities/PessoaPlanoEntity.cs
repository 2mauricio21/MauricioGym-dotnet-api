using System;

namespace MauricioGym.Entities
{
    public class PessoaPlanoEntity
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public int PlanoId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
