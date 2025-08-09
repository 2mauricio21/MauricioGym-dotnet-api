namespace MauricioGym.Seguranca.Entities.Request
{
    public class AlterarSenhaRequestEntity
    {
        public int IdUsuario { get; set; }
        public string SenhaAtual { get; set; } = string.Empty;
        public string NovaSenha { get; set; } = string.Empty;
    }
}