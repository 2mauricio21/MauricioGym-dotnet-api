using Dapper;
using MauricioGym.Infra.Entities.Interfaces;
using System.Data;
using static Dapper.SqlMapper;

namespace MauricioGym.Infra.Repositories.Interfaces
{
    public interface ISqlServerRepository : IDisposable
    {
        int ExecuteNonQuery(string commandText, IEntity entity, CommandType commandType = CommandType.Text);

        IEnumerable<TEntity> Query<TEntity>(string commandText, IEntity entity, CommandType commandType);

        IEnumerable<TEntity> Query<TEntity>(string commandText, CommandType commandType = CommandType.Text);

        TEntity QueryFirst<TEntity>(string commandText, IEntity entity, CommandType commandType = CommandType.Text);

        TEntity QueryFirst<TEntity>(string commandText, DynamicParameters parameters, CommandType commandType = CommandType.Text);

        TEntity QueryFirst<TEntity>(string commandText, CommandType commandType = CommandType.Text);

        GridReader QueryMultiple(string commandText, IEntity entity, CommandType commandType = CommandType.Text);

        IEnumerable<TEntity> Query<TEntity>(string commandText, DynamicParameters parameters, CommandType commandType = CommandType.Text);
    }
}
