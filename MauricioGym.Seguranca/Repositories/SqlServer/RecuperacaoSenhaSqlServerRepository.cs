using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Seguranca.Entities;
using MauricioGym.Seguranca.Repositories.SqlServer.Interfaces;

using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Seguranca.Repositories.SqlServer
{
    public class RecuperacaoSenhaSqlServerRepository : SqlServerRepository, IRecuperacaoSenhaSqlServerRepository
    {
        public RecuperacaoSenhaSqlServerRepository(SQLServerDbContext sQLServerDbContext) : base(sQLServerDbContext)
        {
        }

        public async Task<IResultadoValidacao<int>> IncluirSolicitacaoAsync(RecuperacaoSenhaEntity recuperacao)
        {
            try
            {
                var sql = @"
                    INSERT INTO RecuperacaoSenha 
                    (Email, Token, DataSolicitacao, DataExpiracao, IdUsuario, EnderecoIP, UserAgent, Utilizado, Ativo)
                    VALUES 
                    (@Email, @Token, @DataSolicitacao, @DataExpiracao, @IdUsuario, @EnderecoIP, @UserAgent, @Utilizado, @Ativo);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = (await QueryAsync<int>(sql, recuperacao)).Single();
                return new ResultadoValidacao<int>(id);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<int>(ex, $"Erro ao incluir solicitação de recuperação: {ex.Message}");
            }
        }

        public async Task<IResultadoValidacao<RecuperacaoSenhaEntity?>> ConsultarPorTokenAsync(string token)
        {
            try
            {
                var sql = @"
                    SELECT * FROM RecuperacaoSenha 
                    WHERE Token = @Token 
                    AND DataExpiracao > GETDATE() 
                    AND Utilizado = 0 
                    AND Ativo = 1";

                var p = new DynamicParameters();
                p.Add("@Token", token);
                
                var recuperacao = (await QueryAsync<RecuperacaoSenhaEntity>(sql, p)).FirstOrDefault();
                return new ResultadoValidacao<RecuperacaoSenhaEntity?>(recuperacao);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<RecuperacaoSenhaEntity?>(ex, $"Erro ao consultar por token: {ex.Message}");
            }
        }

        public async Task<IResultadoValidacao<bool>> MarcarComoUtilizadoAsync(int idRecuperacao)
        {
            try
            {
                var sql = @"
                    UPDATE RecuperacaoSenha 
                    SET Utilizado = 1, DataUtilizacao = GETDATE()
                    WHERE IdRecuperacao = @IdRecuperacao";

                var p = new DynamicParameters();
                p.Add("@IdRecuperacao", idRecuperacao);
                
                var linhasAfetadas = await ExecuteNonQueryAsync(sql, p);
                return new ResultadoValidacao<bool>(linhasAfetadas > 0);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, $"Erro ao marcar como utilizado: {ex.Message}");
            }
        }

        public async Task<IResultadoValidacao<bool>> InvalidarSolicitacoesAnterioresAsync(int idUsuario)
        {
            try
            {
                var sql = @"
                    UPDATE RecuperacaoSenha 
                    SET Ativo = 0
                    WHERE IdUsuario = @IdUsuario 
                    AND Utilizado = 0 
                    AND Ativo = 1";

                var p = new DynamicParameters();
                p.Add("@IdUsuario", idUsuario);
                
                var linhasAfetadas = await ExecuteNonQueryAsync(sql, p);
                return new ResultadoValidacao<bool>(true); // Sempre retorna sucesso, mesmo que não tenha linhas para invalidar
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, $"Erro ao invalidar solicitações anteriores: {ex.Message}");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<RecuperacaoSenhaEntity>>> ListarSolicitacoesPorUsuarioAsync(int idUsuario)
        {
            try
            {
                var sql = @"
                    SELECT * FROM RecuperacaoSenha 
                    WHERE IdUsuario = @IdUsuario 
                    ORDER BY DataSolicitacao DESC";

                var p = new DynamicParameters();
                p.Add("@IdUsuario", idUsuario);
                
                var solicitacoes = await QueryAsync<RecuperacaoSenhaEntity>(sql, p);
                return new ResultadoValidacao<IEnumerable<RecuperacaoSenhaEntity>>(solicitacoes);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<RecuperacaoSenhaEntity>>(ex, $"Erro ao listar solicitações por usuário: {ex.Message}");
            }
        }

        public async Task<IResultadoValidacao<bool>> LimparSolicitacoesExpiradasAsync()
        {
            try
            {
                var sql = @"
                    UPDATE RecuperacaoSenha 
                    SET Ativo = 0
                    WHERE DataExpiracao < GETDATE() 
                    AND Ativo = 1";

                var linhasAfetadas = await ExecuteNonQueryAsync(sql);
                return new ResultadoValidacao<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, $"Erro ao limpar solicitações expiradas: {ex.Message}");
            }
        }
    }
}