namespace MauricioGym.Acesso.Repositories.SqlServer.Queries
{
    public class AcessoSqlServerQuery
    {
        public static string IncluirAcesso => @"
            INSERT INTO Acessos (
                IdUsuarioAcademia, IdUsuario, IdAcademia, DataEntrada, DataSaida,
                StatusAcesso, Observacao
            ) VALUES (
                @IdUsuarioAcademia, @IdUsuario, @IdAcademia, @DataEntrada, @DataSaida,
                @StatusAcesso, @Observacao
            );
            SELECT CAST(SCOPE_IDENTITY() as int)";

        public static string ConsultarAcesso => @"
            SELECT 
                IdAcesso, IdUsuarioAcademia, IdUsuario, IdAcademia, DataEntrada,
                DataSaida, StatusAcesso, Observacao
            FROM Acessos
            WHERE IdAcesso = @IdAcesso";

        public static string ConsultarAcessosPorUsuario => @"
            SELECT 
                IdAcesso, IdUsuarioAcademia, IdUsuario, IdAcademia, DataEntrada,
                DataSaida, StatusAcesso, Observacao
            FROM Acessos
            WHERE IdUsuario = @IdUsuario
            ORDER BY DataEntrada DESC";

        public static string ConsultarAcessosPorAcademia => @"
            SELECT 
                IdAcesso, IdUsuarioAcademia, IdUsuario, IdAcademia, DataEntrada,
                DataSaida, StatusAcesso, Observacao
            FROM Acessos
            WHERE IdAcademia = @IdAcademia
            ORDER BY DataEntrada DESC";

        public static string ConsultarAcessosAtivosPorAcademia => @"
            SELECT 
                IdAcesso, IdUsuarioAcademia, IdUsuario, IdAcademia, DataEntrada,
                DataSaida, StatusAcesso, Observacao
            FROM Acessos
            WHERE IdAcademia = @IdAcademia AND StatusAcesso = 'Ativo'
            ORDER BY DataEntrada DESC";

        public static string AlterarAcesso => @"
            UPDATE Acessos SET
                IdUsuarioAcademia = @IdUsuarioAcademia,
                IdUsuario = @IdUsuario,
                IdAcademia = @IdAcademia,
                DataEntrada = @DataEntrada,
                DataSaida = @DataSaida,
                StatusAcesso = @StatusAcesso,
                Observacao = @Observacao
            WHERE IdAcesso = @IdAcesso";

        public static string RegistrarSaida => @"
            UPDATE Acessos SET
                DataSaida = @DataSaida,
                StatusAcesso = 'Finalizado'
            WHERE IdAcesso = @IdAcesso";

        public static string ListarAcessos => @"
            SELECT 
                IdAcesso, IdUsuarioAcademia, IdUsuario, IdAcademia, DataEntrada,
                DataSaida, StatusAcesso, Observacao
            FROM Acessos
            ORDER BY DataEntrada DESC";
    }
}