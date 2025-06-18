using Dapper;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer.Queries;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using System.Data;

namespace MauricioGym.Administrador.Repositories.SqlServer
{
    public class PermissaoSqlServerRepository : SqlServerRepository, IPermissaoSqlServerRepository
    {
        public PermissaoSqlServerRepository(SQLServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
        }

        public async Task<PermissaoEntity> ObterPorIdAsync(int id)
        {
            var permissao = new PermissaoEntity { Id = id };
            var resultado = await QueryAsync<PermissaoEntity>(
                PermissaoSqlServerQuery.ObterPorId, permissao);

            return (PermissaoEntity)resultado;
        }

        public async Task<PermissaoEntity> ObterPorNomeAsync(string nome)
        {
            var permissao = new PermissaoEntity { Nome = nome };
            var resultado = await QueryAsync<PermissaoEntity>(
                PermissaoSqlServerQuery.ObterPorNome, permissao);

            return (PermissaoEntity)resultado;
        }

        public async Task<PermissaoEntity> ObterPorRecursoAcaoAsync(string recurso, string acao)
        {
            var permissao = new PermissaoEntity { Recurso = recurso, Acao = acao };
            var resultado = await QueryAsync<PermissaoEntity>(
                PermissaoSqlServerQuery.ObterPorRecursoAcao, permissao);

            return (PermissaoEntity)resultado;
        }

        public async Task<IEnumerable<PermissaoEntity>> ObterTodosAsync()
        {
            return await QueryAsync<PermissaoEntity>(PermissaoSqlServerQuery.ObterTodos);
        }

        public async Task<IEnumerable<PermissaoEntity>> ObterPorRecursoAsync(string recurso)
        {
            var permissao = new PermissaoEntity { Recurso = recurso };
            return await QueryAsync<PermissaoEntity>(
                PermissaoSqlServerQuery.ObterPorRecurso, permissao);
        }

        public async Task<IEnumerable<PermissaoEntity>> ObterPorPapelIdAsync(int papelId)
        {
            var p = new DynamicParameters();
            p.Add("@PapelId", papelId, DbType.Int32);
            return await QueryAsync<PermissaoEntity>(PermissaoSqlServerQuery.ObterPorPapelId, p);
        }

        public async Task<PermissaoEntity> IncluirAsync(PermissaoEntity permissao)
        {
            permissao.DataInclusao = DateTime.Now;
            permissao.Ativo = true;

            var id = await QueryAsync<int>(
                PermissaoSqlServerQuery.Criar, permissao);

            permissao.Id = id.FirstOrDefault();
            return permissao;
        }

        public async Task<PermissaoEntity> AlterarAsync(PermissaoEntity permissao)
        {
            permissao.DataAlteracao = DateTime.Now;

            await ExecuteNonQueryAsync(PermissaoSqlServerQuery.Atualizar, permissao);
            return permissao;
        }

        public async Task<bool> ExcluirLogicamenteAsync(int id, int usuarioId)
        {
            var permissao = new PermissaoEntity
            {
                Id = id,
                DataExclusao = DateTime.Now,
                UsuarioAlteracao = usuarioId
            };

            var rowsAffected = await ExecuteNonQueryAsync(
                PermissaoSqlServerQuery.ExcluirLogico, permissao);

            return rowsAffected > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            var permissao = new PermissaoEntity { Id = id };
            var count = await QueryAsync<int>(
                PermissaoSqlServerQuery.VerificarExistencia, permissao);

            return count != null;
        }

        public async Task<bool> ExistePorNomeAsync(string nome, int? idExcluir = null)
        {
            var permissao = new PermissaoEntity
            {
                Nome = nome,
                Id = idExcluir ?? 0
            };

            var count = await QueryAsync<int>(
                PermissaoSqlServerQuery.VerificarExistenciaNome, permissao);
            return count != null;
        }

        // Métodos para compatibilidade com interface original
        public async Task<IEnumerable<PermissaoEntity>> ListarAsync()
        {
            return await ObterTodosAsync();
        }

        public async Task<IEnumerable<PermissaoEntity>> ListarPorRecursoAsync(string recurso)
        {
            return await ObterPorRecursoAsync(recurso);
        }

        public async Task<PermissaoEntity> CriarAsync(PermissaoEntity permissao)
        {
            return await IncluirAsync(permissao);
        }

        public async Task<PermissaoEntity> AtualizarAsync(PermissaoEntity permissao)
        {
            return await AlterarAsync(permissao);
        }

        public async Task ExcluirAsync(int id)
        {
            await ExcluirLogicamenteAsync(id, 0);
        }

        public async Task RemoverLogicamenteAsync(int id)
        {
            await ExcluirLogicamenteAsync(id, 0);
        }

        public async Task<bool> ExisteNomeAsync(string nome)
        {
            return await ExistePorNomeAsync(nome);
        }
    }
}
