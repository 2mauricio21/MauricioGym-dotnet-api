using System.Security.Cryptography;
using System.Text;
using MauricioGym.Infra.Services.Interfaces;

namespace MauricioGym.Infra.Services
{
    public class HashService : IHashService
    {
        /// <summary>
        /// Gera hash SHA256 de uma string
        /// </summary>
        /// <param name="input">String para gerar hash</param>
        /// <returns>Hash SHA256 em hexadecimal</returns>
        public string GenerateSHA256Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToHexString(hashBytes).ToLower();
            }
        }

        /// <summary>
        /// Verifica se uma string corresponde ao hash fornecido
        /// </summary>
        /// <param name="input">String para verificar</param>
        /// <param name="hash">Hash para comparar</param>
        /// <returns>True se corresponder</returns>
        public bool VerifyHash(string input, string hash)
        {
            var inputHash = GenerateSHA256Hash(input);
            return string.Equals(inputHash, hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}