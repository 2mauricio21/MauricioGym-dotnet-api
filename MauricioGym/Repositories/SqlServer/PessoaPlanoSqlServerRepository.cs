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
    public class PessoaPlanoSqlServerRepository : IPessoaPlanoSqlServerRepository
    {
        private readonly string _connectionString;

        public PessoaPlanoSqlServerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<PessoaPlanoEntity> ObterPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<PessoaPlanoEntity>(PessoaPlanoSqlServerQuery.ObterPorId, new { Id = id });
        }

        public async Task<IEnumerable<PessoaPlanoEntity>> ListarPorPessoaAsync(int pessoaId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<PessoaPlanoEntity>(PessoaPlanoSqlServerQuery.ListarPorPessoa, new { PessoaId = pessoaId });
        }

        public async Task<int> CriarAsync(PessoaPlanoEntity pessoaPlano)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(PessoaPlanoSqlServerQuery.InserirPessoaPlano, pessoaPlano);
        }

        public async Task<bool> AtualizarAsync(PessoaPlanoEntity pessoaPlano)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(PessoaPlanoSqlServerQuery.Atualizar, pessoaPlano);
            return result > 0;
        }

        public async Task<bool> RemoverLogicamenteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(PessoaPlanoSqlServerQuery.RemoverLogico, new { Id = id });
            return result > 0;
        }
    }
}
