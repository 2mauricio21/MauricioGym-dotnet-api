using System;

namespace MauricioGym.Entities
{
    public class PermissaoManipulacaoUsuarioEntity
    {
        public int Id { get; set; }
        public int AdministradorId { get; set; }
        public int PessoaId { get; set; }
        public bool PodeEditar { get; set; }
        public bool PodeExcluir { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
