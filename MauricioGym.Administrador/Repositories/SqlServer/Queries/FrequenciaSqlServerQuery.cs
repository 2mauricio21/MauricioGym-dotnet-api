namespace MauricioGym.Administrador.Repositories.SqlServer.Queries
{
    public static class FrequenciaSqlServerQuery
    {
        public static string ObterPorId => @"
            SELECT Id, AlunoId, Data, HoraEntrada, HoraSaida, Observacoes, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Frequencia 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ObterPorAlunoData => @"
            SELECT Id, AlunoId, Data, HoraEntrada, HoraSaida, Observacoes, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Frequencia 
            WHERE AlunoId = @AlunoId AND Data = @Data AND DataExclusao IS NULL";

        public static string ListarPorAluno => @"
            SELECT Id, AlunoId, Data, HoraEntrada, HoraSaida, Observacoes, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Frequencia 
            WHERE AlunoId = @AlunoId AND DataExclusao IS NULL
            ORDER BY Data DESC";

        public static string ListarPorPeriodo => @"
            SELECT Id, AlunoId, Data, HoraEntrada, HoraSaida, Observacoes, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Frequencia 
            WHERE Data BETWEEN @DataInicio AND @DataFim AND DataExclusao IS NULL
            ORDER BY Data DESC";

        public static string ListarPorAlunoPeriodo => @"
            SELECT Id, AlunoId, Data, HoraEntrada, HoraSaida, Observacoes, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Frequencia 
            WHERE AlunoId = @AlunoId AND Data BETWEEN @DataInicio AND @DataFim AND DataExclusao IS NULL
            ORDER BY Data DESC";

        public static string Criar => @"
            INSERT INTO Frequencia (AlunoId, Data, HoraEntrada, HoraSaida, Observacoes, Ativo, DataInclusao)
            OUTPUT INSERTED.Id
            VALUES (@AlunoId, @Data, @HoraEntrada, @HoraSaida, @Observacoes, @Ativo, @DataInclusao)";

        public static string Atualizar => @"
            UPDATE Frequencia 
            SET AlunoId = @AlunoId,
                Data = @Data,
                HoraEntrada = @HoraEntrada,
                HoraSaida = @HoraSaida,
                Observacoes = @Observacoes,
                DataAlteracao = @DataAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ExcluirLogico => @"
            UPDATE Frequencia 
            SET DataExclusao = @DataExclusao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM Frequencia 
            WHERE AlunoId = @AlunoId AND Data = @Data AND DataExclusao IS NULL";
    }
}