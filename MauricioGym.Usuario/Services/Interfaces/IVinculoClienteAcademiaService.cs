using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Validators;

namespace MauricioGym.Usuario.Services.Interfaces
{
    public interface IVinculoClienteAcademiaService : IService<VinculoClienteAcademiaValidator>
    {
        Task<VinculoClienteAcademiaEntity?> ObterPorIdAsync(int id);
        Task<VinculoClienteAcademiaEntity?> ObterPorClienteEAcademiaAsync(int clienteId, int academiaId);
        Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterPorClienteIdAsync(int clienteId);
        Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterPorAcademiaIdAsync(int academiaId);
        Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterAtivosPorClienteIdAsync(int clienteId);
        Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterAtivosPorAcademiaIdAsync(int academiaId);
        Task<int> CriarAsync(VinculoClienteAcademiaEntity vinculo);
        Task<bool> AtualizarAsync(VinculoClienteAcademiaEntity vinculo);
        Task<bool> ExcluirAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<bool> ExisteVinculoAsync(int clienteId, int academiaId);
        Task<bool> ExisteVinculoAtivoAsync(int clienteId, int academiaId);
    }
}
