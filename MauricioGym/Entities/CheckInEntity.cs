using System;

namespace MauricioGym.Entities
{
    public class CheckInEntity
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public DateTime DataHora { get; set; }
    }
}
