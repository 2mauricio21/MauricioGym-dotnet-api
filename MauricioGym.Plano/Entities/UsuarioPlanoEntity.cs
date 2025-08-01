using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Plano.Entities
{
    public class UsuarioPlanoEntity : IEntity
    {
        public int IdUsuarioPlano { get; set; }
        public int IdUsuario { get; set; }
        public int IdPlano { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal ValorPago { get; set; }
        public string StatusPlano { get; set; } = "Ativo"; // Ativo, Suspenso, Cancelado, Expirado
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}