using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Repositories.Interfaces
{
    public interface IMensalidadeSqlServerRepository
    {
        Task<IEnumerable<MensalidadeEntity>> ObterTodosAsync();
        Task<MensalidadeEntity?> ObterPorIdAsync(int id);
        Task<IEnumerable<MensalidadeEntity>> ObterPorUsuarioAsync(int usuarioId);
        Task<IEnumerable<MensalidadeEntity>> ObterPendentesAsync();
        Task<IEnumerable<MensalidadeEntity>> ObterVencendasAsync(int dias);
        Task<bool> EstaEmDiaAsync(int usuarioId);
        Task<bool> RemoverAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<int> CriarAsync(MensalidadeEntity mensalidade);        Task<bool> AtualizarAsync(MensalidadeEntity mensalidade);
        Task<MensalidadeEntity?> ObterMensalidadeAtualAsync(int usuarioId);
    }
}
