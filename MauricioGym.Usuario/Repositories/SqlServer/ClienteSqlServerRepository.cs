using Dapper;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.SqlServer.Interfaces;
using MauricioGym.Usuario.Repositories.SqlServer.Queries;

namespace MauricioGym.Usuario.Repositories.SqlServer
{
    public class ClienteSqlServerRepository : SqlServerRepository, IClienteSqlServerRepository
    {
        public ClienteSqlServerRepository(SQLServerDbContext sqlServerDbContext)
            : base(sqlServerDbContext) { }

        public async Task<ClienteEntity?> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entidades = await QueryAsync<ClienteEntity>(ClienteSqlServerQuery.ObterPorId, p);
            return entidades.FirstOrDefault();
        }

        public async Task<ClienteEntity?> ObterPorCpfAsync(string cpf)
        {
            var p = new DynamicParameters();
            p.Add("@Cpf", cpf);

            var entidades = await QueryAsync<ClienteEntity>(ClienteSqlServerQuery.ObterPorCpf, p);
            return entidades.FirstOrDefault();
        }

        public async Task<ClienteEntity?> ObterPorEmailAsync(string email)
        {
            var p = new DynamicParameters();
            p.Add("@Email", email);

            var entidades = await QueryAsync<ClienteEntity>(ClienteSqlServerQuery.ObterPorEmail, p);
            return entidades.FirstOrDefault();
        }

        public async Task<IEnumerable<ClienteEntity>> ObterTodosAsync()
        {
            return await ListarAsync();
        }

        public async Task<IEnumerable<ClienteEntity>> ObterAtivosAsync()
        {
            return await ListarAtivosAsync();
        }

        public async Task<IEnumerable<ClienteEntity>> ListarAsync()
        {
            DynamicParameters? p = null;
            return await QueryAsync<ClienteEntity>(ClienteSqlServerQuery.ObterTodos, p);
        }

        public async Task<IEnumerable<ClienteEntity>> ListarAtivosAsync()
        {
            DynamicParameters? p = null;
            return await QueryAsync<ClienteEntity>(ClienteSqlServerQuery.ObterAtivos, p);
        }

        public async Task<int> CriarAsync(ClienteEntity cliente)
        {
            var p = new DynamicParameters();
            p.Add("@Cpf", cliente.Cpf);
            p.Add("@Nome", cliente.Nome);
            p.Add("@Email", cliente.Email);
            p.Add("@Telefone", cliente.Telefone);
            p.Add("@DataNascimento", cliente.DataNascimento);
            p.Add("@Endereco", cliente.Endereco);
            p.Add("@Cidade", cliente.Cidade);
            p.Add("@Estado", cliente.Estado);
            p.Add("@Cep", cliente.Cep);
            p.Add("@Observacoes", cliente.Observacoes);
            p.Add("@Ativo", cliente.Ativo);
            p.Add("@DataInclusao", cliente.DataInclusao);
            p.Add("@UsuarioInclusao", cliente.UsuarioInclusao);

            var entidade = await QueryAsync<int>(ClienteSqlServerQuery.Criar, p);
            return entidade.FirstOrDefault();
        }

        public async Task<bool> AtualizarAsync(ClienteEntity cliente)
        {
            var p = new DynamicParameters();
            p.Add("@Id", cliente.Id);
            p.Add("@Cpf", cliente.Cpf);
            p.Add("@Nome", cliente.Nome);
            p.Add("@Email", cliente.Email);
            p.Add("@Telefone", cliente.Telefone);
            p.Add("@DataNascimento", cliente.DataNascimento);
            p.Add("@Endereco", cliente.Endereco);
            p.Add("@Cidade", cliente.Cidade);
            p.Add("@Estado", cliente.Estado);
            p.Add("@Cep", cliente.Cep);
            p.Add("@Observacoes", cliente.Observacoes);
            p.Add("@Ativo", cliente.Ativo);
            p.Add("@DataAlteracao", cliente.DataAlteracao);
            p.Add("@UsuarioAlteracao", cliente.UsuarioAlteracao);

            var rowsAffected = await ExecuteNonQueryAsync(ClienteSqlServerQuery.Atualizar, p);
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

            var rowsAffected = await ExecuteNonQueryAsync(ClienteSqlServerQuery.ExcluirLogico, p);
            return rowsAffected > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var count = await QueryAsync<int>(ClienteSqlServerQuery.VerificarExistencia, p);
            return count != null;
        }

        public async Task<bool> ExisteCpfAsync(string cpf, int? idExcluir = null)
        {
            var p = new DynamicParameters();
            p.Add("@Cpf", cpf);
            p.Add("@IdExcluir", idExcluir);

            var count = await QueryAsync<int>(ClienteSqlServerQuery.VerificarExistenciaCpf, p);
            return count != null;
        }

        public async Task<bool> ExisteEmailAsync(string email, int? idExcluir = null)
        {
            var p = new DynamicParameters();
            p.Add("@Email", email);
            p.Add("@IdExcluir", idExcluir);

            var count = await QueryAsync<int>(ClienteSqlServerQuery.VerificarExistenciaEmail, p);
            return count != null;
        }
    }
}
