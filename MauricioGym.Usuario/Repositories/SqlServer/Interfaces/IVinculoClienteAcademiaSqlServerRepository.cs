using MauricioGym.Infra.Repositories.Interfaces;
using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Repositories.SqlServer.Interfaces
{
    public interface IVinculoClienteAcademiaSqlServerRepository : ISqlServerRepository
    {
        Task<VinculoClienteAcademiaEntity?> ObterPorIdAsync(int id);
        Task<VinculoClienteAcademiaEntity?> ObterPorClienteEAcademiaAsync(int clienteId, int academiaId);
        Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterPorClienteIdAsync(int clienteId);
        Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterPorAcademiaIdAsync(int academiaId);
        Task<IEnumerable<VinculoClienteAcademiaEntity>> ListarAsync();
        Task<IEnumerable<VinculoClienteAcademiaEntity>> ListarAtivosAsync();
        Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterAtivosPorClienteIdAsync(int clienteId);
        Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterAtivosPorAcademiaIdAsync(int academiaId);
        Task<int> CriarAsync(VinculoClienteAcademiaEntity vinculo);
        Task<bool> AtualizarAsync(VinculoClienteAcademiaEntity vinculo);
        Task<bool> RemoverAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<bool> ExisteVinculoAsync(int clienteId, int academiaId);
        Task<bool> ExisteVinculoAtivoAsync(int clienteId, int academiaId);
        Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterPorAcademiaAsync(int idAcademia);
        Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterPorClienteAsync(string cpfCliente);
        Task<VinculoClienteAcademiaEntity?> ObterVinculoAsync(string cpfCliente, int idAcademia);
        Task<bool> DesativarVinculoAsync(string cpfCliente, int idAcademia);
        Task<bool> ExisteVinculoAtivoAsync(string cpfCliente, int idAcademia);
        Task<int> ContarClientesAtivosAcademiaAsync(int idAcademia);

        // Métodos para compatibilidade com padrão Juris
        Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterTodosAsync();
        Task ExcluirAsync(int id);
    }
}
