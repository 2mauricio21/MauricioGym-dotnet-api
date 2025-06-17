using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MauricioGym.Entities;
using MauricioGym.Repositories.SqlServer.Interfaces;
using MauricioGym.Repositories.SqlServer.Queries;
using System.Data.SqlClient;

namespace MauricioGym.Repositories.SqlServer
{
    public class CaixaSqlServerRepository : ICaixaSqlServerRepository
    {
        private readonly string _connectionString;

        public CaixaSqlServerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<CaixaEntity> ObterPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<CaixaEntity>(CaixaSqlServerQuery.ObterPorId, new { Id = id });
        }

        public async Task<IEnumerable<CaixaEntity>> ListarAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<CaixaEntity>(CaixaSqlServerQuery.Listar);
        }

        public async Task<int> CriarAsync(CaixaEntity caixa)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(CaixaSqlServerQuery.InserirCaixa, caixa);
        }

        public async Task<bool> AtualizarAsync(CaixaEntity caixa)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(CaixaSqlServerQuery.Atualizar, caixa);
            return result > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(CaixaSqlServerQuery.Remover, new { Id = id });
            return result > 0;
        }
    }
}
