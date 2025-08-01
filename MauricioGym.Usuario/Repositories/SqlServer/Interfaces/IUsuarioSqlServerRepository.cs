using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Infra.Repositories.Interfaces;
using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Repositories.SqlServer.Interfaces
{
    public interface IUsuarioSqlServerRepository : ISqlServerRepository
    {
        Task<UsuarioEntity> IncluirUsuarioAsync(UsuarioEntity usuario);
        Task<UsuarioEntity> ConsultarUsuarioAsync(int idUsuario);
        Task<UsuarioEntity> ConsultarUsuarioPorEmailAsync(string email);
        Task<UsuarioEntity> ConsultarUsuarioPorCPFAsync(string cpf);
        Task AlterarUsuarioAsync(UsuarioEntity usuario);
        Task ExcluirUsuarioAsync(int idUsuario);
        Task<IEnumerable<UsuarioEntity>> ListarUsuariosAsync();
        Task<IEnumerable<UsuarioEntity>> ListarUsuariosAtivosAsync();
    }
}