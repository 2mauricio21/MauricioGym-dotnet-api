using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Seguranca.Entities;

namespace MauricioGym.Seguranca.Repositories.SqlServer.Interfaces
{
    /// <summary>
    /// Interface para repositório de recuperação de senha no SQL Server
    /// </summary>
    public interface IRecuperacaoSenhaSqlServerRepository
    {
        Task<IResultadoValidacao<int>> IncluirSolicitacaoAsync(RecuperacaoSenhaEntity recuperacao);
        Task<IResultadoValidacao<RecuperacaoSenhaEntity?>> ConsultarPorTokenAsync(string token);
        Task<IResultadoValidacao<bool>> MarcarComoUtilizadoAsync(int idRecuperacao);
        Task<IResultadoValidacao<bool>> InvalidarSolicitacoesAnterioresAsync(int idUsuario);
        Task<IResultadoValidacao<IEnumerable<RecuperacaoSenhaEntity>>> ListarSolicitacoesPorUsuarioAsync(int idUsuario);
        Task<IResultadoValidacao<bool>> LimparSolicitacoesExpiradasAsync();
    }
}