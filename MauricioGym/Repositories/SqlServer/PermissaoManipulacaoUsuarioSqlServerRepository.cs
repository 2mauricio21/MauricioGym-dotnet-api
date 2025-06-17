using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MauricioGym.Entities;
using MauricioGym.Repositories.SqlServer.Interfaces;
using MauricioGym.Repositories.SqlServer.Queries;
using System.Data.SqlClient;

namespace MauricioGym.Repositories.SqlServer
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

        public async Task<IEnumerable<PermissaoManipulacaoUsuarioEntity>> ListarPorPessoaAsync(int pessoaId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<PermissaoManipulacaoUsuarioEntity>(PermissaoManipulacaoUsuarioSqlServerQuery.ListarPorPessoa, new { PessoaId = pessoaId });
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
