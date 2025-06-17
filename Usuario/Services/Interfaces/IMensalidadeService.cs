using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Services.Interfaces
{
    public interface IMensalidadeService
    {
        Task<IEnumerable<MensalidadeEntity>> ObterTodosAsync();
        Task<MensalidadeEntity?> ObterPorIdAsync(int id);
        Task<IEnumerable<MensalidadeEntity>> ObterPorUsuarioAsync(int usuarioId);
        Task<IEnumerable<MensalidadeEntity>> ObterPendentesAsync();
        Task<IEnumerable<MensalidadeEntity>> ObterVencendasAsync(int dias = 7);
        Task<bool> EstaEmDiaAsync(int usuarioId);
        Task<bool> RemoverAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<IEnumerable<MensalidadeEntity>> ListarPorUsuarioAsync(int usuarioId);
        Task<MensalidadeEntity?> ObterMensalidadeAtualAsync(int usuarioId);
        Task<int> RegistrarPagamentoMensalidadeAsync(int usuarioId, int planoId, int meses, decimal valorBase, DateTime dataInicio);        Task<bool> VerificarMensalidadeEmDiaAsync(int usuarioId);
        decimal CalcularValorComDesconto(int meses, decimal valorBase);
    }
}
