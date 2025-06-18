using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Administrador.Entities
{
    public class PapelPermissaoEntity : IEntity
    {
        public int Id { get; set; }
        public int IdPapel { get; set; }
        public int IdPermissao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool Ativo { get; set; } = true;

        // Navegação
        public PapelEntity Papel { get; set; } = null!;
        public PermissaoEntity Permissao { get; set; } = null!;
    }
}
