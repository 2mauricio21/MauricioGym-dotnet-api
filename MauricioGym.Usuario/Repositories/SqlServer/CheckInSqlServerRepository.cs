using Dapper;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.Interfaces;
using System.Data.SqlClient;

namespace MauricioGym.Usuario.Repositories.SqlServer
{
    public class CheckInSqlServerRepository : ICheckInSqlServerRepository
    {
        private readonly string _connectionString;

        public CheckInSqlServerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<CheckInEntity>> ObterTodosAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT Id, UsuarioId, DataHora, Observacoes, Ativo, DataCriacao, DataAtualizacao 
                FROM CheckIn 
                WHERE Ativo = 1
                ORDER BY DataHora DESC";
            
            return await connection.QueryAsync<CheckInEntity>(sql);
        }

        public async Task<CheckInEntity?> ObterPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT Id, UsuarioId, DataHora, Observacoes, Ativo, DataCriacao, DataAtualizacao 
                FROM CheckIn 
                WHERE Id = @Id AND Ativo = 1";
            
            return await connection.QueryFirstOrDefaultAsync<CheckInEntity>(sql, new { Id = id });
        }

        public async Task<IEnumerable<CheckInEntity>> ObterPorUsuarioAsync(int usuarioId)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT Id, UsuarioId, DataHora, Observacoes, Ativo, DataCriacao, DataAtualizacao 
                FROM CheckIn 
                WHERE UsuarioId = @UsuarioId AND Ativo = 1
                ORDER BY DataHora DESC";
            
            return await connection.QueryAsync<CheckInEntity>(sql, new { UsuarioId = usuarioId });
        }

        public async Task<IEnumerable<CheckInEntity>> ObterPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT Id, UsuarioId, DataHora, Observacoes, Ativo, DataCriacao, DataAtualizacao 
                FROM CheckIn 
                WHERE DataHora >= @DataInicio AND DataHora <= @DataFim AND Ativo = 1
                ORDER BY DataHora DESC";
            
            return await connection.QueryAsync<CheckInEntity>(sql, new { DataInicio = dataInicio, DataFim = dataFim });
        }

        public async Task<int> CriarAsync(CheckInEntity checkIn)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                INSERT INTO CheckIn (UsuarioId, DataHora, Observacoes, Ativo, DataCriacao)
                VALUES (@UsuarioId, @DataHora, @Observacoes, @Ativo, GETDATE());
                SELECT CAST(SCOPE_IDENTITY() as int)";
            
            return await connection.QuerySingleAsync<int>(sql, checkIn);
        }

        public async Task<bool> AtualizarAsync(CheckInEntity checkIn)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                UPDATE CheckIn 
                SET UsuarioId = @UsuarioId, DataHora = @DataHora, Observacoes = @Observacoes, DataAtualizacao = GETDATE() 
                WHERE Id = @Id AND Ativo = 1";
            
            var rowsAffected = await connection.ExecuteAsync(sql, checkIn);
            return rowsAffected > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = "UPDATE CheckIn SET Ativo = 0, DataAtualizacao = GETDATE() WHERE Id = @Id";
            
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT COUNT(1) FROM CheckIn WHERE Id = @Id AND Ativo = 1";
            
            var count = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });
            return count > 0;
        }

        public async Task<int> ContarCheckInsPorUsuarioMesAsync(int usuarioId, int ano, int mes)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT COUNT(1) 
                FROM CheckIn 
                WHERE UsuarioId = @UsuarioId 
                  AND YEAR(DataHora) = @Ano 
                  AND MONTH(DataHora) = @Mes 
                  AND Ativo = 1";
            
            return await connection.ExecuteScalarAsync<int>(sql, new { UsuarioId = usuarioId, Ano = ano, Mes = mes });
        }
    }
}
