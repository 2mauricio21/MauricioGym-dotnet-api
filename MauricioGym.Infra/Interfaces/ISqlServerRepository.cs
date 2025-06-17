using Dapper;
using MauricioGym.Infra.Entities.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MauricioGym.Infra.Interfaces
{
    public interface ISqlServerRepository
    {
        SqlConnection SqlServerConnection { get; set; }

        ValueTask<IDbTransaction> GetTransactionAsync();

        Task<int> ExecuteScalarAsync(string commandText, IEntity entity);

        int ExecuteNonQuery(string commandText, IEntity? entity = null, CommandType commandType = CommandType.Text);

        Task<int> ExecuteNonQueryAsync(string commandText, IEntity? entity, CommandType commandType = CommandType.Text);

        SqlDataReader ExecuteReader(string commandText, IEnumerable<SqlParameter> parameters, CommandType commandType = CommandType.Text);

        int ExecuteNonQuery(string commandText, DynamicParameters? parameters = null, CommandType commandType = CommandType.Text);

        Task<int> ExecuteNonQueryAsync(string commandText, DynamicParameters? parameters = null, CommandType commandType = CommandType.Text);

        IEnumerable<TEntity> Query<TEntity>(string commandText, IEntity? entity, CommandType commandType = CommandType.Text);

        Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string commandText, IEntity? entity, CommandType commandType = CommandType.Text);

        IEnumerable<TEntity> Query<TEntity>(string commandText, DynamicParameters? parameters = null, CommandType commandType = CommandType.Text);

        Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string commandText, DynamicParameters? parameters = null, CommandType commandType = CommandType.Text);

        TEntity? QueryFirstOrDefault<TEntity>(string commandText, IEntity? entity, CommandType commandType = CommandType.Text);

        Task<TEntity?> QueryFirstOrDefaultAsync<TEntity>(string commandText, IEntity? entity, CommandType commandType = CommandType.Text);
    }
}
