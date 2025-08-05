namespace MauricioGym.Acesso.Repositories.SqlServer.Queries
{
    public class BloqueioAcessoSqlServerQuery
    {
        public static string IncluirBloqueioAcesso => @"
            INSERT INTO BloqueiosAcesso (
                IdUsuario, IdAcademia, Motivo, DataInicio, DataFim, Ativo
            ) VALUES (
                @IdUsuario, @IdAcademia, @Motivo, @DataInicio, @DataFim, @Ativo
            );
            SELECT CAST(SCOPE_IDENTITY() as int)";

        public static string ConsultarBloqueioAcesso => @"
            SELECT 
                IdBloqueioAcesso, IdUsuario, IdAcademia, Motivo, DataInicio,
                DataFim, Ativo
            FROM BloqueiosAcesso
            WHERE IdBloqueioAcesso = @IdBloqueioAcesso";

        public static string ConsultarBloqueiosPorUsuario => @"
            SELECT 
                IdBloqueioAcesso, IdUsuario, IdAcademia, Motivo, DataInicio,
                DataFim, Ativo
            FROM BloqueiosAcesso
            WHERE IdUsuario = @IdUsuario
            ORDER BY DataInicio DESC";

        public static string ConsultarBloqueiosPorUsuarioAcademia => @"
            SELECT 
                IdBloqueioAcesso, IdUsuario, IdAcademia, Motivo, DataInicio,
                DataFim, Ativo
            FROM BloqueiosAcesso
            WHERE IdUsuario = @IdUsuario AND IdAcademia = @IdAcademia
            ORDER BY DataInicio DESC";

        public static string ConsultarBloqueioAtivoPorUsuarioAcademia => @"
            SELECT 
                IdBloqueioAcesso, IdUsuario, IdAcademia, Motivo, DataInicio,
                DataFim, Ativo
            FROM BloqueiosAcesso
            WHERE IdUsuario = @IdUsuario AND IdAcademia = @IdAcademia 
                AND Ativo = 1 AND GETDATE() BETWEEN DataInicio AND ISNULL(DataFim, DATEADD(YEAR, 100, GETDATE()))";

        public static string AlterarBloqueioAcesso => @"
            UPDATE BloqueiosAcesso SET
                IdUsuario = @IdUsuario,
                IdAcademia = @IdAcademia,
                Motivo = @Motivo,
                DataInicio = @DataInicio,
                DataFim = @DataFim,
                Ativo = @Ativo
            WHERE IdBloqueioAcesso = @IdBloqueioAcesso";

        public static string CancelarBloqueioAcesso => @"
            UPDATE BloqueiosAcesso SET Ativo = 0 WHERE IdBloqueioAcesso = @IdBloqueioAcesso";

        public static string ListarBloqueiosAcesso => @"
            SELECT 
                IdBloqueioAcesso, IdUsuario, IdAcademia, Motivo, DataInicio,
                DataFim, Ativo
            FROM BloqueiosAcesso
            ORDER BY DataInicio DESC";

        public static string ListarBloqueiosAtivos => @"
            SELECT 
                IdBloqueioAcesso, IdUsuario, IdAcademia, Motivo, DataInicio,
                DataFim, Ativo
            FROM BloqueiosAcesso
            WHERE Ativo = 1 AND (DataFim IS NULL OR DataFim > GETDATE())
            ORDER BY DataInicio DESC";
    }
}