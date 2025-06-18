namespace MauricioGym.Administrador.Repositories.SqlServer.Queries
{
    public static class AcademiaSqlServerQuery
    {
        public static string ObterPorId => @"
            SELECT Id, Nome, Cnpj, Telefone, Email, Endereco, Cidade, Estado, Cep, 
                   DataVencimentoLicenca, MaximoClientes, LicencaAtiva, Ativo, 
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Academia 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ObterPorCnpj => @"
            SELECT Id, Nome, Cnpj, Telefone, Email, Endereco, Cidade, Estado, Cep, 
                   DataVencimentoLicenca, MaximoClientes, LicencaAtiva, Ativo, 
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Academia 
            WHERE Cnpj = @Cnpj AND DataExclusao IS NULL";

        public static string ObterTodos => @"
            SELECT Id, Nome, Cnpj, Telefone, Email, Endereco, Cidade, Estado, Cep, 
                   DataVencimentoLicenca, MaximoClientes, LicencaAtiva, Ativo, 
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Academia 
            WHERE DataExclusao IS NULL
            ORDER BY Nome";

        public static string ObterAtivos => @"
            SELECT Id, Nome, Cnpj, Telefone, Email, Endereco, Cidade, Estado, Cep, 
                   DataVencimentoLicenca, MaximoClientes, LicencaAtiva, Ativo, 
                   DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM Academia 
            WHERE Ativo = 1 AND DataExclusao IS NULL
            ORDER BY Nome";

        public static string Criar => @"
            INSERT INTO Academia (Nome, Cnpj, Telefone, Email, Endereco, Cidade, Estado, Cep, 
                                 DataVencimentoLicenca, MaximoClientes, LicencaAtiva, Ativo, 
                                 DataInclusao, UsuarioInclusao)
            OUTPUT INSERTED.Id
            VALUES (@Nome, @Cnpj, @Telefone, @Email, @Endereco, @Cidade, @Estado, @Cep, 
                    @DataVencimentoLicenca, @MaximoClientes, @LicencaAtiva, @Ativo, 
                    @DataInclusao, @UsuarioInclusao)";

        public static string Atualizar => @"
            UPDATE Academia 
            SET Nome = @Nome,
                Cnpj = @Cnpj,
                Telefone = @Telefone,
                Email = @Email,
                Endereco = @Endereco,
                Cidade = @Cidade,
                Estado = @Estado,
                Cep = @Cep,
                DataVencimentoLicenca = @DataVencimentoLicenca,
                MaximoClientes = @MaximoClientes,
                LicencaAtiva = @LicencaAtiva,
                Ativo = @Ativo,
                DataAlteracao = @DataAlteracao,
                UsuarioAlteracao = @UsuarioAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ExcluirLogico => @"
            UPDATE Academia 
            SET DataExclusao = @DataExclusao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM Academia 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistenciaCnpj => @"
            SELECT COUNT(1) 
            FROM Academia 
            WHERE Cnpj = @Cnpj 
                AND DataExclusao IS NULL
                AND (@IdExcluir IS NULL OR Id != @IdExcluir)";
    }
}
