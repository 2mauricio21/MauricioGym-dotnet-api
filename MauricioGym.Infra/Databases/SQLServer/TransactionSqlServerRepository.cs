using MauricioGym.Infra.SQLServer;
using MauricioGym.Infra.Repositories.SqlServer.Interfaces;
using System.Runtime.CompilerServices;

namespace MauricioGym.Infra.Repositories
{
    public class TransactionSqlServerRepository : ITransactionSqlServerRepository
    {
        #region [ Campos ]

        private readonly SQLServerDbContext dbContext;

        #endregion

        #region [ Construtor ]

        public TransactionSqlServerRepository(SQLServerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #endregion

        #region [ Métodos Púbicos ]

        public async Task BeginTransactionAsync([CallerMemberName] string callerMemberName = "")
        {
            await dbContext.BeginTransactionAsync(callerMemberName);
        }

        public async Task CommitAsync([CallerMemberName] string callerMemberName = "")
        {
            await dbContext.CommitAsync(callerMemberName);
        }

        public async Task RollbackAsync([CallerMemberName] string callerMemberName = "")
        {
            await dbContext.RollbackAsync(callerMemberName);
        }

        public void Dispose()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }

        #endregion
    }
}
