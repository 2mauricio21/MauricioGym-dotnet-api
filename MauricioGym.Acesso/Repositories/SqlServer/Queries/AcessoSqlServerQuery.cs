namespace MauricioGym.Acesso.Repositories.SqlServer.Queries
{
    public class AcessoSqlServerQuery
    {
        public static string IncluirAcesso => @"
            INSERT INTO Acessos (
                IdUsuario, IdAcademia, DataHoraEntrada, DataHoraSaida,
                TipoAcesso, ObservacaoAcesso, AcessoLiberado, MotivoNegacao
            ) VALUES (
                @IdUsuario, @IdAcademia, @DataHoraEntrada, @DataHoraSaida,
                @TipoAcesso, @ObservacaoAcesso, @AcessoLiberado, @MotivoNegacao
            );
            SELECT CAST(SCOPE_IDENTITY() as int)";

        public static string ConsultarAcesso => @"
            SELECT 
                IdAcesso, IdUsuario, IdAcademia, DataHoraEntrada,
                DataHoraSaida, TipoAcesso, ObservacaoAcesso, AcessoLiberado, MotivoNegacao
            FROM Acessos
            WHERE IdAcesso = @IdAcesso";

        public static string ConsultarAcessosPorUsuario => @"
            SELECT 
                IdAcesso, IdUsuario, IdAcademia, DataHoraEntrada,
                DataHoraSaida, TipoAcesso, ObservacaoAcesso, AcessoLiberado, MotivoNegacao
            FROM Acessos
            WHERE IdUsuario = @IdUsuario
            ORDER BY DataHoraEntrada DESC";

        public static string ConsultarAcessosPorAcademia => @"
            SELECT 
                IdAcesso, IdUsuario, IdAcademia, DataHoraEntrada,
                DataHoraSaida, TipoAcesso, ObservacaoAcesso, AcessoLiberado, MotivoNegacao
            FROM Acessos
            WHERE IdAcademia = @IdAcademia
            ORDER BY DataHoraEntrada DESC";

        public static string ConsultarAcessosAtivosPorAcademia => @"
            SELECT 
                IdAcesso, IdUsuario, IdAcademia, DataHoraEntrada,
                DataHoraSaida, TipoAcesso, ObservacaoAcesso, AcessoLiberado, MotivoNegacao
            FROM Acessos
            WHERE IdAcademia = @IdAcademia AND TipoAcesso = 'Ativo'
            ORDER BY DataHoraEntrada DESC";

        public static string AlterarAcesso => @"
            UPDATE Acessos SET
                IdUsuario = @IdUsuario,
                IdAcademia = @IdAcademia,
                DataHoraEntrada = @DataHoraEntrada,
                DataHoraSaida = @DataHoraSaida,
                TipoAcesso = @TipoAcesso,
                ObservacaoAcesso = @ObservacaoAcesso,
                AcessoLiberado = @AcessoLiberado,
                MotivoNegacao = @MotivoNegacao
            WHERE IdAcesso = @IdAcesso";

        public static string RegistrarSaida => @"
            UPDATE Acessos SET
                DataHoraSaida = @DataHoraSaida,
                TipoAcesso = 'Finalizado'
            WHERE IdAcesso = @IdAcesso";

        public static string ListarAcessos => @"
            SELECT 
                IdAcesso, IdUsuario, IdAcademia, DataHoraEntrada,
                DataHoraSaida, TipoAcesso, ObservacaoAcesso, AcessoLiberado, MotivoNegacao
            FROM Acessos
            ORDER BY DataHoraEntrada DESC";
    }
}