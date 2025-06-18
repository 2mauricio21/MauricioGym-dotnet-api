using Dapper;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer.Queries;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;

namespace MauricioGym.Administrador.Repositories.SqlServer
{
    public class ExercicioSqlServerRepository : SqlServerRepository, IExercicioSqlServerRepository
    {
        public ExercicioSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<ExercicioEntity> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entidade = await QueryAsync<ExercicioEntity>(ExercicioSqlServerQuery.ObterPorId, p);
            return entidade.FirstOrDefault();
        }

        public async Task<ExercicioEntity> ObterPorNomeAsync(string nome)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", nome);

            var entidade = await QueryAsync<ExercicioEntity>(ExercicioSqlServerQuery.ObterPorNome, p);
            return entidade.FirstOrDefault();
        }

        public async Task<IEnumerable<ExercicioEntity>> ListarAtivosAsync()
        {
            return await QueryAsync<ExercicioEntity>(ExercicioSqlServerQuery.ListarAtivos);
        }

        public async Task<IEnumerable<ExercicioEntity>> ListarPorGrupoMuscularAsync(string grupoMuscular)
        {
            var p = new DynamicParameters();
            p.Add("@GrupoMuscular", grupoMuscular);

            return await QueryAsync<ExercicioEntity>(ExercicioSqlServerQuery.ListarPorGrupoMuscular, p);
        }

        public async Task<IEnumerable<ExercicioEntity>> ListarPorNivelAsync(string nivel)
        {
            var p = new DynamicParameters();
            p.Add("@Nivel", nivel);

            return await QueryAsync<ExercicioEntity>(ExercicioSqlServerQuery.ListarPorNivel, p);
        }

        public async Task<IEnumerable<ExercicioEntity>> ListarPorTipoAsync(string tipo)
        {
            var p = new DynamicParameters();
            p.Add("@Tipo", tipo);

            return await QueryAsync<ExercicioEntity>(ExercicioSqlServerQuery.ListarPorTipo, p);
        }

        public async Task<int> CriarAsync(ExercicioEntity exercicio)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", exercicio.Nome);
            p.Add("@Descricao", exercicio.Descricao);
            p.Add("@GrupoMuscular", exercicio.GrupoMuscular);
            p.Add("@Nivel", exercicio.Nivel);
            p.Add("@Tipo", exercicio.Tipo);
            p.Add("@Instrucoes", exercicio.Instrucoes);
            p.Add("@DataInclusao", exercicio.DataInclusao);
            p.Add("@Ativo", exercicio.Ativo);

            var entidade = await QueryAsync<int>(ExercicioSqlServerQuery.Criar, p);
            return entidade.FirstOrDefault();
        }

        public async Task<bool> AtualizarAsync(ExercicioEntity exercicio)
        {
            var p = new DynamicParameters();
            p.Add("@Id", exercicio.Id);
            p.Add("@Nome", exercicio.Nome);
            p.Add("@Descricao", exercicio.Descricao);
            p.Add("@GrupoMuscular", exercicio.GrupoMuscular);
            p.Add("@Nivel", exercicio.Nivel);
            p.Add("@Tipo", exercicio.Tipo);
            p.Add("@Instrucoes", exercicio.Instrucoes);
            p.Add("@DataAtualizacao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(ExercicioSqlServerQuery.Atualizar, p);
            return rowsAffected > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@DataExclusao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(ExercicioSqlServerQuery.ExcluirLogico, p);
            return rowsAffected > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var count = await QueryAsync<int>(ExercicioSqlServerQuery.VerificarExistencia, p);
            return count != null;
        }

        public async Task<bool> ExistePorNomeAsync(string nome)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", nome);

            var count = await QueryAsync<int>(ExercicioSqlServerQuery.VerificarExistenciaPorNome, p);
            return count != null;
        }
    }
}