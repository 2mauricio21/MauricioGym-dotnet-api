using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Administrador.Entities
{
    public class AdministradorPapelEntity : IEntity
    {
        public int Id { get; set; }
        public int AdministradorId { get; set; }
        public int PapelId { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public int? UsuarioInclusao { get; set; }
        public int? UsuarioAlteracao { get; set; }
        
        // Navegação
        public AdministradorEntity Administrador { get; set; } = null!;
        public PapelEntity Papel { get; set; } = null!;
    }
}
