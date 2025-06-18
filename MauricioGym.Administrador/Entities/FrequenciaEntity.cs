using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Administrador.Entities
{
    public class FrequenciaEntity : IEntity
    {
        public int Id { get; set; }
        public int IdAluno { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan? HoraEntrada { get; set; }
        public TimeSpan? HoraSaida { get; set; }
        public string Observacoes { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public int UsuarioInclusao { get; set; }
        public int? UsuarioAlteracao { get; set; }
    }
} 