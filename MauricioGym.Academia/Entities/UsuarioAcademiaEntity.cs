using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Academia.Entities
{
    public class UsuarioAcademiaEntity : IEntity
    {
        public int IdUsuarioAcademia { get; set; }
        public int IdUsuario { get; set; }
        public int IdAcademia { get; set; }
        public DateTime DataVinculo { get; set; } = DateTime.Now;
        public bool Ativo { get; set; } = true;
    }
}