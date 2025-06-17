using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioEntity>> ObterTodosAsync();
        Task<UsuarioEntity?> ObterPorIdAsync(int id);
        Task<UsuarioEntity?> ObterPorEmailAsync(string email);
        Task<int> CriarAsync(UsuarioEntity usuario);
        Task<bool> AtualizarAsync(UsuarioEntity usuario);
        Task<bool> RemoverAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<bool> PodeFazerCheckInAsync(int usuarioId);
    }
}
