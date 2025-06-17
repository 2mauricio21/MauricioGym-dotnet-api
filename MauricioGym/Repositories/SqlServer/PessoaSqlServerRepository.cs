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
    public class PessoaSqlServerRepository : IPessoaSqlServerRepository
    {
        private readonly string _connectionString;

        public PessoaSqlServerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<PessoaEntity> ObterPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<PessoaEntity>(PessoaSqlServerQuery.ObterPorId, new { Id = id });
        }

        public async Task<IEnumerable<PessoaEntity>> ListarAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<PessoaEntity>(PessoaSqlServerQuery.Listar);
        }

        public async Task<int> CriarAsync(PessoaEntity pessoa)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(PessoaSqlServerQuery.InserirPessoa, pessoa);
        }

        public async Task<bool> AtualizarAsync(PessoaEntity pessoa)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(PessoaSqlServerQuery.Atualizar, pessoa);
            return result > 0;
        }

        public async Task<bool> RemoverLogicamenteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(PessoaSqlServerQuery.RemoverLogico, new { Id = id });
            return result > 0;
        }
    }
}
