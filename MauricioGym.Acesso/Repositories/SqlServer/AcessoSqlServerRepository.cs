using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Acesso.Entities;
using MauricioGym.Acesso.Repositories.SqlServer.Interfaces;
using MauricioGym.Acesso.Repositories.SqlServer.Queries;

namespace MauricioGym.Acesso.Repositories.SqlServer
{
    public class AcessoSqlServerRepository : SqlServerRepository, IAcessoSqlServerRepository
    {
        public AcessoSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<AcessoEntity> IncluirAcessoAsync(AcessoEntity acesso)
        {
            acesso.IdAcesso = (await QueryAsync<int>(AcessoSqlServerQuery.IncluirAcesso, acesso)).Single();
            return acesso;
        }

        public async Task<AcessoEntity> ConsultarAcessoAsync(int idAcesso)
        {
            var p = new DynamicParameters();
            p.Add("@IdAcesso", idAcesso);
            return (await QueryAsync<AcessoEntity>(AcessoSqlServerQuery.ConsultarAcesso, p)).FirstOrDefault();
        }

        public async Task<IEnumerable<AcessoEntity>> ConsultarAcessosPorUsuarioAsync(int idUsuario)
        {
            var p = new DynamicParameters();
            p.Add("@IdUsuario", idUsuario);
            return await QueryAsync<AcessoEntity>(AcessoSqlServerQuery.ConsultarAcessosPorUsuario, p);
        }

        public async Task<IEnumerable<AcessoEntity>> ConsultarAcessosPorAcademiaAsync(int idAcademia)
        {
            var p = new DynamicParameters();
            p.Add("@IdAcademia", idAcademia);
            return await QueryAsync<AcessoEntity>(AcessoSqlServerQuery.ConsultarAcessosPorAcademia, p);
        }

        public async Task<IEnumerable<AcessoEntity>> ConsultarAcessosAtivosPorAcademiaAsync(int idAcademia)
        {
            var p = new DynamicParameters();
            p.Add("@IdAcademia", idAcademia);
            return await QueryAsync<AcessoEntity>(AcessoSqlServerQuery.ConsultarAcessosAtivosPorAcademia, p);
        }

        public async Task<bool> AlterarAcessoAsync(AcessoEntity acesso)
        {
            var p = new DynamicParameters();
            p.Add("@IdAcesso", acesso.IdAcesso);
            p.Add("@IdUsuario", acesso.IdUsuario);
            p.Add("@IdAcademia", acesso.IdAcademia);
            p.Add("@TipoAcesso", acesso.TipoAcesso);
            p.Add("@ObservacaoAcesso", acesso.ObservacaoAcesso);
            p.Add("@MotivoNegacao", acesso.MotivoNegacao);
            var affectedRows = await ExecuteNonQueryAsync(AcessoSqlServerQuery.AlterarAcesso, p);
            return affectedRows > 0;
        }

        public async Task<bool> RegistrarSaidaAsync(int idAcesso, DateTime dataSaida)
        {
            var p = new DynamicParameters();
            p.Add("@IdAcesso", idAcesso);
            p.Add("@DataSaida", dataSaida);
            var affectedRows = await ExecuteNonQueryAsync(AcessoSqlServerQuery.RegistrarSaida, p);
            return affectedRows > 0;
        }

        public async Task<IEnumerable<AcessoEntity>> ListarAcessosAsync()
        {
            return await QueryAsync<AcessoEntity>(AcessoSqlServerQuery.ListarAcessos);
        }
    }
}