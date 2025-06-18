namespace MauricioGym.Usuario.Repositories.SqlServer.Queries
{
    public static class ModalidadeSqlServerQuery
    {
        public static string ObterPorId => @"
            SELECT Id, Nome, Descricao, AcademiaId, Ativo, 
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Modalidade 
            WHERE Id = @Id AND DataExclusao IS NULL";
            
        public static string ObterPorAcademiaId => @"
            SELECT Id, Nome, Descricao, AcademiaId, Ativo, 
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Modalidade 
            WHERE AcademiaId = @AcademiaId AND DataExclusao IS NULL
            ORDER BY Nome";
            
        public static string ListarAtivas => @"
            SELECT Id, Nome, Descricao, AcademiaId, Ativo, 
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Modalidade 
            WHERE Ativo = 1 AND DataExclusao IS NULL
            ORDER BY Nome";
            
        public static string Criar => @"
            INSERT INTO Modalidade (Nome, Descricao, AcademiaId, Ativo, 
                                 DataInclusao, UsuarioInclusao)
            OUTPUT INSERTED.Id
            VALUES (@Nome, @Descricao, @AcademiaId, @Ativo, 
                    @DataInclusao, @UsuarioInclusao)";
            
        public static string Atualizar => @"
            UPDATE Modalidade 
            SET Nome = @Nome, 
                Descricao = @Descricao,
                AcademiaId = @AcademiaId,
                Ativo = @Ativo,
                DataAlteracao = @DataAlteracao,
                UsuarioAlteracao = @UsuarioAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";
            
        public static string ExcluirLogico => @"
            UPDATE Modalidade 
            SET DataExclusao = @DataExclusao
            WHERE Id = @Id AND DataExclusao IS NULL";
            
        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM Modalidade 
            WHERE Id = @Id AND DataExclusao IS NULL";
            
        public static string VerificarExistenciaNome => @"
            SELECT COUNT(1) 
            FROM Modalidade 
            WHERE Nome = @Nome 
                AND AcademiaId = @AcademiaId 
                AND DataExclusao IS NULL
                AND (@IdExcluir IS NULL OR Id != @IdExcluir)";

        public static string ObterPorNome => @"
            SELECT Id, Nome, Descricao, AcademiaId, Ativo, 
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Modalidade 
            WHERE Nome = @Nome AND DataExclusao IS NULL";

        public static string ObterTodos => @"
            SELECT Id, Nome, Descricao, AcademiaId, Ativo, 
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Modalidade 
            WHERE DataExclusao IS NULL
            ORDER BY Nome";
    }
}
