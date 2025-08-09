using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Seguranca.Entities;

namespace MauricioGym.Seguranca.Services.Interfaces
{
    /// <summary>
    /// Interface para serviços de recuperação de senha
    /// </summary>
    public interface IRecuperacaoSenhaService
    {
        /// <summary>
        /// Solicita recuperação de senha
        /// </summary>
        /// <param name="email">Email do usuário</param>
        /// <param name="enderecoIP">Endereço IP da solicitação</param>
        /// <param name="userAgent">User Agent da solicitação</param>
        /// <returns>Resultado da operação</returns>
        Task<IResultadoValidacao<bool>> SolicitarRecuperacaoAsync(string email, string? enderecoIP = null, string? userAgent = null);

        /// <summary>
        /// Valida o token de recuperação
        /// </summary>
        /// <param name="token">Token de recuperação</param>
        /// <returns>Resultado da validação</returns>
        Task<IResultadoValidacao<bool>> ValidarTokenRecuperacaoAsync(string token);

        /// <summary>
        /// Redefine a senha usando o token de recuperação
        /// </summary>
        /// <param name="token">Token de recuperação</param>
        /// <param name="novaSenha">Nova senha</param>
        /// <returns>Resultado da operação</returns>
        Task<IResultadoValidacao<bool>> RedefinirSenhaAsync(string token, string novaSenha);

        /// <summary>
        /// Lista as solicitações de recuperação de um usuário
        /// </summary>
        /// <param name="idUsuario">ID do usuário</param>
        /// <returns>Lista de solicitações</returns>
        Task<IResultadoValidacao<IEnumerable<RecuperacaoSenhaEntity>>> ListarSolicitacoesPorUsuarioAsync(int idUsuario);

        /// <summary>
        /// Limpa solicitações expiradas do sistema
        /// </summary>
        /// <returns>Resultado da operação</returns>
        Task<IResultadoValidacao<bool>> LimparSolicitacoesExpiradasAsync();
    }
}