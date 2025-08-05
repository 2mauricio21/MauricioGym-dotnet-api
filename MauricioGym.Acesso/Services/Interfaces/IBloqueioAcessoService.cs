using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Acesso.Entities;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Acesso.Services.Interfaces
{
    public interface IBloqueioAcessoService
    {
        Task<IResultadoValidacao<BloqueioAcessoEntity>> IncluirBloqueioAcessoAsync(BloqueioAcessoEntity bloqueioAcesso);
        Task<IResultadoValidacao<BloqueioAcessoEntity>> ConsultarBloqueioAcessoAsync(int idBloqueioAcesso);
        Task<IResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>> ConsultarBloqueiosPorUsuarioAsync(int idUsuario);
        Task<IResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>> ConsultarBloqueiosPorUsuarioAcademiaAsync(int idUsuario, int idAcademia);
        Task<IResultadoValidacao<BloqueioAcessoEntity>> ConsultarBloqueioAtivoPorUsuarioAcademiaAsync(int idUsuario, int idAcademia);
        Task<IResultadoValidacao> AlterarBloqueioAcessoAsync(BloqueioAcessoEntity bloqueioAcesso);
        Task<IResultadoValidacao> CancelarBloqueioAcessoAsync(int idBloqueioAcesso);
        Task<IResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>> ListarBloqueiosAcessoAsync();
        Task<IResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>> ListarBloqueiosAtivosAsync();
    }
}