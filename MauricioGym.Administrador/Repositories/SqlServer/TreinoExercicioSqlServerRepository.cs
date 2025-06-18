using Dapper;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer.Queries;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;

namespace MauricioGym.Administrador.Repositories.SqlServer
{
    public class TreinoExercicioSqlServerRepository : SqlServerRepository, ITreinoExercicioSqlServerRepository
    {
        public TreinoExercicioSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<TreinoExercicioEntity> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entidade = await QueryAsync<TreinoExercicioEntity>(TreinoExercicioSqlServerQuery.ListarPorId, p);
            return entidade.FirstOrDefault();
        }

        public async Task<TreinoExercicioEntity> ObterPorTreinoExercicioAsync(int treinoId, int exercicioId)
        {
            var p = new DynamicParameters();
            p.Add("@TreinoId", treinoId);
            p.Add("@ExercicioId", exercicioId);

            var entidade = await QueryAsync<TreinoExercicioEntity>(TreinoExercicioSqlServerQuery.ListarPorTreinoExercicio, p);
            return entidade.FirstOrDefault();
        }

        public async Task<IEnumerable<TreinoExercicioEntity>> ListarPorTreinoAsync(int treinoId)
        {
            var p = new DynamicParameters();
            p.Add("@TreinoId", treinoId);

            return await QueryAsync<TreinoExercicioEntity>(TreinoExercicioSqlServerQuery.ListarPorTreino, p);
        }

        public async Task<IEnumerable<TreinoExercicioEntity>> ListarPorExercicioAsync(int exercicioId)
        {
            var p = new DynamicParameters();
            p.Add("@ExercicioId", exercicioId);

            return await QueryAsync<TreinoExercicioEntity>(TreinoExercicioSqlServerQuery.ListarPorExercicio, p);
        }

        public async Task<IEnumerable<ExercicioEntity>> ListarExerciciosDoTreinoAsync(int treinoId)
        {
            var p = new DynamicParameters();
            p.Add("@TreinoId", treinoId);

            return await QueryAsync<ExercicioEntity>(TreinoExercicioSqlServerQuery.ListarExerciciosDoTreino, p);
        }

        public async Task<IEnumerable<TreinoEntity>> ListarTreinosComExercicioAsync(int exercicioId)
        {
            var p = new DynamicParameters();
            p.Add("@ExercicioId", exercicioId);

            return await QueryAsync<TreinoEntity>(TreinoExercicioSqlServerQuery.ListarTreinosComExercicio, p);
        }

        public async Task<int> CriarAsync(TreinoExercicioEntity treinoExercicio)
        {
            var p = new DynamicParameters();
            p.Add("@IdTreino", treinoExercicio.IdTreino);
            p.Add("@IdExercicio", treinoExercicio.IdExercicio);
            p.Add("@Series", treinoExercicio.Series);
            p.Add("@Repeticoes", treinoExercicio.Repeticoes);
            p.Add("@Carga", treinoExercicio.Carga);
            p.Add("@TempoDescanso", treinoExercicio.TempoDescanso);
            p.Add("@Ordem", treinoExercicio.Ordem);
            p.Add("@DataInclusao", treinoExercicio.DataInclusao);
            p.Add("@Ativo", treinoExercicio.Ativo);

            var entidade = await QueryAsync<int>(TreinoExercicioSqlServerQuery.Criar, p);
            return entidade.FirstOrDefault();
        }

        public async Task<bool> AtualizarAsync(TreinoExercicioEntity treinoExercicio)
        {
            var p = new DynamicParameters();
            p.Add("@Id", treinoExercicio.Id);
            p.Add("@IdTreino", treinoExercicio.IdTreino);
            p.Add("@IdExercicio", treinoExercicio.IdExercicio);
            p.Add("@Series", treinoExercicio.Series);
            p.Add("@Repeticoes", treinoExercicio.Repeticoes);
            p.Add("@Carga", treinoExercicio.Carga);
            p.Add("@TempoDescanso", treinoExercicio.TempoDescanso);
            p.Add("@Ordem", treinoExercicio.Ordem);
            p.Add("@DataAtualizacao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(TreinoExercicioSqlServerQuery.Atualizar, p);
            return rowsAffected > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@DataExclusao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(TreinoExercicioSqlServerQuery.ExcluirLogico, p);
            return rowsAffected > 0;
        }

        public async Task<bool> ExisteAsync(int treinoId, int exercicioId)
        {
            var p = new DynamicParameters();
            p.Add("@TreinoId", treinoId);
            p.Add("@ExercicioId", exercicioId);

            var count = await QueryAsync<int>(TreinoExercicioSqlServerQuery.VerificarExistencia, p);
            return count != null;
        }
    }
}