using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Infra.Entities
{
    public abstract class EntityBase : IEntity
    {
        public int Id { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int UsuarioInclusao { get; set; }
        public int? UsuarioAlteracao { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
