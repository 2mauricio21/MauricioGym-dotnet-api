using Dapper;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.Interfaces;
using System.Data.SqlClient;

namespace MauricioGym.Usuario.Repositories.SqlServer
{
    public class PlanoSqlServerRepository : IPlanoSqlServerRepository
    {
        private readonly string _connectionString;

        public PlanoSqlServerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<PlanoEntity>> ObterTodosAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                SELECT Id, Nome, Descricao, Valor, DuracaoMeses, Removido
                FROM Plano 
                WHERE Removido = 0 
                ORDER BY Nome";
            
            return await connection.QueryAsync<PlanoEntity>(query);
        }

        public async Task<PlanoEntity?> ObterPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                SELECT Id, Nome, Descricao, Valor, DuracaoMeses, Removido
                FROM Plano 
                WHERE Id = @Id AND Removido = 0";
            
            return await connection.QueryFirstOrDefaultAsync<PlanoEntity>(query, new { Id = id });
        }

        public async Task<bool> ExisteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT COUNT(1) FROM Plano WHERE Id = @Id AND Removido = 0";
            
            var count = await connection.ExecuteScalarAsync<int>(query, new { Id = id });
            return count > 0;
        }
    }
}
