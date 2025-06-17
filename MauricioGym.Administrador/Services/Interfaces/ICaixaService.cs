using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Administrador.Entities;

namespace MauricioGym.Administrador.Services.Interfaces
{
    public interface ICaixaService
    {
        Task<CaixaEntity> ObterPorIdAsync(int id);
        Task<IEnumerable<CaixaEntity>> ListarAsync();
        Task<int> CriarAsync(CaixaEntity caixa);
        Task<bool> AtualizarAsync(CaixaEntity caixa);
        Task<bool> RemoverAsync(int id);
    }
}
