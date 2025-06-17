namespace MauricioGym.Usuario.Repositories.SqlServer.Queries
{
    public static class PlanoSqlServerQuery
    {
        public static string ObterTodos => @"
            SELECT Id, Nome, Valor, DuracaoMeses, Ativo, DataCriacao, DataAtualizacao
            FROM Plano 
            WHERE Ativo = 1 
            ORDER BY Nome";
            
        public static string ObterPorId => @"
            SELECT Id, Nome, Valor, DuracaoMeses, Ativo, DataCriacao, DataAtualizacao
            FROM Plano 
            WHERE Id = @Id AND Ativo = 1";
            
        public static string Existe => "SELECT COUNT(1) FROM Plano WHERE Id = @Id AND Ativo = 1";
    }
}
