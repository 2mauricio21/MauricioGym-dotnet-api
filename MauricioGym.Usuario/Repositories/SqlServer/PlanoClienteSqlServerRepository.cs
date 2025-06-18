using Dapper;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.SqlServer.Interfaces;
using MauricioGym.Usuario.Repositories.SqlServer.Queries;

namespace MauricioGym.Usuario.Repositories.SqlServer
{
    public class PlanoClienteSqlServerRepository : SqlServerRepository, IPlanoClienteSqlServerRepository
    {
        public PlanoClienteSqlServerRepository(SQLServerDbContext sqlServerDbContext)
            : base(sqlServerDbContext) { }

        public async Task<PlanoClienteEntity?> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entidades = await QueryAsync<PlanoClienteEntity>(PlanoClienteSqlServerQuery.ListarPorId, p);
            return entidades.FirstOrDefault();
        }

        public async Task<IEnumerable<PlanoClienteEntity>> ObterPorClienteIdAsync(int clienteId)
        {
            var p = new DynamicParameters();
            p.Add("@ClienteId", clienteId);

            return await QueryAsync<PlanoClienteEntity>(PlanoClienteSqlServerQuery.ListarPorClienteId, p);
        }

        public async Task<IEnumerable<PlanoClienteEntity>> ObterPorPlanoIdAsync(int planoId)
        {
            var p = new DynamicParameters();
            p.Add("@PlanoId", planoId);

            return await QueryAsync<PlanoClienteEntity>(PlanoClienteSqlServerQuery.ListarPorPlanoId, p);
        }

        public async Task<IEnumerable<PlanoClienteEntity>> ObterTodosAsync()
        {
            return await ListarAsync();
        }

        public async Task<IEnumerable<PlanoClienteEntity>> ListarAsync()
        {
            return await QueryAsync<PlanoClienteEntity>(PlanoClienteSqlServerQuery.ListarTodos);
        }

        public async Task<IEnumerable<PlanoClienteEntity>> ListarAtivosAsync()
        {
            return await QueryAsync<PlanoClienteEntity>(PlanoClienteSqlServerQuery.ListarAtivo);
        }

        public async Task<IEnumerable<PlanoClienteEntity>> ObterAtivosPorClienteIdAsync(int clienteId)
        {
            var p = new DynamicParameters();
            p.Add("@ClienteId", clienteId);

            return await QueryAsync<PlanoClienteEntity>(PlanoClienteSqlServerQuery.ListarAtivosPorClienteId, p);
        }

        public async Task<IEnumerable<PlanoClienteEntity>> ObterVencidosPorClienteIdAsync(int clienteId)
        {
            var p = new DynamicParameters();
            p.Add("@ClienteId", clienteId);
            p.Add("@DataAtual", DateTime.Now.Date);

            return await QueryAsync<PlanoClienteEntity>(PlanoClienteSqlServerQuery.ListarVencidosPorClienteId, p);
        }

        public async Task<IEnumerable<PlanoClienteEntity>> ObterVencendoAsync(int diasAntecedencia = 7)
        {
            var p = new DynamicParameters();
            p.Add("@DataLimite", DateTime.Now.Date.AddDays(diasAntecedencia));
            p.Add("@DataAtual", DateTime.Now.Date);

            return await QueryAsync<PlanoClienteEntity>(PlanoClienteSqlServerQuery.ListarVencendo, p);
        }

        public async Task<int> CriarAsync(PlanoClienteEntity planoCliente)
        {
            var p = new DynamicParameters();
            p.Add("@ClienteId", planoCliente.ClienteId);
            p.Add("@PlanoId", planoCliente.PlanoId);
            p.Add("@AcademiaId", planoCliente.AcademiaId);
            p.Add("@DataInicio", planoCliente.DataInicio);
            p.Add("@DataVencimento", planoCliente.DataVencimento);
            p.Add("@Valor", planoCliente.Valor);
            p.Add("@MesesContratados", planoCliente.MesesContratados);
            p.Add("@PlanoAtivo", planoCliente.PlanoAtivo);
            p.Add("@Ativo", planoCliente.Ativo);
            p.Add("@DataInclusao", planoCliente.DataInclusao);
            p.Add("@UsuarioInclusao", planoCliente.UsuarioInclusao);

            var entidade = await QueryAsync<int>(PlanoClienteSqlServerQuery.Criar, p);
            return entidade.FirstOrDefault();
        }

