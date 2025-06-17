using Dapper;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.Interfaces;
using MauricioGym.Usuario.Repositories.SqlServer.Queries;
using System.Data.SqlClient;

namespace MauricioGym.Usuario.Repositories.SqlServer
{
    public class CheckInSqlServerRepository : ICheckInSqlServerRepository
    {
        private readonly string _connectionString;

        public CheckInSqlServerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }        public async Task<IEnumerable<CheckInEntity>> ObterTodosAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<CheckInEntity>(CheckInSqlServerQuery.ObterTodos);
        }        public async Task<CheckInEntity?> ObterPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<CheckInEntity>(CheckInSqlServerQuery.ObterPorId, new { Id = id });
        }        public async Task<IEnumerable<CheckInEntity>> ObterPorUsuarioAsync(int usuarioId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<CheckInEntity>(CheckInSqlServerQuery.ObterPorUsuario, new { UsuarioId = usuarioId });
        }        public async Task<IEnumerable<CheckInEntity>> ObterPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<CheckInEntity>(CheckInSqlServerQuery.ObterPorPeriodo, new { DataInicio = dataInicio, DataFim = dataFim });
        }        public async Task<int> CriarAsync(CheckInEntity checkIn)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleAsync<int>(CheckInSqlServerQuery.Criar, checkIn);
        }        public async Task<bool> AtualizarAsync(CheckInEntity checkIn)
        {
            using var connection = new SqlConnection(_connectionString);
            var rowsAffected = await connection.ExecuteAsync(CheckInSqlServerQuery.Atualizar, checkIn);
            return rowsAffected > 0;
        }        public async Task<bool> RemoverAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var rowsAffected = await connection.ExecuteAsync(CheckInSqlServerQuery.Remover, new { Id = id });
            return rowsAffected > 0;
        }        public async Task<bool> ExisteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var count = await connection.ExecuteScalarAsync<int>(CheckInSqlServerQuery.Existe, new { Id = id });
            return count > 0;
        }        public async Task<int> ContarCheckInsPorUsuarioMesAsync(int usuarioId, int ano, int mes)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(CheckInSqlServerQuery.ContarCheckInsPorUsuarioMes, new { UsuarioId = usuarioId, Ano = ano, Mes = mes });
        }
    }
}
