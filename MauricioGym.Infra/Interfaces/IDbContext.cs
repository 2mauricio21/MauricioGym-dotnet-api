using System.Data;
using System.Data.Common;

namespace MauricioGym.Infra.Interfaces
{
    public interface IDbContext<TConnection> : IDisposable where TConnection : IDbConnection
    {
        TConnection Connection { get; }
        bool IsInTransaction { get; }
        DbTransaction? Transaction { get; }
        Task BeginTransactionAsync(string callerMemberName = "");
        Task CommitAsync(string callerMemberName = "");
        Task RollbackAsync(string callerMemberName = "");
    }
}
