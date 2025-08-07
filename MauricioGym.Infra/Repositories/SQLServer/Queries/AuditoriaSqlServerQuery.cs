namespace MauricioGym.Infra.Repositories.SqlServer.Queries
{
    public static class AuditoriaSqlServerQuery
    {
        public static string INSERT => @"
            INSERT INTO Auditoria (IdUsuario, Descricao, Data)
            OUTPUT INSERTED.IdAuditoria
            VALUES (@IdUsuario, @Descricao, @Data)";

        public static string SELECT_BY_ID => @"
            SELECT IdAuditoria, IdUsuario, Descricao, Data
            FROM Auditoria
            WHERE IdAuditoria = @IdAuditoria";

        public static string SELECT_ALL => @"
            SELECT IdAuditoria, IdUsuario, Descricao, Data
            FROM Auditoria
            ORDER BY Data DESC";

        public static string SELECT_BY_USUARIO => @"
            SELECT IdAuditoria, IdUsuario, Descricao, Data
            FROM Auditoria
            WHERE IdUsuario = @IdUsuario
            ORDER BY Data DESC";
    }
}