namespace MauricioGym.Pagamento.Repositories.SqlServer.Queries
{
    public class FormaPagamentoSqlServerQuery
    {
        public static string IncluirFormaPagamento => @"
            INSERT INTO FormasPagamento (
                IdAcademia, Nome, Descricao, AceitaParcelamento, MaximoParcelas,
                TaxaProcessamento, Ativo
            ) VALUES (
                @IdAcademia, @Nome, @Descricao, @AceitaParcelamento, @MaximoParcelas,
                @TaxaProcessamento, @Ativo
            );
            SELECT CAST(SCOPE_IDENTITY() as int)";

        public static string ConsultarFormaPagamento => @"
            SELECT 
                IdFormaPagamento, IdAcademia, Nome, Descricao, AceitaParcelamento,
                MaximoParcelas, TaxaProcessamento, Ativo
            FROM FormasPagamento
            WHERE IdFormaPagamento = @IdFormaPagamento AND Ativo = 1";

        public static string ConsultarFormaPagamentoPorNome => @"
            SELECT 
                IdFormaPagamento, IdAcademia, Nome, Descricao, AceitaParcelamento,
                MaximoParcelas, TaxaProcessamento, Ativo
            FROM FormasPagamento
            WHERE Nome = @Nome AND IdAcademia = @IdAcademia AND Ativo = 1";

        public static string AlterarFormaPagamento => @"
            UPDATE FormasPagamento SET
                IdAcademia = @IdAcademia,
                Nome = @Nome,
                Descricao = @Descricao,
                AceitaParcelamento = @AceitaParcelamento,
                MaximoParcelas = @MaximoParcelas,
                TaxaProcessamento = @TaxaProcessamento,
                Ativo = @Ativo
            WHERE IdFormaPagamento = @IdFormaPagamento";

        public static string ExcluirFormaPagamento => @"
            UPDATE FormasPagamento SET Ativo = 0 WHERE IdFormaPagamento = @IdFormaPagamento";

        public static string ListarFormasPagamento => @"
            SELECT 
                IdFormaPagamento, IdAcademia, Nome, Descricao, AceitaParcelamento,
                MaximoParcelas, TaxaProcessamento, Ativo
            FROM FormasPagamento
            ORDER BY Nome";

        public static string ListarFormasPagamentoPorAcademia => @"
            SELECT 
                IdFormaPagamento, IdAcademia, Nome, Descricao, AceitaParcelamento,
                MaximoParcelas, TaxaProcessamento, Ativo
            FROM FormasPagamento
            WHERE IdAcademia = @IdAcademia AND Ativo = 1
            ORDER BY Nome";

        public static string ListarFormasPagamentoAtivas => @"
            SELECT 
                IdFormaPagamento, IdAcademia, Nome, Descricao, AceitaParcelamento,
                MaximoParcelas, TaxaProcessamento, Ativo
            FROM FormasPagamento
            WHERE Ativo = 1
            ORDER BY Nome";
    }
}