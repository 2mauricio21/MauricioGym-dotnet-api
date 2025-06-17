using System;

namespace MauricioGym.Administrador.Entities
{    public class PermissaoManipulacaoUsuarioEntity
    {
        public int Id { get; set; }
        public int AdministradorId { get; set; }
        public int UsuarioId { get; set; }
        public string TipoPermissao { get; set; } = string.Empty; // Cadastrar, Editar, Excluir
        public DateTime DataConcessao { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
