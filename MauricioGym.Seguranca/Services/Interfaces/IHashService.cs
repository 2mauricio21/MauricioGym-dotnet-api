using System;

namespace MauricioGym.Seguranca.Services.Interfaces
{
    /// <summary>
    /// Interface para serviços de hash
    /// </summary>
    public interface IHashService
    {
        /// <summary>
        /// Gera hash SHA256 para o texto fornecido
        /// </summary>
        /// <param name="texto">Texto a ser convertido em hash</param>
        /// <returns>Hash SHA256 do texto</returns>
        string GerarHashSHA256(string texto);

        /// <summary>
        /// Verifica se um texto corresponde ao hash fornecido
        /// </summary>
        /// <param name="texto">Texto original</param>
        /// <param name="hash">Hash para comparação</param>
        /// <returns>True se o texto corresponde ao hash</returns>
        bool VerificarHash(string texto, string hash);
    }
}