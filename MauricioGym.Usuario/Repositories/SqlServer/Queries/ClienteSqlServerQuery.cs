namespace MauricioGym.Usuario.Repositories.SqlServer.Queries
{
    public static class ClienteSqlServerQuery
    {
        public static string ObterPorId => @"
            SELECT Id, Cpf, Nome, Email, Telefone, DataNascimento, 
                   Endereco, Cidade, Estado, Cep, Observacoes, Ativo,
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Cliente 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ObterPorCpf => @"
            SELECT Id, Cpf, Nome, Email, Telefone, DataNascimento, 
                   Endereco, Cidade, Estado, Cep, Observacoes, Ativo,
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Cliente 
            WHERE Cpf = @Cpf AND DataExclusao IS NULL";

        public static string ObterPorEmail => @"
            SELECT Id, Cpf, Nome, Email, Telefone, DataNascimento, 
                   Endereco, Cidade, Estado, Cep, Observacoes, Ativo,
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Cliente 
            WHERE Email = @Email AND DataExclusao IS NULL";

        public static string ObterTodos => @"
            SELECT Id, Cpf, Nome, Email, Telefone, DataNascimento, 
                   Endereco, Cidade, Estado, Cep, Observacoes, Ativo,
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Cliente 
            WHERE DataExclusao IS NULL
            ORDER BY Nome";

        public static string ObterAtivos => @"
            SELECT Id, Cpf, Nome, Email, Telefone, DataNascimento, 
                   Endereco, Cidade, Estado, Cep, Observacoes, Ativo,
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Cliente 
            WHERE Ativo = 1 AND DataExclusao IS NULL
            ORDER BY Nome";

        public static string Criar => @"
            INSERT INTO Cliente (
                Cpf, Nome, Email, Telefone, DataNascimento, 
                Endereco, Cidade, Estado, Cep, Observacoes, Ativo,
                DataInclusao, UsuarioInclusao
            ) 
            OUTPUT INSERTED.Id
            VALUES (
                @Cpf, @Nome, @Email, @Telefone, @DataNascimento, 
                @Endereco, @Cidade, @Estado, @Cep, @Observacoes, @Ativo,
                @DataInclusao, @UsuarioInclusao
            )";

        public static string Atualizar => @"
            UPDATE Cliente SET 
                Cpf = @Cpf,
                Nome = @Nome,
                Email = @Email,
                Telefone = @Telefone,
                DataNascimento = @DataNascimento,
                Endereco = @Endereco,
                Cidade = @Cidade,
                Estado = @Estado,
                Cep = @Cep,
                Observacoes = @Observacoes,
                Ativo = @Ativo,
                DataAlteracao = @DataAlteracao,
                UsuarioAlteracao = @UsuarioAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ExcluirLogico => @"
            UPDATE Cliente SET 
                DataExclusao = @DataExclusao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM Cliente 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistenciaCpf => @"
            SELECT COUNT(1) 
            FROM Cliente 
            WHERE Cpf = @Cpf 
                AND DataExclusao IS NULL
                AND (@IdExcluir IS NULL OR Id != @IdExcluir)";

        public static string VerificarExistenciaEmail => @"
            SELECT COUNT(1) 
            FROM Cliente 
            WHERE Email = @Email 
                AND DataExclusao IS NULL
                AND (@IdExcluir IS NULL OR Id != @IdExcluir)";
    }
}
