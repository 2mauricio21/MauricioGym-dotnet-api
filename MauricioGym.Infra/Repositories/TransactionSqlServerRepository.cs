using MauricioGym.Infra.Databases.SqlServer;
using MauricioGym.Infra.Interfaces;

namespace MauricioGym.Infra.Repositories
{
    public class TransactionSqlServerRepository : ITransactionSqlServerRepository
    {
        private readonly SqlServerDbContext context;

        public TransactionSqlServerRepository(SqlServerDbContext context)
        {
            this.context = context;
        }

        public async Task BeginTransactionAsync(string callerMemberName = "")
        {
            await context.BeginTransactionAsync(callerMemberName);
        }

        public async Task CommitAsync(string callerMemberName = "")
        {
            await context.CommitAsync(callerMemberName);
        }

        public async Task RollbackAsync(string callerMemberName = "")
        {
            await context.RollbackAsync(callerMemberName);
        }
    }
}
