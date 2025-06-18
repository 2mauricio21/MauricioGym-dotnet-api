using Dapper;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer.Queries;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using System.Data;

namespace MauricioGym.Administrador.Repositories.SqlServer
{
    public class AdministradorPapelSqlServerRepository : SqlServerRepository, IAdministradorPapelSqlServerRepository
    {
        public AdministradorPapelSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<AdministradorPapelEntity> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id, DbType.Int32);
            var entidade = await QueryAsync<AdministradorPapelEntity>(AdministradorPapelSqlServerQuery.ObterPorId, p);

            return entidade.FirstOrDefault();
        }

        public async Task<AdministradorPapelEntity> ObterPorAdministradorEPapelAsync(int administradorId, int papelId)
        {
            var p = new DynamicParameters();
            p.Add("@AdministradorId", administradorId, DbType.Int32);
            p.Add("@PapelId", papelId, DbType.Int32);

            var resultado = await QueryAsync<AdministradorPapelEntity>(AdministradorPapelSqlServerQuery.ObterPorAdministradorEPapel, p);

            return resultado.FirstOrDefault();
        }

        public async Task<IEnumerable<AdministradorPapelEntity>> ObterPorAdministradorIdAsync(int administradorId)
        {
            var p = new DynamicParameters();
            p.Add("@AdministradorId", administradorId, DbType.Int32);
            var resultado = await QueryAsync<AdministradorPapelEntity>(AdministradorPapelSqlServerQuery.ObterPorAdministradorId, p);
            return resultado;
        }

        public async Task<IEnumerable<AdministradorPapelEntity>> ObterPorPapelIdAsync(int papelId)
        {
            var p = new DynamicParameters();
            p.Add("@PapelId", papelId, DbType.Int32);

            return await QueryAsync<AdministradorPapelEntity>(AdministradorPapelSqlServerQuery.ObterPorPapelId, p);
        }

        public async Task<IEnumerable<PapelEntity>> ListarPapeisDoAdministradorAsync(int idAdministrador)
        {
            var p = new DynamicParameters();
            p.Add("@Id", idAdministrador, DbType.Int32);

            return await QueryAsync<PapelEntity>(PapelSqlServerQuery.ObterPorAdministradorId, p);
        }

        public async Task<IEnumerable<AdministradorEntity>> ListarAdministradoresComPapelAsync(int idPapel)
        {
            var p = new DynamicParameters();
            p.Add("@Id", idPapel, DbType.Int32);
            return await QueryAsync<AdministradorEntity>(AdministradorPapelSqlServerQuery.ListarAdministradoresPapeis, p);
        }

        public async Task<AdministradorPapelEntity> IncluirAsync(AdministradorPapelEntity administradorPapel)
        {
            administradorPapel.DataInclusao = DateTime.Now;

            var ids = await QueryAsync<int>(AdministradorPapelSqlServerQuery.Criar, administradorPapel);
            administradorPapel.Id = ids.FirstOrDefault();

            return administradorPapel;
        }

        public async Task ExcluirAsync(int id)
        {
            var entidade = new AdministradorPapelEntity
            {
                Id = id,
                DataExclusao = DateTime.Now
            };

            await ExecuteNonQueryAsync(
                AdministradorPapelSqlServerQuery.ExcluirLogico,
                entidade);
        }

        public async Task ExcluirPorAdministradorEPapelAsync(int administradorId, int papelId)
        {
            var entidade = new AdministradorPapelEntity
            {
                AdministradorId = administradorId,
                PapelId = papelId,
                DataExclusao = DateTime.Now
            };

            await ExecuteNonQueryAsync(
                AdministradorPapelSqlServerQuery.ExcluirLogicoPorAdministradorEPapel,
                entidade);
        }

        public async Task<bool> ExisteAsync(int id)
        {
            var entidade = new AdministradorPapelEntity { Id = id };
            var count = await QueryAsync<int>(
                AdministradorPapelSqlServerQuery.VerificarExistencia,
                entidade);

            return count != null;
        }

        public async Task<bool> ExisteAssociacaoAsync(int administradorId, int papelId)
        {
            var entidade = new AdministradorPapelEntity
            {
                AdministradorId = administradorId,
                PapelId = papelId
            };

            var count = await QueryAsync<int>(
                AdministradorPapelSqlServerQuery.VerificarExistenciaAssociacao,
                entidade);

            return count != null;
        }

        Task<AdministradorPapelEntity> IAdministradorPapelSqlServerRepository.ObterPorAdministradorPapelAsync(int idAdministrador, int idPapel)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<AdministradorPapelEntity>> IAdministradorPapelSqlServerRepository.ListarPorAdministradorAsync(int idAdministrador)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<AdministradorPapelEntity>> IAdministradorPapelSqlServerRepository.ListarPorPapelAsync(int idPapel)
        {
            throw new NotImplementedException();
        }

        Task IAdministradorPapelSqlServerRepository.ExcluirPorAdministradorPapelAsync(int idAdministrador, int idPapel)
        {
            throw new NotImplementedException();
        }

        Task<bool> IAdministradorPapelSqlServerRepository.ExisteAsync(int idAdministrador, int idPapel)
        {
            throw new NotImplementedException();
        }

        Task<AdministradorPapelEntity> IAdministradorPapelSqlServerRepository.CriarAsync(AdministradorPapelEntity administradorPapel)
        {
            throw new NotImplementedException();
        }

        Task IAdministradorPapelSqlServerRepository.RemoverLogicamenteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<bool> IAdministradorPapelSqlServerRepository.AdministradorPossuiPapelAsync(int administradorId, int papelId)
        {
            throw new NotImplementedException();
        }
    }
}
