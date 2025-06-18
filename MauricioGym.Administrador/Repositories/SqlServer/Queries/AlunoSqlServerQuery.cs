namespace MauricioGym.Administrador.Repositories.SqlServer.Queries
{
    public static class AlunoSqlServerQuery
    {
        public static string ObterPorId => @"
            SELECT Id, Nome, Email, Cpf, Telefone, DataNascimento, Endereco, Cidade, Estado, Cep, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Aluno 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ObterPorEmail => @"
            SELECT Id, Nome, Email, Cpf, Telefone, DataNascimento, Endereco, Cidade, Estado, Cep, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Aluno 
            WHERE Email = @Email AND DataExclusao IS NULL";

        public static string ObterPorCpf => @"
            SELECT Id, Nome, Email, Cpf, Telefone, DataNascimento, Endereco, Cidade, Estado, Cep, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Aluno 
            WHERE Cpf = @Cpf AND DataExclusao IS NULL";

        public static string ObterTodos => @"
            SELECT Id, Nome, Email, Cpf, Telefone, DataNascimento, Endereco, Cidade, Estado, Cep, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Aluno 
            WHERE DataExclusao IS NULL
            ORDER BY Nome";

        public static string ListarAtivos => @"
            SELECT Id, Nome, Email, Cpf, Telefone, DataNascimento, Endereco, Cidade, Estado, Cep, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Aluno 
            WHERE Ativo = 1 AND DataExclusao IS NULL
            ORDER BY Nome";

        public static string ListarPorNome => @"
            SELECT Id, Nome, Email, Cpf, Telefone, DataNascimento, Endereco, Cidade, Estado, Cep, Ativo, DataInclusao, DataAlteracao, DataExclusao
            FROM Aluno 
            WHERE Nome LIKE '%' + @Nome + '%' AND DataExclusao IS NULL
            ORDER BY Nome";

        public static string ListarPorPlano => @"
            SELECT a.Id, a.Nome, a.Email, a.Cpf, a.Telefone, a.DataNascimento, a.Endereco, a.Cidade, a.Estado, a.Cep, a.Ativo, a.DataInclusao, a.DataAlteracao, a.DataExclusao
            FROM Aluno a
            INNER JOIN PlanoAluno pa ON a.Id = pa.AlunoId
            WHERE pa.PlanoId = @PlanoId AND a.DataExclusao IS NULL
            ORDER BY a.Nome";

        //ListarComMensalidadeVencida
        public static string ListarComMensalidadeVencida => @"
            SELECT a.Id, a.Nome, a.Email, a.Cpf, a.Telefone, a.DataNascimento, a.Endereco, a.Cidade, a.Estado, a.Cep, a.Ativo, a.DataInclusao, a.DataAlteracao, a.DataExclusao
            FROM Aluno a
            INNER JOIN Mensalidade m ON a.Id = m.AlunoId
            WHERE m.Vencimento < GETDATE() AND m.Paga = 0 AND a.DataExclusao IS NULL
            ORDER BY a.Nome";

        public static string Criar => @"
            INSERT INTO Aluno (Nome, Email, Cpf, Telefone, DataNascimento, Endereco, Cidade, Estado, Cep, Ativo, DataInclusao)
            OUTPUT INSERTED.Id
            VALUES (@Nome, @Email, @Cpf, @Telefone, @DataNascimento, @Endereco, @Cidade, @Estado, @Cep, @Ativo, @DataInclusao)";

        public static string Atualizar => @"
            UPDATE Aluno 
            SET Nome = @Nome,
                Email = @Email,
                Cpf = @Cpf,
                Telefone = @Telefone,
                DataNascimento = @DataNascimento,
                Endereco = @Endereco,
                Cidade = @Cidade,
                Estado = @Estado,
                Cep = @Cep,
                Ativo = @Ativo,
                DataAlteracao = @DataAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ExcluirLogico => @"
            UPDATE Aluno 
            SET DataExclusao = @DataExclusao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM Aluno 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistenciaPorEmail => @"
            SELECT COUNT(1) 
            FROM Aluno 
            WHERE Email = @Email 
                AND DataExclusao IS NULL
                AND (@IdExcluir IS NULL OR Id != @IdExcluir)";

        public static string VerificarExistenciaPorCpf => @"
            SELECT COUNT(1) 
            FROM Aluno 
            WHERE Cpf = @Cpf 
                AND DataExclusao IS NULL
                AND (@IdExcluir IS NULL OR Id != @IdExcluir)";
    }
}