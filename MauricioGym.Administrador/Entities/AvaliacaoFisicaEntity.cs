using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Administrador.Entities
{
    public class AvaliacaoFisicaEntity : IEntity
    {
        public int Id { get; set; }
        public int IdAluno { get; set; }
        public decimal Peso { get; set; }
        public decimal Altura { get; set; }
        public decimal Imc { get; set; }
        public decimal PercentualGordura { get; set; }
        public decimal MassaMuscular { get; set; }
        public decimal CircunferenciaCintura { get; set; }
        public decimal CircunferenciaBraco { get; set; }
        public decimal CircunferenciaPerna { get; set; }
        public string Observacoes { get; set; } = string.Empty;
        public DateTime DataAvaliacao { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public int UsuarioInclusao { get; set; }
        public int? UsuarioAlteracao { get; set; }
    }
} 