using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Repositories.Interfaces;

namespace MauricioGym.Administrador.Repositories.SqlServer.Interfaces
{
    public interface IAlunoSqlServerRepository : ISqlServerRepository
    {
        Task<AlunoEntity?> ObterPorIdAsync(int id);
        Task<AlunoEntity?> ObterPorEmailAsync(string email);
        Task<AlunoEntity?> ObterPorCpfAsync(string cpf);
        Task<IEnumerable<AlunoEntity>> ListarAtivosAsync();
        Task<IEnumerable<AlunoEntity>> ListarPorNomeAsync(string nome);
        Task<IEnumerable<AlunoEntity>> ListarPorPlanoAsync(int planoId);
        Task<IEnumerable<AlunoEntity>> ListarComMensalidadeVencidaAsync();
        Task<int> CriarAsync(AlunoEntity aluno);
        Task<bool> AtualizarAsync(AlunoEntity aluno);
        Task<bool> RemoverAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<bool> ExistePorEmailAsync(string email);
        Task<bool> ExistePorCpfAsync(string cpf);
    }
}