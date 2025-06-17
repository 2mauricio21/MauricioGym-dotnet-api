namespace MauricioGym.Repositories.SqlServer.Queries
{
    public static class CaixaSqlServerQuery
    {
        public static string InserirCaixa => "INSERT INTO Caixa (QuantidadeAlunos, ValorTotal, DataAtualizacao) VALUES (@QuantidadeAlunos, @ValorTotal, GETDATE()); SELECT CAST(SCOPE_IDENTITY() as int);";
        public static string ObterPorId => "SELECT * FROM Caixa WHERE Id = @Id";
        public static string Listar => "SELECT * FROM Caixa";
        public static string Atualizar => "UPDATE Caixa SET QuantidadeAlunos = @QuantidadeAlunos, ValorTotal = @ValorTotal, DataAtualizacao = GETDATE() WHERE Id = @Id";
        public static string Remover => "DELETE FROM Caixa WHERE Id = @Id";
    }
}
