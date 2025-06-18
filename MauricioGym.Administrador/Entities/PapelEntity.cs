using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Administrador.Entities
{
    public class PapelEntity : IEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public bool EhSistema { get; set; } = false;
        public bool Ativo { get; set; } = true;
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public int? UsuarioInclusao { get; set; }
        public int? UsuarioAlteracao { get; set; }

        // Navegação
        public IEnumerable<PapelPermissaoEntity> PapeisPermissoes { get; set; } = new List<PapelPermissaoEntity>();
        public IEnumerable<AdministradorPapelEntity> AdministradoresPapeis { get; set; } = new List<AdministradorPapelEntity>();
    }
}
