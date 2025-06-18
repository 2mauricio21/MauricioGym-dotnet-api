using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Repositories.Interfaces;

namespace MauricioGym.Administrador.Repositories.SqlServer.Interfaces
{
    public interface IAdministradorSqlServerRepository : ISqlServerRepository
    {
        Task<AdministradorEntity?> ObterPorIdAsync(int id);
        Task<AdministradorEntity?> ObterPorEmailAsync(string email);
        Task<AdministradorEntity?> ObterPorCpfAsync(string cpf);
        Task<IEnumerable<AdministradorEntity>> ObterTodosAsync();
        Task<IEnumerable<AdministradorEntity>> ObterAtivosAsync();
        Task<int> CriarAsync(AdministradorEntity administrador);
        Task<bool> AtualizarAsync(AdministradorEntity administrador);
        Task<bool> ExcluirAsync(int id, int usuarioId);
        Task<bool> ExisteAsync(int id);
        Task<bool> ExistePorEmailAsync(string email, int? idExcluir = null);
        Task<bool> ExistePorCpfAsync(string cpf, int? idExcluir = null);
    }
}