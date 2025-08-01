using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Usuario.Entities
{
    public class RecursoEntity : IEntity
    {
        public int IdRecurso { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
    }
}