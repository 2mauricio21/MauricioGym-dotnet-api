using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Administrador.Entities
{
    public class AdministradorEntity : IEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public int? UsuarioInclusao { get; set; }
        public int? UsuarioAlteracao { get; set; }

        // Propriedades de navegação
        public virtual ICollection<AdministradorPapelEntity> AdministradorPapeis { get; set; } = new List<AdministradorPapelEntity>();
        public virtual ICollection<AcademiaEntity> Academias { get; set; } = new List<AcademiaEntity>();
    }
}