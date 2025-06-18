namespace MauricioGym.Usuario.Repositories.SqlServer.Queries
{
    public static class CheckInSqlServerQuery
    {
        public static string ObterPorId => @"
            SELECT Id, CpfCliente, IdAcademia, DataHoraEntrada, DataHoraSaida, TipoCheckIn, 
                   ValorPago, Observacoes, EhAvulsa, Ativo, DataCriacao, DataAtualizacao, DataExclusao
            FROM CheckIn 
            WHERE Id = @Id AND Ativo = 1";
            
        public static string ObterPorClienteId => @"
            SELECT Id, CpfCliente, IdAcademia, DataHoraEntrada, DataHoraSaida, TipoCheckIn, 
                   ValorPago, Observacoes, EhAvulsa, Ativo, DataCriacao, DataAtualizacao, DataExclusao
            FROM CheckIn 
            WHERE CpfCliente = @ClienteId AND Ativo = 1
            ORDER BY DataHoraEntrada DESC";
            
        public static string ObterPorAcademiaId => @"
            SELECT Id, CpfCliente, IdAcademia, DataHoraEntrada, DataHoraSaida, TipoCheckIn, 
                   ValorPago, Observacoes, EhAvulsa, Ativo, DataCriacao, DataAtualizacao, DataExclusao
            FROM CheckIn 
            WHERE IdAcademia = @AcademiaId AND Ativo = 1
            ORDER BY DataHoraEntrada DESC";
            
        public static string ObterPorPeriodo => @"
            SELECT Id, CpfCliente, IdAcademia, DataHoraEntrada, DataHoraSaida, TipoCheckIn, 
                   ValorPago, Observacoes, EhAvulsa, Ativo, DataCriacao, DataAtualizacao, DataExclusao
            FROM CheckIn 
            WHERE DataHoraEntrada >= @DataInicio AND DataHoraEntrada <= @DataFim AND Ativo = 1
            ORDER BY DataHoraEntrada DESC";
            
        public static string ObterPorPeriodoEAcademia => @"
            SELECT Id, CpfCliente, IdAcademia, DataHoraEntrada, DataHoraSaida, TipoCheckIn, 
                   ValorPago, Observacoes, EhAvulsa, Ativo, DataCriacao, DataAtualizacao, DataExclusao
            FROM CheckIn 
            WHERE DataHoraEntrada >= @DataInicio AND DataHoraEntrada <= @DataFim 
              AND IdAcademia = @AcademiaId AND Ativo = 1
            ORDER BY DataHoraEntrada DESC";
            
        public static string ObterPorClienteEPeriodo => @"
            SELECT Id, CpfCliente, IdAcademia, DataHoraEntrada, DataHoraSaida, TipoCheckIn, 
                   ValorPago, Observacoes, EhAvulsa, Ativo, DataCriacao, DataAtualizacao, DataExclusao
            FROM CheckIn 
            WHERE CpfCliente = @ClienteId 
              AND DataHoraEntrada >= @DataInicio AND DataHoraEntrada <= @DataFim 
              AND Ativo = 1
            ORDER BY DataHoraEntrada DESC";
            
        public static string ObterUltimoCheckInCliente => @"
            SELECT TOP 1 Id, CpfCliente, IdAcademia, DataHoraEntrada, DataHoraSaida, TipoCheckIn, 
                   ValorPago, Observacoes, EhAvulsa, Ativo, DataCriacao, DataAtualizacao, DataExclusao
            FROM CheckIn 
            WHERE CpfCliente = @ClienteId AND IdAcademia = @AcademiaId AND Ativo = 1
            ORDER BY DataHoraEntrada DESC";
            
        public static string ContarCheckInsPorPeriodo => @"
            SELECT COUNT(1) 
            FROM CheckIn 
            WHERE CpfCliente = @ClienteId 
              AND DataHoraEntrada >= @DataInicio AND DataHoraEntrada <= @DataFim 
              AND Ativo = 1";
            
        public static string Criar => @"
            INSERT INTO CheckIn (CpfCliente, IdAcademia, DataHoraEntrada, DataHoraSaida, TipoCheckIn, 
                               ValorPago, Observacoes, EhAvulsa, Ativo, DataCriacao)
            VALUES (@CpfCliente, @IdAcademia, @DataHoraEntrada, @DataHoraSaida, @TipoCheckIn, 
                    @ValorPago, @Observacoes, @EhAvulsa, 1, GETDATE());
            SELECT CAST(SCOPE_IDENTITY() as int)";
            
        public static string Atualizar => @"
            UPDATE CheckIn 
            SET CpfCliente = @CpfCliente, 
                IdAcademia = @IdAcademia,
                DataHoraEntrada = @DataHoraEntrada,
                DataHoraSaida = @DataHoraSaida,
                TipoCheckIn = @TipoCheckIn,
                ValorPago = @ValorPago,
                Observacoes = @Observacoes,
                EhAvulsa = @EhAvulsa,
                DataAtualizacao = GETDATE() 
            WHERE Id = @Id AND Ativo = 1";
            
        public static string ExcluirLogico => @"
            UPDATE CheckIn 
            SET Ativo = 0, DataExclusao = @DataExclusao, DataAtualizacao = GETDATE() 
            WHERE Id = @Id";
            
        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM CheckIn 
            WHERE Id = @Id AND Ativo = 1";

        public static string ObterTodos => @"
            SELECT Id, CpfCliente, IdAcademia, DataHoraEntrada, DataHoraSaida, TipoCheckIn, 
                   ValorPago, Observacoes, EhAvulsa, Ativo, DataCriacao, DataAtualizacao, DataExclusao
            FROM CheckIn 
            WHERE Ativo = 1
            ORDER BY DataHoraEntrada DESC";

        public static string ObterPorUsuario => @"
            SELECT Id, CpfCliente, IdAcademia, DataHoraEntrada, DataHoraSaida, TipoCheckIn, 
                   ValorPago, Observacoes, EhAvulsa, Ativo, DataCriacao, DataAtualizacao, DataExclusao
            FROM CheckIn 
            WHERE CpfCliente = @UsuarioId AND Ativo = 1
            ORDER BY DataHoraEntrada DESC";

        public static string ClienteJaFezCheckInHoje => @"
            SELECT COUNT(1)
            FROM CheckIn
            WHERE CpfCliente = @ClienteId AND IdAcademia = @AcademiaId AND CONVERT(date, DataHoraEntrada) = @DataHoje AND Ativo = 1";

        public static string ContarCheckInsPorUsuarioMes => @"
            SELECT COUNT(1)
            FROM CheckIn
            WHERE CpfCliente = @UsuarioId AND YEAR(DataHoraEntrada) = @Ano AND MONTH(DataHoraEntrada) = @Mes AND Ativo = 1";

        public static string ObterUltimoIdInserido => @"
            SELECT TOP 1 Id
            FROM CheckIn
            WHERE CpfCliente = @ClienteId AND IdAcademia = @AcademiaId
            ORDER BY Id DESC";
    }
}
