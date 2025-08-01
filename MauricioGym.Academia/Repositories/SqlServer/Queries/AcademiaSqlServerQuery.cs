namespace MauricioGym.Academia.Repositories.SqlServer.Queries
{
    public class AcademiaSqlServerQuery
    {
        public static string IncluirAcademia => @"
            INSERT INTO Academias (
                Nome, CNPJ, Endereco, Cidade, Estado, CEP, Telefone, Email,
                HorarioAbertura, HorarioFechamento, Ativo, DataCadastro
            ) VALUES (
                @Nome, @CNPJ, @Endereco, @Cidade, @Estado, @CEP, @Telefone, @Email,
                @HorarioAbertura, @HorarioFechamento, @Ativo, @DataCadastro
            );
            SELECT CAST(SCOPE_IDENTITY() as int)";

        public static string ConsultarAcademia => @"
            SELECT 
                IdAcademia, Nome, CNPJ, Endereco, Cidade, Estado, CEP, Telefone,
                Email, HorarioAbertura, HorarioFechamento, Ativo, DataCadastro
            FROM Academias
            WHERE IdAcademia = @IdAcademia AND Ativo = 1";

        public static string ConsultarAcademiaPorCNPJ => @"
            SELECT 
                IdAcademia, Nome, CNPJ, Endereco, Cidade, Estado, CEP, Telefone,
                Email, HorarioAbertura, HorarioFechamento, Ativo, DataCadastro
            FROM Academias
            WHERE CNPJ = @CNPJ AND Ativo = 1";

        public static string AlterarAcademia => @"
            UPDATE Academias SET
                Nome = @Nome,
                CNPJ = @CNPJ,
                Endereco = @Endereco,
                Cidade = @Cidade,
                Estado = @Estado,
                CEP = @CEP,
                Telefone = @Telefone,
                Email = @Email,
                HorarioAbertura = @HorarioAbertura,
                HorarioFechamento = @HorarioFechamento,
                Ativo = @Ativo
            WHERE IdAcademia = @IdAcademia";

        public static string ExcluirAcademia => @"
            UPDATE Academias SET Ativo = 0 WHERE IdAcademia = @IdAcademia";

        public static string ListarAcademias => @"
            SELECT 
                IdAcademia, Nome, CNPJ, Endereco, Cidade, Estado, CEP, Telefone,
                Email, HorarioAbertura, HorarioFechamento, Ativo, DataCadastro
            FROM Academias
            ORDER BY Nome";

        public static string ListarAcademiasAtivas => @"
            SELECT 
                IdAcademia, Nome, CNPJ, Endereco, Cidade, Estado, CEP, Telefone,
                Email, HorarioAbertura, HorarioFechamento, Ativo, DataCadastro
            FROM Academias
            WHERE Ativo = 1
            ORDER BY Nome";
    }
}