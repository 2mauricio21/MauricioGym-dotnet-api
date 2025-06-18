using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Usuario.Entities
{
    public class CheckInEntity : IEntity
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int AcademiaId { get; set; }
        public int? PlanoClienteId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataCheckIn { get; set; }
        public DateTime? DataSaida { get; set; }
        public DateTime DataHora { get; set; }
        public string TipoCheckIn { get; set; } = "PLANO"; // PLANO, AVULSA
        public decimal? ValorPago { get; set; } // Para check-ins avulsos
        public string? Observacoes { get; set; }
        public bool EhAvulsa { get; set; } = false;
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataExclusao { get; set; }
        
        // Propriedades de navegação
        public ClienteEntity? Cliente { get; set; }
        public PlanoClienteEntity? PlanoCliente { get; set; }
    }
}
