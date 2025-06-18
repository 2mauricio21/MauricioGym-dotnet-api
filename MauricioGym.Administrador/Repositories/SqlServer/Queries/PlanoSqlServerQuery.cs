namespace MauricioGym.Administrador.Repositories.SqlServer.Queries
{
    public static class PlanoSqlServerQuery
    {
        public static string ListarPorId => @"
            SELECT Id, Nome, Descricao, Valor, DuracaoMeses, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Plano 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ListarPorNome => @"
            SELECT Id, Nome, Descricao, Valor, DuracaoMeses, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Plano 
            WHERE Nome = @Nome AND DataExclusao IS NULL";

        public static string ListarTodos => @"
            SELECT Id, Nome, Descricao, Valor, DuracaoMeses, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Plano 
            WHERE DataExclusao IS NULL
            ORDER BY Nome";

        public static string ListarAtivos => @"
            SELECT Id, Nome, Descricao, Valor, DuracaoMeses, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Plano 
            WHERE Ativo = 1 AND DataExclusao IS NULL
            ORDER BY Nome";

        public static string ListarPorTipo => @" 
            SELECT Id, Nome, Descricao, Valor, DuracaoMeses, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Plano 
            WHERE Tipo = @Tipo AND DataExclusao IS NULL
            ORDER BY Nome";

        public static string ListarPorFaixaPreco => @" 
            SELECT Id, Nome, Descricao, Valor, DuracaoMeses, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Plano 
            WHERE Valor BETWEEN @PrecoMinimo AND @PrecoMaximo AND DataExclusao IS NULL
            ORDER BY Valor";

        public static string Criar => @"
            INSERT INTO Plano (Nome, Descricao, Valor, DuracaoMeses, Ativo, DataInclusao)
            OUTPUT INSERTED.Id
            VALUES (@Nome, @Descricao, @Valor, @DuracaoMeses, @Ativo, @DataInclusao)";

        public static string Atualizar => @"
            UPDATE Plano 
            SET Nome = @Nome,
                Descricao = @Descricao,
                Valor = @Valor,
                DuracaoMeses = @DuracaoMeses,
                Ativo = @Ativo,
                DataAlteracao = @DataAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ExcluirLogico => @"
            UPDATE Plano 
            SET DataExclusao = @DataExclusao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM Plano 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistenciaPorNome => @"
            SELECT COUNT(1) 
            FROM Plano 
            WHERE Nome = @Nome 
                AND DataExclusao IS NULL
                AND (@IdExcluir IS NULL OR Id != @IdExcluir)";
    }
}