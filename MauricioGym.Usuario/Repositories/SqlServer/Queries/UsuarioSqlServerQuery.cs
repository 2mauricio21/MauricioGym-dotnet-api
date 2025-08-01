namespace MauricioGym.Usuario.Repositories.SqlServer.Queries
{
    public class UsuarioSqlServerQuery
    {
        public static string IncluirUsuario => @"
            INSERT INTO Usuarios (
                Nome, Sobrenome, Email, Senha, CPF, Telefone, DataNascimento, 
                Endereco, Cidade, Estado, CEP, Ativo, DataCadastro, DataUltimoLogin
            ) VALUES (
                @Nome, @Sobrenome, @Email, @Senha, @CPF, @Telefone, @DataNascimento,
                @Endereco, @Cidade, @Estado, @CEP, @Ativo, @DataCadastro, @DataUltimoLogin
            );
            SELECT CAST(SCOPE_IDENTITY() as int)";

        public static string ConsultarUsuario => @"
            SELECT 
                IdUsuario, Nome, Sobrenome, Email, Senha, CPF, Telefone, 
                DataNascimento, Endereco, Cidade, Estado, CEP, Ativo, 
                DataCadastro, DataUltimoLogin
            FROM Usuarios
            WHERE IdUsuario = @IdUsuario AND Ativo = 1";

        public static string ConsultarUsuarioPorEmail => @"
            SELECT 
                IdUsuario, Nome, Sobrenome, Email, Senha, CPF, Telefone, 
                DataNascimento, Endereco, Cidade, Estado, CEP, Ativo, 
                DataCadastro, DataUltimoLogin
            FROM Usuarios
            WHERE Email = @Email AND Ativo = 1";

        public static string ConsultarUsuarioPorCPF => @"
            SELECT 
                IdUsuario, Nome, Sobrenome, Email, Senha, CPF, Telefone, 
                DataNascimento, Endereco, Cidade, Estado, CEP, Ativo, 
                DataCadastro, DataUltimoLogin
            FROM Usuarios
            WHERE CPF = @CPF AND Ativo = 1";

        public static string AlterarUsuario => @"
            UPDATE Usuarios SET
                Nome = @Nome,
                Sobrenome = @Sobrenome,
                Email = @Email,
                Senha = @Senha,
                CPF = @CPF,
                Telefone = @Telefone,
                DataNascimento = @DataNascimento,
                Endereco = @Endereco,
                Cidade = @Cidade,
                Estado = @Estado,
                CEP = @CEP,
                Ativo = @Ativo,
                DataUltimoLogin = @DataUltimoLogin
            WHERE IdUsuario = @IdUsuario";

        public static string ExcluirUsuario => @"
            UPDATE Usuarios SET Ativo = 0 WHERE IdUsuario = @IdUsuario";

        public static string ListarUsuarios => @"
            SELECT 
                IdUsuario, Nome, Sobrenome, Email, Senha, CPF, Telefone, 
                DataNascimento, Endereco, Cidade, Estado, CEP, Ativo, 
                DataCadastro, DataUltimoLogin
            FROM Usuarios
            ORDER BY Nome, Sobrenome";

        public static string ListarUsuariosAtivos => @"
            SELECT 
                IdUsuario, Nome, Sobrenome, Email, Senha, CPF, Telefone, 
                DataNascimento, Endereco, Cidade, Estado, CEP, Ativo, 
                DataCadastro, DataUltimoLogin
            FROM Usuarios
            WHERE Ativo = 1
            ORDER BY Nome, Sobrenome";
    }
}