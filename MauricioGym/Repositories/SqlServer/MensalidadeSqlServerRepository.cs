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
    public class MensalidadeSqlServerRepository : IMensalidadeSqlServerRepository
    {
        private readonly string _connectionString;

        public MensalidadeSqlServerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<MensalidadeEntity> ObterPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<MensalidadeEntity>(MensalidadeSqlServerQuery.ObterPorId, new { Id = id });
        }

        public async Task<IEnumerable<MensalidadeEntity>> ListarPorPessoaAsync(int pessoaId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<MensalidadeEntity>(MensalidadeSqlServerQuery.ListarPorPessoa, new { PessoaId = pessoaId });
        }

        public async Task<int> CriarAsync(MensalidadeEntity mensalidade)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(MensalidadeSqlServerQuery.InserirMensalidade, mensalidade);
        }

        public async Task<decimal> ObterTotalRecebidoAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<decimal>(MensalidadeSqlServerQuery.ObterTotalRecebido);
        }
    }
}
