namespace MauricioGym.Repositories.SqlServer.Queries
{
    public static class PessoaSqlServerQuery
    {
        public static string InserirPessoa => "INSERT INTO Pessoa (Nome, Email, Telefone, DataNascimento, Ativo, DataCriacao) VALUES (@Nome, @Email, @Telefone, @DataNascimento, 1, GETDATE()); SELECT CAST(SCOPE_IDENTITY() as int);";
        public static string ObterPorId => "SELECT * FROM Pessoa WHERE Id = @Id AND Ativo = 1";
        public static string Listar => "SELECT * FROM Pessoa WHERE Ativo = 1";
        public static string Atualizar => "UPDATE Pessoa SET Nome = @Nome, Email = @Email, Telefone = @Telefone, DataNascimento = @DataNascimento, DataAtualizacao = GETDATE() WHERE Id = @Id AND Ativo = 1";
        public static string RemoverLogico => "UPDATE Pessoa SET Ativo = 0, DataAtualizacao = GETDATE() WHERE Id = @Id AND Ativo = 1";
    }
}
