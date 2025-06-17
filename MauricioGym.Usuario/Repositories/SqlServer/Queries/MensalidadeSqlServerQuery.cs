namespace MauricioGym.Usuario.Repositories.SqlServer.Queries
{
    public static class MensalidadeSqlServerQuery
    {
        public static string ObterTodos => @"
            SELECT Id, UsuarioPlanoId, MesReferencia, AnoReferencia, Valor, DataVencimento, DataPagamento, Status, Ativo, DataCriacao, DataAtualizacao 
            FROM Mensalidade 
            WHERE Ativo = 1
            ORDER BY DataVencimento DESC";
            
        public static string ObterPorId => @"
            SELECT Id, UsuarioPlanoId, MesReferencia, AnoReferencia, Valor, DataVencimento, DataPagamento, Status, Ativo, DataCriacao, DataAtualizacao 
            FROM Mensalidade 
            WHERE Id = @Id AND Ativo = 1";
            
        public static string ObterPorUsuario => @"
            SELECT m.Id, m.UsuarioPlanoId, m.MesReferencia, m.AnoReferencia, m.Valor, m.DataVencimento, 
                   m.DataPagamento, m.Status, m.Ativo, m.DataCriacao, m.DataAtualizacao 
            FROM Mensalidade m
            INNER JOIN UsuarioPlano up ON m.UsuarioPlanoId = up.Id
            WHERE up.UsuarioId = @UsuarioId AND m.Ativo = 1
            ORDER BY m.DataVencimento DESC";
            
        public static string ObterPendentes => @"
            SELECT Id, UsuarioPlanoId, MesReferencia, AnoReferencia, Valor, DataVencimento, 
                   DataPagamento, Status, Ativo, DataCriacao, DataAtualizacao 
            FROM Mensalidade 
            WHERE Status = 'Pendente' AND Ativo = 1
            ORDER BY DataVencimento ASC";
            
        public static string ObterVencendas => @"
            SELECT Id, UsuarioPlanoId, MesReferencia, AnoReferencia, Valor, DataVencimento, 
                   DataPagamento, Status, Ativo, DataCriacao, DataAtualizacao 
            FROM Mensalidade 
            WHERE DataVencimento <= DATEADD(day, @Dias, GETDATE()) 
              AND Status = 'Pendente' AND Ativo = 1
            ORDER BY DataVencimento ASC";
            
        public static string EstaEmDia => @"
            SELECT COUNT(1) 
            FROM Mensalidade m
            INNER JOIN UsuarioPlano up ON m.UsuarioPlanoId = up.Id
            WHERE up.UsuarioId = @UsuarioId 
              AND m.DataVencimento >= GETDATE() 
              AND m.Status = 'Paga' 
              AND m.Ativo = 1";
            
        public static string Remover => "UPDATE Mensalidade SET Ativo = 0, DataAtualizacao = GETDATE() WHERE Id = @Id";
            
        public static string Existe => "SELECT COUNT(1) FROM Mensalidade WHERE Id = @Id AND Ativo = 1";
            
        public static string Criar => @"
            INSERT INTO Mensalidade (UsuarioPlanoId, MesReferencia, AnoReferencia, Valor, DataVencimento, DataPagamento, Status, Ativo, DataCriacao)
            VALUES (@UsuarioPlanoId, @MesReferencia, @AnoReferencia, @Valor, @DataVencimento, @DataPagamento, @Status, @Ativo, GETDATE());
            SELECT CAST(SCOPE_IDENTITY() as int)";
            
        public static string Atualizar => @"
            UPDATE Mensalidade 
            SET UsuarioPlanoId = @UsuarioPlanoId, 
                MesReferencia = @MesReferencia,
                AnoReferencia = @AnoReferencia,
                DataVencimento = @DataVencimento, 
                DataPagamento = @DataPagamento, 
                Valor = @Valor,
                Status = @Status,
                DataAtualizacao = GETDATE()
            WHERE Id = @Id AND Ativo = 1";
            
        public static string ObterMensalidadeAtual => @"
            SELECT TOP 1 m.Id, m.UsuarioPlanoId, m.MesReferencia, m.AnoReferencia, m.Valor, m.DataVencimento, 
                   m.DataPagamento, m.Status, m.Ativo, m.DataCriacao, m.DataAtualizacao 
            FROM Mensalidade m
            INNER JOIN UsuarioPlano up ON m.UsuarioPlanoId = up.Id
            WHERE up.UsuarioId = @UsuarioId 
              AND m.DataVencimento >= GETDATE() 
              AND m.Ativo = 1
            ORDER BY m.DataVencimento ASC";
    }
}
