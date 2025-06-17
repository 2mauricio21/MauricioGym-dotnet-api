using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer.Queries;
using System.Data.SqlClient;

namespace MauricioGym.Administrador.Repositories.SqlServer
{
    public class PermissaoManipulacaoUsuarioSqlServerRepository : IPermissaoManipulacaoUsuarioSqlServerRepository
    {
        private readonly string _connectionString;

        public PermissaoManipulacaoUsuarioSqlServerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<PermissaoManipulacaoUsuarioEntity> ObterPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<PermissaoManipulacaoUsuarioEntity>(PermissaoManipulacaoUsuarioSqlServerQuery.ObterPorId, new { Id = id });
        }

        public async Task<IEnumerable<PermissaoManipulacaoUsuarioEntity>> ListarPorUsuarioAsync(int usuarioId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<PermissaoManipulacaoUsuarioEntity>(PermissaoManipulacaoUsuarioSqlServerQuery.ListarPorUsuario, new { UsuarioId = usuarioId });
        }

        public async Task<int> CriarAsync(PermissaoManipulacaoUsuarioEntity permissao)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(PermissaoManipulacaoUsuarioSqlServerQuery.InserirPermissao, permissao);
        }

        public async Task<bool> AtualizarAsync(PermissaoManipulacaoUsuarioEntity permissao)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(PermissaoManipulacaoUsuarioSqlServerQuery.Atualizar, permissao);
            return result > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(PermissaoManipulacaoUsuarioSqlServerQuery.Remover, new { Id = id });
            return result > 0;
        }
    }
}
