namespace MauricioGym.Usuario.Repositories.SqlServer.Queries
{
    public static class VinculoClienteAcademiaSqlServerQuery
    {
        public static string ObterPorId => @"
            SELECT Id, ClienteId, AcademiaId, DataVinculo, DataDesvinculo, Ativo, 
                   Observacoes, DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM VinculoClienteAcademia 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ObterPorClienteEAcademia => @"
            SELECT Id, ClienteId, AcademiaId, DataVinculo, DataDesvinculo, Ativo,
                   Observacoes, DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM VinculoClienteAcademia 
            WHERE ClienteId = @ClienteId 
                AND AcademiaId = @AcademiaId 
                AND DataExclusao IS NULL";

        public static string ObterPorClienteId => @"
            SELECT Id, ClienteId, AcademiaId, DataVinculo, DataDesvinculo, Ativo,
                   Observacoes, DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM VinculoClienteAcademia 
            WHERE ClienteId = @ClienteId AND DataExclusao IS NULL
            ORDER BY DataVinculo DESC";

        public static string ObterPorAcademiaId => @"
            SELECT Id, ClienteId, AcademiaId, DataVinculo, DataDesvinculo, Ativo,
                   Observacoes, DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM VinculoClienteAcademia 
            WHERE AcademiaId = @AcademiaId AND DataExclusao IS NULL
            ORDER BY DataVinculo DESC";

        public static string ObterTodos => @"
            SELECT Id, ClienteId, AcademiaId, DataVinculo, DataDesvinculo, Ativo,
                   Observacoes, DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM VinculoClienteAcademia 
            WHERE DataExclusao IS NULL
            ORDER BY DataVinculo DESC";

        public static string ObterAtivos => @"
            SELECT Id, ClienteId, AcademiaId, DataVinculo, DataDesvinculo, Ativo,
                   Observacoes, DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM VinculoClienteAcademia 
            WHERE Ativo = 1 AND DataExclusao IS NULL
            ORDER BY DataVinculo DESC";

        public static string ObterAtivosPorClienteId => @"
            SELECT Id, ClienteId, AcademiaId, DataVinculo, DataDesvinculo, Ativo,
                   Observacoes, DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM VinculoClienteAcademia 
            WHERE ClienteId = @ClienteId 
                AND Ativo = 1 
                AND DataExclusao IS NULL
            ORDER BY DataVinculo DESC";

        public static string ObterAtivosPorAcademiaId => @"
            SELECT Id, ClienteId, AcademiaId, DataVinculo, DataDesvinculo, Ativo,
                   Observacoes, DataInclusao, DataAlteracao, DataExclusao, UsuarioInclusao, UsuarioAlteracao
            FROM VinculoClienteAcademia 
            WHERE AcademiaId = @AcademiaId 
                AND Ativo = 1 
                AND DataExclusao IS NULL
            ORDER BY DataVinculo DESC";

        public static string Criar => @"
            INSERT INTO VinculoClienteAcademia (
                ClienteId, AcademiaId, DataVinculo, DataDesvinculo, Ativo,
                Observacoes, DataInclusao, UsuarioInclusao
            )
            OUTPUT INSERTED.Id
            VALUES (
                @ClienteId, @AcademiaId, @DataVinculo, @DataDesvinculo, @Ativo,
                @Observacoes, @DataInclusao, @UsuarioInclusao
            )";

        public static string Atualizar => @"
            UPDATE VinculoClienteAcademia 
            SET ClienteId = @ClienteId,
                AcademiaId = @AcademiaId,
                DataVinculo = @DataVinculo,
                DataDesvinculo = @DataDesvinculo,
                Ativo = @Ativo,
                Observacoes = @Observacoes,
                DataAlteracao = @DataAlteracao,
                UsuarioAlteracao = @UsuarioAlteracao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ExcluirLogico => @"
            UPDATE VinculoClienteAcademia 
            SET DataExclusao = @DataExclusao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM VinculoClienteAcademia 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string VerificarExistenciaVinculo => @"
            SELECT COUNT(1) 
            FROM VinculoClienteAcademia 
            WHERE ClienteId = @ClienteId 
                AND AcademiaId = @AcademiaId 
                AND DataExclusao IS NULL";

        public static string VerificarExistenciaVinculoAtivo => @"
            SELECT COUNT(1) 
            FROM VinculoClienteAcademia 
            WHERE ClienteId = @ClienteId 
                AND AcademiaId = @AcademiaId 
                AND Ativo = 1 
                AND DataExclusao IS NULL";

        public static string ObterPorAcademia => @"
            SELECT vca.Id, vca.ClienteId, vca.AcademiaId, vca.DataVinculo, vca.DataDesvinculo, vca.Ativo,
                   vca.Observacoes, vca.DataInclusao, vca.DataAlteracao, vca.DataExclusao, vca.UsuarioInclusao, vca.UsuarioAlteracao
            FROM VinculoClienteAcademia vca
            WHERE vca.AcademiaId = @AcademiaId 
                AND vca.DataExclusao IS NULL
            ORDER BY vca.DataVinculo DESC";

        public static string ObterPorCliente => @"
            SELECT vca.Id, vca.ClienteId, vca.AcademiaId, vca.DataVinculo, vca.DataDesvinculo, vca.Ativo,
                   vca.Observacoes, vca.DataInclusao, vca.DataAlteracao, vca.DataExclusao, vca.UsuarioInclusao, vca.UsuarioAlteracao
            FROM VinculoClienteAcademia vca
            INNER JOIN Cliente c ON vca.ClienteId = c.Id
            WHERE c.Cpf = @CpfCliente 
                AND vca.DataExclusao IS NULL
            ORDER BY vca.DataVinculo DESC";

        public static string ObterVinculo => @"
            SELECT vca.Id, vca.ClienteId, vca.AcademiaId, vca.DataVinculo, vca.DataDesvinculo, vca.Ativo,
                   vca.Observacoes, vca.DataInclusao, vca.DataAlteracao, vca.DataExclusao, vca.UsuarioInclusao, vca.UsuarioAlteracao
            FROM VinculoClienteAcademia vca
            INNER JOIN Cliente c ON vca.ClienteId = c.Id
            WHERE c.Cpf = @CpfCliente 
                AND vca.AcademiaId = @AcademiaId 
                AND vca.DataExclusao IS NULL
            ORDER BY vca.DataVinculo DESC";

        public static string DesativarVinculo => @"
            UPDATE VinculoClienteAcademia 
            SET Ativo = 0, 
                DataDesvinculo = @DataDesativacao,
                DataAlteracao = GETDATE()
            WHERE Id IN (
                SELECT vca.Id
                FROM VinculoClienteAcademia vca
                INNER JOIN Cliente c ON vca.ClienteId = c.Id
                WHERE c.Cpf = @CpfCliente 
                    AND vca.AcademiaId = @AcademiaId 
                    AND vca.Ativo = 1
                    AND vca.DataExclusao IS NULL
            )";

        public static string VerificarVinculoAtivoPorCpf => @"
            SELECT COUNT(1) 
            FROM VinculoClienteAcademia vca
            INNER JOIN Cliente c ON vca.ClienteId = c.Id
            WHERE c.Cpf = @CpfCliente 
                AND vca.AcademiaId = @AcademiaId 
                AND vca.Ativo = 1 
                AND vca.DataExclusao IS NULL";

        public static string ContarClientesAtivos => @"
            SELECT COUNT(1) 
            FROM VinculoClienteAcademia 
            WHERE AcademiaId = @AcademiaId 
                AND Ativo = 1 
                AND DataExclusao IS NULL";
    }
}
