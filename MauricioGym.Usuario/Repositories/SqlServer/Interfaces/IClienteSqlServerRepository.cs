using MauricioGym.Infra.Repositories.Interfaces;
using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Repositories.SqlServer.Interfaces
{
    public interface IClienteSqlServerRepository : ISqlServerRepository
    {
        Task<ClienteEntity?> ObterPorIdAsync(int id);
        Task<ClienteEntity?> ObterPorCpfAsync(string cpf);
        Task<ClienteEntity?> ObterPorEmailAsync(string email);
        Task<IEnumerable<ClienteEntity>> ListarAsync();
        Task<IEnumerable<ClienteEntity>> ListarAtivosAsync();
        Task<int> CriarAsync(ClienteEntity cliente);
        Task<bool> AtualizarAsync(ClienteEntity cliente);
        Task<bool> RemoverAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<bool> ExisteCpfAsync(string cpf, int? idExcluir = null);
        Task<bool> ExisteEmailAsync(string email, int? idExcluir = null);

        // Métodos para compatibilidade com padrão Juris
        Task<IEnumerable<ClienteEntity>> ObterTodosAsync();
        Task<IEnumerable<ClienteEntity>> ObterAtivosAsync();
        Task ExcluirAsync(int id);
    }
}
