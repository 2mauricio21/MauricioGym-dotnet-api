using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Usuario.Entities
{
    public class PlanoModalidadeEntity : IEntity
    {
        public int Id { get; set; }
        public int PlanoId { get; set; }
        public int ModalidadeId { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataExclusao { get; set; }
        
        // Propriedades de navegação
        public PlanoEntity? Plano { get; set; }
        public ModalidadeEntity? Modalidade { get; set; }
    }
}
