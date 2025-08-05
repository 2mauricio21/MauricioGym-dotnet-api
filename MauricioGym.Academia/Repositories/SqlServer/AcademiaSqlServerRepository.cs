using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MauricioGym.Academia.Entities;
using MauricioGym.Academia.Repositories.SqlServer.Interfaces;
using MauricioGym.Academia.Repositories.SqlServer.Queries;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;

namespace MauricioGym.Academia.Repositories.SqlServer
{
    public class AcademiaSqlServerRepository : SqlServerRepository, IAcademiaSqlServerRepository
    {
        public AcademiaSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<AcademiaEntity> IncluirAcademiaAsync(AcademiaEntity academia)
        {
            academia.IdAcademia = (await QueryAsync<int>(AcademiaSqlServerQuery.IncluirAcademia, academia)).Single();
            return academia;
        }

        public async Task<AcademiaEntity> ConsultarAcademiaAsync(int idAcademia)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@IdAcademia", idAcademia);
            var entity = await QueryAsync<AcademiaEntity>(AcademiaSqlServerQuery.ConsultarAcademia, parameters);
            return entity.FirstOrDefault();
        }

        public async Task<AcademiaEntity> ConsultarAcademiaPorCNPJAsync(string cnpj)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CNPJ", cnpj);
            var entity = await QueryAsync<AcademiaEntity>(AcademiaSqlServerQuery.ConsultarAcademiaPorCNPJ, parameters);
            return entity.FirstOrDefault();
        }

        public async Task<bool> AlterarAcademiaAsync(AcademiaEntity academia)
        {
            var rows = await ExecuteNonQueryAsync(AcademiaSqlServerQuery.AlterarAcademia, academia);
            return rows > 0;
        }

        public async Task<bool> ExcluirAcademiaAsync(int idAcademia)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@IdAcademia", idAcademia);
            var rows = await ExecuteNonQueryAsync(AcademiaSqlServerQuery.ExcluirAcademia, parameters);
            return rows > 0;
        }

        public async Task<IEnumerable<AcademiaEntity>> ListarAcademiasAsync()
        {
            return await QueryAsync<AcademiaEntity>(AcademiaSqlServerQuery.ListarAcademias);
        }

        public async Task<IEnumerable<AcademiaEntity>> ListarAcademiasAtivasAsync()
        {
            return await QueryAsync<AcademiaEntity>(AcademiaSqlServerQuery.ListarAcademiasAtivas);
        }
    }
}