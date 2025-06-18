namespace MauricioGym.Usuario.Repositories.SqlServer.Queries
{
    public static class PlanoClienteSqlServerQuery
    {
        public static string ListarPorId => @"
            SELECT Id, ClienteId, PlanoId, AcademiaId, DataInicio, DataVencimento, Valor, 
                   MesesContratados, PlanoAtivo, DataCancelamento, MotivoCancelamento, Ativo,
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM PlanoCliente 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ListarPorClienteId => @"
            SELECT Id, ClienteId, PlanoId, AcademiaId, DataInicio, DataVencimento, Valor, 
                   MesesContratados, PlanoAtivo, DataCancelamento, MotivoCancelamento, Ativo,
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM PlanoCliente 
            WHERE ClienteId = @ClienteId AND DataExclusao IS NULL
            ORDER BY DataInicio DESC";

        public static string ListarPorPlanoId => @"
            SELECT Id, ClienteId, PlanoId, AcademiaId, DataInicio, DataVencimento, Valor, 
                   MesesContratados, PlanoAtivo, DataCancelamento, MotivoCancelamento, Ativo,
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM PlanoCliente 
            WHERE PlanoId = @PlanoId AND DataExclusao IS NULL
            ORDER BY DataInicio DESC";

        public static string ListarAtivosPorClienteId => @"
            SELECT Id, ClienteId, PlanoId, AcademiaId, DataInicio, DataVencimento, Valor, 
                   MesesContratados, PlanoAtivo, DataCancelamento, MotivoCancelamento, Ativo,
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM PlanoCliente 
            WHERE ClienteId = @ClienteId 
                AND Ativo = 1 
                AND DataVencimento >= CAST(GETDATE() AS DATE)
                AND DataExclusao IS NULL
            ORDER BY DataVencimento ASC";

        public static string ListarVencidosPorClienteId => @"
            SELECT Id, ClienteId, PlanoId, AcademiaId, DataInicio, DataVencimento, Valor, 
                   MesesContratados, PlanoAtivo, DataCancelamento, MotivoCancelamento, Ativo,
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM PlanoCliente 
            WHERE ClienteId = @ClienteId 
                AND Ativo = 1 
                AND DataVencimento < @DataAtual
                AND DataExclusao IS NULL
            ORDER BY DataVencimento DESC";

        public static string ListarVencendo => @"
            SELECT Id, ClienteId, PlanoId, AcademiaId, DataInicio, DataVencimento, Valor, 
                   MesesContratados, PlanoAtivo, DataCancelamento, MotivoCancelamento, Ativo,
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM PlanoCliente 
            WHERE Ativo = 1 
                AND DataVencimento >= @DataAtual
                AND DataVencimento <= @DataLimite
                AND DataExclusao IS NULL
            ORDER BY DataVencimento ASC";

        public static string Criar => @"
            INSERT INTO PlanoCliente (
                ClienteId, PlanoId, AcademiaId, DataInicio, DataVencimento, Valor, 
                MesesContratados, PlanoAtivo, Ativo, DataInclusao, UsuarioInclusao
            )
            OUTPUT INSERTED.Id
            VALUES (
                @ClienteId, @PlanoId, @AcademiaId, @DataInicio, @DataVencimento, @Valor, 
                @MesesContratados, @PlanoAtivo, @Ativo, @DataInclusao, @UsuarioInclusao
            )";

        public static string Atualizar => @"
            UPDATE PlanoCliente 
            SET ClienteId = @ClienteId,
                PlanoId = @PlanoId,
                AcademiaId = @AcademiaId,
                DataInicio = @DataInicio,
                DataVencimento = @DataVencimento,
                Valor = @Valor,
                MesesContratados = @MesesContratados,
                PlanoAtivo = @PlanoAtivo,
                Ativo = @Ativo,
                DataAlteracao = @DataAlteracao,
                UsuarioAlteracao = @UsuarioAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ExcluirLogico => @"
            UPDATE PlanoCliente 
            SET DataExclusao = @DataExclusao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM PlanoCliente 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarPlanoAtivoCliente => @"
            SELECT COUNT(1) 
            FROM PlanoCliente 
            WHERE ClienteId = @ClienteId 
                AND PlanoId = @PlanoId 
                AND Ativo = 1 
                AND DataVencimento >= @DataAtual
                AND DataExclusao IS NULL";

        public static string ListarPorCliente => @"
            SELECT pc.Id, pc.ClienteId, pc.PlanoId, pc.AcademiaId, pc.DataInicio, pc.DataVencimento, pc.Valor, 
                   pc.MesesContratados, pc.PlanoAtivo, pc.DataCancelamento, pc.MotivoCancelamento, pc.Ativo,
                   pc.DataInclusao, pc.DataAlteracao, pc.DataExclusao, pc.UsuarioInclusao, pc.UsuarioAlteracao
            FROM PlanoCliente pc
            INNER JOIN Cliente c ON pc.ClienteId = c.Id
            WHERE c.Cpf = @CpfCliente 
                AND pc.AcademiaId = @AcademiaId 
                AND pc.DataExclusao IS NULL
            ORDER BY pc.DataInicio DESC";

        public static string ListarPlanoAtivo => @"
            SELECT pc.Id, pc.ClienteId, pc.PlanoId, pc.AcademiaId, pc.DataInicio, pc.DataVencimento, pc.Valor, 
                   pc.MesesContratados, pc.PlanoAtivo, pc.DataCancelamento, pc.MotivoCancelamento, pc.Ativo,
                   pc.DataInclusao, pc.DataAlteracao, pc.DataExclusao, pc.UsuarioInclusao, pc.UsuarioAlteracao
            FROM PlanoCliente pc
            INNER JOIN Cliente c ON pc.ClienteId = c.Id
            WHERE c.Cpf = @CpfCliente 
                AND pc.AcademiaId = @AcademiaId 
                AND pc.Ativo = 1 
                AND pc.DataVencimento >= @DataAtual
                AND pc.DataExclusao IS NULL
            ORDER BY pc.DataVencimento ASC";

        public static string ListarPlanosVencendo => @"
            SELECT pc.Id, pc.ClienteId, pc.PlanoId, pc.AcademiaId, pc.DataInicio, pc.DataVencimento, pc.Valor, 
                   pc.MesesContratados, pc.PlanoAtivo, pc.DataCancelamento, pc.MotivoCancelamento, pc.Ativo,
                   pc.DataInclusao, pc.DataAlteracao, pc.DataExclusao, pc.UsuarioInclusao, pc.UsuarioAlteracao
            FROM PlanoCliente pc
            WHERE pc.AcademiaId = @AcademiaId 
                AND pc.Ativo = 1 
                AND pc.DataVencimento >= @DataAtual
                AND pc.DataVencimento <= @DataLimite
                AND pc.DataExclusao IS NULL
            ORDER BY pc.DataVencimento ASC";

        public static string CancelarPlano => @"
            UPDATE PlanoCliente 
            SET PlanoAtivo = 0, 
                DataCancelamento = @DataCancelamento,
                MotivoCancelamento = @Motivo,
                DataAlteracao = GETDATE()
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string RenovarPlano => @"
            UPDATE PlanoCliente 
            SET DataVencimento = DATEADD(MONTH, @MesesAdicionais, DataVencimento),
                Valor = Valor + @ValorPago,
                DataAlteracao = GETDATE()
            WHERE Id IN (
                SELECT TOP 1 pc.Id
                FROM PlanoCliente pc
                INNER JOIN Cliente c ON pc.ClienteId = c.Id
                WHERE c.Cpf = @CpfCliente 
                    AND pc.AcademiaId = @AcademiaId 
                    AND pc.Ativo = 1
                    AND pc.DataExclusao IS NULL
                ORDER BY pc.DataVencimento DESC
            )";

        public static string VerificarPlanoAtivoPorCpf => @"
            SELECT COUNT(1) 
            FROM PlanoCliente pc
            INNER JOIN Cliente c ON pc.ClienteId = c.Id
            WHERE c.Cpf = @CpfCliente 
                AND pc.AcademiaId = @AcademiaId 
                AND pc.Ativo = 1 
                AND pc.DataVencimento >= @DataAtual
                AND pc.DataExclusao IS NULL";

        public static string ListarTodos => @"
            SELECT Id, ClienteId, PlanoId, AcademiaId, DataInicio, DataVencimento, Valor, 
                   MesesContratados, PlanoAtivo, DataCancelamento, MotivoCancelamento, Ativo,
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM PlanoCliente 
            WHERE DataExclusao IS NULL
            ORDER BY DataInclusao DESC";

        public static string ListarAtivo => @"
            SELECT Id, ClienteId, PlanoId, AcademiaId, DataInicio, DataVencimento, Valor, 
                   MesesContratados, PlanoAtivo, DataCancelamento, MotivoCancelamento, Ativo,
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM PlanoCliente 
            WHERE Ativo = 1 AND DataExclusao IS NULL
            ORDER BY DataInclusao DESC";
    }
}
