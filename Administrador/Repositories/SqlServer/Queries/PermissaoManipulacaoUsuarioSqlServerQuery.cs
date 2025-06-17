namespace MauricioGym.Administrador.Repositories.SqlServer.Queries
{
    public static class PermissaoManipulacaoUsuarioSqlServerQuery
    {
        public static string InserirPermissao => "INSERT INTO PermissaoManipulacaoUsuario (AdministradorId, UsuarioId, TipoPermissao, Ativo, DataCriacao) VALUES (@AdministradorId, @UsuarioId, @TipoPermissao, 1, GETDATE()); SELECT CAST(SCOPE_IDENTITY() as int);";
        public static string ObterPorId => "SELECT Id, AdministradorId, UsuarioId, TipoPermissao, Ativo, DataCriacao, DataAtualizacao FROM PermissaoManipulacaoUsuario WHERE Id = @Id AND Ativo = 1";
        public static string ListarPorUsuario => "SELECT Id, AdministradorId, UsuarioId, TipoPermissao, Ativo, DataCriacao, DataAtualizacao FROM PermissaoManipulacaoUsuario WHERE UsuarioId = @UsuarioId AND Ativo = 1";
        public static string Atualizar => "UPDATE PermissaoManipulacaoUsuario SET TipoPermissao = @TipoPermissao, DataAtualizacao = GETDATE() WHERE Id = @Id AND Ativo = 1";
        public static string Remover => "UPDATE PermissaoManipulacaoUsuario SET Ativo = 0, DataAtualizacao = GETDATE() WHERE Id = @Id";
    }
}
