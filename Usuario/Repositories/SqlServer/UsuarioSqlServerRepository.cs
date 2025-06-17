using Dapper;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.Interfaces;
using System.Data.SqlClient;

namespace MauricioGym.Usuario.Repositories.SqlServer
{
    public class UsuarioSqlServerRepository : IUsuarioSqlServerRepository
    {
        private readonly string _connectionString;

        public UsuarioSqlServerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }        public async Task<IEnumerable<UsuarioEntity>> ObterTodosAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                SELECT Id, Nome, Email, DataNascimento, Telefone, Ativo, DataCriacao, DataAtualizacao
                FROM Usuario 
                WHERE Ativo = 1 
                ORDER BY Nome";
            
            return await connection.QueryAsync<UsuarioEntity>(query);
        }

        public async Task<UsuarioEntity?> ObterPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                SELECT Id, Nome, Email, DataNascimento, Telefone, Ativo, DataCriacao, DataAtualizacao
                FROM Usuario 
                WHERE Id = @Id AND Ativo = 1";
            
            return await connection.QueryFirstOrDefaultAsync<UsuarioEntity>(query, new { Id = id });
        }

        public async Task<UsuarioEntity?> ObterPorEmailAsync(string email)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                SELECT Id, Nome, Email, DataNascimento, Telefone, Ativo, DataCriacao, DataAtualizacao
                FROM Usuario 
                WHERE Email = @Email AND Ativo = 1";
            
            return await connection.QueryFirstOrDefaultAsync<UsuarioEntity>(query, new { Email = email });
        }        public async Task<int> CriarAsync(UsuarioEntity usuario)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                INSERT INTO Usuario (Nome, Email, DataNascimento, Telefone, Ativo, DataCriacao)
                VALUES (@Nome, @Email, @DataNascimento, @Telefone, @Ativo, GETDATE());
                SELECT CAST(SCOPE_IDENTITY() as int)";
            
            return await connection.QuerySingleAsync<int>(query, usuario);
        }

        public async Task<bool> AtualizarAsync(UsuarioEntity usuario)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                UPDATE Usuario 
                SET Nome = @Nome, Email = @Email, DataNascimento = @DataNascimento, 
                    Telefone = @Telefone, DataAtualizacao = GETDATE()
                WHERE Id = @Id AND Ativo = 1";
            
            var linhasAfetadas = await connection.ExecuteAsync(query, usuario);
            return linhasAfetadas > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "UPDATE Usuario SET Ativo = 0, DataAtualizacao = GETDATE() WHERE Id = @Id";
            
            var linhasAfetadas = await connection.ExecuteAsync(query, new { Id = id });
            return linhasAfetadas > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT COUNT(1) FROM Usuario WHERE Id = @Id AND Ativo = 1";
            
            var count = await connection.ExecuteScalarAsync<int>(query, new { Id = id });
            return count > 0;
        }

        public async Task<bool> ExisteEmailAsync(string email, int? excludeId = null)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT COUNT(1) FROM Usuario WHERE Email = @Email AND Ativo = 1";
            
            if (excludeId.HasValue)
            {
                query += " AND Id != @ExcludeId";
                var count = await connection.ExecuteScalarAsync<int>(query, new { Email = email, ExcludeId = excludeId.Value });
                return count > 0;
            }
            
            var countWithoutExclude = await connection.ExecuteScalarAsync<int>(query, new { Email = email });
            return countWithoutExclude > 0;
        }
    }
}
