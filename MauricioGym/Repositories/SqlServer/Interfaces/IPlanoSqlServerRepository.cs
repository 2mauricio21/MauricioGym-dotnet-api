using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;

namespace MauricioGym.Repositories.SqlServer.Interfaces
{
    public interface IPlanoSqlServerRepository
    {
        Task<PlanoEntity> ObterPorIdAsync(int id);
        Task<IEnumerable<PlanoEntity>> ListarAsync();
        Task<int> CriarAsync(PlanoEntity plano);
        Task<bool> AtualizarAsync(PlanoEntity plano);
        Task<bool> RemoverLogicamenteAsync(int id);
    }
}
