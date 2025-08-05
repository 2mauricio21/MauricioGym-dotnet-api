namespace MauricioGym.Plano.Repositories.SqlServer.Queries
{
    public class PlanoSqlServerQuery
    {
        public static string IncluirPlano => @"
            INSERT INTO Planos (
                IdAcademia, Nome, Descricao, Valor, DuracaoEmDias, 
                PermiteAcessoTotal, HorarioInicioPermitido, HorarioFimPermitido, 
                Ativo, DataCadastro
            ) VALUES (
                @IdAcademia, @Nome, @Descricao, @Valor, @DuracaoEmDias,
                @PermiteAcessoTotal, @HorarioInicioPermitido, @HorarioFimPermitido,
                @Ativo, @DataCadastro
            );
            SELECT CAST(SCOPE_IDENTITY() as int)";

        public static string ConsultarPlano => @"
            SELECT 
                IdPlano, IdAcademia, Nome, Descricao, Valor, DuracaoEmDias,
                PermiteAcessoTotal, HorarioInicioPermitido, HorarioFimPermitido,
                Ativo, DataCadastro
            FROM Planos
            WHERE IdPlano = @IdPlano AND Ativo = 1";

        public static string ConsultarPlanoPorNome => @"
            SELECT 
                IdPlano, IdAcademia, Nome, Descricao, Valor, DuracaoEmDias,
                PermiteAcessoTotal, HorarioInicioPermitido, HorarioFimPermitido,
                Ativo, DataCadastro
            FROM Planos
            WHERE Nome = @Nome AND IdAcademia = @IdAcademia AND Ativo = 1";

        public static string AlterarPlano => @"
            UPDATE Planos SET
                IdAcademia = @IdAcademia,
                Nome = @Nome,
                Descricao = @Descricao,
                Valor = @Valor,
                DuracaoEmDias = @DuracaoEmDias,
                PermiteAcessoTotal = @PermiteAcessoTotal,
                HorarioInicioPermitido = @HorarioInicioPermitido,
                HorarioFimPermitido = @HorarioFimPermitido,
                Ativo = @Ativo
            WHERE IdPlano = @IdPlano";

        public static string ExcluirPlano => @"
            UPDATE Planos SET Ativo = 0 WHERE IdPlano = @IdPlano";

        public static string ListarPlanos => @"
            SELECT 
                IdPlano, IdAcademia, Nome, Descricao, Valor, DuracaoEmDias,
                PermiteAcessoTotal, HorarioInicioPermitido, HorarioFimPermitido,
                Ativo, DataCadastro
            FROM Planos
            ORDER BY Nome";

        public static string ListarPlanosPorAcademia => @"
            SELECT 
                IdPlano, IdAcademia, Nome, Descricao, Valor, DuracaoEmDias,
                PermiteAcessoTotal, HorarioInicioPermitido, HorarioFimPermitido,
                Ativo, DataCadastro
            FROM Planos
            WHERE IdAcademia = @IdAcademia AND Ativo = 1
            ORDER BY Nome";

        public static string ListarPlanosAtivos => @"
            SELECT 
                IdPlano, IdAcademia, Nome, Descricao, Valor, DuracaoEmDias,
                PermiteAcessoTotal, HorarioInicioPermitido, HorarioFimPermitido,
                Ativo, DataCadastro
            FROM Planos
            WHERE Ativo = 1
            ORDER BY Nome";
    }
}