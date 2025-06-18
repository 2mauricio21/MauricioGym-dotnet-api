namespace MauricioGym.Administrador.Repositories.SqlServer.Queries
{
    public static class MensalidadeSqlServerQuery
    {
        public static string ObterPorId => @"
            SELECT Id, AlunoId, PlanoId, DataVencimento, DataPagamento, Valor, Status, Ativo, DataCriacao, DataAlteracao, DataExclusao
            FROM Mensalidade 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ListarPorAluno => @"
            SELECT Id, AlunoId, PlanoId, DataVencimento, DataPagamento, Valor, Status, Ativo, DataCriacao, DataAlteracao, DataExclusao
            FROM Mensalidade 
            WHERE AlunoId = @AlunoId AND DataExclusao IS NULL
            ORDER BY DataVencimento DESC";

        public static string ObterPorPlano => @"
            SELECT Id, AlunoId, PlanoId, DataVencimento, DataPagamento, Valor, Status, Ativo, DataCriacao, DataAlteracao, DataExclusao
            FROM Mensalidade 
            WHERE PlanoId = @PlanoId AND DataExclusao IS NULL
            ORDER BY DataVencimento DESC";

        public static string ListarPorStatus => @"
            SELECT Id, AlunoId, PlanoId, DataVencimento, DataPagamento, Valor, Status, Ativo, DataCriacao, DataAlteracao, DataExclusao
            FROM Mensalidade 
            WHERE Status = @Status AND DataExclusao IS NULL
            ORDER BY DataVencimento DESC";
        public static string ListarVencidas => @"
            SELECT Id, AlunoId, PlanoId, DataVencimento, DataPagamento, Valor, Status, Ativo, DataCriacao, DataAlteracao, DataExclusao
            FROM Mensalidade 
            WHERE DataVencimento < GETDATE() AND Status <> 'PAGO' AND DataExclusao IS NULL
            ORDER BY DataVencimento DESC";
        public static string ListarPorPeriodo => @"
            SELECT Id, AlunoId, PlanoId, DataVencimento, DataPagamento, Valor, Status, Ativo, DataCriacao, DataAlteracao, DataExclusao
            FROM Mensalidade 
            WHERE DataVencimento BETWEEN @DataInicio AND @DataFim AND DataExclusao IS NULL
            ORDER BY DataVencimento DESC";

        public static string Criar => @"
            INSERT INTO Mensalidade (AlunoId, PlanoId, DataVencimento, DataPagamento, Valor, Status, Ativo, DataCriacao)
            OUTPUT INSERTED.Id
            VALUES (@AlunoId, @PlanoId, @DataVencimento, @DataPagamento, @Valor, @Status, @Ativo, @DataCriacao)";

        public static string Atualizar => @"
            UPDATE Mensalidade 
            SET AlunoId = @AlunoId,
                PlanoId = @PlanoId,
                DataVencimento = @DataVencimento,
                DataPagamento = @DataPagamento,
                Valor = @Valor,
                Status = @Status,
                DataAlteracao = @DataAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ExcluirLogico => @"
            UPDATE Mensalidade 
            SET DataExclusao = @DataExclusao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM Mensalidade 
            WHERE Id = @Id AND DataExclusao IS NULL";
    }
}