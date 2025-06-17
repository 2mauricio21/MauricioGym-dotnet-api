using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Interfaces
{
    public interface ICaixaService
    {
        Task<IResultadoValidacao<CaixaEntity>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<IEnumerable<CaixaEntity>>> ListarAsync();
        Task<IResultadoValidacao<int>> CriarAsync(CaixaEntity caixa);
        Task<IResultadoValidacao<bool>> AtualizarAsync(CaixaEntity caixa);
        Task<IResultadoValidacao<bool>> RemoverAsync(int id);
    }
}
