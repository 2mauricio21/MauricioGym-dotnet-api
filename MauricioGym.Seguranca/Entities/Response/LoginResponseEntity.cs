namespace MauricioGym.Seguranca.Entities.Response
{
    public class LoginResponseEntity
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime Expiracao { get; set; }
        public int IdUsuario { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
    }
}