using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Administrador.Entities
{
    public class TreinoExercicioEntity : IEntity
    {
        public int Id { get; set; }
        public int IdTreino { get; set; }
        public int IdExercicio { get; set; }
        public int Series { get; set; }
        public int Repeticoes { get; set; }
        public decimal? Carga { get; set; }
        public int? TempoDescanso { get; set; }
        public int Ordem { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public int UsuarioInclusao { get; set; }
        public int? UsuarioAlteracao { get; set; }
    }
} 