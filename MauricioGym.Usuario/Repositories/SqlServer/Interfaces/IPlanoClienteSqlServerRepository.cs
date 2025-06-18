using MauricioGym.Infra.Repositories.Interfaces;
using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Repositories.SqlServer.Interfaces
{
    public interface IPlanoClienteSqlServerRepository : ISqlServerRepository
    {
        Task<PlanoClienteEntity?> ObterPorIdAsync(int id);
        Task<IEnumerable<PlanoClienteEntity>> ObterPorClienteIdAsync(int clienteId);
        Task<IEnumerable<PlanoClienteEntity>> ObterPorPlanoIdAsync(int planoId);
        Task<IEnumerable<PlanoClienteEntity>> ListarAsync();
        Task<IEnumerable<PlanoClienteEntity>> ListarAtivosAsync();
        Task<IEnumerable<PlanoClienteEntity>> ObterAtivosPorClienteIdAsync(int clienteId);
        Task<IEnumerable<PlanoClienteEntity>> ObterVencidosPorClienteIdAsync(int clienteId);
        Task<IEnumerable<PlanoClienteEntity>> ObterVencendoAsync(int diasAntecedencia = 7);
        Task<int> CriarAsync(PlanoClienteEntity planoCliente);
        Task<bool> AtualizarAsync(PlanoClienteEntity planoCliente);
        Task<bool> RemoverAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<bool> ClientePossuiPlanoAtivoAsync(int clienteId, int planoId);
        Task<IEnumerable<PlanoClienteEntity>> ObterPorClienteAsync(string cpfCliente, int idAcademia);
        Task<PlanoClienteEntity?> ObterPlanoAtivoAsync(string cpfCliente, int idAcademia);
        Task<IEnumerable<PlanoClienteEntity>> ObterPlanosVencendoAsync(int idAcademia, int dias = 7);
        Task<bool> CancelarPlanoAsync(int id, string motivo);
        Task<bool> RenovarPlanoAsync(string cpfCliente, int idAcademia, int mesesAdicionais, decimal valorPago);
        Task<bool> ClientePossuiPlanoAtivoAsync(string cpfCliente, int idAcademia);

        // Métodos para compatibilidade com padrão Juris
        Task<IEnumerable<PlanoClienteEntity>> ObterTodosAsync();
        Task ExcluirAsync(int id);
    }
}
