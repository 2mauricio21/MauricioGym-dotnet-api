namespace MauricioGym.Administrador.Repositories.SqlServer.Queries
{    public static class UsuarioPlanoSqlServerQuery
    {
        public static string InserirUsuarioPlano => "INSERT INTO UsuarioPlano (UsuarioId, PlanoId, DataInicio, DataFim, Ativo, DataCriacao) VALUES (@UsuarioId, @PlanoId, @DataInicio, @DataFim, 1, GETDATE()); SELECT CAST(SCOPE_IDENTITY() as int);";
        public static string ObterPorId => "SELECT * FROM UsuarioPlano WHERE Id = @Id AND Ativo = 1";
        public static string ListarPorUsuario => "SELECT * FROM UsuarioPlano WHERE UsuarioId = @UsuarioId AND Ativo = 1";
        public static string Atualizar => "UPDATE UsuarioPlano SET PlanoId = @PlanoId, DataInicio = @DataInicio, DataFim = @DataFim, DataAtualizacao = GETDATE() WHERE Id = @Id AND Ativo = 1";
        public static string RemoverLogico => "UPDATE UsuarioPlano SET Ativo = 0, DataAtualizacao = GETDATE() WHERE Id = @Id AND Ativo = 1";
    }
}
