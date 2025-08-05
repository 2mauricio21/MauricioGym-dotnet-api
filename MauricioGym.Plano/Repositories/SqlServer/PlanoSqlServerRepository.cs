using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Plano.Entities;
using MauricioGym.Plano.Repositories.SqlServer.Interfaces;
using MauricioGym.Plano.Repositories.SqlServer.Queries;

namespace MauricioGym.Plano.Repositories.SqlServer
{
    public class PlanoSqlServerRepository : SqlServerRepository, IPlanoSqlServerRepository
    {
        public PlanoSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<PlanoEntity> IncluirPlanoAsync(PlanoEntity plano)
        {
            plano.IdPlano = (await QueryAsync<int>(PlanoSqlServerQuery.IncluirPlano, plano)).Single();
            return plano;
        }

        public async Task<PlanoEntity> ConsultarPlanoAsync(int idPlano)
        {
            var p = new DynamicParameters();
            p.Add("@IdPlano", idPlano);
            return (await QueryAsync<PlanoEntity>(PlanoSqlServerQuery.ConsultarPlano, p)).FirstOrDefault();
        }

        public async Task<PlanoEntity> ConsultarPlanoPorNomeAsync(string nome, int idAcademia)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", nome);
            p.Add("@IdAcademia", idAcademia);
            return (await QueryAsync<PlanoEntity>(PlanoSqlServerQuery.ConsultarPlanoPorNome, p)).FirstOrDefault();
        }

        public async Task AlterarPlanoAsync(PlanoEntity plano)
        {
            await ExecuteNonQueryAsync(PlanoSqlServerQuery.AlterarPlano, plano);
        }

        public async Task ExcluirPlanoAsync(int idPlano)
        {
            var p = new DynamicParameters();
            p.Add("@IdPlano", idPlano);
            await ExecuteNonQueryAsync(PlanoSqlServerQuery.ExcluirPlano, p);
        }

        public async Task<IEnumerable<PlanoEntity>> ListarPlanosAsync()
        {
            return await QueryAsync<PlanoEntity>(PlanoSqlServerQuery.ListarPlanos);
        }

        public async Task<IEnumerable<PlanoEntity>> ListarPlanosPorAcademiaAsync(int idAcademia)
        {
            var p = new DynamicParameters();
            p.Add("@IdAcademia", idAcademia);
            return await QueryAsync<PlanoEntity>(PlanoSqlServerQuery.ListarPlanosPorAcademia, p);
        }

        public async Task<IEnumerable<PlanoEntity>> ListarPlanosAtivosAsync()
        {
            return await QueryAsync<PlanoEntity>(PlanoSqlServerQuery.ListarPlanosAtivos);
        }
    }
}