using MauricioGym.Infra.Config;
using MauricioGym.Infra.Interfaces;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace MauricioGym.Infra.Databases.SqlServer
{
    public class SqlServerDbContext : IDbContext<SqlConnection>
    {
        public SqlConnection Connection { get; private set; }

        public static string Database { get; set; } = string.Empty;

        public bool IsInTransaction { get; private set; }

        public DbTransaction? Transaction { get; private set; }

        private string callerMemberName = string.Empty;        public SqlServerDbContext(IOptions<SqlServerConnectionOptions> options)
        {
            var connectionString = options.Value.DefaultConnection ?? AppConfig.SqlServerConnectionString;
            Connection = new SqlConnection(connectionString);
        }

        public void Dispose()
        {
            if (Connection != null)
                Connection.Close();

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task BeginTransactionAsync(string callerMemberName = "")
        {
            if (!IsInTransaction)
            {
                this.callerMemberName = callerMemberName;
                if (Connection.State == ConnectionState.Closed)
                    await Connection.OpenAsync();

                Transaction = await Connection.BeginTransactionAsync();
                IsInTransaction = true;
            }
        }

        public async Task CommitAsync(string callerMemberName = "")
        {
            if (IsInTransaction && this.callerMemberName == callerMemberName && Transaction != null)
            {
                await Transaction.CommitAsync();
                IsInTransaction = false;
                this.callerMemberName = string.Empty;
            }
        }

        public async Task RollbackAsync(string callerMemberName = "")
        {
            if (IsInTransaction && this.callerMemberName == callerMemberName && Transaction != null)
            {
                await Transaction.RollbackAsync();
                IsInTransaction = false;
                this.callerMemberName = string.Empty;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Connection != null)
                {
                    Connection.Dispose();
                    Connection = null!;
                }
            }
        }
    }
}
