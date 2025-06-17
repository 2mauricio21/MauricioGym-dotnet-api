using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer.Queries;
using System.Data.SqlClient;

namespace MauricioGym.Administrador.Repositories.SqlServer
{
    public class UsuarioPlanoSqlServerRepository : IUsuarioPlanoSqlServerRepository
    {
        private readonly string _connectionString;

        public UsuarioPlanoSqlServerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<UsuarioPlanoEntity> ObterPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<UsuarioPlanoEntity>(UsuarioPlanoSqlServerQuery.ObterPorId, new { Id = id });
        }

        public async Task<IEnumerable<UsuarioPlanoEntity>> ListarPorUsuarioAsync(int usuarioId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<UsuarioPlanoEntity>(UsuarioPlanoSqlServerQuery.ListarPorUsuario, new { UsuarioId = usuarioId });
        }

        public async Task<int> CriarAsync(UsuarioPlanoEntity usuarioPlano)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(UsuarioPlanoSqlServerQuery.InserirUsuarioPlano, usuarioPlano);
        }

        public async Task<bool> AtualizarAsync(UsuarioPlanoEntity usuarioPlano)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(UsuarioPlanoSqlServerQuery.Atualizar, usuarioPlano);
            return result > 0;
        }

        public async Task<bool> RemoverLogicamenteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(UsuarioPlanoSqlServerQuery.RemoverLogico, new { Id = id });
            return result > 0;
        }
    }
}
