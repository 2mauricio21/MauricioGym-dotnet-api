namespace MauricioGym.Seguranca.Repositories.SqlServer.Queries
{
    public class AutenticacaoSqlServerQuery
    {
        public static string IncluirAutenticacao => @"
            INSERT INTO Autenticacao 
            (Email, Senha, IdUsuario, TentativasLogin, ContaBloqueada, DataCriacao, Ativo)
            VALUES 
            (@Email, @Senha, @IdUsuario, @TentativasLogin, @ContaBloqueada, @DataCriacao, @Ativo);
            SELECT CAST(SCOPE_IDENTITY() as int)";

        public static readonly string ConsultarAutenticacaoPorEmail = @"
            SELECT 
                IdAutenticacao, IdUsuario, Email, Senha, TentativasLogin, 
                ContaBloqueada, DataCriacao, DataUltimoLogin, DataUltimaTentativa, 
                DataBloqueio, RefreshToken, DataExpiracaoRefreshToken, Ativo, TokenRecuperacao
            FROM Autenticacao 
            WHERE Email = @Email AND Ativo = 1";

        public static string ConsultarAutenticacaoPorUsuario => @"
            SELECT * FROM Autenticacao 
            WHERE IdUsuario = @IdUsuario AND Ativo = 1";

        public static string AlterarSenha => @"
            UPDATE Autenticacao 
            SET Senha = @NovaSenha, TentativasLogin = 0, ContaBloqueada = 0, DataBloqueio = NULL
            WHERE IdUsuario = @IdUsuario";

        public static string AtualizarTentativasLogin => @"
            UPDATE Autenticacao 
            SET TentativasLogin = @Tentativas, DataUltimaTentativa = GETDATE()
            WHERE IdUsuario = @IdUsuario";

        public static string BloquearConta => @"
            UPDATE Autenticacao 
            SET ContaBloqueada = 1, DataBloqueio = GETDATE()
            WHERE IdUsuario = @IdUsuario";

        public static string DesbloquearConta => @"
            UPDATE Autenticacao 
            SET ContaBloqueada = 0, TentativasLogin = 0, DataBloqueio = NULL
            WHERE IdUsuario = @IdUsuario";

        public static string AtualizarUltimoLogin => @"
            UPDATE Autenticacao 
            SET DataUltimoLogin = GETDATE(), TentativasLogin = 0
            WHERE IdUsuario = @IdUsuario";

        public static string AtualizarRefreshToken => @"
            UPDATE Autenticacao 
            SET RefreshToken = @RefreshToken, DataExpiracaoRefreshToken = @DataExpiracao
            WHERE IdUsuario = @IdUsuario";

        public static string RemoverRefreshToken => @"
            UPDATE Autenticacao 
            SET RefreshToken = NULL, DataExpiracaoRefreshToken = NULL
            WHERE IdUsuario = @IdUsuario";

        public static string ConsultarPorRefreshToken => @"
            SELECT * FROM Autenticacao 
            WHERE RefreshToken = @RefreshToken 
            AND DataExpiracaoRefreshToken > GETDATE() 
            AND Ativo = 1";
    }
}