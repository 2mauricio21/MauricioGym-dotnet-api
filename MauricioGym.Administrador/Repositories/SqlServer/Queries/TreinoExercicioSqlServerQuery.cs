namespace MauricioGym.Administrador.Repositories.SqlServer.Queries
{
    public static class TreinoExercicioSqlServerQuery
    {
        public static string ListarPorId => @"
            SELECT Id, TreinoId, ExercicioId, Series, Repeticoes, Carga, Descanso, Ordem, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM TreinoExercicio 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ListarPorTreinoExercicio => @"
            SELECT Id, TreinoId, ExercicioId, Series, Repeticoes, Carga, Descanso, Ordem, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM TreinoExercicio 
            WHERE TreinoId = @TreinoId AND ExercicioId = @ExercicioId AND DataExclusao IS NULL";

        public static string ListarPorTreino => @"
            SELECT Id, TreinoId, ExercicioId, Series, Repeticoes, Carga, Descanso, Ordem, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM TreinoExercicio 
            WHERE TreinoId = @TreinoId AND DataExclusao IS NULL
            ORDER BY Ordem";

        public static string ListarPorExercicio => @"
            SELECT Id, TreinoId, ExercicioId, Series, Repeticoes, Carga, Descanso, Ordem, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM TreinoExercicio 
            WHERE ExercicioId = @ExercicioId AND DataExclusao IS NULL
            ORDER BY Ordem";

        public static string ListarExerciciosDoTreino => @"
            SELECT e.Id, e.Nome, e.Descricao, e.Tipo, e.Duracao, e.Ativo, te.Series, te.Repeticoes, te.Carga, te.Descanso, te.Ordem
            FROM TreinoExercicio te
            INNER JOIN Exercicio e ON te.ExercicioId = e.Id
            WHERE te.TreinoId = @TreinoId AND te.DataExclusao IS NULL AND e.DataExclusao IS NULL
            ORDER BY te.Ordem";

        public static string ListarTreinosComExercicio => @"
            SELECT t.Id, t.Nome, t.Descricao, t.DataInclusao, t.DataAlteracao, t.DataExclusao, te.Series, te.Repeticoes, te.Carga, te.Descanso
            FROM TreinoExercicio te
            INNER JOIN Treino t ON te.TreinoId = t.Id
            WHERE te.ExercicioId = @ExercicioId AND te.DataExclusao IS NULL AND t.DataExclusao IS NULL
            ORDER BY t.Nome";

        public static string ListarTodos => @"
            SELECT Id, TreinoId, ExercicioId, Series, Repeticoes, Carga, Descanso, Ordem, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM TreinoExercicio 
            WHERE DataExclusao IS NULL
            ORDER BY TreinoId, Ordem";

        public static string Criar => @"
            INSERT INTO TreinoExercicio (TreinoId, ExercicioId, Series, Repeticoes, Carga, Descanso, Ordem, Ativo, DataInclusao)
            OUTPUT INSERTED.Id
            VALUES (@TreinoId, @ExercicioId, @Series, @Repeticoes, @Carga, @Descanso, @Ordem, @Ativo, @DataInclusao)";

        public static string Atualizar => @"
            UPDATE TreinoExercicio 
            SET TreinoId = @TreinoId,
                ExercicioId = @ExercicioId,
                Series = @Series,
                Repeticoes = @Repeticoes,
                Carga = @Carga,
                Descanso = @Descanso,
                Ordem = @Ordem,
                Ativo = @Ativo,
                DataAlteracao = @DataAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ExcluirLogico => @"
            UPDATE TreinoExercicio 
            SET DataExclusao = @DataExclusao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM TreinoExercicio 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistenciaTreinoExercicio => @"
            SELECT COUNT(1) 
            FROM TreinoExercicio 
            WHERE TreinoId = @TreinoId AND ExercicioId = @ExercicioId AND DataExclusao IS NULL";
    }
}