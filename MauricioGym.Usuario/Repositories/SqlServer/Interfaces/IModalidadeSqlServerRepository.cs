using MauricioGym.Infra.Repositories.Interfaces;
using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Repositories.SqlServer.Interfaces
{
    public interface IModalidadeSqlServerRepository : ISqlServerRepository
    {
        Task<ModalidadeEntity?> ObterPorIdAsync(int id);
        Task<ModalidadeEntity?> ObterPorNomeAsync(string nome);
        Task<IEnumerable<ModalidadeEntity>> ListarAsync();
        Task<IEnumerable<ModalidadeEntity>> ListarAtivasAsync();
        Task<IEnumerable<ModalidadeEntity>> ObterPorAcademiaIdAsync(int academiaId);
        Task<int> CriarAsync(ModalidadeEntity modalidade);
        Task<bool> AtualizarAsync(ModalidadeEntity modalidade);
        Task<bool> RemoverAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<bool> ExisteNomeAsync(string nome, int academiaId, int? idExcluir = null);

        // Métodos para compatibilidade com padrão Juris
        Task<IEnumerable<ModalidadeEntity>> ObterTodosAsync();
        Task ExcluirAsync(int id);
    }
}
