namespace MauricioGym.Seguranca.Entities.Response
{
    public class RefreshTokenResponseEntity
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime Expiracao { get; set; }
    }
}