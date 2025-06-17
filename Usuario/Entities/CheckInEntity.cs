namespace MauricioGym.Usuario.Entities
{
    public class CheckInEntity
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataHora { get; set; }
        public bool Removido { get; set; } = false;
    }
}
