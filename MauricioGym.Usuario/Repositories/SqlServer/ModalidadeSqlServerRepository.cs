using Dapper;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.SqlServer.Interfaces;
using MauricioGym.Usuario.Repositories.SqlServer.Queries;

namespace MauricioGym.Usuario.Repositories.SqlServer
{
    public class ModalidadeSqlServerRepository : SqlServerRepository, IModalidadeSqlServerRepository
    {
        public ModalidadeSqlServerRepository(SQLServerDbContext sqlServerDbContext)
            : base(sqlServerDbContext) { }

        public async Task<ModalidadeEntity?> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entidades = await QueryAsync<ModalidadeEntity>(ModalidadeSqlServerQuery.ObterPorId, p);
            return entidades.FirstOrDefault();
        }

        public async Task<ModalidadeEntity?> ObterPorNomeAsync(string nome)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", nome);

            var entidades = await QueryAsync<ModalidadeEntity>(ModalidadeSqlServerQuery.ObterPorNome, p);
            return entidades.FirstOrDefault();
        }

        public async Task<IEnumerable<ModalidadeEntity>> ObterTodosAsync()
        {
            return await ListarAsync();
        }

        public async Task<IEnumerable<ModalidadeEntity>> ListarAsync()
        {
            DynamicParameters? p = null;
            return await QueryAsync<ModalidadeEntity>(ModalidadeSqlServerQuery.ObterTodos, p);
        }

        public async Task<IEnumerable<ModalidadeEntity>> ListarAtivasAsync()
        {
            DynamicParameters? p = null;
            return await QueryAsync<ModalidadeEntity>(ModalidadeSqlServerQuery.ListarAtivas, p);
        }

        public async Task<IEnumerable<ModalidadeEntity>> ObterPorAcademiaIdAsync(int academiaId)
        {
            var p = new DynamicParameters();
            p.Add("@AcademiaId", academiaId);

            return await QueryAsync<ModalidadeEntity>(ModalidadeSqlServerQuery.ObterPorAcademiaId, p);
        }

        public async Task<int> CriarAsync(ModalidadeEntity modalidade)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", modalidade.Nome);
            p.Add("@Descricao", modalidade.Descricao);
            p.Add("@AcademiaId", modalidade.AcademiaId);
            p.Add("@Ativo", modalidade.Ativo);
            p.Add("@DataInclusao", modalidade.DataInclusao);
            p.Add("@UsuarioInclusao", modalidade.UsuarioInclusao);

            var entidade = await QueryAsync<int>(ModalidadeSqlServerQuery.Criar, p);
            return entidade.FirstOrDefault();
        }

        public async Task<bool> AtualizarAsync(ModalidadeEntity modalidade)
        {
            var p = new DynamicParameters();
            p.Add("@Id", modalidade.Id);
            p.Add("@Nome", modalidade.Nome);
            p.Add("@Descricao", modalidade.Descricao);
            p.Add("@AcademiaId", modalidade.AcademiaId);
            p.Add("@Ativo", modalidade.Ativo);
            p.Add("@DataAlteracao", modalidade.DataAlteracao);
            p.Add("@UsuarioAlteracao", modalidade.UsuarioAlteracao);

            var rowsAffected = await ExecuteNonQueryAsync(ModalidadeSqlServerQuery.Atualizar, p);
            return rowsAffected > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@DataExclusao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(ModalidadeSqlServerQuery.ExcluirLogico, p);
            return rowsAffected > 0;
        }

        public async Task ExcluirAsync(int id)
        {
            await RemoverAsync(id);
        }

        public async Task<bool> ExisteAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var count = await QueryAsync<int>(ModalidadeSqlServerQuery.VerificarExistencia, p);
            return count != null;
        }

        public async Task<bool> ExisteNomeAsync(string nome, int academiaId, int? idExcluir = null)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", nome);
            p.Add("@AcademiaId", academiaId);
            p.Add("@IdExcluir", idExcluir);

            var count = await QueryAsync<int>(ModalidadeSqlServerQuery.VerificarExistenciaNome, p);
            return count != null;
        }
    }
}
