using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MauricioGym.Entities;
using MauricioGym.Repositories.SqlServer.Interfaces;
using MauricioGym.Repositories.SqlServer.Queries;
using System.Data.SqlClient;

namespace MauricioGym.Repositories.SqlServer
{
    public class PlanoSqlServerRepository : IPlanoSqlServerRepository
    {
        private readonly string _connectionString;

        public PlanoSqlServerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CriarAsync(PlanoEntity plano)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(PlanoSqlServerQuery.InserirPlano, plano);
        }

        public async Task<PlanoEntity> ObterPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<PlanoEntity>(PlanoSqlServerQuery.ObterPorId, new { Id = id });
        }

        public async Task<IEnumerable<PlanoEntity>> ListarAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<PlanoEntity>(PlanoSqlServerQuery.Listar);
        }

        public async Task<bool> AtualizarAsync(PlanoEntity plano)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(PlanoSqlServerQuery.Atualizar, plano);
            return result > 0;
        }

        public async Task<bool> RemoverLogicamenteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(PlanoSqlServerQuery.RemoverLogico, new { Id = id });
            return result > 0;
        }
    }
}
