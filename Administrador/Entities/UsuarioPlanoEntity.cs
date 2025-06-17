using System;

namespace MauricioGym.Administrador.Entities
{
    public class UsuarioPlanoEntity
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int PlanoId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
