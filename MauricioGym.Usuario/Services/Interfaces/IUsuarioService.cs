using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IResultadoValidacao<IEnumerable<UsuarioEntity>>> ObterTodosAsync();
        Task<IResultadoValidacao<UsuarioEntity?>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<UsuarioEntity?>> ObterPorEmailAsync(string email);
        Task<IResultadoValidacao<int>> CriarAsync(UsuarioEntity usuario);
        Task<IResultadoValidacao<bool>> AtualizarAsync(UsuarioEntity usuario);
        Task<IResultadoValidacao<bool>> RemoverAsync(int id);
        Task<IResultadoValidacao<bool>> ExisteAsync(int id);
        Task<IResultadoValidacao<bool>> ExisteEmailAsync(string email, int? excludeId = null);
        Task<IResultadoValidacao<bool>> PodeFazerCheckInAsync(int usuarioId);
    }
}
