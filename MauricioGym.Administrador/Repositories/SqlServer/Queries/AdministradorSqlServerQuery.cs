namespace MauricioGym.Administrador.Repositories.SqlServer.Queries
{
    public static class AdministradorSqlServerQuery
    {
        public static string InserirAdministrador => "INSERT INTO Administrador (Nome, Email, SenhaHash, Ativo, DataCriacao) VALUES (@Nome, @Email, @SenhaHash, 1, GETDATE()); SELECT CAST(SCOPE_IDENTITY() as int);";
        public static string ObterPorId => "SELECT * FROM Administrador WHERE Id = @Id AND Ativo = 1";
        public static string Listar => "SELECT * FROM Administrador WHERE Ativo = 1";
        public static string Atualizar => "UPDATE Administrador SET Nome = @Nome, Email = @Email, SenhaHash = @SenhaHash, DataAtualizacao = GETDATE() WHERE Id = @Id AND Ativo = 1";
        public static string RemoverLogico => "UPDATE Administrador SET Ativo = 0, DataAtualizacao = GETDATE() WHERE Id = @Id AND Ativo = 1";
    }
}
