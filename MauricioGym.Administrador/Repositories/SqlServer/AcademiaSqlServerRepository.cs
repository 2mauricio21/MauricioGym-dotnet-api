using Dapper;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer.Queries;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;

namespace MauricioGym.Administrador.Repositories.SqlServer
{
    public class AcademiaSqlServerRepository : SqlServerRepository, IAcademiaSqlServerRepository
    {
        public AcademiaSqlServerRepository(SQLServerDbContext sqlServerDbContext)
            : base(sqlServerDbContext) { }

        public async Task<AcademiaEntity?> ObterPorIdAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var academias = await QueryAsync<AcademiaEntity>(AcademiaSqlServerQuery.ObterPorId);
            return academias.FirstOrDefault();
        }

        public async Task<AcademiaEntity?> ObterPorCnpjAsync(string cnpj)
        {
            var p = new DynamicParameters();
            p.Add("@Cnpj", cnpj);

            var academias = await QueryAsync<AcademiaEntity>(AcademiaSqlServerQuery.ObterPorCnpj);
            return academias.FirstOrDefault();
        }

        public async Task<IEnumerable<AcademiaEntity>> ListarAsync()
        {
            DynamicParameters? p = null;
            return await QueryAsync<AcademiaEntity>(AcademiaSqlServerQuery.ObterTodos, p);
        }

        public async Task<IEnumerable<AcademiaEntity>> ListarAtivosAsync()
        {
            DynamicParameters? p = null;
            return await QueryAsync<AcademiaEntity>(AcademiaSqlServerQuery.ObterAtivos, p);
        }

        public async Task<int> CriarAsync(AcademiaEntity academia)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", academia.Nome);
            p.Add("@Cnpj", academia.Cnpj);
            p.Add("@Telefone", academia.Telefone);
            p.Add("@Email", academia.Email);
            p.Add("@Endereco", academia.Endereco);
            p.Add("@Cidade", academia.Cidade);
            p.Add("@Estado", academia.Estado);
            p.Add("@Cep", academia.Cep);
            p.Add("@DataVencimentoLicenca", academia.DataVencimentoLicenca);
            p.Add("@MaximoClientes", academia.MaximoClientes);
            p.Add("@LicencaAtiva", academia.LicencaAtiva);
            p.Add("@Ativo", academia.Ativo);
            p.Add("@DataInclusao", academia.DataInclusao);
            p.Add("@UsuarioInclusao", academia.UsuarioInclusao);

            var criacao = await QueryAsync<int>(AcademiaSqlServerQuery.Criar, p);
            return criacao.FirstOrDefault();
        }

        public async Task<bool> AtualizarAsync(AcademiaEntity academia)
        {
            var p = new DynamicParameters();
            p.Add("@Id", academia.Id);
            p.Add("@Nome", academia.Nome);
            p.Add("@Cnpj", academia.Cnpj);
            p.Add("@Telefone", academia.Telefone);
            p.Add("@Email", academia.Email);
            p.Add("@Endereco", academia.Endereco);
            p.Add("@Cidade", academia.Cidade);
            p.Add("@Estado", academia.Estado);
            p.Add("@Cep", academia.Cep);
            p.Add("@DataVencimentoLicenca", academia.DataVencimentoLicenca);
            p.Add("@MaximoClientes", academia.MaximoClientes);
            p.Add("@LicencaAtiva", academia.LicencaAtiva);
            p.Add("@Ativo", academia.Ativo);
            p.Add("@DataAlteracao", academia.DataAlteracao);
            p.Add("@UsuarioAlteracao", academia.UsuarioAlteracao);

            var rowsAffected = await ExecuteNonQueryAsync(AcademiaSqlServerQuery.Atualizar);
            return rowsAffected > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            p.Add("@DataExclusao", DateTime.Now);

            var rowsAffected = await ExecuteNonQueryAsync(AcademiaSqlServerQuery.ExcluirLogico);
            return rowsAffected > 0;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var count = await QueryAsync<int>(AcademiaSqlServerQuery.VerificarExistencia, p);
            return count != null;
        }

        public async Task<bool> ExisteCnpjAsync(string cnpj, int? idExcluir = null)
        {
            var p = new DynamicParameters();
            p.Add("@Cnpj", cnpj);
            p.Add("@IdExcluir", idExcluir);

            var count = await QueryAsync<int>(AcademiaSqlServerQuery.VerificarExistenciaCnpj, p);
            return count != null;
        }

        public async Task<IEnumerable<AcademiaEntity>> ObterTodosAsync()
        {
            return await ListarAsync();
        }

        public async Task<IEnumerable<AcademiaEntity>> ObterAtivosAsync()
        {
            return await ListarAtivosAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            await RemoverAsync(id);
        }
    }
}
