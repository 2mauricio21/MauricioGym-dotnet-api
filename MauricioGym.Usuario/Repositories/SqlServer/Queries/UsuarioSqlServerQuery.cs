namespace MauricioGym.Usuario.Repositories.SqlServer.Queries
{
    public static class UsuarioSqlServerQuery
    {
        public static string ObterTodos => @"
            SELECT Id, Nome, Email, DataNascimento, Telefone, Ativo, DataCriacao, DataAtualizacao
            FROM Usuario 
            WHERE Ativo = 1 
            ORDER BY Nome";
            
        public static string ObterPorId => @"
            SELECT Id, Nome, Email, DataNascimento, Telefone, Ativo, DataCriacao, DataAtualizacao
            FROM Usuario 
            WHERE Id = @Id AND Ativo = 1";
            
        public static string ObterPorEmail => @"
            SELECT Id, Nome, Email, DataNascimento, Telefone, Ativo, DataCriacao, DataAtualizacao
            FROM Usuario 
            WHERE Email = @Email AND Ativo = 1";
            
        public static string Criar => @"
            INSERT INTO Usuario (Nome, Email, DataNascimento, Telefone, Ativo, DataCriacao)
            VALUES (@Nome, @Email, @DataNascimento, @Telefone, @Ativo, GETDATE());
            SELECT CAST(SCOPE_IDENTITY() as int)";
            
        public static string Atualizar => @"
            UPDATE Usuario 
            SET Nome = @Nome, Email = @Email, DataNascimento = @DataNascimento, 
                Telefone = @Telefone, DataAtualizacao = GETDATE()
            WHERE Id = @Id AND Ativo = 1";
            
        public static string Remover => "UPDATE Usuario SET Ativo = 0, DataAtualizacao = GETDATE() WHERE Id = @Id";
            
        public static string Existe => "SELECT COUNT(1) FROM Usuario WHERE Id = @Id AND Ativo = 1";
            
        public static string ExisteEmail => "SELECT COUNT(1) FROM Usuario WHERE Email = @Email AND Ativo = 1";
        
        public static string ExisteEmailExcluindoId => "SELECT COUNT(1) FROM Usuario WHERE Email = @Email AND Ativo = 1 AND Id != @ExcludeId";
    }
}
