using System.Security.Cryptography;
using System.Text;
using MauricioGym.Seguranca.Services.Interfaces;

namespace MauricioGym.Seguranca.Services
{
    /// <summary>
    /// Implementação do serviço de hash usando SHA256
    /// </summary>
    public class HashService : IHashService
    {
        public string GerarHashSHA256(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return string.Empty;

            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(texto);
                var hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToHexString(hashBytes).ToLower();
            }
        }

        public bool VerificarHash(string texto, string hash)
        {
            if (string.IsNullOrEmpty(texto) || string.IsNullOrEmpty(hash))
                return false;

            var hashTexto = GerarHashSHA256(texto);
            return string.Equals(hashTexto, hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}