namespace MauricioGym.Seguranca.Entities.Request
{
    public class CadastroUsuarioRequestEntity
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string? CPF { get; set; }
        public string? Telefone { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Endereco { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? CEP { get; set; }
    }
}
