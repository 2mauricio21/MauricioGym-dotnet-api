namespace MauricioGym.Repositories.SqlServer.Queries
{
    public static class PermissaoManipulacaoUsuarioSqlServerQuery
    {
        public static string InserirPermissao => "INSERT INTO PermissaoManipulacaoUsuario (AdministradorId, PessoaId, PodeEditar, PodeExcluir, DataCriacao) VALUES (@AdministradorId, @PessoaId, @PodeEditar, @PodeExcluir, GETDATE()); SELECT CAST(SCOPE_IDENTITY() as int);";
        public static string ObterPorId => "SELECT * FROM PermissaoManipulacaoUsuario WHERE Id = @Id";
        public static string ListarPorPessoa => "SELECT * FROM PermissaoManipulacaoUsuario WHERE PessoaId = @PessoaId";
        public static string Atualizar => "UPDATE PermissaoManipulacaoUsuario SET PodeEditar = @PodeEditar, PodeExcluir = @PodeExcluir WHERE Id = @Id";
        public static string Remover => "DELETE FROM PermissaoManipulacaoUsuario WHERE Id = @Id";
    }
}
