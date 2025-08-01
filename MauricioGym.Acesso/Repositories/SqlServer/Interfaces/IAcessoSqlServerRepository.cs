using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Infra.Repositories.Interfaces;
using MauricioGym.Acesso.Entities;

namespace MauricioGym.Acesso.Repositories.SqlServer.Interfaces
{
    public interface IAcessoSqlServerRepository : ISqlServerRepository
    {
        Task<AcessoEntity> IncluirAcessoAsync(AcessoEntity acesso);
        Task<AcessoEntity> ConsultarAcessoAsync(int idAcesso);
        Task AlterarAcessoAsync(AcessoEntity acesso);
        Task<IEnumerable<AcessoEntity>> ListarAcessosAsync();
        Task<IEnumerable<AcessoEntity>> ListarAcessosPorUsuarioAsync(int idUsuario);
        Task<IEnumerable<AcessoEntity>> ListarAcessosPorAcademiaAsync(int idAcademia);
        Task<IEnumerable<AcessoEntity>> ListarAcessosAtivosAsync();
        Task<BloqueioAcessoEntity> IncluirBloqueioAsync(BloqueioAcessoEntity bloqueio);
        Task<BloqueioAcessoEntity> ConsultarBloqueioAtivoAsync(int idUsuario, int idAcademia);
        Task<IEnumerable<BloqueioAcessoEntity>> ListarBloqueiosPorUsuarioAsync(int idUsuario);
    }
}