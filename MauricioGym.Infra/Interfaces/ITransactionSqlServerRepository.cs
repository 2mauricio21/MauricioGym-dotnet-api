namespace MauricioGym.Infra.Interfaces
{
    public interface ITransactionSqlServerRepository
    {
        Task BeginTransactionAsync(string callerMemberName = "");
        Task CommitAsync(string callerMemberName = "");
        Task RollbackAsync(string callerMemberName = "");
    }
}
