namespace MauricioGym.Administrador.Repositories.SqlServer.Queries
{
    public static class AdministradorSqlServerQuery
    {
        public static string ObterPorId => @"
            SELECT Id, Nome, Cpf, Email, Senha, Telefone, Ativo, 
                   DataInclusao, DataAlteracao, DataExclusao, 
                   UsuarioInclusao, UsuarioAlteracao
            FROM Administrador 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ObterPorEmail => @"
            SELECT Id, Nome, Cpf, Email, Senha, Telefone, Ativo, 
                   DataInclusao, DataAlteracao, DataExclusao, 
                   UsuarioInclusao, UsuarioAlteracao
            FROM Administrador 
            WHERE Email = @Email AND DataExclusao IS NULL";

        public static string ObterPorCpf => @"
            SELECT Id, Nome, Cpf, Email, Senha, Telefone, Ativo, 
                   DataInclusao, DataAlteracao, DataExclusao, 
                   UsuarioInclusao, UsuarioAlteracao
            FROM Administrador 
            WHERE Cpf = @Cpf AND DataExclusao IS NULL";

        public static string ObterTodos => @"
            SELECT Id, Nome, Cpf, Email, Senha, Telefone, Ativo, 
                   DataInclusao, DataAlteracao, DataExclusao, 
                   UsuarioInclusao, UsuarioAlteracao
            FROM Administrador 
            WHERE DataExclusao IS NULL
            ORDER BY Nome";

        public static string ObterAtivos => @"
            SELECT Id, Nome, Cpf, Email, Senha, Telefone, Ativo, 
                   DataInclusao, DataAlteracao, DataExclusao, 
                   UsuarioInclusao, UsuarioAlteracao
            FROM Administrador 
            WHERE Ativo = 1 AND DataExclusao IS NULL
            ORDER BY Nome";

        public static string Criar => @"
            INSERT INTO Administrador (Nome, Cpf, Email, Senha, Telefone, Ativo, 
                                     DataInclusao, UsuarioInclusao)
            OUTPUT INSERTED.Id
            VALUES (@Nome, @Cpf, @Email, @Senha, @Telefone, @Ativo, 
                    @DataInclusao, @UsuarioInclusao)";

        public static string Atualizar => @"
            UPDATE Administrador 
            SET Nome = @Nome, 
                Cpf = @Cpf, 
                Email = @Email, 
                Senha = @Senha, 
                Telefone = @Telefone, 
                Ativo = @Ativo, 
                DataAlteracao = @DataAlteracao,
                UsuarioAlteracao = @UsuarioAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ExcluirLogico => @"
            UPDATE Administrador 
            SET DataExclusao = @DataExclusao,
                UsuarioAlteracao = @UsuarioAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM Administrador 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistenciaPorEmail => @"
            SELECT COUNT(1) 
            FROM Administrador 
            WHERE Email = @Email AND DataExclusao IS NULL
                AND (@Id = 0 OR Id != @Id)";

        public static string VerificarExistenciaPorCpf => @"
            SELECT COUNT(1) 
            FROM Administrador 
            WHERE Cpf = @Cpf AND DataExclusao IS NULL
                AND (@Id = 0 OR Id != @Id)";
    }
}
