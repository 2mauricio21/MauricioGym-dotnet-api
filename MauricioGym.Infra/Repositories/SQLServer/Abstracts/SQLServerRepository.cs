using Dapper;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Infra.Entities.Interfaces;
using MauricioGym.Infra.Repositories.Interfaces;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Transactions;

namespace MauricioGym.Infra.Repositories.SQLServer.Abstracts
{
    public static class SqlServerRepositoryExtensions
    {
        public static T GetValue<T>(this DbDataReader dataReader, string fieldName)
        {
            return (T)Convert.ChangeType(dataReader[fieldName].ToString(), typeof(T));
        }

        public static T GetNullableValue<T>(this DbDataReader dataReader, string fieldName)
        {
            if (dataReader.IsDBNull(dataReader.GetOrdinal(fieldName)))
                return default;

            return (T)Convert.ChangeType(dataReader[fieldName].ToString(), Type.GetType(Nullable.GetUnderlyingType(typeof(T)).FullName));
        }
    }

    public class SqlServerRepository : ISqlServerRepository
    {
        #region [ Campos ]

        public SqlConnection SQLServerConnection { get; set; }

        protected SQLServerDbContext DbContext { get; }

        #endregion

        #region [ Construtores ]

        public SqlServerRepository(SQLServerDbContext sqlServerDbContext)
        {
            SQLServerConnection = sqlServerDbContext.Connection;
            DbContext = sqlServerDbContext;
        }

        public async ValueTask<IDbTransaction> GetTransactionAsync()
        {
            if (SQLServerConnection.State == ConnectionState.Closed)
                SQLServerConnection.Open();

            return await SQLServerConnection.BeginTransactionAsync();
        }

        public async Task<int> ExecuteScalarAsync(string commandText, IEntity entity)
        {
            return await SQLServerConnection.ExecuteScalarAsync<int>(commandText, entity);
        }

        public int ExecuteNonQuery(string commandText, IEntity entity = null, CommandType commandType = CommandType.Text)
        {
            if (SQLServerConnection.State == ConnectionState.Closed)
                SQLServerConnection.Open();

            return SQLServerConnection.Execute(commandText, entity, DbContext.Transaction, commandType: commandType);
        }

        public async Task<int> ExecuteNonQueryAsync(string commandText, IEntity entity, CommandType commandType = CommandType.Text)
        {
            if (SQLServerConnection.State == ConnectionState.Closed)
                SQLServerConnection.Open();

            return await SQLServerConnection.ExecuteAsync(commandText, entity, DbContext.Transaction, commandType: commandType);
        }

        public SqlDataReader ExecuteReader(string commandText, IEnumerable<SqlParameter> parameters, CommandType commandType = CommandType.Text)
        {
            var cmd = new SqlCommand(commandText, SQLServerConnection) { CommandType = commandType };
            cmd.Parameters.AddRange(parameters.ToArray());

            return cmd.ExecuteReader();
        }

        public int ExecuteNonQuery(string commandText, DynamicParameters parameters = null, CommandType commandType = CommandType.Text)
        {
            if (SQLServerConnection.State == ConnectionState.Closed)
                SQLServerConnection.Open();

            return SQLServerConnection.Execute(commandText, parameters, DbContext.Transaction, commandType: commandType);
        }

        public async Task<int> ExecuteNonQueryAsync(string commandText, DynamicParameters parameters = null, CommandType commandType = CommandType.Text)
        {
            if (SQLServerConnection.State == ConnectionState.Closed)
                SQLServerConnection.Open();

            return await SQLServerConnection.ExecuteAsync(commandText, parameters, DbContext.Transaction, commandType: commandType);
        }

        public IEnumerable<TEntity> Query<TEntity>(string commandText, IEntity entity, CommandType commandType = CommandType.Text)
        {
            if (SQLServerConnection.State == ConnectionState.Closed)
                SQLServerConnection.Open();

            return SQLServerConnection.Query<TEntity>(commandText, entity, commandType: commandType);
        }

        public async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string commandText, IEntity entity, CommandType commandType = CommandType.Text)
        {
            if (SQLServerConnection.State == ConnectionState.Closed)
                SQLServerConnection.Open();

            return await SQLServerConnection.QueryAsync<TEntity>(commandText, entity, commandType: commandType, transaction: DbContext.Transaction);
        }

        public IEnumerable<TEntity> Query<TEntity>(string commandText, DynamicParameters parameters, CommandType commandType = CommandType.Text)
        {
            if (SQLServerConnection.State == ConnectionState.Closed)
                SQLServerConnection.Open();

            return SQLServerConnection.Query<TEntity>(commandText, parameters, commandType: commandType, transaction: DbContext.Transaction);
        }

        public async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string commandText, DynamicParameters parameters, CommandType commandType = CommandType.Text)
        {
            if (SQLServerConnection.State == ConnectionState.Closed)
                SQLServerConnection.Open();

            return await SQLServerConnection.QueryAsync<TEntity>(commandText, parameters, commandType: commandType, transaction: DbContext.Transaction);
        }

        public async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string commandText, CommandType commandType = CommandType.Text)
        {
            if (SQLServerConnection.State == ConnectionState.Closed)
                SQLServerConnection.Open();

            return await SQLServerConnection.QueryAsync<TEntity>(commandText, commandType: commandType);
        }

        public IEnumerable<TEntity> Query<TEntity>(string commandText, CommandType commandType = CommandType.Text)
        {
            if (SQLServerConnection.State == ConnectionState.Closed)
                SQLServerConnection.Open();

            return SQLServerConnection.Query<TEntity>(commandText, commandType: commandType, commandTimeout: 999999);
        }

        public TEntity QueryFirst<TEntity>(string commandText, IEntity entity, CommandType commandType = CommandType.Text)
        {
            if (SQLServerConnection.State == ConnectionState.Closed)
                SQLServerConnection.Open();

            return SQLServerConnection.QueryFirst<TEntity>(commandText, entity, commandType: commandType);
        }

        public TEntity QueryFirst<TEntity>(string commandText, CommandType commandType = CommandType.Text)
        {
            if (SQLServerConnection.State == ConnectionState.Closed)
                SQLServerConnection.Open();

            return SQLServerConnection.QueryFirst<TEntity>(commandText, commandType: commandType);
        }

        public SqlMapper.GridReader QueryMultiple(string commandText, IEntity entity, CommandType commandType = CommandType.Text)
        {
            if (SQLServerConnection.State == ConnectionState.Closed)
                SQLServerConnection.Open();

            return SQLServerConnection.QueryMultiple(commandText, entity, commandType: commandType);
        }

        public TransactionScope GetTransactionScope()
        {
            return new TransactionScope();
        }

        public TEntity QueryFirst<TEntity>(string commandText, DynamicParameters parameters, CommandType commandType = CommandType.Text)
        {
            if (SQLServerConnection.State == ConnectionState.Closed)
                SQLServerConnection.Open();

            return SQLServerConnection.QueryFirst<TEntity>(commandText, parameters, commandType: commandType);
        }

        public void Dispose()
        {
            SQLServerConnection.Close();
            SQLServerConnection.Dispose();

            GC.SuppressFinalize(this);
        }

        #endregion
    }
}