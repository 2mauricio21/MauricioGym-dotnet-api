namespace MauricioGym.Usuario.Repositories.SqlServer.Queries
{
    public static class CheckInSqlServerQuery
    {
        public static string ObterTodos => @"
            SELECT Id, UsuarioId, DataHora, Observacoes, Ativo, DataCriacao, DataAtualizacao 
            FROM CheckIn 
            WHERE Ativo = 1
            ORDER BY DataHora DESC";
            
        public static string ObterPorId => @"
            SELECT Id, UsuarioId, DataHora, Observacoes, Ativo, DataCriacao, DataAtualizacao 
            FROM CheckIn 
            WHERE Id = @Id AND Ativo = 1";
            
        public static string ObterPorUsuario => @"
            SELECT Id, UsuarioId, DataHora, Observacoes, Ativo, DataCriacao, DataAtualizacao 
            FROM CheckIn 
            WHERE UsuarioId = @UsuarioId AND Ativo = 1
            ORDER BY DataHora DESC";
            
        public static string ObterPorPeriodo => @"
            SELECT Id, UsuarioId, DataHora, Observacoes, Ativo, DataCriacao, DataAtualizacao 
            FROM CheckIn 
            WHERE DataHora >= @DataInicio AND DataHora <= @DataFim AND Ativo = 1
            ORDER BY DataHora DESC";
            
        public static string Criar => @"
            INSERT INTO CheckIn (UsuarioId, DataHora, Observacoes, Ativo, DataCriacao)
            VALUES (@UsuarioId, @DataHora, @Observacoes, @Ativo, GETDATE());
            SELECT CAST(SCOPE_IDENTITY() as int)";
            
        public static string Atualizar => @"
            UPDATE CheckIn 
            SET UsuarioId = @UsuarioId, DataHora = @DataHora, Observacoes = @Observacoes, DataAtualizacao = GETDATE() 
            WHERE Id = @Id AND Ativo = 1";
            
        public static string Remover => "UPDATE CheckIn SET Ativo = 0, DataAtualizacao = GETDATE() WHERE Id = @Id";
            
        public static string Existe => "SELECT COUNT(1) FROM CheckIn WHERE Id = @Id AND Ativo = 1";
            
        public static string ContarCheckInsPorUsuarioMes => @"
            SELECT COUNT(1) 
            FROM CheckIn 
            WHERE UsuarioId = @UsuarioId 
              AND YEAR(DataHora) = @Ano 
              AND MONTH(DataHora) = @Mes 
              AND Ativo = 1";
    }
}
