namespace MauricioGym.Infra.Services.Interfaces
{
    public interface IHashService
    {
        /// <summary>
        /// Gera hash SHA256 de uma string
        /// </summary>
        /// <param name="input">String para gerar hash</param>
        /// <returns>Hash SHA256 em hexadecimal</returns>
        string GenerateSHA256Hash(string input);

        /// <summary>
        /// Verifica se uma string corresponde ao hash fornecido
        /// </summary>
        /// <param name="input">String para verificar</param>
        /// <param name="hash">Hash para comparar</param>
        /// <returns>True se corresponder</returns>
        bool VerifyHash(string input, string hash);
    }
}