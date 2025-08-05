using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Infra.Repositories.Interfaces;
using MauricioGym.Plano.Entities;

namespace MauricioGym.Plano.Repositories.SqlServer.Interfaces
{
    public interface IUsuarioPlanoSqlServerRepository : ISqlServerRepository
    {
        Task<UsuarioPlanoEntity> IncluirAsync(UsuarioPlanoEntity usuarioPlano);
        Task<UsuarioPlanoEntity> ObterPorIdAsync(int idUsuarioPlano);
        Task<UsuarioPlanoEntity> ObterAtivoPorUsuarioAsync(int idUsuario);
        Task<IEnumerable<UsuarioPlanoEntity>> ObterPorUsuarioAsync(int idUsuario);
        Task<bool> AtualizarAsync(UsuarioPlanoEntity usuarioPlano);
        Task<bool> CancelarAsync(int idUsuarioPlano);
        Task<IEnumerable<UsuarioPlanoEntity>> ListarTodosAsync();
        Task<IEnumerable<UsuarioPlanoEntity>> ListarAtivosAsync();
        Task<IEnumerable<UsuarioPlanoEntity>> ListarPorStatusAsync(string status);
    }
}