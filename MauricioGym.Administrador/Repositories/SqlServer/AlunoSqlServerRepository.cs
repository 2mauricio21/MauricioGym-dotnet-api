using Dapper;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer.Queries;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;

namespace MauricioGym.Administrador.Repositories.SqlServer
{
    public class AlunoSqlServerRepository : SqlServerRepository, IAlunoSqlServerRepository
    {
        public AlunoSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<AlunoEntity> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entidade = await QueryAsync<AlunoEntity>(AlunoSqlServerQuery.ObterPorId, p);
            return entidade.FirstOrDefault();
        }

        public async Task<AlunoEntity> ObterPorEmailAsync(string email)
        {
            var p = new DynamicParameters();
            p.Add("@Email", email);

            var entidade = await QueryAsync<AlunoEntity>(AlunoSqlServerQuery.ObterPorEmail, p);
            return entidade.FirstOrDefault();
        }

        public async Task<AlunoEntity> ObterPorCpfAsync(string cpf)
        {
            var p = new DynamicParameters();
            p.Add("@Cpf", cpf);

            var entidade = await QueryAsync<AlunoEntity>(AlunoSqlServerQuery.ObterPorCpf, p);
            return entidade.FirstOrDefault();
        }

        public async Task<IEnumerable<AlunoEntity>> ListarAtivosAsync()
        {
            return await QueryAsync<AlunoEntity>(AlunoSqlServerQuery.ListarAtivos);
        }

        public async Task<IEnumerable<AlunoEntity>> ListarPorNomeAsync(string nome)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", nome);

            return await QueryAsync<AlunoEntity>(AlunoSqlServerQuery.ListarPorNome, p);
        }

        public async Task<IEnumerable<AlunoEntity>> ListarPorPlanoAsync(int planoId)
        {
            var p = new DynamicParameters();
            p.Add("@PlanoId", planoId);

            return await QueryAsync<AlunoEntity>(AlunoSqlServerQuery.ListarPorPlano, p);
        }

        public async Task<IEnumerable<AlunoEntity>> ListarComMensalidadeVencidaAsync()
        {
            return await QueryAsync<AlunoEntity>(AlunoSqlServerQuery.ListarComMensalidadeVencida);
        }

        public async Task<int> CriarAsync(AlunoEntity aluno)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", aluno.Nome);
            p.Add("@Email", aluno.Email);
            p.Add("@Cpf", aluno.Cpf);
            p.Add("@Telefone", aluno.Telefone);
            p.Add("@DataNascimento", aluno.DataNascimento);
            p.Add("@Endereco", aluno.Endereco);
            p.Add("@IdPlano", aluno.IdPlano);
            p.Add("@DataInclusao", aluno.DataInclusao);
            p.Add("@Ativo", aluno.Ativo);

            var retorno = await QueryAsync<int>(AlunoSqlServerQuery.Criar, p);
            return retorno.FirstOrDefault();
        }

        public async Task<bool> AtualizarAsync(AlunoEntity aluno)
        {
            var p = new DynamicParameters();
            p.Add("@Id", aluno.Id);
            p.Add("@Nome", aluno.Nome);
            p.Add("@Email", aluno.Email);
            p.Add("@Cpf", aluno.Cpf);
            p.Add("@Telefone", aluno.Telefone);
            p.Add("@DataNascimento", aluno.DataNascimento);
            p.Add("@Endereco", aluno.Endereco);
            p.Add("@IdPlano", aluno.IdPlano);
            p.Add("@DataAtualizacao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(AlunoSqlServerQuery.Atualizar, p);
            return rowsAffected > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@DataExclusao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(AlunoSqlServerQuery.ExcluirLogico, p);
            return rowsAffected > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var count = await QueryAsync<int>(AlunoSqlServerQuery.VerificarExistencia, p);
            return count != null;
        }

        public async Task<bool> ExistePorEmailAsync(string email)
        {
            var p = new DynamicParameters();
            p.Add("@Email", email);

            var count = await QueryAsync<int>(AlunoSqlServerQuery.VerificarExistenciaPorEmail, p);
            return count != null;
        }

        public async Task<bool> ExistePorCpfAsync(string cpf)
        {
            var p = new DynamicParameters();
            p.Add("@Cpf", cpf);

            var count = await QueryAsync<int>(AlunoSqlServerQuery.VerificarExistenciaPorCpf, p);
            return count != null;
        }
    }
}