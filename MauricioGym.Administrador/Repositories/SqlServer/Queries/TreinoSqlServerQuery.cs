namespace MauricioGym.Administrador.Repositories.SqlServer.Queries
{
    public static class TreinoSqlServerQuery
    {
        public static string ListarPorId => @"
            SELECT Id, Nome, Descricao, AlunoId, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Treino 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ListarPorNome => @"
            SELECT Id, Nome, Descricao, AlunoId, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Treino 
            WHERE Nome = @Nome AND DataExclusao IS NULL";

        public static string ListarPorAluno => @"
            SELECT Id, Nome, Descricao, AlunoId, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Treino 
            WHERE AlunoId = @AlunoId AND DataExclusao IS NULL
            ORDER BY Nome";

        public static string ListarTodos => @"
            SELECT Id, Nome, Descricao, AlunoId, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Treino 
            WHERE DataExclusao IS NULL
            ORDER BY Nome";

        public static string ListarAtivos => @"
            SELECT Id, Nome, Descricao, AlunoId, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Treino 
            WHERE Ativo = 1 AND DataExclusao IS NULL
            ORDER BY Nome";

        public static string ListarPorNivel => @"
            SELECT Id, Nome, Descricao, AlunoId, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Treino 
            WHERE Nivel = @Nivel AND DataExclusao IS NULL
            ORDER BY Nome";
        public static string ListarPorTipo => @"
            SELECT Id, Nome, Descricao, AlunoId, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Treino 
            WHERE Tipo = @Tipo AND DataExclusao IS NULL
            ORDER BY Nome";

        public static string Criar => @"
            INSERT INTO Treino (Nome, Descricao, AlunoId, Ativo, DataInclusao)
            OUTPUT INSERTED.Id
            VALUES (@Nome, @Descricao, @AlunoId, @Ativo, @DataInclusao)";

        public static string Atualizar => @"
            UPDATE Treino 
            SET Nome = @Nome,
                Descricao = @Descricao,
                AlunoId = @AlunoId,
                Ativo = @Ativo,
                DataAlteracao = @DataAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ExcluirLogico => @"
            UPDATE Treino 
            SET DataExclusao = @DataExclusao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM Treino 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistenciaPorNome => @"
            SELECT COUNT(1) 
            FROM Treino 
            WHERE Nome = @Nome 
                AND DataExclusao IS NULL
                AND (@IdExcluir IS NULL OR Id != @IdExcluir)";
    }
}