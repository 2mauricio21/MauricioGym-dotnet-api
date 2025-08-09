using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Seguranca.Entities;

namespace MauricioGym.Seguranca.Repositories.SqlServer.Interfaces
{
    /// <summary>
    /// Interface para repositório de autenticação no SQL Server
    /// </summary>
    public interface IAutenticacaoSqlServerRepository
    {
        Task<IResultadoValidacao<int>> IncluirAutenticacaoAsync(AutenticacaoEntity autenticacao);
        Task<IResultadoValidacao<AutenticacaoEntity?>> ConsultarAutenticacaoPorEmailAsync(string email);
        Task<IResultadoValidacao<AutenticacaoEntity?>> ConsultarAutenticacaoPorUsuarioAsync(int idUsuario);
        Task<IResultadoValidacao<bool>> AlterarSenhaAsync(int idUsuario, string novaSenha);
        Task<IResultadoValidacao<bool>> AtualizarTentativasLoginAsync(int idUsuario, int tentativas);
        Task<IResultadoValidacao<bool>> BloquearContaAsync(int idUsuario);
        Task<IResultadoValidacao<bool>> DesbloquearContaAsync(int idUsuario);
        Task<IResultadoValidacao<bool>> AtualizarUltimoLoginAsync(int idUsuario);
        Task<IResultadoValidacao<bool>> AtualizarRefreshTokenAsync(int idUsuario, string refreshToken, DateTime dataExpiracao);
        Task<IResultadoValidacao<bool>> RemoverRefreshTokenAsync(int idUsuario);
        Task<IResultadoValidacao<AutenticacaoEntity?>> ConsultarPorRefreshTokenAsync(string refreshToken);
    }
}