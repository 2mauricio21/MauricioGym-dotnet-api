namespace MauricioGym.Infra.Entities
{
    public class AuditoriaEntity
    {
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int UsuarioInclusao { get; set; }
        public int? UsuarioAlteracao { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
