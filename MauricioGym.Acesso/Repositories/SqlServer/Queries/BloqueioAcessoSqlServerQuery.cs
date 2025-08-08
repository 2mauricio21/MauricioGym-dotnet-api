namespace MauricioGym.Acesso.Repositories.SqlServer.Queries
{
    public class BloqueioAcessoSqlServerQuery
    {
        public static string IncluirBloqueioAcesso => @"
            INSERT INTO BloqueiosAcesso (
                IdUsuario, IdAcademia, MotivoBloqueio, DataInicioBloqueio, DataFimBloqueio, ObservacaoBloqueio, Ativo, IdUsuarioResponsavel
            ) VALUES (
                @IdUsuario, @IdAcademia, @MotivoBloqueio, @DataInicioBloqueio, @DataFimBloqueio, @ObservacaoBloqueio, @Ativo, @IdUsuarioResponsavel
            );
            SELECT CAST(SCOPE_IDENTITY() as int)";

        public static string ConsultarBloqueioAcesso => @"
            SELECT 
                IdBloqueioAcesso, IdUsuario, IdAcademia, MotivoBloqueio, DataInicioBloqueio,
                DataFimBloqueio, ObservacaoBloqueio, Ativo, IdUsuarioResponsavel
            FROM BloqueiosAcesso
            WHERE IdBloqueioAcesso = @IdBloqueioAcesso";

        public static string ConsultarBloqueiosPorUsuario => @"
            SELECT 
                IdBloqueioAcesso, IdUsuario, IdAcademia, MotivoBloqueio, DataInicioBloqueio,
                DataFimBloqueio, ObservacaoBloqueio, Ativo, IdUsuarioResponsavel
            FROM BloqueiosAcesso
            WHERE IdUsuario = @IdUsuario
            ORDER BY DataInicioBloqueio DESC";

        public static string ConsultarBloqueiosPorUsuarioAcademia => @"
            SELECT 
                IdBloqueioAcesso, IdUsuario, IdAcademia, MotivoBloqueio, DataInicioBloqueio,
                DataFimBloqueio, ObservacaoBloqueio, Ativo, IdUsuarioResponsavel
            FROM BloqueiosAcesso
            WHERE IdUsuario = @IdUsuario AND IdAcademia = @IdAcademia
            ORDER BY DataInicioBloqueio DESC";

        public static string ConsultarBloqueioAtivoPorUsuarioAcademia => @"
            SELECT 
                IdBloqueioAcesso, IdUsuario, IdAcademia, MotivoBloqueio, DataInicioBloqueio,
                DataFimBloqueio, ObservacaoBloqueio, Ativo, IdUsuarioResponsavel
            FROM BloqueiosAcesso
            WHERE IdUsuario = @IdUsuario AND IdAcademia = @IdAcademia 
                AND Ativo = 1 AND GETDATE() BETWEEN DataInicioBloqueio AND ISNULL(DataFimBloqueio, DATEADD(YEAR, 100, GETDATE()))";

        public static string AlterarBloqueioAcesso => @"
            UPDATE BloqueiosAcesso SET
                IdUsuario = @IdUsuario,
                IdAcademia = @IdAcademia,
                MotivoBloqueio = @MotivoBloqueio,
                DataInicioBloqueio = @DataInicioBloqueio,
                DataFimBloqueio = @DataFimBloqueio,
                ObservacaoBloqueio = @ObservacaoBloqueio,
                Ativo = @Ativo,
                IdUsuarioResponsavel = @IdUsuarioResponsavel
            WHERE IdBloqueioAcesso = @IdBloqueioAcesso";

        public static string CancelarBloqueioAcesso => @"
            UPDATE BloqueiosAcesso SET Ativo = 0 WHERE IdBloqueioAcesso = @IdBloqueioAcesso";

        public static string ListarBloqueiosAcesso => @"
            SELECT 
                IdBloqueioAcesso, IdUsuario, IdAcademia, MotivoBloqueio, DataInicioBloqueio,
                DataFimBloqueio, ObservacaoBloqueio, Ativo, IdUsuarioResponsavel
            FROM BloqueiosAcesso
            ORDER BY DataInicioBloqueio DESC";

        public static string ListarBloqueiosAtivos => @"
            SELECT 
                IdBloqueioAcesso, IdUsuario, IdAcademia, MotivoBloqueio, DataInicioBloqueio,
                DataFimBloqueio, ObservacaoBloqueio, Ativo, IdUsuarioResponsavel
            FROM BloqueiosAcesso
            WHERE Ativo = 1 AND (DataFimBloqueio IS NULL OR DataFimBloqueio > GETDATE())
            ORDER BY DataInicioBloqueio DESC";
    }
}