using Dapper;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer.Queries;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;

namespace MauricioGym.Administrador.Repositories.SqlServer
{
    public class FrequenciaSqlServerRepository : SqlServerRepository, IFrequenciaSqlServerRepository
    {
        public FrequenciaSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<FrequenciaEntity?> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entidade = await QueryAsync<FrequenciaEntity>(FrequenciaSqlServerQuery.ObterPorId, p);
            return entidade.FirstOrDefault();
        }

        public async Task<FrequenciaEntity?> ObterPorAlunoDataAsync(int alunoId, DateTime data)
        {
            var p = new DynamicParameters();
            p.Add("@AlunoId", alunoId);
            p.Add("@Data", data.Date);

            var entidade = await QueryAsync<FrequenciaEntity>(FrequenciaSqlServerQuery.ObterPorAlunoData, p);
            return entidade.FirstOrDefault();
        }

        public async Task<IEnumerable<FrequenciaEntity>> ListarPorAlunoAsync(int alunoId)
        {
            var p = new DynamicParameters();
            p.Add("@AlunoId", alunoId);

            return await QueryAsync<FrequenciaEntity>(FrequenciaSqlServerQuery.ListarPorAluno, p);
        }

        public async Task<IEnumerable<FrequenciaEntity>> ListarPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            var p = new DynamicParameters();
            p.Add("@DataInicio", dataInicio.Date);
            p.Add("@DataFim", dataFim.Date);

            return await QueryAsync<FrequenciaEntity>(FrequenciaSqlServerQuery.ListarPorPeriodo, p);
        }

        public async Task<IEnumerable<FrequenciaEntity>> ListarPorAlunoPeriodoAsync(int alunoId, DateTime dataInicio, DateTime dataFim)
        {
            var p = new DynamicParameters();
            p.Add("@AlunoId", alunoId);
            p.Add("@DataInicio", dataInicio.Date);
            p.Add("@DataFim", dataFim.Date);

            return await QueryAsync<FrequenciaEntity>(FrequenciaSqlServerQuery.ListarPorAlunoPeriodo, p);
        }

        public async Task<int> CriarAsync(FrequenciaEntity frequencia)
        {
            var p = new DynamicParameters();
            p.Add("@IdAluno", frequencia.IdAluno);
            p.Add("@Data", frequencia.Data);
            p.Add("@HoraEntrada", frequencia.HoraEntrada);
            p.Add("@HoraSaida", frequencia.HoraSaida);
            p.Add("@Observacoes", frequencia.Observacoes);
            p.Add("@DataInclusao", frequencia.DataInclusao);
            p.Add("@Ativo", frequencia.Ativo);

            var entidade = await QueryAsync<int>(FrequenciaSqlServerQuery.Criar, p);
            return entidade.FirstOrDefault();
        }

        public async Task<bool> AtualizarAsync(FrequenciaEntity frequencia)
        {
            var p = new DynamicParameters();
            p.Add("@Id", frequencia.Id);
            p.Add("@IdAluno", frequencia.IdAluno);
            p.Add("@Data", frequencia.Data);
            p.Add("@HoraEntrada", frequencia.HoraEntrada);
            p.Add("@HoraSaida", frequencia.HoraSaida);
            p.Add("@Observacoes", frequencia.Observacoes);
            p.Add("@DataAtualizacao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(FrequenciaSqlServerQuery.Atualizar, p);
            return rowsAffected > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@DataExclusao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(FrequenciaSqlServerQuery.ExcluirLogico, p);
            return rowsAffected > 0;
        }

        public async Task<bool> ExisteAsync(int alunoId, DateTime data)
        {
            var p = new DynamicParameters();
            p.Add("@AlunoId", alunoId);
            p.Add("@Data", data.Date);

            var count = await QueryAsync<int>(FrequenciaSqlServerQuery.VerificarExistencia, p);
            return count != null;
        }
    }
}