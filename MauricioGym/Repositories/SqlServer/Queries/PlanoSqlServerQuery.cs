namespace MauricioGym.Repositories.SqlServer.Queries
{
    public static class PlanoSqlServerQuery
    {
        public static string InserirPlano => "INSERT INTO Plano (Nome, Valor, DuracaoMeses, Ativo, DataCriacao) VALUES (@Nome, @Valor, @DuracaoMeses, 1, GETDATE()); SELECT CAST(SCOPE_IDENTITY() as int);";
        public static string ObterPorId => "SELECT * FROM Plano WHERE Id = @Id AND Ativo = 1";
        public static string Listar => "SELECT * FROM Plano WHERE Ativo = 1";
        public static string Atualizar => "UPDATE Plano SET Nome = @Nome, Valor = @Valor, DuracaoMeses = @DuracaoMeses, DataAtualizacao = GETDATE() WHERE Id = @Id AND Ativo = 1";
        public static string RemoverLogico => "UPDATE Plano SET Ativo = 0, DataAtualizacao = GETDATE() WHERE Id = @Id AND Ativo = 1";
    }
}
