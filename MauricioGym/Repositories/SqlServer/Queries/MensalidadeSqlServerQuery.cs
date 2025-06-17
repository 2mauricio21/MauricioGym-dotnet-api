namespace MauricioGym.Repositories.SqlServer.Queries
{
    public static class MensalidadeSqlServerQuery
    {
        public static string InserirMensalidade => "INSERT INTO Mensalidade (PessoaId, PlanoId, ValorPago, DataPagamento, PeriodoInicio, PeriodoFim) VALUES (@PessoaId, @PlanoId, @ValorPago, @DataPagamento, @PeriodoInicio, @PeriodoFim); SELECT CAST(SCOPE_IDENTITY() as int);";
        public static string ObterPorId => "SELECT * FROM Mensalidade WHERE Id = @Id";
        public static string ListarPorPessoa => "SELECT * FROM Mensalidade WHERE PessoaId = @PessoaId";
        public static string ObterTotalRecebido => "SELECT SUM(ValorPago) FROM Mensalidade";
    }
}
