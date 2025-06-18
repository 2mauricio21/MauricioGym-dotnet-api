using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Usuario.Entities
{
    public class PlanoClienteEntity : IEntity
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int PlanoId { get; set; }
        public int AcademiaId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public int MesesContratados { get; set; }
        public bool PlanoAtivo { get; set; } = true;
        public DateTime? DataCancelamento { get; set; }
        public string? MotivoCancelamento { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public int UsuarioInclusao { get; set; }        public int? UsuarioAlteracao { get; set; }
        
        // Propriedades de navegação
        public ClienteEntity? Cliente { get; set; }
        public PlanoEntity? Plano { get; set; }
    }
}
