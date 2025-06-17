using Dapper;
using MauricioGym.Infra.Databases.SqlServer;
using MauricioGym.Infra.Entities.Interfaces;
using MauricioGym.Infra.Interfaces;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace MauricioGym.Infra.Repositories
{
    public static class SqlServerRepositoryExtensions
    {
        public static T GetValue<T>(this DbDataReader dataReader, string fieldName)
        {
            return (T)Convert.ChangeType(dataReader[fieldName].ToString(), typeof(T));
        }

        public static T? GetNullableValue<T>(this DbDataReader dataReader, string fieldName)
        {
            if (dataReader.IsDBNull(dataReader.GetOrdinal(fieldName)))
                return default;

            return (T)Convert.ChangeType(dataReader[fieldName].ToString(), Type.GetType(Nullable.GetUnderlyingType(typeof(T))?.FullName ?? typeof(T).FullName));
        }
    }

    public class SqlServerRepository : ISqlServerRepository
    {
        #region [ Campos ]

        public SqlConnection SqlServerConnection { get; set; }

        protected SqlServerDbContext DbContext { get; }

        #endregion

        #region [ Construtores ]

        public SqlServerRepository(SqlServerDbContext sqlServerDbContext)
        {
            SqlServerConnection = sqlServerDbContext.Connection;
            DbContext = sqlServerDbContext;
        }

        public async ValueTask<IDbTransaction> GetTransactionAsync()
        {
            if (SqlServerConnection.State == ConnectionState.Closed)
                await SqlServerConnection.OpenAsync();

            return await SqlServerConnection.BeginTransactionAsync();
        }

        public async Task<int> ExecuteScalarAsync(string commandText, IEntity entity)
        {
            return await SqlServerConnection.ExecuteScalarAsync<int>(commandText, entity, DbContext.Transaction);
        }

        public int ExecuteNonQuery(string commandText, IEntity? entity = null, CommandType commandType = CommandType.Text)
        {
            if (SqlServerConnection.State == ConnectionState.Closed)
                SqlServerConnection.Open();

            return SqlServerConnection.Execute(commandText, entity, DbContext.Transaction, commandType: commandType);
        }

        public async Task<int> ExecuteNonQueryAsync(string commandText, IEntity? entity, CommandType commandType = CommandType.Text)
        {
            if (SqlServerConnection.State == ConnectionState.Closed)
                await SqlServerConnection.OpenAsync();

            return await SqlServerConnection.ExecuteAsync(commandText, entity, DbContext.Transaction, commandType: commandType);
        }

        public SqlDataReader ExecuteReader(string commandText, IEnumerable<SqlParameter> parameters, CommandType commandType = CommandType.Text)
        {
            if (SqlServerConnection.State == ConnectionState.Closed)
                SqlServerConnection.Open();

            var cmd = new SqlCommand(commandText, SqlServerConnection, (SqlTransaction?)DbContext.Transaction) 
            { 
                CommandType = commandType 
            };
            
            cmd.Parameters.AddRange(parameters.ToArray());
            return cmd.ExecuteReader();
        }

        public int ExecuteNonQuery(string commandText, DynamicParameters? parameters = null, CommandType commandType = CommandType.Text)
        {
            if (SqlServerConnection.State == ConnectionState.Closed)
                SqlServerConnection.Open();

            return SqlServerConnection.Execute(commandText, parameters, DbContext.Transaction, commandType: commandType);
        }

        public async Task<int> ExecuteNonQueryAsync(string commandText, DynamicParameters? parameters = null, CommandType commandType = CommandType.Text)
        {
            if (SqlServerConnection.State == ConnectionState.Closed)
                await SqlServerConnection.OpenAsync();

            return await SqlServerConnection.ExecuteAsync(commandText, parameters, DbContext.Transaction, commandType: commandType);
        }

        public IEnumerable<TEntity> Query<TEntity>(string commandText, IEntity? entity, CommandType commandType = CommandType.Text)
        {
            if (SqlServerConnection.State == ConnectionState.Closed)
                SqlServerConnection.Open();

            return SqlServerConnection.Query<TEntity>(commandText, entity, DbContext.Transaction, commandType: commandType);
        }

        public async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string commandText, IEntity? entity, CommandType commandType = CommandType.Text)
        {
            if (SqlServerConnection.State == ConnectionState.Closed)
                await SqlServerConnection.OpenAsync();

            return await SqlServerConnection.QueryAsync<TEntity>(commandText, entity, DbContext.Transaction, commandType: commandType);
        }

        public IEnumerable<TEntity> Query<TEntity>(string commandText, DynamicParameters? parameters = null, CommandType commandType = CommandType.Text)
        {
            if (SqlServerConnection.State == ConnectionState.Closed)
                SqlServerConnection.Open();

            return SqlServerConnection.Query<TEntity>(commandText, parameters, DbContext.Transaction, commandType: commandType);
        }

        public async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string commandText, DynamicParameters? parameters = null, CommandType commandType = CommandType.Text)
        {
            if (SqlServerConnection.State == ConnectionState.Closed)
                await SqlServerConnection.OpenAsync();

            return await SqlServerConnection.QueryAsync<TEntity>(commandText, parameters, DbContext.Transaction, commandType: commandType);
        }

        public TEntity? QueryFirstOrDefault<TEntity>(string commandText, IEntity? entity, CommandType commandType = CommandType.Text)
        {
            if (SqlServerConnection.State == ConnectionState.Closed)
                SqlServerConnection.Open();

            return SqlServerConnection.QueryFirstOrDefault<TEntity>(commandText, entity, DbContext.Transaction, commandType: commandType);
        }

        public async Task<TEntity?> QueryFirstOrDefaultAsync<TEntity>(string commandText, IEntity? entity, CommandType commandType = CommandType.Text)
        {
            if (SqlServerConnection.State == ConnectionState.Closed)
                await SqlServerConnection.OpenAsync();

            return await SqlServerConnection.QueryFirstOrDefaultAsync<TEntity>(commandText, entity, DbContext.Transaction, commandType: commandType);
        }

        #endregion
    }
}
