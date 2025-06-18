namespace MauricioGym.Usuario.Repositories.SqlServer.Queries
{
    public static class PagamentoSqlServerQuery
    {
        public static string ObterPorId => @"
            SELECT Id, ClienteId, AcademiaId, PlanoClienteId, DataPagamento, Valor, FormaPagamento, 
                   Status, Observacoes, NumeroParcelas, ParcelaAtual, ValorParcela, DataVencimento, 
                   CodigoTransacao, Ativo, DataInclusao, UsuarioInclusao, DataAlteracao, UsuarioAlteracao
            FROM Pagamento 
            WHERE Id = @Id AND Ativo = 1";

        public static string ObterPorClienteId => @"
            SELECT Id, ClienteId, AcademiaId, PlanoClienteId, DataPagamento, Valor, FormaPagamento, 
                   Status, Observacoes, NumeroParcelas, ParcelaAtual, ValorParcela, DataVencimento, 
                   CodigoTransacao, Ativo, DataInclusao, UsuarioInclusao, DataAlteracao, UsuarioAlteracao
            FROM Pagamento 
            WHERE ClienteId = @ClienteId AND Ativo = 1
            ORDER BY DataPagamento DESC";

        public static string ObterPorAcademiaId => @"
            SELECT Id, ClienteId, AcademiaId, PlanoClienteId, DataPagamento, Valor, FormaPagamento, 
                   Status, Observacoes, NumeroParcelas, ParcelaAtual, ValorParcela, DataVencimento, 
                   CodigoTransacao, Ativo, DataInclusao, UsuarioInclusao, DataAlteracao, UsuarioAlteracao
            FROM Pagamento 
            WHERE AcademiaId = @AcademiaId AND Ativo = 1
            ORDER BY DataPagamento DESC";

        public static string ObterPorPlanoClienteId => @"
            SELECT Id, ClienteId, AcademiaId, PlanoClienteId, DataPagamento, Valor, FormaPagamento, 
                   Status, Observacoes, NumeroParcelas, ParcelaAtual, ValorParcela, DataVencimento, 
                   CodigoTransacao, Ativo, DataInclusao, UsuarioInclusao, DataAlteracao, UsuarioAlteracao
            FROM Pagamento 
            WHERE PlanoClienteId = @PlanoClienteId AND Ativo = 1
            ORDER BY DataPagamento DESC";

        public static string ObterPorPeriodo => @"
            SELECT Id, ClienteId, AcademiaId, PlanoClienteId, DataPagamento, Valor, FormaPagamento, 
                   Status, Observacoes, NumeroParcelas, ParcelaAtual, ValorParcela, DataVencimento, 
                   CodigoTransacao, Ativo, DataInclusao, UsuarioInclusao, DataAlteracao, UsuarioAlteracao
            FROM Pagamento 
            WHERE DataPagamento BETWEEN @DataInicio AND @DataFim AND Ativo = 1
            ORDER BY DataPagamento DESC";

        public static string ObterPorPeriodoEAcademia => @"
            SELECT Id, ClienteId, AcademiaId, PlanoClienteId, DataPagamento, Valor, FormaPagamento, 
                   Status, Observacoes, NumeroParcelas, ParcelaAtual, ValorParcela, DataVencimento, 
                   CodigoTransacao, Ativo, DataInclusao, UsuarioInclusao, DataAlteracao, UsuarioAlteracao
            FROM Pagamento 
            WHERE DataPagamento BETWEEN @DataInicio AND @DataFim 
              AND AcademiaId = @AcademiaId AND Ativo = 1
            ORDER BY DataPagamento DESC";

        public static string ObterPendentes => @"
            SELECT Id, ClienteId, AcademiaId, PlanoClienteId, DataPagamento, Valor, FormaPagamento, 
                   Status, Observacoes, NumeroParcelas, ParcelaAtual, ValorParcela, DataVencimento, 
                   CodigoTransacao, Ativo, DataInclusao, UsuarioInclusao, DataAlteracao, UsuarioAlteracao
            FROM Pagamento 
            WHERE Status = 'Pendente' AND Ativo = 1
            ORDER BY DataVencimento ASC";

        public static string ObterPendentesPorAcademia => @"
            SELECT Id, ClienteId, AcademiaId, PlanoClienteId, DataPagamento, Valor, FormaPagamento, 
                   Status, Observacoes, NumeroParcelas, ParcelaAtual, ValorParcela, DataVencimento, 
                   CodigoTransacao, Ativo, DataInclusao, UsuarioInclusao, DataAlteracao, UsuarioAlteracao
            FROM Pagamento 
            WHERE Status = 'Pendente' AND AcademiaId = @AcademiaId AND Ativo = 1
            ORDER BY DataVencimento ASC";

        public static string ObterTotalRecebidoPorPeriodo => @"
            SELECT COALESCE(SUM(Valor), 0)
            FROM Pagamento 
            WHERE DataPagamento BETWEEN @DataInicio AND @DataFim 
              AND Status = 'Confirmado' AND Ativo = 1";

        public static string ObterTotalRecebidoPorPeriodoEAcademia => @"
            SELECT COALESCE(SUM(Valor), 0)
            FROM Pagamento 
            WHERE DataPagamento BETWEEN @DataInicio AND @DataFim 
              AND Status = 'Confirmado' AND AcademiaId = @AcademiaId AND Ativo = 1";

        public static string Criar => @"
            INSERT INTO Pagamento (ClienteId, AcademiaId, PlanoClienteId, DataPagamento, Valor, FormaPagamento, 
                                Status, Observacoes, NumeroParcelas, ParcelaAtual, ValorParcela, DataVencimento, 
                                CodigoTransacao, Ativo, DataInclusao, UsuarioInclusao)
            VALUES (@ClienteId, @AcademiaId, @PlanoClienteId, @DataPagamento, @Valor, @FormaPagamento, 
                    @Status, @Observacoes, @NumeroParcelas, @ParcelaAtual, @ValorParcela, @DataVencimento, 
                    @CodigoTransacao, 1, GETDATE(), @UsuarioInclusao);
            SELECT SCOPE_IDENTITY()";

        public static string Atualizar => @"
            UPDATE Pagamento 
            SET ClienteId = @ClienteId, 
                AcademiaId = @AcademiaId, 
                PlanoClienteId = @PlanoClienteId, 
                DataPagamento = @DataPagamento, 
                Valor = @Valor, 
                FormaPagamento = @FormaPagamento, 
                Status = @Status, 
                Observacoes = @Observacoes,
                NumeroParcelas = @NumeroParcelas,
                ParcelaAtual = @ParcelaAtual,
                ValorParcela = @ValorParcela,
                DataVencimento = @DataVencimento,
                CodigoTransacao = @CodigoTransacao,
                DataAlteracao = GETDATE(),
                UsuarioAlteracao = @UsuarioAlteracao
            WHERE Id = @Id AND Ativo = 1";

        public static string ExcluirLogico => @"
            UPDATE Pagamento 
            SET Ativo = 0, 
                DataExclusao = @DataExclusao
            WHERE Id = @Id";

        public static string VerificarExistencia => "SELECT COUNT(1) FROM Pagamento WHERE Id = @Id AND Ativo = 1";

        public static string ObterTodos => @"
            SELECT Id, ClienteId, AcademiaId, PlanoClienteId, DataPagamento, Valor, FormaPagamento, 
                   Status, Observacoes, NumeroParcelas, ParcelaAtual, ValorParcela, DataVencimento, 
                   CodigoTransacao, Ativo, DataInclusao, UsuarioInclusao, DataAlteracao, UsuarioAlteracao
            FROM Pagamento 
            WHERE Ativo = 1
            ORDER BY DataPagamento DESC";

        public static string ObterPagamentosEmAtrasoPorUsuario => @"
            SELECT Id, ClienteId, AcademiaId, PlanoClienteId, DataPagamento, Valor, FormaPagamento, 
                   Status, Observacoes, NumeroParcelas, ParcelaAtual, ValorParcela, DataVencimento, 
                   CodigoTransacao, Ativo, DataInclusao, UsuarioInclusao, DataAlteracao, UsuarioAlteracao
            FROM Pagamento 
            WHERE ClienteId = @UsuarioId 
                AND Status = 'Pendente' 
                AND DataVencimento < GETDATE()
                AND Ativo = 1
            ORDER BY DataVencimento ASC";
    }
}