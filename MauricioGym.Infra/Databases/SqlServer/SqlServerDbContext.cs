
using MauricioGym.Infra.Config;
using MauricioGym.Infra.SQLServer.Interfaces;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace MauricioGym.Infra.SQLServer
{
    public class SQLServerDbContext : IDbContext<SqlConnection>
    {
        public SqlConnection Connection { get; private set; }

        public static string Database { get; set; }

        public bool IsInTransaction { get; private set; }

        public DbTransaction Transaction { get; private set; }

        private string callerMemberName;

        public SQLServerDbContext(IOptions<SQLServerConnectionOptions> options)
        {
            Connection = new SqlConnection(AppConfig.SqlServerConnectionString);
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
            if (IsInTransaction && this.callerMemberName == callerMemberName)
            {
                await Transaction.CommitAsync();
                IsInTransaction = false;
                this.callerMemberName = string.Empty;
            }
        }

        public async Task RollbackAsync(string callerMemberName = "")
        {
            if (IsInTransaction && this.callerMemberName == callerMemberName)
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
                    Connection = null;
                }
            }
        }
    }
}
