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
        Task<bool> AlterarAcessoAsync(AcessoEntity acesso);
        Task<bool> RegistrarSaidaAsync(int idAcesso, DateTime dataSaida);
        Task<IEnumerable<AcessoEntity>> ListarAcessosAsync();
        Task<IEnumerable<AcessoEntity>> ConsultarAcessosPorUsuarioAsync(int idUsuario);
        Task<IEnumerable<AcessoEntity>> ConsultarAcessosPorAcademiaAsync(int idAcademia);
        Task<IEnumerable<AcessoEntity>> ConsultarAcessosAtivosPorAcademiaAsync(int idAcademia);
    }
}