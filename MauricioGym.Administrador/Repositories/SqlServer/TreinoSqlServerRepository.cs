using Dapper;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer.Queries;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;

namespace MauricioGym.Administrador.Repositories.SqlServer
{
    public class TreinoSqlServerRepository : SqlServerRepository, ITreinoSqlServerRepository
    {
        public TreinoSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<TreinoEntity> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entidade = await QueryAsync<TreinoEntity>(TreinoSqlServerQuery.ListarPorId, p);
            return entidade.FirstOrDefault();
        }

        public async Task<TreinoEntity> ObterPorNomeAsync(string nome)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", nome);

            var entidade = await QueryAsync<TreinoEntity>(TreinoSqlServerQuery.ListarPorNome, p);
            return entidade.FirstOrDefault();
        }

        public async Task<IEnumerable<TreinoEntity>> ListarAtivosAsync()
        {
            return await QueryAsync<TreinoEntity>(TreinoSqlServerQuery.ListarAtivos);
        }

        public async Task<IEnumerable<TreinoEntity>> ListarPorTipoAsync(string tipo)
        {
            var p = new DynamicParameters();
            p.Add("@Tipo", tipo);

            return await QueryAsync<TreinoEntity>(TreinoSqlServerQuery.ListarPorTipo, p);
        }

        public async Task<IEnumerable<TreinoEntity>> ListarPorNivelAsync(string nivel)
        {
            var p = new DynamicParameters();
            p.Add("@Nivel", nivel);

            return await QueryAsync<TreinoEntity>(TreinoSqlServerQuery.ListarPorNivel, p);
        }

        public async Task<IEnumerable<TreinoEntity>> ListarPorAlunoAsync(int alunoId)
        {
            var p = new DynamicParameters();
            p.Add("@AlunoId", alunoId);

            return await QueryAsync<TreinoEntity>(TreinoSqlServerQuery.ListarPorAluno, p);
        }

        public async Task<int> CriarAsync(TreinoEntity treino)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", treino.Nome);
            p.Add("@Descricao", treino.Descricao);
            p.Add("@Tipo", treino.Tipo);
            p.Add("@Nivel", treino.Nivel);
            p.Add("@IdAluno", treino.IdAluno);
            p.Add("@DataInclusao", treino.DataInclusao);
            p.Add("@Ativo", treino.Ativo);

            var entidade = await QueryAsync<int>(TreinoSqlServerQuery.Criar, p);
            return entidade.FirstOrDefault();
        }

        public async Task<bool> AtualizarAsync(TreinoEntity treino)
        {
            var p = new DynamicParameters();
            p.Add("@Id", treino.Id);
            p.Add("@Nome", treino.Nome);
            p.Add("@Descricao", treino.Descricao);
            p.Add("@Tipo", treino.Tipo);
            p.Add("@Nivel", treino.Nivel);
            p.Add("@IdAluno", treino.IdAluno);
            p.Add("@DataAtualizacao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(TreinoSqlServerQuery.Atualizar, p);
            return rowsAffected > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@DataExclusao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(TreinoSqlServerQuery.ExcluirLogico, p);
            return rowsAffected > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var count = await QueryAsync<int>(TreinoSqlServerQuery.VerificarExistencia, p);
            return count != null;
        }

        public async Task<bool> ExistePorNomeAsync(string nome)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", nome);

            var count = await QueryAsync<int>(TreinoSqlServerQuery.VerificarExistenciaPorNome, p);
            return count != null;
        }
    }
}