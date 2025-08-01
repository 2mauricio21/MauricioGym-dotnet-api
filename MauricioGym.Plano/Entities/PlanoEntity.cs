using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Plano.Entities
{
    public class PlanoEntity : IEntity
    {
        public int IdPlano { get; set; }
        public int IdAcademia { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int DuracaoEmDias { get; set; }
        public bool PermiteAcessoTotal { get; set; } = true;
        public TimeSpan? HorarioInicioPermitido { get; set; }
        public TimeSpan? HorarioFimPermitido { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}