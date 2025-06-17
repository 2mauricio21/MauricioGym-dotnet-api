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
                SELECT Id, UsuarioPlanoId, MesReferencia, AnoReferencia, Valor, DataVencimento, DataPagamento, Status, Ativo, DataCriacao, DataAtualizacao 
                FROM Mensalidade 
                WHERE Ativo = 1
                ORDER BY DataVencimento DESC";
            
            return await connection.QueryAsync<MensalidadeEntity>(sql);
        }

        public async Task<MensalidadeEntity?> ObterPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT Id, UsuarioPlanoId, MesReferencia, AnoReferencia, Valor, DataVencimento, DataPagamento, Status, Ativo, DataCriacao, DataAtualizacao 
                FROM Mensalidade 
                WHERE Id = @Id AND Ativo = 1";
            
            return await connection.QueryFirstOrDefaultAsync<MensalidadeEntity>(sql, new { Id = id });
        }

        public async Task<IEnumerable<MensalidadeEntity>> ObterPorUsuarioAsync(int usuarioId)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT m.Id, m.UsuarioPlanoId, m.MesReferencia, m.AnoReferencia, m.Valor, m.DataVencimento, 
                       m.DataPagamento, m.Status, m.Ativo, m.DataCriacao, m.DataAtualizacao 
                FROM Mensalidade m
                INNER JOIN UsuarioPlano up ON m.UsuarioPlanoId = up.Id
                WHERE up.UsuarioId = @UsuarioId AND m.Ativo = 1
                ORDER BY m.DataVencimento DESC";
            
            return await connection.QueryAsync<MensalidadeEntity>(sql, new { UsuarioId = usuarioId });
        }

        public async Task<IEnumerable<MensalidadeEntity>> ObterPendentesAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT Id, UsuarioPlanoId, MesReferencia, AnoReferencia, Valor, DataVencimento, 
                       DataPagamento, Status, Ativo, DataCriacao, DataAtualizacao 
                FROM Mensalidade 
                WHERE Status = 'Pendente' AND Ativo = 1
                ORDER BY DataVencimento ASC";
            
            return await connection.QueryAsync<MensalidadeEntity>(sql);
        }

        public async Task<IEnumerable<MensalidadeEntity>> ObterVencendasAsync(int dias)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT Id, UsuarioPlanoId, MesReferencia, AnoReferencia, Valor, DataVencimento, 
                       DataPagamento, Status, Ativo, DataCriacao, DataAtualizacao 
                FROM Mensalidade 
                WHERE DataVencimento <= DATEADD(day, @Dias, GETDATE()) 
                  AND Status = 'Pendente' AND Ativo = 1
                ORDER BY DataVencimento ASC";
            
            return await connection.QueryAsync<MensalidadeEntity>(sql, new { Dias = dias });
        }

        public async Task<bool> EstaEmDiaAsync(int usuarioId)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT COUNT(1) 
                FROM Mensalidade m
                INNER JOIN UsuarioPlano up ON m.UsuarioPlanoId = up.Id
                WHERE up.UsuarioId = @UsuarioId 
                  AND m.DataVencimento >= GETDATE() 
                  AND m.Status = 'Paga' 
                  AND m.Ativo = 1";
            
            var count = await connection.ExecuteScalarAsync<int>(sql, new { UsuarioId = usuarioId });
            return count > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = "UPDATE Mensalidade SET Ativo = 0, DataAtualizacao = GETDATE() WHERE Id = @Id";
            
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT COUNT(1) FROM Mensalidade WHERE Id = @Id AND Ativo = 1";
            
            var count = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });
            return count > 0;
        }

        public async Task<int> CriarAsync(MensalidadeEntity mensalidade)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                INSERT INTO Mensalidade (UsuarioPlanoId, MesReferencia, AnoReferencia, Valor, DataVencimento, DataPagamento, Status, Ativo, DataCriacao)
                VALUES (@UsuarioPlanoId, @MesReferencia, @AnoReferencia, @Valor, @DataVencimento, @DataPagamento, @Status, @Ativo, GETDATE());
                SELECT CAST(SCOPE_IDENTITY() as int)";
            
            return await connection.QuerySingleAsync<int>(sql, mensalidade);
        }

        public async Task<bool> AtualizarAsync(MensalidadeEntity mensalidade)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                UPDATE Mensalidade 
                SET UsuarioPlanoId = @UsuarioPlanoId, 
                    MesReferencia = @MesReferencia,
                    AnoReferencia = @AnoReferencia,
                    DataVencimento = @DataVencimento, 
                    DataPagamento = @DataPagamento, 
                    Valor = @Valor,
                    Status = @Status,
                    DataAtualizacao = GETDATE()
                WHERE Id = @Id AND Ativo = 1";
            
            var rowsAffected = await connection.ExecuteAsync(sql, mensalidade);
            return rowsAffected > 0;
        }

        public async Task<MensalidadeEntity?> ObterMensalidadeAtualAsync(int usuarioId)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT TOP 1 m.Id, m.UsuarioPlanoId, m.MesReferencia, m.AnoReferencia, m.Valor, m.DataVencimento, 
                       m.DataPagamento, m.Status, m.Ativo, m.DataCriacao, m.DataAtualizacao 
                FROM Mensalidade m
                INNER JOIN UsuarioPlano up ON m.UsuarioPlanoId = up.Id
                WHERE up.UsuarioId = @UsuarioId 
                  AND m.DataVencimento >= GETDATE() 
                  AND m.Ativo = 1
                ORDER BY m.DataVencimento ASC";              return await connection.QueryFirstOrDefaultAsync<MensalidadeEntity>(sql, new { UsuarioId = usuarioId });
        }
    }
}
