using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Repositories.Interfaces;

namespace MauricioGym.Administrador.Repositories.SqlServer.Interfaces
{
    public interface IAcademiaSqlServerRepository : ISqlServerRepository
    {
        Task<AcademiaEntity?> ObterPorIdAsync(int id);
        Task<AcademiaEntity?> ObterPorCnpjAsync(string cnpj);
        Task<IEnumerable<AcademiaEntity>> ListarAsync();
        Task<IEnumerable<AcademiaEntity>> ListarAtivosAsync();
        Task<int> CriarAsync(AcademiaEntity academia);
        Task<bool> AtualizarAsync(AcademiaEntity academia);
        Task<bool> RemoverAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<bool> ExisteCnpjAsync(string cnpj, int? idExcluir = null);

        // Métodos para compatibilidade com padrão Juris
        Task<IEnumerable<AcademiaEntity>> ObterTodosAsync();
        Task<IEnumerable<AcademiaEntity>> ObterAtivosAsync();
        Task ExcluirAsync(int id);
    }
}
