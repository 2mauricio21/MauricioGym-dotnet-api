using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Repositories.Interfaces;

namespace MauricioGym.Administrador.Repositories.SqlServer.Interfaces
{
    public interface IAvaliacaoFisicaSqlServerRepository : ISqlServerRepository
    {
        Task<AvaliacaoFisicaEntity?> ObterPorIdAsync(int id);
        Task<IEnumerable<AvaliacaoFisicaEntity>> ListarPorAlunoAsync(int alunoId);
        Task<IEnumerable<AvaliacaoFisicaEntity>> ListarPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
        Task<AvaliacaoFisicaEntity?> ObterUltimaAvaliacaoAsync(int alunoId);
        Task<int> CriarAsync(AvaliacaoFisicaEntity avaliacaoFisica);
        Task<bool> AtualizarAsync(AvaliacaoFisicaEntity avaliacaoFisica);
        Task<bool> RemoverAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}