        public async Task<bool> AtualizarAsync(PlanoClienteEntity planoCliente)
        {
            var p = new DynamicParameters();
            p.Add("@Id", planoCliente.Id);
            p.Add("@ClienteId", planoCliente.ClienteId);
            p.Add("@PlanoId", planoCliente.PlanoId);
            p.Add("@AcademiaId", planoCliente.AcademiaId);
            p.Add("@DataInicio", planoCliente.DataInicio);
            p.Add("@DataVencimento", planoCliente.DataVencimento);
            p.Add("@Valor", planoCliente.Valor);
            p.Add("@MesesContratados", planoCliente.MesesContratados);
            p.Add("@PlanoAtivo", planoCliente.PlanoAtivo);
            p.Add("@Ativo", planoCliente.Ativo);
            p.Add("@DataAlteracao", planoCliente.DataAlteracao);
            p.Add("@UsuarioAlteracao", planoCliente.UsuarioAlteracao);

            var rowsAffected = await ExecuteNonQueryAsync(PlanoClienteSqlServerQuery.Atualizar, p);
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

            var rowsAffected = await ExecuteNonQueryAsync(PlanoClienteSqlServerQuery.ExcluirLogico, p);
            return rowsAffected > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var count = await QueryAsync<int>(PlanoClienteSqlServerQuery.VerificarExistencia, p);
            return count != null;
        }

        public async Task<bool> ClientePossuiPlanoAtivoAsync(int clienteId, int planoId)
        {
            var p = new DynamicParameters();
            p.Add("@ClienteId", clienteId);
            p.Add("@PlanoId", planoId);
            p.Add("@DataAtual", DateTime.Now.Date);

            var count = await QueryAsync<int>(PlanoClienteSqlServerQuery.VerificarPlanoAtivoCliente, p);
            return count != null;
        }

        public async Task<IEnumerable<PlanoClienteEntity>> ObterPorClienteAsync(string cpfCliente, int idAcademia)
        {
            var p = new DynamicParameters();
            p.Add("@CpfCliente", cpfCliente);
            p.Add("@AcademiaId", idAcademia);

            return await QueryAsync<PlanoClienteEntity>(PlanoClienteSqlServerQuery.ListarPorCliente, p);
        }

        public async Task<PlanoClienteEntity?> ObterPlanoAtivoAsync(string cpfCliente, int idAcademia)
        {
            var p = new DynamicParameters();
            p.Add("@CpfCliente", cpfCliente);
            p.Add("@AcademiaId", idAcademia);
            p.Add("@DataAtual", DateTime.Now.Date);

            var entidades = await QueryAsync<PlanoClienteEntity>(PlanoClienteSqlServerQuery.ListarPlanoAtivo, p);
            return entidades.FirstOrDefault();
        }

        public async Task<IEnumerable<PlanoClienteEntity>> ObterPlanosVencendoAsync(int idAcademia, int dias = 7)
        {
            var p = new DynamicParameters();
            p.Add("@AcademiaId", idAcademia);
            p.Add("@DataLimite", DateTime.Now.Date.AddDays(dias));
            p.Add("@DataAtual", DateTime.Now.Date);

            return await QueryAsync<PlanoClienteEntity>(PlanoClienteSqlServerQuery.ListarPlanosVencendo, p);
        }

        public async Task<bool> CancelarPlanoAsync(int id, string motivo)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@Motivo", motivo);
            p.Add("@DataCancelamento", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(PlanoClienteSqlServerQuery.CancelarPlano, p);
            return rowsAffected > 0;
        }

        public async Task<bool> RenovarPlanoAsync(string cpfCliente, int idAcademia, int mesesAdicionais, decimal valorPago)
        {
            var p = new DynamicParameters();
            p.Add("@CpfCliente", cpfCliente);
            p.Add("@AcademiaId", idAcademia);
            p.Add("@MesesAdicionais", mesesAdicionais);
            p.Add("@ValorPago", valorPago);

            var rowsAffected = await ExecuteNonQueryAsync(PlanoClienteSqlServerQuery.RenovarPlano, p);
            return rowsAffected > 0;
        }

        public async Task<bool> ClientePossuiPlanoAtivoAsync(string cpfCliente, int idAcademia)
        {
            var p = new DynamicParameters();
            p.Add("@CpfCliente", cpfCliente);
            p.Add("@AcademiaId", idAcademia);
            p.Add("@DataAtual", DateTime.Now.Date);

            var count = await QueryAsync<int>(PlanoClienteSqlServerQuery.VerificarPlanoAtivoPorCpf, p);
            return count != null;
        }
    }
}
