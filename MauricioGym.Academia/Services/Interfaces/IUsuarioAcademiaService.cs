using MauricioGym.Academia.Entities;
using MauricioGym.Academia.Services.Validators;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Academia.Services.Interfaces
{
    public interface IUsuarioAcademiaService : IService<UsuarioAcademiaValidator>
    {
        Task<IResultadoValidacao<UsuarioAcademiaEntity>> IncluirUsuarioAcademiaAsync(UsuarioAcademiaEntity usuarioAcademia, int idUsuario);
        Task<IResultadoValidacao<UsuarioAcademiaEntity>> ConsultarUsuarioAcademiaAsync(int idUsuarioAcademia);
        Task<IResultadoValidacao> AlterarUsuarioAcademiaAsync(UsuarioAcademiaEntity usuarioAcademia, int idUsuario);
        Task<IResultadoValidacao> ExcluirUsuarioAcademiaAsync(int idUsuarioAcademia, int idUsuario);
        Task<IResultadoValidacao<IEnumerable<UsuarioAcademiaEntity>>> ListarUsuarioAcademiaAsync();
        Task<IResultadoValidacao<IEnumerable<UsuarioAcademiaEntity>>> ListarUsuarioAcademiaPorUsuarioAsync(int idUsuario);
        Task<IResultadoValidacao<IEnumerable<UsuarioAcademiaEntity>>> ListarUsuarioAcademiaPorAcademiaAsync(int idAcademia);
    }
}