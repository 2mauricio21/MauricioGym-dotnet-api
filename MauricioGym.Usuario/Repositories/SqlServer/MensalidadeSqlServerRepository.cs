using Dapper;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.Interfaces;
using MauricioGym.Usuario.Repositories.SqlServer.Queries;
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
            return await connection.QueryAsync<MensalidadeEntity>(MensalidadeSqlServerQuery.ObterTodos);
        }

        public async Task<MensalidadeEntity?> ObterPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<MensalidadeEntity>(MensalidadeSqlServerQuery.ObterPorId, new { Id = id });
        }

        public async Task<IEnumerable<MensalidadeEntity>> ObterPorUsuarioAsync(int usuarioId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<MensalidadeEntity>(MensalidadeSqlServerQuery.ObterPorUsuario, new { UsuarioId = usuarioId });
        }

        public async Task<IEnumerable<MensalidadeEntity>> ObterPendentesAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<MensalidadeEntity>(MensalidadeSqlServerQuery.ObterPendentes);
        }

        public async Task<IEnumerable<MensalidadeEntity>> ObterVencendasAsync(int dias)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<MensalidadeEntity>(MensalidadeSqlServerQuery.ObterVencendas, new { Dias = dias });
        }        public async Task<bool> EstaEmDiaAsync(int usuarioId)
        {
            using var connection = new SqlConnection(_connectionString);
            var count = await connection.ExecuteScalarAsync<int>(MensalidadeSqlServerQuery.EstaEmDia, new { UsuarioId = usuarioId });
            return count > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var rowsAffected = await connection.ExecuteAsync(MensalidadeSqlServerQuery.Remover, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var count = await connection.ExecuteScalarAsync<int>(MensalidadeSqlServerQuery.Existe, new { Id = id });
            return count > 0;
        }

        public async Task<int> CriarAsync(MensalidadeEntity mensalidade)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleAsync<int>(MensalidadeSqlServerQuery.Criar, mensalidade);
        }

        public async Task<bool> AtualizarAsync(MensalidadeEntity mensalidade)
        {
            using var connection = new SqlConnection(_connectionString);
            var rowsAffected = await connection.ExecuteAsync(MensalidadeSqlServerQuery.Atualizar, mensalidade);
            return rowsAffected > 0;
        }        public async Task<MensalidadeEntity?> ObterMensalidadeAtualAsync(int usuarioId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<MensalidadeEntity>(MensalidadeSqlServerQuery.ObterMensalidadeAtual, new { UsuarioId = usuarioId });
        }
    }
}
