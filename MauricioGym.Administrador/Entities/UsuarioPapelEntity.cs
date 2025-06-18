using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Administrador.Entities
{
    public class UsuarioPapelEntity : IEntity
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdPapel { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public bool Ativo { get; set; } = true;
        public int UsuarioInclusao { get; set; }
    }
} 