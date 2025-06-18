using Dapper;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.SqlServer.Interfaces;
using MauricioGym.Usuario.Repositories.SqlServer.Queries;

namespace MauricioGym.Usuario.Repositories.SqlServer
{
    public class VinculoClienteAcademiaSqlServerRepository : SqlServerRepository, IVinculoClienteAcademiaSqlServerRepository
    {
        public VinculoClienteAcademiaSqlServerRepository(SQLServerDbContext sqlServerDbContext)
            : base(sqlServerDbContext) { }

        public async Task<VinculoClienteAcademiaEntity?> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entidades = await QueryAsync<VinculoClienteAcademiaEntity>(VinculoClienteAcademiaSqlServerQuery.ObterPorId, p);
            return entidades.FirstOrDefault();
        }

        public async Task<VinculoClienteAcademiaEntity?> ObterPorClienteEAcademiaAsync(int clienteId, int academiaId)
        {
            var p = new DynamicParameters();
            p.Add("@ClienteId", clienteId);
            p.Add("@AcademiaId", academiaId);

            var entidades = await QueryAsync<VinculoClienteAcademiaEntity>(
                VinculoClienteAcademiaSqlServerQuery.ObterPorClienteEAcademia, p);
            return entidades.FirstOrDefault();
        }

        public async Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterPorClienteIdAsync(int clienteId)
        {
            var p = new DynamicParameters();
            p.Add("@ClienteId", clienteId);

            return await QueryAsync<VinculoClienteAcademiaEntity>(VinculoClienteAcademiaSqlServerQuery.ObterPorClienteId, p);
        }

        public async Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterPorAcademiaIdAsync(int academiaId)
        {
            var p = new DynamicParameters();
            p.Add("@AcademiaId", academiaId);

            return await QueryAsync<VinculoClienteAcademiaEntity>(VinculoClienteAcademiaSqlServerQuery.ObterPorAcademiaId, p);
        }

        public async Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterTodosAsync()
        {
            return await ListarAsync();
        }

        public async Task<IEnumerable<VinculoClienteAcademiaEntity>> ListarAsync()
        {
            DynamicParameters? p = null;
            return await QueryAsync<VinculoClienteAcademiaEntity>(VinculoClienteAcademiaSqlServerQuery.ObterTodos, p);
        }

        public async Task<IEnumerable<VinculoClienteAcademiaEntity>> ListarAtivosAsync()
        {
            DynamicParameters? p = null;
            return await QueryAsync<VinculoClienteAcademiaEntity>(VinculoClienteAcademiaSqlServerQuery.ObterAtivos, p);
        }

        public async Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterAtivosPorClienteIdAsync(int clienteId)
        {
            var p = new DynamicParameters();
            p.Add("@ClienteId", clienteId);

            return await QueryAsync<VinculoClienteAcademiaEntity>(
                VinculoClienteAcademiaSqlServerQuery.ObterAtivosPorClienteId, p);
        }

        public async Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterAtivosPorAcademiaIdAsync(int academiaId)
        {
            var p = new DynamicParameters();
            p.Add("@AcademiaId", academiaId);

            return await QueryAsync<VinculoClienteAcademiaEntity>(
                VinculoClienteAcademiaSqlServerQuery.ObterAtivosPorAcademiaId, p);
        }

        public async Task<int> CriarAsync(VinculoClienteAcademiaEntity vinculo)
        {
            var p = new DynamicParameters();
            p.Add("@ClienteId", vinculo.ClienteId);
            p.Add("@AcademiaId", vinculo.AcademiaId);
            p.Add("@DataVinculo", vinculo.DataVinculo);
            p.Add("@DataDesvinculo", vinculo.DataDesvinculo);
            p.Add("@Ativo", vinculo.Ativo);
            p.Add("@Observacoes", vinculo.Observacoes);
            p.Add("@DataInclusao", vinculo.DataInclusao);
            p.Add("@UsuarioInclusao", vinculo.UsuarioInclusao);

            var entidade = await QueryAsync<int>(VinculoClienteAcademiaSqlServerQuery.Criar, p);
            return entidade.FirstOrDefault();
        }

