namespace MauricioGym.Administrador.Repositories.SqlServer.Queries
{
    public static class AvaliacaoFisicaSqlServerQuery
    {
        public static string ObterPorId => @"
            SELECT Id, AlunoId, DataAvaliacao, Peso, Altura, IMC, CircunferenciaCintura, CircunferenciaBraco, 
                   PercentualGordura, MassaMuscular, Observacoes, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM AvaliacaoFisica 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ListarPorAluno => @"
            SELECT Id, AlunoId, DataAvaliacao, Peso, Altura, IMC, CircunferenciaCintura, CircunferenciaBraco, 
                   PercentualGordura, MassaMuscular, Observacoes, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM AvaliacaoFisica 
            WHERE AlunoId = @AlunoId AND DataExclusao IS NULL
            ORDER BY DataAvaliacao DESC";

        public static string ObterUltimaAvaliacao => @"
            SELECT TOP 1 Id, AlunoId, DataAvaliacao, Peso, Altura, IMC, CircunferenciaCintura, CircunferenciaBraco, 
                   PercentualGordura, MassaMuscular, Observacoes, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM AvaliacaoFisica 
            WHERE AlunoId = @AlunoId AND DataExclusao IS NULL
            ORDER BY DataAvaliacao DESC";

        public static string ListarPorPeriodo => @"
            SELECT Id, AlunoId, DataAvaliacao, Peso, Altura, IMC, CircunferenciaCintura, CircunferenciaBraco, 
                   PercentualGordura, MassaMuscular, Observacoes, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM AvaliacaoFisica 
            WHERE DataAvaliacao BETWEEN @DataInicio AND @DataFim AND DataExclusao IS NULL
            ORDER BY DataAvaliacao DESC";

        public static string Criar => @"
            INSERT INTO AvaliacaoFisica (AlunoId, DataAvaliacao, Peso, Altura, IMC, CircunferenciaCintura, 
                                        CircunferenciaBraco, PercentualGordura, MassaMuscular, Observacoes, Ativo, DataInclusao)
            OUTPUT INSERTED.Id
            VALUES (@AlunoId, @DataAvaliacao, @Peso, @Altura, @IMC, @CircunferenciaCintura, 
                    @CircunferenciaBraco, @PercentualGordura, @MassaMuscular, @Observacoes, @Ativo, @DataInclusao)";

        public static string Atualizar => @"
            UPDATE AvaliacaoFisica 
            SET AlunoId = @AlunoId,
                DataAvaliacao = @DataAvaliacao,
                Peso = @Peso,
                Altura = @Altura,
                IMC = @IMC,
                CircunferenciaCintura = @CircunferenciaCintura,
                CircunferenciaBraco = @CircunferenciaBraco,
                PercentualGordura = @PercentualGordura,
                MassaMuscular = @MassaMuscular,
                Observacoes = @Observacoes,
                Ativo = @Ativo,
                DataAlteracao = @DataAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ExcluirLogico => @"
            UPDATE AvaliacaoFisica 
            SET DataExclusao = @DataExclusao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM AvaliacaoFisica 
            WHERE Id = @Id AND DataExclusao IS NULL";
    }
}