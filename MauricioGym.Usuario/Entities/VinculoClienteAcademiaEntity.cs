using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Usuario.Entities
{
    public class VinculoClienteAcademiaEntity : IEntity
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int AcademiaId { get; set; }
        public DateTime DataVinculo { get; set; }
        public DateTime? DataDesvinculo { get; set; }
        public bool Ativo { get; set; } = true;
        public string? Observacoes { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public int UsuarioInclusao { get; set; }
        public int? UsuarioAlteracao { get; set; }

        // Navegação
        public ClienteEntity Cliente { get; set; } = null!;
    }
}
