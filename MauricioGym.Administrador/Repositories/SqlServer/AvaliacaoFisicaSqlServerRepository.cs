using Dapper;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer.Queries;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;

namespace MauricioGym.Administrador.Repositories.SqlServer
{
    public class AvaliacaoFisicaSqlServerRepository : SqlServerRepository, IAvaliacaoFisicaSqlServerRepository
    {
        public AvaliacaoFisicaSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<AvaliacaoFisicaEntity> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entidade = await QueryAsync<AvaliacaoFisicaEntity>(AvaliacaoFisicaSqlServerQuery.ObterPorId, p);
            return entidade.FirstOrDefault();
        }

        public async Task<IEnumerable<AvaliacaoFisicaEntity>> ListarPorAlunoAsync(int alunoId)
        {
            var p = new DynamicParameters();
            p.Add("@AlunoId", alunoId);

            return await QueryAsync<AvaliacaoFisicaEntity>(AvaliacaoFisicaSqlServerQuery.ListarPorAluno, p);
        }

        public async Task<IEnumerable<AvaliacaoFisicaEntity>> ListarPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            var p = new DynamicParameters();
            p.Add("@DataInicio", dataInicio);
            p.Add("@DataFim", dataFim);

            return await QueryAsync<AvaliacaoFisicaEntity>(AvaliacaoFisicaSqlServerQuery.ListarPorPeriodo, p);
        }

        public async Task<AvaliacaoFisicaEntity> ObterUltimaAvaliacaoAsync(int alunoId)
        {
            var p = new DynamicParameters();
            p.Add("@AlunoId", alunoId);

            var entidade = await QueryAsync<AvaliacaoFisicaEntity>(AvaliacaoFisicaSqlServerQuery.ObterUltimaAvaliacao, p);
            return entidade.FirstOrDefault();
        }

        public async Task<int> CriarAsync(AvaliacaoFisicaEntity avaliacaoFisica)
        {
            var p = new DynamicParameters();
            p.Add("@IdAluno", avaliacaoFisica.IdAluno);
            p.Add("@Peso", avaliacaoFisica.Peso);
            p.Add("@Altura", avaliacaoFisica.Altura);
            p.Add("@Imc", avaliacaoFisica.Imc);
            p.Add("@PercentualGordura", avaliacaoFisica.PercentualGordura);
            p.Add("@MassaMuscular", avaliacaoFisica.MassaMuscular);
            p.Add("@CircunferenciaCintura", avaliacaoFisica.CircunferenciaCintura);
            p.Add("@CircunferenciaBraco", avaliacaoFisica.CircunferenciaBraco);
            p.Add("@CircunferenciaPerna", avaliacaoFisica.CircunferenciaPerna);
            p.Add("@Observacoes", avaliacaoFisica.Observacoes);
            p.Add("@DataAvaliacao", avaliacaoFisica.DataAvaliacao);
            p.Add("@DataInclusao", avaliacaoFisica.DataInclusao);
            p.Add("@Ativo", avaliacaoFisica.Ativo);

            var resultado = await QueryAsync<int>(AvaliacaoFisicaSqlServerQuery.Criar, p);
            return resultado.FirstOrDefault();
        }

        public async Task<bool> AtualizarAsync(AvaliacaoFisicaEntity avaliacaoFisica)
        {
            var p = new DynamicParameters();
            p.Add("@Id", avaliacaoFisica.Id);
            p.Add("@IdAluno", avaliacaoFisica.IdAluno);
            p.Add("@Peso", avaliacaoFisica.Peso);
            p.Add("@Altura", avaliacaoFisica.Altura);
            p.Add("@Imc", avaliacaoFisica.Imc);
            p.Add("@PercentualGordura", avaliacaoFisica.PercentualGordura);
            p.Add("@MassaMuscular", avaliacaoFisica.MassaMuscular);
            p.Add("@CircunferenciaCintura", avaliacaoFisica.CircunferenciaCintura);
            p.Add("@CircunferenciaBraco", avaliacaoFisica.CircunferenciaBraco);
            p.Add("@CircunferenciaPerna", avaliacaoFisica.CircunferenciaPerna);
            p.Add("@Observacoes", avaliacaoFisica.Observacoes);
            p.Add("@DataAvaliacao", avaliacaoFisica.DataAvaliacao);
            p.Add("@DataAtualizacao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(AvaliacaoFisicaSqlServerQuery.Atualizar, p);
            return rowsAffected > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@DataExclusao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(AvaliacaoFisicaSqlServerQuery.ExcluirLogico, p);
            return rowsAffected > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var count = await QueryAsync<int>(AvaliacaoFisicaSqlServerQuery.VerificarExistencia, p);
            return count != null;
        }
    }
}