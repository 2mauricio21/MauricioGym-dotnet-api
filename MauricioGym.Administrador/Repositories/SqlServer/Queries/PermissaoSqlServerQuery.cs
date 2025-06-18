namespace MauricioGym.Administrador.Repositories.SqlServer.Queries
{
    public static class PermissaoSqlServerQuery
    {
        public static string ObterPorId => @"
            SELECT Id, Nome, Descricao, Recurso, Acao, Tipo, Ativo, 
                   DataInclusao, DataAlteracao, DataExclusao,
                   UsuarioInclusao, UsuarioAlteracao
            FROM Permissao 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ObterPorNome => @"
            SELECT Id, Nome, Descricao, Recurso, Acao, Tipo, Ativo, 
                   DataInclusao, DataAlteracao, DataExclusao,
                   UsuarioInclusao, UsuarioAlteracao
            FROM Permissao 
            WHERE Nome = @Nome AND DataExclusao IS NULL";

        public static string ObterTodos => @"
            SELECT Id, Nome, Descricao, Recurso, Acao, Tipo, Ativo, 
                   DataInclusao, DataAlteracao, DataExclusao,
                   UsuarioInclusao, UsuarioAlteracao
            FROM Permissao 
            WHERE DataExclusao IS NULL
            ORDER BY Recurso, Acao";

        public static string ObterPorPapelId => @"
            SELECT p.Id, p.Nome, p.Descricao, p.Recurso, p.Acao, p.Tipo, p.Ativo, 
                   p.DataInclusao, p.DataAlteracao, p.DataExclusao,
                   p.UsuarioInclusao, p.UsuarioAlteracao
            FROM Permissao p
            INNER JOIN PapelPermissao pp ON p.Id = pp.PermissaoId
            WHERE pp.PapelId = @PapelId 
                AND p.DataExclusao IS NULL 
                AND pp.DataExclusao IS NULL
            ORDER BY p.Recurso, p.Acao";

        public static string ObterPorRecurso => @"
            SELECT Id, Nome, Descricao, Recurso, Acao, Tipo, Ativo, 
                   DataInclusao, DataAlteracao, DataExclusao,
                   UsuarioInclusao, UsuarioAlteracao
            FROM Permissao 
            WHERE Recurso = @Recurso AND DataExclusao IS NULL
            ORDER BY Acao";

        public static string ObterPorRecursoAcao => @"
            SELECT Id, Nome, Descricao, Recurso, Acao, Tipo, Ativo, 
                   DataInclusao, DataAlteracao, DataExclusao,
                   UsuarioInclusao, UsuarioAlteracao
            FROM Permissao 
            WHERE Recurso = @Recurso AND Acao = @Acao AND DataExclusao IS NULL";

        public static string Criar => @"
            INSERT INTO Permissao (Nome, Descricao, Recurso, Acao, Tipo, Ativo, 
                                 DataInclusao, UsuarioInclusao)
            OUTPUT INSERTED.Id
            VALUES (@Nome, @Descricao, @Recurso, @Acao, @Tipo, @Ativo, 
                    @DataInclusao, @UsuarioInclusao)";

        public static string Atualizar => @"
            UPDATE Permissao 
            SET Nome = @Nome,
                Descricao = @Descricao,
                Recurso = @Recurso,
                Acao = @Acao,
                Tipo = @Tipo,
                Ativo = @Ativo,
                DataAlteracao = @DataAlteracao,
                UsuarioAlteracao = @UsuarioAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ExcluirLogico => @"
            UPDATE Permissao 
            SET DataExclusao = @DataExclusao,
                UsuarioAlteracao = @UsuarioAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM Permissao 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistenciaNome => @"
            SELECT COUNT(1) 
            FROM Permissao 
            WHERE Nome = @Nome 
                AND DataExclusao IS NULL
                AND (@Id = 0 OR Id != @Id)";
    }
}
