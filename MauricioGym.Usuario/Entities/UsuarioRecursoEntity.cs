using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Usuario.Entities
{
    public class UsuarioRecursoEntity : IEntity
    {
        public int IdUsuarioRecurso { get; set; }
        public int IdUsuario { get; set; }
        public int IdRecurso { get; set; }
        public int? IdAcademia { get; set; } // Null para recursos globais
        public DateTime DataAtribuicao { get; set; } = DateTime.Now;
        public bool Ativo { get; set; } = true;
    }
}