using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Acesso.Entities
{
    public class BloqueioAcessoEntity : IEntity
    {
        public int IdBloqueioAcesso { get; set; }
        public int IdUsuario { get; set; }
        public int IdAcademia { get; set; }
        public DateTime DataInicioBloqueio { get; set; } = DateTime.Now;
        public DateTime? DataFimBloqueio { get; set; }
        public string MotivoBloqueio { get; set; } = string.Empty;
        public string ObservacaoBloqueio { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public int IdUsuarioResponsavel { get; set; } // Quem aplicou o bloqueio
    }
}