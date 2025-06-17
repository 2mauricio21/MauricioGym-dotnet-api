namespace MauricioGym.Administrador.Repositories.SqlServer.Queries
{
    public static class UsuarioPlanoSqlServerQuery
    {
        public static string InserirUsuarioPlano => "INSERT INTO PessoaPlano (PessoaId, PlanoId, DataInicio, DataFim, Ativo, DataCriacao) VALUES (@UsuarioId, @PlanoId, @DataInicio, @DataFim, 1, GETDATE()); SELECT CAST(SCOPE_IDENTITY() as int);";
        public static string ObterPorId => "SELECT * FROM PessoaPlano WHERE Id = @Id AND Ativo = 1";
        public static string ListarPorUsuario => "SELECT * FROM PessoaPlano WHERE PessoaId = @UsuarioId AND Ativo = 1";
        public static string Atualizar => "UPDATE PessoaPlano SET PlanoId = @PlanoId, DataInicio = @DataInicio, DataFim = @DataFim, DataAtualizacao = GETDATE() WHERE Id = @Id AND Ativo = 1";
        public static string RemoverLogico => "UPDATE PessoaPlano SET Ativo = 0, DataAtualizacao = GETDATE() WHERE Id = @Id AND Ativo = 1";
    }
}
