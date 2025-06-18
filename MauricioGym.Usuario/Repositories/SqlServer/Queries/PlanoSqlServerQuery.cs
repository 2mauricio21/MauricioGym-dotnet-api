namespace MauricioGym.Usuario.Repositories.SqlServer.Queries
{
    public static class PlanoSqlServerQuery
    {
        public static string ObterPorId => @"
            SELECT Id, Nome, Descricao, ValorMensal, DuracaoMeses, IdAcademia, PermiteCongelamento,
                   DiasTolerancia, Ativo, DataCriacao, DataAtualizacao, DataExclusao
            FROM Plano 
            WHERE Id = @Id AND Ativo = 1";

        public static string ObterPorAcademiaId => @"
            SELECT Id, Nome, Descricao, ValorMensal, DuracaoMeses, IdAcademia, PermiteCongelamento,
                   DiasTolerancia, Ativo, DataCriacao, DataAtualizacao, DataExclusao
            FROM Plano 
            WHERE IdAcademia = @AcademiaId AND Ativo = 1
            ORDER BY Nome";

        public static string ListarAtivos => @"
            SELECT Id, Nome, Descricao, ValorMensal, DuracaoMeses, IdAcademia, PermiteCongelamento,
                   DiasTolerancia, Ativo, DataCriacao, DataAtualizacao, DataExclusao
            FROM Plano 
            WHERE Ativo = 1
            ORDER BY Nome";

        public static string ListarPorModalidade => @"
            SELECT p.Id, p.Nome, p.Descricao, p.ValorMensal, p.DuracaoMeses, p.IdAcademia, 
                   p.PermiteCongelamento, p.DiasTolerancia, p.Ativo, p.DataCriacao, p.DataAtualizacao, p.DataExclusao
            FROM Plano p
            INNER JOIN PlanoModalidade pm ON p.Id = pm.IdPlano
            WHERE pm.IdModalidade = @ModalidadeId AND p.Ativo = 1
            ORDER BY p.Nome";

        public static string Criar => @"
            INSERT INTO Plano (Nome, Descricao, ValorMensal, DuracaoMeses, IdAcademia, 
                             PermiteCongelamento, DiasTolerancia, Ativo, DataCriacao)
            VALUES (@Nome, @Descricao, @ValorMensal, @DuracaoMeses, @IdAcademia, 
                    @PermiteCongelamento, @DiasTolerancia, 1, GETDATE());
            SELECT CAST(SCOPE_IDENTITY() as int)";

        public static string Atualizar => @"
            UPDATE Plano 
            SET Nome = @Nome, 
                Descricao = @Descricao,
                ValorMensal = @ValorMensal,
                DuracaoMeses = @DuracaoMeses,
                IdAcademia = @IdAcademia,
                PermiteCongelamento = @PermiteCongelamento,
                DiasTolerancia = @DiasTolerancia,
                DataAtualizacao = GETDATE() 
            WHERE Id = @Id AND Ativo = 1";

        public static string ExcluirLogico => @"
            UPDATE Plano 
            SET Ativo = 0, DataExclusao = @DataExclusao, DataAtualizacao = GETDATE() 
            WHERE Id = @Id";

        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM Plano 
            WHERE Id = @Id AND Ativo = 1";

        public static string VerificarExistenciaNome => @"
            SELECT COUNT(1) 
            FROM Plano 
            WHERE Nome = @Nome AND (IdAcademia = @AcademiaId OR @AcademiaId IS NULL) AND Ativo = 1";

        public static string ObterTodos => @"
            SELECT Id, Nome, Descricao, ValorMensal, DuracaoMeses, IdAcademia, PermiteCongelamento,
                   DiasTolerancia, Ativo, DataCriacao, DataAtualizacao, DataExclusao
            FROM Plano 
            WHERE Ativo = 1
            ORDER BY Nome";
    }
}
