using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Plano.Entities;
using MauricioGym.Plano.Services.Validators;

namespace MauricioGym.Plano.Services.Interfaces
{
    public interface IUsuarioPlanoService : IService<UsuarioPlanoValidator>
    {
        Task<IResultadoValidacao<UsuarioPlanoEntity>> IncluirUsuarioPlanoAsync(UsuarioPlanoEntity usuarioPlano, int idUsuario);
        Task<IResultadoValidacao<UsuarioPlanoEntity>> ConsultarUsuarioPlanoAsync(int idUsuarioPlano);
        Task<IResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>> ConsultarPlanosPorUsuarioAsync(int idUsuario);
        Task<IResultadoValidacao<UsuarioPlanoEntity>> ConsultarPlanoAtivoPorUsuarioAsync(int idUsuario);
        Task<IResultadoValidacao> AlterarUsuarioPlanoAsync(UsuarioPlanoEntity usuarioPlano, int idUsuario);
        Task<IResultadoValidacao> CancelarUsuarioPlanoAsync(int idUsuarioPlano, int idUsuario);
        Task<IResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>> ListarUsuariosPlanosAsync();
        Task<IResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>> ListarUsuariosPlanosAtivosAsync();
        Task<IResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>> ListarUsuariosPlanosPorStatusAsync(string status);
    }
}