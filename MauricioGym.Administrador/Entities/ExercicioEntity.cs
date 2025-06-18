using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Administrador.Entities
{
    public class ExercicioEntity : IEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string GrupoMuscular { get; set; } = string.Empty;
        public string Nivel { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Instrucoes { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public int UsuarioInclusao { get; set; }
        public int? UsuarioAlteracao { get; set; }
    }
} 