namespace MauricioGym.Infra.Repositories.SqlServer.Queries
{
    public static class AuditoriaSqlServerQuery
    {        public static string ObterPorId => @"
            SELECT Id, IdAuditoria, IdUsuario, NomeUsuario, Data, IdRecurso, NomeRecurso, 
                   TipoOperacao, Descricao, Observacao, Ip, DadosAnteriores, DadosNovos,
                   Ativo, DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Auditoria 
            WHERE Id = @Id AND DataExclusao IS NULL";        public static string ObterPorIdAuditoria => @"
            SELECT Id, IdAuditoria, IdUsuario, NomeUsuario, Data, IdRecurso, NomeRecurso, 
                   TipoOperacao, Descricao, Observacao, Ip, DadosAnteriores, DadosNovos,
                   Ativo, DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Auditoria 
            WHERE IdAuditoria = @IdAuditoria AND DataExclusao IS NULL";        public static string ObterPorUsuario => @"
            SELECT Id, IdAuditoria, IdUsuario, NomeUsuario, Data, IdRecurso, NomeRecurso, 
                   TipoOperacao, Descricao, Observacao, Ip, DadosAnteriores, DadosNovos,
                   Ativo, DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Auditoria 
            WHERE IdUsuario = @IdUsuario AND DataExclusao IS NULL
            ORDER BY Data DESC";        public static string ObterPorUsuarioComPeriodo => @"
            SELECT Id, IdAuditoria, IdUsuario, NomeUsuario, Data, IdRecurso, NomeRecurso, 
                   TipoOperacao, Descricao, Observacao, Ip, DadosAnteriores, DadosNovos,
                   Ativo, DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Auditoria 
            WHERE IdUsuario = @IdUsuario 
                AND Data >= @Data 
                AND Data <= @DataFim
                AND DataExclusao IS NULL
            ORDER BY Data DESC";        public static string ObterPorRecurso => @"
            SELECT Id, IdAuditoria, IdUsuario, NomeUsuario, Data, IdRecurso, NomeRecurso, 
                   TipoOperacao, Descricao, Observacao, Ip, DadosAnteriores, DadosNovos,
                   Ativo, DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Auditoria 
            WHERE IdRecurso = @IdRecurso AND DataExclusao IS NULL
            ORDER BY Data DESC";        public static string ObterPorRecursoComPeriodo => @"
            SELECT Id, IdAuditoria, IdUsuario, NomeUsuario, Data, IdRecurso, NomeRecurso, 
                   TipoOperacao, Descricao, Observacao, Ip, DadosAnteriores, DadosNovos,
                   Ativo, DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Auditoria 
            WHERE IdRecurso = @IdRecurso 
                AND Data >= @Data 
                AND Data <= @DataFim
                AND DataExclusao IS NULL
            ORDER BY Data DESC";        public static string ObterPorPeriodo => @"
            SELECT Id, IdAuditoria, IdUsuario, NomeUsuario, Data, IdRecurso, NomeRecurso, 
                   TipoOperacao, Descricao, Observacao, Ip, DadosAnteriores, DadosNovos,
                   Ativo, DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Auditoria 
            WHERE Data >= @Data 
                AND Data <= @DataFim
                AND DataExclusao IS NULL
            ORDER BY Data DESC";        public static string Criar => @"
            INSERT INTO Auditoria (IdAuditoria, IdUsuario, NomeUsuario, Data, IdRecurso, NomeRecurso, 
                                  TipoOperacao, Descricao, Observacao, Ip, DadosAnteriores, DadosNovos,
                                  Ativo, DataInclusao, UsuarioInclusao)
            OUTPUT INSERTED.Id
            VALUES (@IdAuditoria, @IdUsuario, @NomeUsuario, @Data, @IdRecurso, @NomeRecurso, 
                    @TipoOperacao, @Descricao, @Observacao, @Ip, @DadosAnteriores, @DadosNovos,
                    @Ativo, @DataInclusao, @UsuarioInclusao)";

        public static string Atualizar => @"
            UPDATE Auditoria 
            SET IdAuditoria = @IdAuditoria,
                IdUsuario = @IdUsuario,
                NomeUsuario = @NomeUsuario,
                Data = @Data,
                IdRecurso = @IdRecurso,
                NomeRecurso = @NomeRecurso,
                TipoOperacao = @TipoOperacao,
                Descricao = @Descricao,
                Observacao = @Observacao,
                Ip = @Ip,
                DadosAnteriores = @DadosAnteriores,
                DadosNovos = @DadosNovos,
                Ativo = @Ativo,
                DataAlteracao = @DataAlteracao,
                UsuarioAlteracao = @UsuarioAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ExcluirLogico => @"            UPDATE Auditoria 
            SET DataExclusao = @DataExclusao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ObterPorTipoOperacao => @"
            SELECT Id, IdAuditoria, IdUsuario, NomeUsuario, Data, IdRecurso, NomeRecurso, 
                   TipoOperacao, Descricao, Observacao, Ip, DadosAnteriores, DadosNovos,
                   Ativo, DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Auditoria 
            WHERE TipoOperacao = @TipoOperacao AND DataExclusao IS NULL
            ORDER BY Data DESC";
    }
} 