using System;

namespace MauricioGym.Administrador.Entities
{
    public class PermissaoManipulacaoUsuarioEntity
    {
        public int Id { get; set; }
        public int AdministradorId { get; set; }
        public int UsuarioId { get; set; } // Renomeado de PessoaId para UsuarioId
        public bool PodeEditar { get; set; }
        public bool PodeExcluir { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
