using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer.Queries;
using System.Data.SqlClient;

namespace MauricioGym.Administrador.Repositories.SqlServer
{
    public class AdministradorSqlServerRepository : IAdministradorSqlServerRepository
    {
        private readonly string _connectionString;

        public AdministradorSqlServerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<AdministradorEntity> ObterPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<AdministradorEntity>(AdministradorSqlServerQuery.ObterPorId, new { Id = id });
        }

        public async Task<IEnumerable<AdministradorEntity>> ListarAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<AdministradorEntity>(AdministradorSqlServerQuery.Listar);
        }

        public async Task<int> CriarAsync(AdministradorEntity administrador)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(AdministradorSqlServerQuery.InserirAdministrador, administrador);
        }

        public async Task<bool> AtualizarAsync(AdministradorEntity administrador)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(AdministradorSqlServerQuery.Atualizar, administrador);
            return result > 0;
        }

        public async Task<bool> RemoverLogicamenteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(AdministradorSqlServerQuery.RemoverLogico, new { Id = id });
            return result > 0;
        }
    }
}
