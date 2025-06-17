using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer.Queries;
using System.Data.SqlClient;

namespace MauricioGym.Administrador.Repositories.SqlServer
{
    public class UsuarioSqlServerRepository : IUsuarioSqlServerRepository
    {
        private readonly string _connectionString;

        public UsuarioSqlServerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<UsuarioEntity> ObterPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<UsuarioEntity>(UsuarioSqlServerQuery.ObterPorId, new { Id = id });
        }

        public async Task<IEnumerable<UsuarioEntity>> ListarAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<UsuarioEntity>(UsuarioSqlServerQuery.Listar);
        }

        public async Task<int> CriarAsync(UsuarioEntity usuario)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(UsuarioSqlServerQuery.InserirUsuario, usuario);
        }

        public async Task<bool> AtualizarAsync(UsuarioEntity usuario)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(UsuarioSqlServerQuery.Atualizar, usuario);
            return result > 0;
        }

        public async Task<bool> RemoverLogicamenteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(UsuarioSqlServerQuery.RemoverLogico, new { Id = id });
            return result > 0;
        }
    }
}
