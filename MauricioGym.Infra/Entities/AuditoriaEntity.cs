using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Infra.Entities
{
    public class AuditoriaEntity : IEntity
    {
        public int IdAuditoria { get; set; }
        public int IdUsuario { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        
        public AuditoriaEntity()
        {
            Data = DateTime.Now;
        }
        
        public AuditoriaEntity(int idUsuario, string descricao) : this()
        {
            IdUsuario = idUsuario;
            Descricao = descricao;
        }
    }
}