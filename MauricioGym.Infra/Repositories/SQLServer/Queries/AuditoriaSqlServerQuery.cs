namespace MauricioGym.Infra.Repositories.SqlServer.Queries
{
    public static class AuditoriaSqlServerQuery
    {
        public static string ConsultarIdAuditoria => @"
            SELECT IdAuditoria, IdUsuario, Descricao, Data
            FROM Auditorias 
            WHERE IdAuditoria = @IdAuditoria";

        public static string CriarAuditoria => @"

            INSERT INTO Auditorias (IdUsuario, Descricao, Data)
            VALUES (@IdUsuario, @Descricao, @Data);
            SET @IdAuditoria = SCOPE_IDENTITY();";
    }
}