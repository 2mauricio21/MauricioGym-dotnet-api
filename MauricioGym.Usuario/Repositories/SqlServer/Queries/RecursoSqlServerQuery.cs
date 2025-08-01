namespace MauricioGym.Usuario.Repositories.SqlServer.Queries
{
    public class RecursoSqlServerQuery
    {
        public static string IncluirRecurso => @"
            INSERT INTO Recursos (
                Nome, Descricao, Codigo, Ativo
            ) VALUES (
                @Nome, @Descricao, @Codigo, @Ativo
            );
            SELECT CAST(SCOPE_IDENTITY() as int)";

        public static string ConsultarRecurso => @"
            SELECT 
                IdRecurso, Nome, Descricao, Codigo, Ativo
            FROM Recursos
            WHERE IdRecurso = @IdRecurso AND Ativo = 1";

        public static string ConsultarRecursoPorCodigo => @"
            SELECT 
                IdRecurso, Nome, Descricao, Codigo, Ativo
            FROM Recursos
            WHERE Codigo = @Codigo AND Ativo = 1";

        public static string AlterarRecurso => @"
            UPDATE Recursos SET
                Nome = @Nome,
                Descricao = @Descricao,
                Codigo = @Codigo,
                Ativo = @Ativo
            WHERE IdRecurso = @IdRecurso";

        public static string ExcluirRecurso => @"
            UPDATE Recursos SET Ativo = 0 WHERE IdRecurso = @IdRecurso";

        public static string ListarRecursos => @"
            SELECT 
                IdRecurso, Nome, Descricao, Codigo, Ativo
            FROM Recursos
            ORDER BY Nome";

        public static string ListarRecursosAtivos => @"
            SELECT 
                IdRecurso, Nome, Descricao, Codigo, Ativo
            FROM Recursos
            WHERE Ativo = 1
            ORDER BY Nome";
    }
}