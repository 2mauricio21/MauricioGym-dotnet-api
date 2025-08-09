namespace MauricioGym.Seguranca.Entities.Response
{
    public class ValidateTokenResponseEntity
    {
        public bool IsValid { get; set; }
        public int IdUsuario { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime Expiracao { get; set; }
    }
}