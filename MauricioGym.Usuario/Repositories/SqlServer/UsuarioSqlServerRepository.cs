using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.SqlServer.Interfaces;
using MauricioGym.Usuario.Repositories.SqlServer.Queries;

namespace MauricioGym.Usuario.Repositories.SqlServer
{
    public class UsuarioSqlServerRepository : SqlServerRepository, IUsuarioSqlServerRepository
    {
        public UsuarioSqlServerRepository(SQLServerDbContext sQLServerDbContext) : base(sQLServerDbContext)
        {
        }

        public async Task<UsuarioEntity> IncluirUsuarioAsync(UsuarioEntity usuario)
        {
            usuario.IdUsuario = (await QueryAsync<int>(UsuarioSqlServerQuery.IncluirUsuario, usuario)).Single();
            return usuario;
        }

        public async Task<UsuarioEntity> ConsultarUsuarioAsync(int idUsuario)
        {
            var p = new DynamicParameters();
            p.Add("@IdUsuario", idUsuario);
            
            var entity = await QueryAsync<UsuarioEntity>(UsuarioSqlServerQuery.ConsultarUsuario, p);
            return entity.FirstOrDefault();
        }

        public async Task<UsuarioEntity> ConsultarUsuarioPorEmailAsync(string email)
        {
            var p = new DynamicParameters();
            p.Add("@Email", email);
            
            var entity = await QueryAsync<UsuarioEntity>(UsuarioSqlServerQuery.ConsultarUsuarioPorEmail, p);
            return entity.FirstOrDefault();
        }

        public async Task<UsuarioEntity> ConsultarUsuarioPorCPFAsync(string cpf)
        {
            var p = new DynamicParameters();
            p.Add("@CPF", cpf);
            
            var entity = await QueryAsync<UsuarioEntity>(UsuarioSqlServerQuery.ConsultarUsuarioPorCPF, p);
            return entity.FirstOrDefault();
        }

        public async Task AlterarUsuarioAsync(UsuarioEntity usuario)
        {
            await ExecuteNonQueryAsync(UsuarioSqlServerQuery.AlterarUsuario, usuario);
        }

        public async Task ExcluirUsuarioAsync(int idUsuario)
        {
            var p = new DynamicParameters();
            p.Add("@IdUsuario", idUsuario);
            
            await ExecuteNonQueryAsync(UsuarioSqlServerQuery.ExcluirUsuario, p);
        }

        public async Task<IEnumerable<UsuarioEntity>> ListarUsuariosAsync()
        {
            return await QueryAsync<UsuarioEntity>(UsuarioSqlServerQuery.ListarUsuarios);
        }

        public async Task<IEnumerable<UsuarioEntity>> ListarUsuariosAtivosAsync()
        {
            return await QueryAsync<UsuarioEntity>(UsuarioSqlServerQuery.ListarUsuariosAtivos);
        }
    }
}