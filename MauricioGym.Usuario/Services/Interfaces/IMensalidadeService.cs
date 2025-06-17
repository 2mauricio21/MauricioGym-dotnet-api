using MauricioGym.Usuario.Entities;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Usuario.Services.Interfaces
{
    public interface IMensalidadeService
    {
        Task<IResultadoValidacao<IEnumerable<MensalidadeEntity>>> ObterTodosAsync();
        Task<IResultadoValidacao<MensalidadeEntity?>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<IEnumerable<MensalidadeEntity>>> ObterPorUsuarioAsync(int usuarioId);
        Task<IResultadoValidacao<IEnumerable<MensalidadeEntity>>> ObterPendentesAsync();
        Task<IResultadoValidacao<IEnumerable<MensalidadeEntity>>> ObterVencendasAsync(int dias = 7);
        Task<IResultadoValidacao<bool>> EstaEmDiaAsync(int usuarioId);
        Task<IResultadoValidacao<bool>> RemoverAsync(int id);
        Task<IResultadoValidacao<bool>> ExisteAsync(int id);
        Task<IResultadoValidacao<IEnumerable<MensalidadeEntity>>> ListarPorUsuarioAsync(int usuarioId);
        Task<IResultadoValidacao<MensalidadeEntity?>> ObterMensalidadeAtualAsync(int usuarioId);
        Task<IResultadoValidacao<int>> RegistrarPagamentoMensalidadeAsync(int usuarioId, int planoId, int meses, decimal valorBase, DateTime dataInicio);
        Task<IResultadoValidacao<bool>> VerificarMensalidadeEmDiaAsync(int usuarioId);
        decimal CalcularValorComDesconto(int meses, decimal valorBase);
    }
}
