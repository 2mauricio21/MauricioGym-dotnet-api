using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Seguranca.Entities;
using MauricioGym.Seguranca.Repositories.SqlServer.Interfaces;
using MauricioGym.Seguranca.Repositories.SqlServer.Queries;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Seguranca.Repositories.SqlServer
{
    /// <summary>
    /// Repositório para operações de autenticação no SQL Server
    /// </summary>
    public class AutenticacaoSqlServerRepository : SqlServerRepository, IAutenticacaoSqlServerRepository
    {
        public AutenticacaoSqlServerRepository(SQLServerDbContext sQLServerDbContext) : base(sQLServerDbContext)
        {
        }

        public async Task<IResultadoValidacao<int>> IncluirAutenticacaoAsync(AutenticacaoEntity autenticacao)
        {
            try
            {
                var sql = AutenticacaoSqlServerQuery.IncluirAutenticacao;

                var p = new DynamicParameters();
                p.Add("@Email", autenticacao.Email);
                p.Add("@Senha", autenticacao.Senha);
                p.Add("@IdUsuario", autenticacao.IdUsuario);
                p.Add("@TentativasLogin", autenticacao.TentativasLogin);
                p.Add("@ContaBloqueada", autenticacao.ContaBloqueada);
                p.Add("@DataCriacao", autenticacao.DataCriacao);
                p.Add("@Ativo", autenticacao.Ativo);
                
                var idAutenticacao = (await QueryAsync<int>(sql, p)).Single();
                return new ResultadoValidacao<int>(idAutenticacao);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<int>(ex, $"Erro ao incluir autenticação: {ex.Message}");
            }
        }

        public async Task<IResultadoValidacao<AutenticacaoEntity?>> ConsultarAutenticacaoPorEmailAsync(string email)
        {
            try
            {
                var sql = @"SELECT 
                    IdAutenticacao, IdUsuario, Email, Senha, TentativasLogin, 
                    ContaBloqueada, DataCriacao, DataUltimoLogin, DataUltimaTentativa, 
                    DataBloqueio, RefreshToken, DataExpiracaoRefreshToken, Ativo, TokenRecuperacao
                FROM Autenticacao 
                WHERE Email = @Email AND Ativo = 1";

                var p = new DynamicParameters();
                p.Add("@Email", email);
                
                var autenticacao = (await QueryAsync<AutenticacaoEntity>(sql, p)).FirstOrDefault();
                return new ResultadoValidacao<AutenticacaoEntity?>(autenticacao);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<AutenticacaoEntity?>(ex, $"Erro ao consultar autenticação por email: {ex.Message}");
            }
        }

        public async Task<IResultadoValidacao<AutenticacaoEntity?>> ConsultarAutenticacaoPorUsuarioAsync(int idUsuario)
        {
            try
            {
                var sql = @"SELECT 
                    IdAutenticacao, IdUsuario, Email, Senha, TentativasLogin, 
                    ContaBloqueada, DataCriacao, DataUltimoLogin, DataUltimaTentativa, 
                    DataBloqueio, RefreshToken, DataExpiracaoRefreshToken, Ativo, TokenRecuperacao
                FROM Autenticacao 
                WHERE IdUsuario = @IdUsuario AND Ativo = 1";

                var p = new DynamicParameters();
                p.Add("@IdUsuario", idUsuario);
                
                var autenticacao = (await QueryAsync<AutenticacaoEntity>(sql, p)).FirstOrDefault();
                return new ResultadoValidacao<AutenticacaoEntity?>(autenticacao);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<AutenticacaoEntity?>(ex, $"Erro ao consultar autenticação por usuário: {ex.Message}");
            }
        }

        public async Task<IResultadoValidacao<bool>> AlterarSenhaAsync(int idUsuario, string novaSenha)
        {
            try
            {
                var sql = @"UPDATE Autenticacao 
                    SET Senha = @NovaSenha
                    WHERE IdUsuario = @IdUsuario";

                var p = new DynamicParameters();
                p.Add("@NovaSenha", novaSenha);
                p.Add("@IdUsuario", idUsuario);
                
                var linhasAfetadas = await ExecuteNonQueryAsync(sql, p);
                return new ResultadoValidacao<bool>(linhasAfetadas > 0);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, $"Erro ao alterar senha: {ex.Message}");
            }
        }

        public async Task<IResultadoValidacao<bool>> AtualizarTentativasLoginAsync(int idUsuario, int tentativas)
        {
            try
            {
                var sql = @"UPDATE Autenticacao 
                    SET TentativasLogin = @Tentativas, DataUltimaTentativa = GETDATE()
                    WHERE IdUsuario = @IdUsuario";

                var p = new DynamicParameters();
                p.Add("@Tentativas", tentativas);
                p.Add("@IdUsuario", idUsuario);
                
                var linhasAfetadas = await ExecuteNonQueryAsync(sql, p);
                return new ResultadoValidacao<bool>(linhasAfetadas > 0);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, $"Erro ao atualizar tentativas de login: {ex.Message}");
            }
        }

        public async Task<IResultadoValidacao<bool>> BloquearContaAsync(int idUsuario)
        {
            try
            {
                var sql = @"UPDATE Autenticacao 
                    SET ContaBloqueada = 1, DataBloqueio = GETDATE()
                    WHERE IdUsuario = @IdUsuario";

                var p = new DynamicParameters();
                p.Add("@IdUsuario", idUsuario);
                
                var linhasAfetadas = await ExecuteNonQueryAsync(sql, p);
                return new ResultadoValidacao<bool>(linhasAfetadas > 0);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, $"Erro ao bloquear conta: {ex.Message}");
            }
        }

        public async Task<IResultadoValidacao<bool>> DesbloquearContaAsync(int idUsuario)
        {
            try
            {
                var sql = @"UPDATE Autenticacao 
                    SET ContaBloqueada = 0, TentativasLogin = 0, DataBloqueio = NULL
                    WHERE IdUsuario = @IdUsuario";

                var p = new DynamicParameters();
                p.Add("@IdUsuario", idUsuario);
                
                var linhasAfetadas = await ExecuteNonQueryAsync(sql, p);
                return new ResultadoValidacao<bool>(linhasAfetadas > 0);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, $"Erro ao desbloquear conta: {ex.Message}");
            }
        }

        public async Task<IResultadoValidacao<bool>> AtualizarUltimoLoginAsync(int idUsuario)
        {
            try
            {
                var sql = @"UPDATE Autenticacao 
                    SET DataUltimoLogin = GETDATE()
                    WHERE IdUsuario = @IdUsuario";

                var p = new DynamicParameters();
                p.Add("@IdUsuario", idUsuario);
                
                var linhasAfetadas = await ExecuteNonQueryAsync(sql, p);
                return new ResultadoValidacao<bool>(linhasAfetadas > 0);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, $"Erro ao atualizar último login: {ex.Message}");
            }
        }

        public async Task<IResultadoValidacao<bool>> AtualizarRefreshTokenAsync(int idUsuario, string refreshToken, DateTime dataExpiracao)
        {
            try
            {
                var sql = @"UPDATE Autenticacao 
                    SET RefreshToken = @RefreshToken, DataExpiracaoRefreshToken = @DataExpiracao
                    WHERE IdUsuario = @IdUsuario";

                var p = new DynamicParameters();
                p.Add("@RefreshToken", refreshToken);
                p.Add("@DataExpiracao", dataExpiracao);
                p.Add("@IdUsuario", idUsuario);
                
                var linhasAfetadas = await ExecuteNonQueryAsync(sql, p);
                return new ResultadoValidacao<bool>(linhasAfetadas > 0);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, $"Erro ao atualizar refresh token: {ex.Message}");
            }
        }

        public async Task<IResultadoValidacao<bool>> RemoverRefreshTokenAsync(int idUsuario)
        {
            try
            {
                var sql = @"UPDATE Autenticacao 
                    SET RefreshToken = NULL, DataExpiracaoRefreshToken = NULL
                    WHERE IdUsuario = @IdUsuario";

                var p = new DynamicParameters();
                p.Add("@IdUsuario", idUsuario);
                
                var linhasAfetadas = await ExecuteNonQueryAsync(sql, p);
                return new ResultadoValidacao<bool>(linhasAfetadas > 0);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, $"Erro ao remover refresh token: {ex.Message}");
            }
        }

        public async Task<IResultadoValidacao<AutenticacaoEntity?>> ConsultarPorRefreshTokenAsync(string refreshToken)
        {
            try
            {
                var sql = @"SELECT 
                    IdAutenticacao, IdUsuario, Email, Senha, TentativasLogin, 
                    ContaBloqueada, DataCriacao, DataUltimoLogin, DataUltimaTentativa, 
                    DataBloqueio, RefreshToken, DataExpiracaoRefreshToken, Ativo, TokenRecuperacao
                FROM Autenticacao 
                WHERE RefreshToken = @RefreshToken AND Ativo = 1";

                var p = new DynamicParameters();
                p.Add("@RefreshToken", refreshToken);
                
                var autenticacao = (await QueryAsync<AutenticacaoEntity>(sql, p)).FirstOrDefault();
                return new ResultadoValidacao<AutenticacaoEntity?>(autenticacao);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<AutenticacaoEntity?>(ex, $"Erro ao consultar por refresh token: {ex.Message}");
            }
        }
    }
}