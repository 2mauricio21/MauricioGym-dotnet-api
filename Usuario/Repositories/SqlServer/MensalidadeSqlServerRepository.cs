using Dapper;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.Interfaces;
using System.Data.SqlClient;

namespace MauricioGym.Usuario.Repositories.SqlServer
{
    public class MensalidadeSqlServerRepository : IMensalidadeSqlServerRepository
    {
        private readonly string _connectionString;

        public MensalidadeSqlServerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<MensalidadeEntity>> ObterTodosAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT Id, UsuarioId, PlanoId, DataVencimento, DataPagamento, Valor, Desconto, Pago, Removido 
                FROM Mensalidade 
                WHERE Removido = 0
                ORDER BY DataVencimento DESC";
            
            return await connection.QueryAsync<MensalidadeEntity>(sql);
        }

        public async Task<MensalidadeEntity?> ObterPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT Id, UsuarioId, PlanoId, DataVencimento, DataPagamento, Valor, Desconto, Pago, Removido 
                FROM Mensalidade 
                WHERE Id = @Id AND Removido = 0";
            
            return await connection.QueryFirstOrDefaultAsync<MensalidadeEntity>(sql, new { Id = id });
        }

        public async Task<IEnumerable<MensalidadeEntity>> ObterPorUsuarioAsync(int usuarioId)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT Id, UsuarioId, PlanoId, DataVencimento, DataPagamento, Valor, Desconto, Pago, Removido 
                FROM Mensalidade 
                WHERE UsuarioId = @UsuarioId AND Removido = 0
                ORDER BY DataVencimento DESC";
            
            return await connection.QueryAsync<MensalidadeEntity>(sql, new { UsuarioId = usuarioId });
        }

        public async Task<IEnumerable<MensalidadeEntity>> ObterPendentesAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT Id, UsuarioId, PlanoId, DataVencimento, DataPagamento, Valor, Desconto, Pago, Removido 
                FROM Mensalidade 
                WHERE Pago = 0 AND Removido = 0
                ORDER BY DataVencimento ASC";
            
            return await connection.QueryAsync<MensalidadeEntity>(sql);
        }

        public async Task<IEnumerable<MensalidadeEntity>> ObterVencendasAsync(int dias)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT Id, UsuarioId, PlanoId, DataVencimento, DataPagamento, Valor, Desconto, Pago, Removido 
                FROM Mensalidade 
                WHERE DataVencimento <= DATEADD(day, @Dias, GETDATE()) 
                  AND Pago = 0 AND Removido = 0
                ORDER BY DataVencimento ASC";
            
            return await connection.QueryAsync<MensalidadeEntity>(sql, new { Dias = dias });
        }

        public async Task<bool> EstaEmDiaAsync(int usuarioId)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT COUNT(1) 
                FROM Mensalidade 
                WHERE UsuarioId = @UsuarioId 
                  AND DataVencimento >= GETDATE() 
                  AND Pago = 1 
                  AND Removido = 0";
            
            var count = await connection.ExecuteScalarAsync<int>(sql, new { UsuarioId = usuarioId });
            return count > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = "UPDATE Mensalidade SET Removido = 1 WHERE Id = @Id";
            
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT COUNT(1) FROM Mensalidade WHERE Id = @Id AND Removido = 0";
            
            var count = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });
            return count > 0;
        }

        public async Task<int> CriarAsync(MensalidadeEntity mensalidade)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                INSERT INTO Mensalidade (UsuarioId, PlanoId, DataVencimento, DataPagamento, Valor, Desconto, Pago, Removido)
                VALUES (@UsuarioId, @PlanoId, @DataVencimento, @DataPagamento, @Valor, @Desconto, @Pago, @Removido);
                SELECT CAST(SCOPE_IDENTITY() as int)";
            
            return await connection.QuerySingleAsync<int>(sql, mensalidade);
        }

        public async Task<bool> AtualizarAsync(MensalidadeEntity mensalidade)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                UPDATE Mensalidade 
                SET UsuarioId = @UsuarioId, PlanoId = @PlanoId, DataVencimento = @DataVencimento, 
                    DataPagamento = @DataPagamento, Valor = @Valor, Desconto = @Desconto, 
                    Pago = @Pago, Removido = @Removido 
                WHERE Id = @Id";
            
            var rowsAffected = await connection.ExecuteAsync(sql, mensalidade);
            return rowsAffected > 0;
        }

        public async Task<MensalidadeEntity?> ObterMensalidadeAtualAsync(int usuarioId)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT TOP 1 Id, UsuarioId, PlanoId, DataVencimento, DataPagamento, Valor, Desconto, Pago, Removido 
                FROM Mensalidade 
                WHERE UsuarioId = @UsuarioId 
                  AND DataVencimento >= GETDATE() 
                  AND Removido = 0
                ORDER BY DataVencimento ASC";
            
            return await connection.QueryFirstOrDefaultAsync<MensalidadeEntity>(sql, new { UsuarioId = usuarioId });
        }
    }
}
