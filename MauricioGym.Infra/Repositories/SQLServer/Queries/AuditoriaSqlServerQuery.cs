namespace MauricioGym.Infra.Repositories.SqlServer.Queries
{
    public static class AuditoriaSqlServerQuery
    {
        public static string ObterPorUsuario => @"
            SELECT IdAuditoria, IdUsuario, Descricao, Data
            FROM Auditorias 
            WHERE IdUsuario = @IdUsuario
            ORDER BY Data DESC";

        public static string ObterPorPeriodo => @"
            SELECT IdAuditoria, IdUsuario, Descricao, Data
            FROM Auditorias 
            WHERE Data >= @Data 
                AND Data <= @DataFim
            ORDER BY Data DESC";

        public static string ObterTodos => @"
            SELECT IdAuditoria, IdUsuario, Descricao, Data
            FROM Auditorias 
            ORDER BY Data DESC";

        public static string Criar => @"
            INSERT INTO Auditorias (IdUsuario, Descricao, Data)
            VALUES (@IdUsuario, @Descricao, @Data);
            SET @IdAuditoria = SCOPE_IDENTITY();";
    }
}