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
    public class CheckInSqlServerRepository : ICheckInSqlServerRepository
    {
        private readonly string _connectionString;

        public CheckInSqlServerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<CheckInEntity> ObterPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<CheckInEntity>(CheckInSqlServerQuery.ObterPorId, new { Id = id });
        }

        public async Task<IEnumerable<CheckInEntity>> ListarPorPessoaAsync(int pessoaId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<CheckInEntity>(CheckInSqlServerQuery.ListarPorPessoa, new { PessoaId = pessoaId });
        }

        public async Task<int> CriarAsync(CheckInEntity checkIn)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(CheckInSqlServerQuery.InserirCheckIn, checkIn);
        }

        public async Task<int> ContarCheckInsPorPessoaMesAsync(int pessoaId, int ano, int mes)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(CheckInSqlServerQuery.ContarPorPessoaMes, new { PessoaId = pessoaId, Ano = ano, Mes = mes });
        }
    }
}
