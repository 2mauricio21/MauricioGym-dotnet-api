using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Infra.Repositories.Interfaces;
using MauricioGym.Acesso.Entities;

namespace MauricioGym.Acesso.Repositories.SqlServer.Interfaces
{
    public interface IBloqueioAcessoSqlServerRepository : ISqlServerRepository
    {
        Task<BloqueioAcessoEntity> IncluirBloqueioAcessoAsync(BloqueioAcessoEntity bloqueioAcesso);
        Task<BloqueioAcessoEntity> ConsultarBloqueioAcessoAsync(int idBloqueioAcesso);
        Task<IEnumerable<BloqueioAcessoEntity>> ConsultarBloqueiosPorUsuarioAsync(int idUsuario);
        Task<IEnumerable<BloqueioAcessoEntity>> ConsultarBloqueiosPorUsuarioAcademiaAsync(int idUsuario, int idAcademia);
        Task<BloqueioAcessoEntity> ConsultarBloqueioAtivoPorUsuarioAcademiaAsync(int idUsuario, int idAcademia);
        Task<bool> AlterarBloqueioAcessoAsync(BloqueioAcessoEntity bloqueioAcesso);
        Task<bool> CancelarBloqueioAcessoAsync(int idBloqueioAcesso);
        Task<IEnumerable<BloqueioAcessoEntity>> ListarBloqueiosAcessoAsync();
        Task<IEnumerable<BloqueioAcessoEntity>> ListarBloqueiosAtivosAsync();
    }
}