namespace MauricioGym.Administrador.Repositories.SqlServer.Queries
{
    public static class PapelSqlServerQuery
    {
        public static string ObterPorId => @"
            SELECT Id, Nome, Descricao, EhSistema, Ativo, DataInclusao, DataAlteracao, DataExclusao, 
                   UsuarioInclusao, UsuarioAlteracao
            FROM Papel 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ObterPorNome => @"
            SELECT Id, Nome, Descricao, EhSistema, Ativo, DataInclusao, DataAlteracao, DataExclusao, 
                   UsuarioInclusao, UsuarioAlteracao
            FROM Papel 
            WHERE Nome = @Nome AND DataExclusao IS NULL";

        public static string ObterTodos => @"
            SELECT Id, Nome, Descricao, EhSistema, Ativo, DataInclusao, DataAlteracao, DataExclusao, 
                   UsuarioInclusao, UsuarioAlteracao
            FROM Papel 
            WHERE DataExclusao IS NULL
            ORDER BY Nome";

        public static string ObterPorAdministradorId => @"
            SELECT p.Id, p.Nome, p.Descricao, p.EhSistema, p.Ativo, p.DataInclusao, p.DataAlteracao, p.DataExclusao, 
                   p.UsuarioInclusao, p.UsuarioAlteracao
            FROM Papel p
            INNER JOIN AdministradorPapel ap ON p.Id = ap.PapelId
            WHERE ap.AdministradorId = @Id 
                AND p.DataExclusao IS NULL 
                AND ap.DataExclusao IS NULL
            ORDER BY p.Nome";

        public static string Criar => @"
            INSERT INTO Papel (Nome, Descricao, EhSistema, Ativo, DataInclusao, UsuarioInclusao)
            OUTPUT INSERTED.Id
            VALUES (@Nome, @Descricao, @EhSistema, @Ativo, @DataInclusao, @UsuarioInclusao)";

        public static string Atualizar => @"
            UPDATE Papel 
            SET Nome = @Nome,
                Descricao = @Descricao,
                EhSistema = @EhSistema,
                Ativo = @Ativo,
                DataAlteracao = @DataAlteracao,
                UsuarioAlteracao = @UsuarioAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ExcluirLogico => @"
            UPDATE Papel 
            SET DataExclusao = @DataExclusao,
                UsuarioAlteracao = @UsuarioAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM Papel 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistenciaNome => @"
            SELECT COUNT(1) 
            FROM Papel 
            WHERE Nome = @Nome 
                AND DataExclusao IS NULL
                AND (@Id = 0 OR Id != @Id)";
    }
}
