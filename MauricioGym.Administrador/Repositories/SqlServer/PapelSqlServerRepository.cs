using Dapper;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer.Queries;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using System.Data;

namespace MauricioGym.Administrador.Repositories.SqlServer
{
    public class PapelSqlServerRepository : SqlServerRepository, IPapelSqlServerRepository
    {
        public PapelSqlServerRepository(SQLServerDbContext sqlServerDbContext)
            : base(sqlServerDbContext) { }

        public async Task<PapelEntity> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id, DbType.Int32);
            var resultado = await QueryAsync<PapelEntity>(PapelSqlServerQuery.ObterPorId, p);
            return (PapelEntity)resultado;
        }

        public async Task<PapelEntity> ObterPorNomeAsync(string nome)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", nome, DbType.String);
            var resultado = await QueryAsync<PapelEntity>(PapelSqlServerQuery.ObterPorNome, p);
            return (PapelEntity)resultado;
        }

        public async Task<IEnumerable<PapelEntity>> ListarAsync()
        {
            return await QueryAsync<PapelEntity>(PapelSqlServerQuery.ObterTodos);
        }

        public async Task<IEnumerable<PapelEntity>> ListarPapeisSistemaAsync()
        {
            // Por enquanto, retorna todos os papéis ativos
            // Pode ser expandido para filtrar apenas papéis do sistema
            return await QueryAsync<PapelEntity>(PapelSqlServerQuery.ObterTodos);
        }

        public async Task<PapelEntity> IncluirAsync(PapelEntity papel)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", papel.Nome, DbType.String);
            p.Add("@Descricao", papel.Descricao, DbType.String);
            p.Add("@EhSistema", papel.EhSistema, DbType.Boolean);
            p.Add("@Ativo", papel.Ativo, DbType.Boolean);
            p.Add("@DataInclusao", DateTime.Now, DbType.DateTime);
            p.Add("@UsuarioInclusao", papel.UsuarioInclusao, DbType.Int32);

            var id = await QueryAsync<int>(PapelSqlServerQuery.Criar, papel);
            papel.Id = id.FirstOrDefault();
            return papel;
        }

        public async Task<PapelEntity> AlterarAsync(PapelEntity papel)
        {
            papel.DataAlteracao = DateTime.Now;
            var rowsAffected = await ExecuteNonQueryAsync(PapelSqlServerQuery.Atualizar, papel);
            return rowsAffected > 0 ? papel : new PapelEntity();
        }

        public async Task ExcluirAsync(int id)
        {
            var papel = new PapelEntity
            {
                Id = id,
                DataExclusao = DateTime.Now
            };
            await ExecuteNonQueryAsync(PapelSqlServerQuery.ExcluirLogico, papel);
        }

        public async Task<bool> ExisteAsync(int id)
        {
            var papel = new PapelEntity { Id = id };
            var count = await QueryAsync<int>(PapelSqlServerQuery.VerificarExistencia, papel);
            return count != null;
        }

        public async Task<bool> ExistePorNomeAsync(string nome)
        {
            var papel = new PapelEntity { Nome = nome };
            var count = await QueryAsync<int>(PapelSqlServerQuery.VerificarExistenciaNome, papel);
            return count != null;
        }

        // Métodos para compatibilidade com padrão Juris
        public async Task<IEnumerable<PapelEntity>> ObterTodosAsync()
        {
            return await ListarAsync();
        }

        public async Task<IEnumerable<PapelEntity>> ObterPorAdministradorIdAsync(int administradorId)
        {
            var papel = new PapelEntity { Id = administradorId }; // Usando Id temporariamente para passar o administradorId
            return await QueryAsync<PapelEntity>(PapelSqlServerQuery.ObterPorAdministradorId, papel);
        }

        public async Task<PapelEntity> CriarAsync(PapelEntity papel)
        {
            return await IncluirAsync(papel);
        }

        public async Task<PapelEntity> AtualizarAsync(PapelEntity papel)
        {
            return await AlterarAsync(papel);
        }

        public async Task RemoverLogicamenteAsync(int id)
        {
            await ExcluirAsync(id);
        }

        public async Task<bool> ExisteNomeAsync(string nome)
        {
            var papel = new PapelEntity { Nome = nome };
            var count = await QueryAsync<int>(PapelSqlServerQuery.VerificarExistenciaNome, papel);
            return count != null;
        }
    }
}
