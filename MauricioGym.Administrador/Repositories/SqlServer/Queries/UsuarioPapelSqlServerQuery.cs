namespace MauricioGym.Administrador.Repositories.SqlServer.Queries
{
    public static class UsuarioPapelSqlServerQuery
    {
        public static string ObterPorId => @"
            SELECT Id, IdUsuario, IdPapel, DataInclusao, DataAlteracao, DataExclusao, Ativo
            FROM UsuarioPapel 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ObterPorUsuarioPapel => @"
            SELECT Id, IdUsuario, IdPapel, DataInclusao, DataAlteracao, DataExclusao, Ativo
            FROM UsuarioPapel 
            WHERE IdUsuario = @UsuarioId AND IdPapel = @PapelId AND DataExclusao IS NULL";

        public static string ListarPorUsuario => @"
            SELECT Id, IdUsuario, IdPapel, DataInclusao, DataAlteracao, DataExclusao, Ativo
            FROM UsuarioPapel 
            WHERE IdUsuario = @UsuarioId AND DataExclusao IS NULL
            ORDER BY IdPapel";

        public static string ListarPorPapel => @"
            SELECT Id, IdUsuario, IdPapel, DataInclusao, DataAlteracao, DataExclusao, Ativo
            FROM UsuarioPapel 
            WHERE IdPapel = @PapelId AND DataExclusao IS NULL
            ORDER BY IdUsuario";

        public static string ListarPapeisDoUsuario => @"
            SELECT p.Id, p.Nome, p.Descricao, p.Ativo, p.DataInclusao, p.DataAlteracao, p.DataExclusao
            FROM Papel p
            INNER JOIN UsuarioPapel up ON p.Id = up.IdPapel
            WHERE up.IdUsuario = @UsuarioId AND up.DataExclusao IS NULL AND p.DataExclusao IS NULL
            ORDER BY p.Nome";

        public static string Criar => @"
            INSERT INTO UsuarioPapel (IdUsuario, IdPapel, DataInclusao, Ativo)
            OUTPUT INSERTED.Id
            VALUES (@IdUsuario, @IdPapel, @DataInclusao, @Ativo)";

        public static string ExcluirLogico => @"
            UPDATE UsuarioPapel 
            SET DataExclusao = @DataExclusao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM UsuarioPapel 
            WHERE IdUsuario = @UsuarioId AND IdPapel = @PapelId AND DataExclusao IS NULL";
    }
}