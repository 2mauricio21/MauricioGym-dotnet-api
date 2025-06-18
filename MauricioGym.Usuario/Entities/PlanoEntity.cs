using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Usuario.Entities
{
    public class PlanoEntity : IEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal ValorMensal { get; set; }
        public int DuracaoMeses { get; set; }
        public bool PermiteCongelamento { get; set; }
        public int DiasTolerancia { get; set; }
        public bool Ativo { get; set; } = true;
        public int AcademiaId { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public int UsuarioInclusao { get; set; }
        public int? UsuarioAlteracao { get; set; }

        // Navegação
        public IEnumerable<PlanoClienteEntity> PlanosClientes { get; set; } = new List<PlanoClienteEntity>();
    }
}
