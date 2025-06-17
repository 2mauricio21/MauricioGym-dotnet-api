using Dapper;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.Interfaces;
using MauricioGym.Usuario.Repositories.SqlServer.Queries;
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
            return await connection.QueryAsync<UsuarioEntity>(UsuarioSqlServerQuery.ObterTodos);
        }        public async Task<UsuarioEntity?> ObterPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<UsuarioEntity>(UsuarioSqlServerQuery.ObterPorId, new { Id = id });
        }        public async Task<UsuarioEntity?> ObterPorEmailAsync(string email)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<UsuarioEntity>(UsuarioSqlServerQuery.ObterPorEmail, new { Email = email });
        }        public async Task<int> CriarAsync(UsuarioEntity usuario)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleAsync<int>(UsuarioSqlServerQuery.Criar, usuario);
        }        public async Task<bool> AtualizarAsync(UsuarioEntity usuario)
        {
            using var connection = new SqlConnection(_connectionString);
            var linhasAfetadas = await connection.ExecuteAsync(UsuarioSqlServerQuery.Atualizar, usuario);
            return linhasAfetadas > 0;
        }        public async Task<bool> RemoverAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var linhasAfetadas = await connection.ExecuteAsync(UsuarioSqlServerQuery.Remover, new { Id = id });
            return linhasAfetadas > 0;
        }        public async Task<bool> ExisteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var count = await connection.ExecuteScalarAsync<int>(UsuarioSqlServerQuery.Existe, new { Id = id });
            return count > 0;
        }        public async Task<bool> ExisteEmailAsync(string email, int? excludeId = null)
        {
            using var connection = new SqlConnection(_connectionString);
            if (excludeId.HasValue)
            {
                var count = await connection.ExecuteScalarAsync<int>(UsuarioSqlServerQuery.ExisteEmailExcluindoId, new { Email = email, ExcludeId = excludeId.Value });
                return count > 0;
            }
            
            var countWithoutExclude = await connection.ExecuteScalarAsync<int>(UsuarioSqlServerQuery.ExisteEmail, new { Email = email });
            return countWithoutExclude > 0;
        }
    }
}
