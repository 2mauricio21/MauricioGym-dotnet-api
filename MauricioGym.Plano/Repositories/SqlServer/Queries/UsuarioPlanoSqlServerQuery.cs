namespace MauricioGym.Plano.Repositories.SqlServer.Queries
{
    public class UsuarioPlanoSqlServerQuery
    {
        public static string IncluirUsuarioPlano => @"
            INSERT INTO UsuarioPlanos (
                IdUsuario, IdPlano, DataInicio, DataFim, ValorPago, StatusPlano, Ativo, DataCadastro
            ) VALUES (
                @IdUsuario, @IdPlano, @DataInicio, @DataFim, @ValorPago, @StatusPlano, @Ativo, @DataCadastro
            );
            SELECT CAST(SCOPE_IDENTITY() as int)";

        public static string ConsultarUsuarioPlano => @"
            SELECT 
                IdUsuarioPlano, IdUsuario, IdPlano, DataInicio, DataFim, ValorPago, StatusPlano,
                Ativo, DataCadastro
            FROM UsuarioPlanos
            WHERE IdUsuarioPlano = @IdUsuarioPlano AND Ativo = 1";

        public static string ConsultarUsuarioPlanoAtivoPorUsuario => @"
            SELECT 
                IdUsuarioPlano, IdUsuario, IdPlano, DataInicio, DataFim, ValorPago, StatusPlano,
                Ativo, DataCadastro
            FROM UsuarioPlanos
            WHERE IdUsuario = @IdUsuario AND StatusPlano = 'Ativo' AND Ativo = 1";

        public static string ConsultarUsuarioPlanosPorUsuario => @"
            SELECT 
                IdUsuarioPlano, IdUsuario, IdPlano, DataInicio, DataFim, ValorPago, StatusPlano,
                Ativo, DataCadastro
            FROM UsuarioPlanos
            WHERE IdUsuario = @IdUsuario AND Ativo = 1
            ORDER BY DataInicio DESC";

        public static string AlterarUsuarioPlano => @"
            UPDATE UsuarioPlanos SET
                IdUsuario = @IdUsuario,
                IdPlano = @IdPlano,
                DataInicio = @DataInicio,
                DataFim = @DataFim,
                ValorPago = @ValorPago,
                StatusPlano = @StatusPlano,
                Ativo = @Ativo
            WHERE IdUsuarioPlano = @IdUsuarioPlano";

        public static string CancelarUsuarioPlano => @"
            UPDATE UsuarioPlanos SET
                StatusPlano = 'Cancelado',
                Ativo = 0
            WHERE IdUsuarioPlano = @IdUsuarioPlano";

        public static string ListarUsuarioPlanos => @"
            SELECT 
                IdUsuarioPlano, IdUsuario, IdPlano, DataInicio, DataFim, ValorPago, StatusPlano,
                Ativo, DataCadastro
            FROM UsuarioPlanos
            ORDER BY DataCadastro DESC";

        public static string ListarUsuarioPlanosAtivos => @"
            SELECT 
                IdUsuarioPlano, IdUsuario, IdPlano, DataInicio, DataFim, ValorPago, StatusPlano,
                Ativo, DataCadastro
            FROM UsuarioPlanos
            WHERE StatusPlano = 'Ativo' AND Ativo = 1
            ORDER BY DataInicio DESC";

        public static string ListarUsuarioPlanosPorStatus => @"
            SELECT 
                IdUsuarioPlano, IdUsuario, IdPlano, DataInicio, DataFim, ValorPago, StatusPlano,
                Ativo, DataCadastro
            FROM UsuarioPlanos
            WHERE StatusPlano = @StatusPlano AND Ativo = 1
            ORDER BY DataInicio DESC";
    }
}