        public async Task<bool> AtualizarAsync(VinculoClienteAcademiaEntity vinculo)
        {
            var p = new DynamicParameters();
            p.Add("@Id", vinculo.Id);
            p.Add("@ClienteId", vinculo.ClienteId);
            p.Add("@AcademiaId", vinculo.AcademiaId);
            p.Add("@DataVinculo", vinculo.DataVinculo);
            p.Add("@DataDesvinculo", vinculo.DataDesvinculo);
            p.Add("@Ativo", vinculo.Ativo);
            p.Add("@Observacoes", vinculo.Observacoes);
            p.Add("@DataAlteracao", vinculo.DataAlteracao);
            p.Add("@UsuarioAlteracao", vinculo.UsuarioAlteracao);

            var rowsAffected = await ExecuteNonQueryAsync(VinculoClienteAcademiaSqlServerQuery.Atualizar, p);
            return rowsAffected > 0;
        }

        public async Task ExcluirAsync(int id)
        {
            await RemoverAsync(id);
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@DataExclusao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(VinculoClienteAcademiaSqlServerQuery.ExcluirLogico, p);
            return rowsAffected > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var count = await QueryAsync<int>(VinculoClienteAcademiaSqlServerQuery.VerificarExistencia, p);
            return count != null;
        }

        public async Task<bool> ExisteVinculoAsync(int clienteId, int academiaId)
        {
            var p = new DynamicParameters();
            p.Add("@ClienteId", clienteId);
            p.Add("@AcademiaId", academiaId);

            var count = await QueryAsync<int>(VinculoClienteAcademiaSqlServerQuery.VerificarExistenciaVinculo, p);
            return count != null;
        }

        public async Task<bool> ExisteVinculoAtivoAsync(int clienteId, int academiaId)
        {
            var p = new DynamicParameters();
            p.Add("@ClienteId", clienteId);
            p.Add("@AcademiaId", academiaId);

            var count = await QueryAsync<int>(VinculoClienteAcademiaSqlServerQuery.VerificarExistenciaVinculoAtivo, p);
            return count != null;
        }

        public async Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterPorAcademiaAsync(int idAcademia)
        {
            var p = new DynamicParameters();
            p.Add("@AcademiaId", idAcademia);

            return await QueryAsync<VinculoClienteAcademiaEntity>(VinculoClienteAcademiaSqlServerQuery.ObterPorAcademia, p);
        }

        public async Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterPorClienteAsync(string cpfCliente)
        {
            var p = new DynamicParameters();
            p.Add("@CpfCliente", cpfCliente);

            return await QueryAsync<VinculoClienteAcademiaEntity>(VinculoClienteAcademiaSqlServerQuery.ObterPorCliente, p);
        }

        public async Task<VinculoClienteAcademiaEntity?> ObterVinculoAsync(string cpfCliente, int idAcademia)
        {
            var p = new DynamicParameters();
            p.Add("@CpfCliente", cpfCliente);
            p.Add("@AcademiaId", idAcademia);

            var entidades = await QueryAsync<VinculoClienteAcademiaEntity>(VinculoClienteAcademiaSqlServerQuery.ObterVinculo, p);
            return entidades.FirstOrDefault();
        }

        public async Task<bool> DesativarVinculoAsync(string cpfCliente, int idAcademia)
        {
            var p = new DynamicParameters();
            p.Add("@CpfCliente", cpfCliente);
            p.Add("@AcademiaId", idAcademia);
            p.Add("@DataDesativacao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(VinculoClienteAcademiaSqlServerQuery.DesativarVinculo, p);
            return rowsAffected > 0;
        }

        public async Task<bool> ExisteVinculoAtivoAsync(string cpfCliente, int idAcademia)
        {
            var p = new DynamicParameters();
            p.Add("@CpfCliente", cpfCliente);
            p.Add("@AcademiaId", idAcademia);

            var count = await QueryAsync<int>(VinculoClienteAcademiaSqlServerQuery.VerificarVinculoAtivoPorCpf, p);
            return count != null;
        }

        public async Task<int> ContarClientesAtivosAcademiaAsync(int idAcademia)
        {
            var p = new DynamicParameters();
            p.Add("@AcademiaId", idAcademia);

            var resultado = await QueryAsync<int>(VinculoClienteAcademiaSqlServerQuery.ContarClientesAtivos, p);
            return resultado.FirstOrDefault();
        }
    }
}
