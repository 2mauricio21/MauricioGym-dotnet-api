using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Acesso.Entities
{
    public class AcessoEntity : IEntity
    {
        public int IdAcesso { get; set; }
        public int IdUsuario { get; set; }
        public int IdAcademia { get; set; }
        public DateTime DataHoraEntrada { get; set; } = DateTime.Now;
        public DateTime? DataHoraSaida { get; set; }
        public string TipoAcesso { get; set; } = string.Empty; // Normal, Visitante, Funcionario
        public string ObservacaoAcesso { get; set; } = string.Empty;
        public bool AcessoLiberado { get; set; } = true;
        public string MotivoNegacao { get; set; } = string.Empty;
    }
}