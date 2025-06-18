using Dapper;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer.Queries;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;

namespace MauricioGym.Administrador.Repositories.SqlServer
{
    public class MensalidadeSqlServerRepository : SqlServerRepository, IMensalidadeSqlServerRepository
    {
        public MensalidadeSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<MensalidadeEntity> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entidade = await QueryAsync<MensalidadeEntity>(MensalidadeSqlServerQuery.ObterPorId, p);
            return entidade.FirstOrDefault();
        }

        public async Task<IEnumerable<MensalidadeEntity>> ListarPorAlunoAsync(int alunoId)
        {
            var p = new DynamicParameters();
            p.Add("@AlunoId", alunoId);

            return await QueryAsync<MensalidadeEntity>(MensalidadeSqlServerQuery.ListarPorAluno, p);
        }

        public async Task<IEnumerable<MensalidadeEntity>> ListarPorStatusAsync(string status)
        {
            var p = new DynamicParameters();
            p.Add("@Status", status);

            return await QueryAsync<MensalidadeEntity>(MensalidadeSqlServerQuery.ListarPorStatus, p);
        }

        public async Task<IEnumerable<MensalidadeEntity>> ListarVencidasAsync()
        {
            return await QueryAsync<MensalidadeEntity>(MensalidadeSqlServerQuery.ListarVencidas);
        }

        public async Task<IEnumerable<MensalidadeEntity>> ListarPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            var p = new DynamicParameters();
            p.Add("@DataInicio", dataInicio);
            p.Add("@DataFim", dataFim);

            return await QueryAsync<MensalidadeEntity>(MensalidadeSqlServerQuery.ListarPorPeriodo, p);
        }

        public async Task<int> CriarAsync(MensalidadeEntity mensalidade)
        {
            var p = new DynamicParameters();
            p.Add("@IdAluno", mensalidade.IdAluno);
            p.Add("@IdPlano", mensalidade.IdPlano);
            p.Add("@Valor", mensalidade.Valor);
            p.Add("@DataVencimento", mensalidade.DataVencimento);
            p.Add("@Status", mensalidade.Status);
            p.Add("@DataCriacao", mensalidade.DataCriacao);
            p.Add("@Ativo", mensalidade.Ativo);

            var entidade = await QueryAsync<int>(MensalidadeSqlServerQuery.Criar, p);
            return entidade.FirstOrDefault();
        }

        public async Task<bool> AtualizarAsync(MensalidadeEntity mensalidade)
        {
            var p = new DynamicParameters();
            p.Add("@Id", mensalidade.Id);
            p.Add("@IdAluno", mensalidade.IdAluno);
            p.Add("@IdPlano", mensalidade.IdPlano);
            p.Add("@Valor", mensalidade.Valor);
            p.Add("@DataVencimento", mensalidade.DataVencimento);
            p.Add("@Status", mensalidade.Status);
            p.Add("@DataPagamento", mensalidade.DataPagamento);
            p.Add("@DataAtualizacao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(MensalidadeSqlServerQuery.Atualizar, p);
            return rowsAffected > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@DataExclusao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(MensalidadeSqlServerQuery.ExcluirLogico, p);
            return rowsAffected > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var count = await QueryAsync<int>(MensalidadeSqlServerQuery.VerificarExistencia, p);
            return count != null;
        }
    }
}