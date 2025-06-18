namespace MauricioGym.Administrador.Repositories.SqlServer.Queries
{
    public static class AdministradorPapelSqlServerQuery
    {

        public static string ListarAdministradoresPapeis => @"
                SELECT a.Id, a.Nome, a.Email, a.Cpf, a.Telefone, a.Ativo, 
                       a.DataInclusao, a.DataAlteracao, a.DataExclusao,
                       a.UsuarioInclusao, a.UsuarioAlteracao
                FROM Administrador a
                INNER JOIN AdministradorPapel ap ON a.Id = ap.AdministradorId
                WHERE ap.PapelId = @PapelId 
                    AND a.DataExclusao IS NULL 
                    AND ap.DataExclusao IS NULL
                ORDER BY a.Nome";
        public static string ObterPorId => @"
            SELECT Id, AdministradorId, PapelId, DataInclusao, DataExclusao,
                   UsuarioInclusao, UsuarioAlteracao
            FROM AdministradorPapel 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ObterPorAdministradorEPapel => @"
            SELECT Id, AdministradorId, PapelId, DataInclusao, DataExclusao,
                   UsuarioInclusao, UsuarioAlteracao
            FROM AdministradorPapel 
            WHERE AdministradorId = @AdministradorId 
                AND PapelId = @PapelId 
                AND DataExclusao IS NULL";

        public static string ObterPorAdministradorId => @"
            SELECT Id, AdministradorId, PapelId, DataInclusao, DataExclusao,
                   UsuarioInclusao, UsuarioAlteracao
            FROM AdministradorPapel 
            WHERE AdministradorId = @AdministradorId AND DataExclusao IS NULL
            ORDER BY DataInclusao";

        public static string ObterPorPapelId => @"
            SELECT Id, AdministradorId, PapelId, DataInclusao, DataExclusao,
                   UsuarioInclusao, UsuarioAlteracao
            FROM AdministradorPapel 
            WHERE PapelId = @PapelId AND DataExclusao IS NULL
            ORDER BY DataInclusao";

        public static string Criar => @"
            INSERT INTO AdministradorPapel (AdministradorId, PapelId, DataInclusao, UsuarioInclusao)
            OUTPUT INSERTED.Id
            VALUES (@AdministradorId, @PapelId, @DataInclusao, @UsuarioInclusao)";

        public static string ExcluirLogico => @"
            UPDATE AdministradorPapel 
            SET DataExclusao = @DataExclusao,
                UsuarioAlteracao = @UsuarioAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ExcluirLogicoPorAdministradorEPapel => @"
            UPDATE AdministradorPapel 
            SET DataExclusao = @DataExclusao,
                UsuarioAlteracao = @UsuarioAlteracao
            WHERE AdministradorId = @AdministradorId 
                AND PapelId = @PapelId 
                AND DataExclusao IS NULL";

        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM AdministradorPapel 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistenciaAssociacao => @"
            SELECT COUNT(1) 
            FROM AdministradorPapel 
            WHERE AdministradorId = @AdministradorId 
                AND PapelId = @PapelId 
                AND DataExclusao IS NULL";
    }
}
