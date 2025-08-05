namespace MauricioGym.Pagamento.Repositories.SqlServer.Queries
{
    public class PagamentoSqlServerQuery
    {
        public static string IncluirPagamento => @"
            INSERT INTO Pagamentos (
                IdUsuario, IdUsuarioPlano, Valor, DataPagamento, DataVencimento,
                FormaPagamento, StatusPagamento, TransacaoId, ObservacaoPagamento,
                Ativo, DataCadastro
            ) VALUES (
                @IdUsuario, @IdUsuarioPlano, @Valor, @DataPagamento, @DataVencimento,
                @FormaPagamento, @StatusPagamento, @TransacaoId, @ObservacaoPagamento,
                @Ativo, @DataCadastro
            );
            SELECT CAST(SCOPE_IDENTITY() as int)";

        public static string ConsultarPagamento => @"
            SELECT 
                IdPagamento, IdUsuario, IdUsuarioPlano, Valor, DataPagamento,
                DataVencimento, FormaPagamento, StatusPagamento, TransacaoId,
                ObservacaoPagamento, Ativo, DataCadastro
            FROM Pagamentos
            WHERE IdPagamento = @IdPagamento AND Ativo = 1";

        public static string ConsultarPagamentoPorTransacao => @"
            SELECT 
                IdPagamento, IdUsuario, IdUsuarioPlano, Valor, DataPagamento,
                DataVencimento, FormaPagamento, StatusPagamento, TransacaoId,
                ObservacaoPagamento, Ativo, DataCadastro
            FROM Pagamentos
            WHERE TransacaoId = @TransacaoId AND Ativo = 1";

        public static string ConsultarPagamentosPorUsuario => @"
            SELECT 
                IdPagamento, IdUsuario, IdUsuarioPlano, Valor, DataPagamento,
                DataVencimento, FormaPagamento, StatusPagamento, TransacaoId,
                ObservacaoPagamento, Ativo, DataCadastro
            FROM Pagamentos
            WHERE IdUsuario = @IdUsuario AND Ativo = 1
            ORDER BY DataPagamento DESC";

        public static string ConsultarPagamentosPorUsuarioPlano => @"
            SELECT 
                IdPagamento, IdUsuario, IdUsuarioPlano, Valor, DataPagamento,
                DataVencimento, FormaPagamento, StatusPagamento, TransacaoId,
                ObservacaoPagamento, Ativo, DataCadastro
            FROM Pagamentos
            WHERE IdUsuarioPlano = @IdUsuarioPlano AND Ativo = 1
            ORDER BY DataPagamento DESC";

        public static string AlterarPagamento => @"
            UPDATE Pagamentos SET
                IdUsuario = @IdUsuario,
                IdUsuarioPlano = @IdUsuarioPlano,
                Valor = @Valor,
                DataPagamento = @DataPagamento,
                DataVencimento = @DataVencimento,
                FormaPagamento = @FormaPagamento,
                StatusPagamento = @StatusPagamento,
                TransacaoId = @TransacaoId,
                ObservacaoPagamento = @ObservacaoPagamento,
                Ativo = @Ativo
            WHERE IdPagamento = @IdPagamento";

        public static string CancelarPagamento => @"
            UPDATE Pagamentos SET
                StatusPagamento = 'Cancelado',
                Ativo = 0
            WHERE IdPagamento = @IdPagamento";

        public static string ListarPagamentos => @"
            SELECT 
                IdPagamento, IdUsuario, IdUsuarioPlano, Valor, DataPagamento,
                DataVencimento, FormaPagamento, StatusPagamento, TransacaoId,
                ObservacaoPagamento, Ativo, DataCadastro
            FROM Pagamentos
            ORDER BY DataCadastro DESC";

        public static string ListarPagamentosPendentes => @"
            SELECT 
                IdPagamento, IdUsuario, IdUsuarioPlano, Valor, DataPagamento,
                DataVencimento, FormaPagamento, StatusPagamento, TransacaoId,
                ObservacaoPagamento, Ativo, DataCadastro
            FROM Pagamentos
            WHERE StatusPagamento = 'Pendente' AND Ativo = 1
            ORDER BY DataVencimento ASC";

        public static string ListarPagamentosPorStatus => @"
            SELECT 
                IdPagamento, IdUsuario, IdUsuarioPlano, Valor, DataPagamento,
                DataVencimento, FormaPagamento, StatusPagamento, TransacaoId,
                ObservacaoPagamento, Ativo, DataCadastro
            FROM Pagamentos
            WHERE StatusPagamento = @Status AND Ativo = 1
            ORDER BY DataPagamento DESC";
    }
}