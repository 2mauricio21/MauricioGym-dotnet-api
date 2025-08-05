using MauricioGym.Academia.Entities;

namespace MauricioGym.Academia.Repositories.SqlServer.Interfaces
{
    public interface IUsuarioAcademiaSqlServerRepository
    {
        Task<UsuarioAcademiaEntity> IncluirUsuarioAcademiaAsync(UsuarioAcademiaEntity usuarioAcademia);
        Task<UsuarioAcademiaEntity> ConsultarUsuarioAcademiaAsync(int idUsuarioAcademia);
        Task AlterarUsuarioAcademiaAsync(UsuarioAcademiaEntity usuarioAcademia);
        Task ExcluirUsuarioAcademiaAsync(int idUsuarioAcademia);
        Task<IEnumerable<UsuarioAcademiaEntity>> ListarUsuarioAcademiaAsync();
        Task<IEnumerable<UsuarioAcademiaEntity>> ListarUsuarioAcademiaPorUsuarioAsync(int idUsuario);
        Task<IEnumerable<UsuarioAcademiaEntity>> ListarUsuarioAcademiaPorAcademiaAsync(int idAcademia);
    }
}