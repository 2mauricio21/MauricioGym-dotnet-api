using Dapper;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.SqlServer.Interfaces;
using MauricioGym.Usuario.Repositories.SqlServer.Queries;

namespace MauricioGym.Usuario.Repositories.SqlServer
{
    public class CheckInSqlServerRepository : SqlServerRepository, ICheckInSqlServerRepository
    {
        public CheckInSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<CheckInEntity?> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entidade = await QueryAsync<CheckInEntity>(CheckInSqlServerQuery.ObterPorId, p);
            return entidade.FirstOrDefault();
        }

        public async Task<IEnumerable<CheckInEntity>> ObterPorClienteIdAsync(int clienteId)
        {
            var p = new DynamicParameters();
            p.Add("@ClienteId", clienteId);

            return await QueryAsync<CheckInEntity>(CheckInSqlServerQuery.ObterPorClienteId, p);
        }

        public async Task<IEnumerable<CheckInEntity>> ObterPorAcademiaIdAsync(int academiaId)
        {
            var p = new DynamicParameters();
            p.Add("@AcademiaId", academiaId);

            return await QueryAsync<CheckInEntity>(CheckInSqlServerQuery.ObterPorAcademiaId, p);
        }

        public async Task<IEnumerable<CheckInEntity>> ObterPorPeriodoAsync(DateTime dataInicio, DateTime dataFim, int? academiaId = null)
        {
            var p = new DynamicParameters();
            p.Add("@DataInicio", dataInicio.Date);
            p.Add("@DataFim", dataFim.Date);
            p.Add("@AcademiaId", academiaId);

            return await QueryAsync<CheckInEntity>(CheckInSqlServerQuery.ObterPorPeriodo, p);
        }

        public async Task<IEnumerable<CheckInEntity>> ObterPorClienteEPeriodoAsync(int clienteId, DateTime dataInicio, DateTime dataFim)
        {
            var p = new DynamicParameters();
            p.Add("@ClienteId", clienteId);
            p.Add("@DataInicio", dataInicio.Date);
            p.Add("@DataFim", dataFim.Date);

            return await QueryAsync<CheckInEntity>(CheckInSqlServerQuery.ObterPorClienteEPeriodo, p);
        }

        public async Task<CheckInEntity?> ObterUltimoCheckInClienteAsync(int clienteId, int academiaId)
        {
            var p = new DynamicParameters();
            p.Add("@ClienteId", clienteId);
            p.Add("@AcademiaId", academiaId);

            var entidade = await QueryAsync<CheckInEntity>(CheckInSqlServerQuery.ObterUltimoCheckInCliente, p);
            return entidade.FirstOrDefault();
        }

        public async Task<int> ContarCheckInsPorPeriodoAsync(int clienteId, DateTime dataInicio, DateTime dataFim)
        {
            var p = new DynamicParameters();
            p.Add("@ClienteId", clienteId);
            p.Add("@DataInicio", dataInicio.Date);
            p.Add("@DataFim", dataFim.Date);

            var entidades = await QueryAsync<int>(CheckInSqlServerQuery.ContarCheckInsPorPeriodo, p);
            return entidades.FirstOrDefault();
        }

        public async Task<int> CriarAsync(CheckInEntity checkIn)
        {
            await ExecuteNonQueryAsync(CheckInSqlServerQuery.Criar, checkIn);
            var p = new DynamicParameters();
            p.Add("@ClienteId", checkIn.ClienteId);
            p.Add("@AcademiaId", checkIn.AcademiaId);

            var ultimoId = await QueryAsync<int>(CheckInSqlServerQuery.ObterUltimoIdInserido, p);
            return ultimoId.FirstOrDefault();
        }

        public async Task<bool> AtualizarAsync(CheckInEntity checkIn)
        {
            var linhasAfetadas = await ExecuteNonQueryAsync(CheckInSqlServerQuery.Atualizar, checkIn);
            return linhasAfetadas > 0;
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@DataExclusao", DateTime.Now);

            var linhasAfetadas = await ExecuteNonQueryAsync(CheckInSqlServerQuery.ExcluirLogico, p);
            return linhasAfetadas > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entidades = await QueryAsync<int>(CheckInSqlServerQuery.VerificarExistencia, p);
            return entidades.FirstOrDefault() > 0;
        }

        public async Task<IEnumerable<CheckInEntity>> ObterTodosAsync()
        {
            return await QueryAsync<CheckInEntity>(CheckInSqlServerQuery.ObterTodos);
        }

        public async Task<IEnumerable<CheckInEntity>> ObterPorUsuarioAsync(int usuarioId)
        {
            var p = new DynamicParameters();
            p.Add("@UsuarioId", usuarioId);
            return await QueryAsync<CheckInEntity>(CheckInSqlServerQuery.ObterPorUsuario, p);
        }

        public async Task<bool> ClienteJaFezCheckInHojeAsync(int clienteId, int academiaId)
        {
            var p = new DynamicParameters();
            p.Add("@ClienteId", clienteId);
            p.Add("@AcademiaId", academiaId);
            p.Add("@DataHoje", DateTime.Now.Date);
            var result = await QueryAsync<int>(CheckInSqlServerQuery.ClienteJaFezCheckInHoje, p);
            return result.FirstOrDefault() > 0;
        }

        public async Task<int> ContarCheckInsPorUsuarioMesAsync(int usuarioId, int ano, int mes)
        {
            var p = new DynamicParameters();
            p.Add("@UsuarioId", usuarioId);
            p.Add("@Ano", ano);
            p.Add("@Mes", mes);
            var result = await QueryAsync<int>(CheckInSqlServerQuery.ContarCheckInsPorUsuarioMes, p);
            return result.FirstOrDefault();
        }
    }
}