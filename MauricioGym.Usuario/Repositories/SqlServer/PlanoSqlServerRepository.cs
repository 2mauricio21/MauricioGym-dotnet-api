using Dapper;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.Interfaces;
using MauricioGym.Usuario.Repositories.SqlServer.Queries;
using System.Data.SqlClient;

namespace MauricioGym.Usuario.Repositories.SqlServer
{
    public class PlanoSqlServerRepository : IPlanoSqlServerRepository
    {
        private readonly string _connectionString;

        public PlanoSqlServerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }        public async Task<IEnumerable<PlanoEntity>> ObterTodosAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<PlanoEntity>(PlanoSqlServerQuery.ObterTodos);
        }

        public async Task<PlanoEntity?> ObterPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<PlanoEntity>(PlanoSqlServerQuery.ObterPorId, new { Id = id });
        }

        public async Task<bool> ExisteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var count = await connection.ExecuteScalarAsync<int>(PlanoSqlServerQuery.Existe, new { Id = id });
            return count > 0;
        }
    }
}
