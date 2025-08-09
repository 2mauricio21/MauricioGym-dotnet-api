using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Validators;

namespace MauricioGym.Usuario.Services.Interfaces
{
    public interface IUsuarioService : IService<UsuarioValidator>
    {
        Task<IResultadoValidacao<int>> IncluirUsuarioAsync(UsuarioEntity usuario);
        Task<IResultadoValidacao<UsuarioEntity>> ConsultarUsuarioAsync(int idUsuario);
        Task<IResultadoValidacao<UsuarioEntity>> ConsultarUsuarioPorEmailAsync(string email);
        Task<IResultadoValidacao<UsuarioEntity>> ConsultarUsuarioPorCPFAsync(string cpf);
        Task<IResultadoValidacao> AlterarUsuarioAsync(UsuarioEntity usuario);
        Task<IResultadoValidacao> ExcluirUsuarioAsync(int idUsuario);
        Task<IResultadoValidacao<IEnumerable<UsuarioEntity>>> ListarUsuariosAsync();
        Task<IResultadoValidacao<IEnumerable<UsuarioEntity>>> ListarUsuariosAtivosAsync();
        // ValidarLoginAsync foi movido para MauricioGym.Seguranca.Services.IAutenticacaoService
    }
}