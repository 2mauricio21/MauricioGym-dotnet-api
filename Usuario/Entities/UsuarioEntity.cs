namespace MauricioGym.Usuario.Entities
{
    public class UsuarioEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; } = true;
        public bool Removido { get; set; } = false;
    }
}
