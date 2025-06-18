namespace MauricioGym.Administrador.Repositories.SqlServer.Queries
{
    public static class ExercicioSqlServerQuery
    {
        public static string ObterPorId => @"
            SELECT Id, Nome, Descricao, GrupoMuscular, Dificuldade, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Exercicio 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ObterPorNome => @"
            SELECT Id, Nome, Descricao, GrupoMuscular, Dificuldade, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Exercicio 
            WHERE Nome = @Nome AND DataExclusao IS NULL";

        public static string ListarPorGrupoMuscular => @"
            SELECT Id, Nome, Descricao, GrupoMuscular, Dificuldade, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Exercicio 
            WHERE GrupoMuscular = @GrupoMuscular AND DataExclusao IS NULL
            ORDER BY Nome";
        public static string ListarPorNivel => @"
            SELECT Id, Nome, Descricao, GrupoMuscular, Dificuldade, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Exercicio 
            WHERE Dificuldade = @Nivel AND DataExclusao IS NULL
            ORDER BY Nome";

        public static string ListarPorTipo => @"
            SELECT Id, Nome, Descricao, GrupoMuscular, Dificuldade, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Exercicio 
            WHERE Tipo = @Tipo AND DataExclusao IS NULL
            ORDER BY Nome";

        public static string ObterTodos => @"
            SELECT Id, Nome, Descricao, GrupoMuscular, Dificuldade, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Exercicio 
            WHERE DataExclusao IS NULL
            ORDER BY Nome";

        public static string ListarAtivos => @"
            SELECT Id, Nome, Descricao, GrupoMuscular, Dificuldade, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Exercicio 
            WHERE Ativo = 1 AND DataExclusao IS NULL
            ORDER BY Nome";

        public static string Criar => @"
            INSERT INTO Exercicio (Nome, Descricao, GrupoMuscular, Dificuldade, Ativo, DataInclusao)
            OUTPUT INSERTED.Id
            VALUES (@Nome, @Descricao, @GrupoMuscular, @Dificuldade, @Ativo, @DataInclusao)";

        public static string Atualizar => @"
            UPDATE Exercicio 
            SET Nome = @Nome,
                Descricao = @Descricao,
                GrupoMuscular = @GrupoMuscular,
                Dificuldade = @Dificuldade,
                Ativo = @Ativo,
                DataAlteracao = @DataAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ExcluirLogico => @"
            UPDATE Exercicio 
            SET DataExclusao = @DataExclusao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM Exercicio 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistenciaPorNome => @"
            SELECT COUNT(1) 
            FROM Exercicio 
            WHERE Nome = @Nome 
                AND DataExclusao IS NULL
                AND (@IdExcluir IS NULL OR Id != @IdExcluir)";
    }
}