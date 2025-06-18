using System.Runtime.CompilerServices;

namespace MauricioGym.Infra.Repositories.SqlServer.Interfaces
{
    public interface ITransactionSqlServerRepository : IDisposable
    {
        Task BeginTransactionAsync([CallerMemberName] string callerMemberName = "");

        Task CommitAsync([CallerMemberName] string callerMemberName = "");

        Task RollbackAsync([CallerMemberName] string callerMemberName = "");
    }
}
