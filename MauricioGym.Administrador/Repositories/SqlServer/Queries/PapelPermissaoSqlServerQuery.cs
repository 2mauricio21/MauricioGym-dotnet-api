namespace MauricioGym.Administrador.Repositories.SqlServer.Queries
{
    public static class PapelPermissaoSqlServerQuery
    {
        public static string ListarPorId => @"
            SELECT Id, PapelId, PermissaoId, DataCriacao, DataExclusao
            FROM PapelPermissao 
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ListarPorPapelPermissao => @"
            SELECT Id, PapelId, PermissaoId, DataCriacao, DataExclusao
            FROM PapelPermissao 
            WHERE PapelId = @PapelId 
                AND PermissaoId = @PermissaoId 
                AND DataExclusao IS NULL";

        public static string ListarPorPapelId => @"
            SELECT Id, PapelId, PermissaoId, DataCriacao, DataExclusao
            FROM PapelPermissao 
            WHERE PapelId = @PapelId AND DataExclusao IS NULL
            ORDER BY DataCriacao";

        public static string ListarPorPermissaoId => @"
            SELECT Id, PapelId, PermissaoId, DataCriacao, DataExclusao
            FROM PapelPermissao 
            WHERE PermissaoId = @PermissaoId AND DataExclusao IS NULL
            ORDER BY DataCriacao";

        public static string ListarPermissoesDoPapel => @"
            SELECT p.Id, p.Nome, p.Descricao, p.Recurso, p.Acao, p.Ativo, p.DataCriacao, p.DataAlteracao, p.DataExclusao
            FROM Permissao p
            INNER JOIN PapelPermissao pp ON p.Id = pp.PermissaoId
            WHERE pp.PapelId = @PapelId 
                AND p.DataExclusao IS NULL 
                AND pp.DataExclusao IS NULL
            ORDER BY p.Recurso, p.Acao";

        public static string ListarPapeisComPermissao => @"
            SELECT p.Id, p.Nome, p.Descricao, p.EhSistema, p.Ativo, p.DataCriacao, p.DataAlteracao, p.DataExclusao
            FROM Papel p
            INNER JOIN PapelPermissao pp ON p.Id = pp.PapelId
            WHERE pp.PermissaoId = @PermissaoId 
                AND p.DataExclusao IS NULL 
                AND pp.DataExclusao IS NULL
            ORDER BY p.Nome";

        public static string Criar => @"
            INSERT INTO PapelPermissao (PapelId, PermissaoId, DataCriacao)
            OUTPUT INSERTED.Id
            VALUES (@PapelId, @PermissaoId, @DataCriacao)";

        public static string ExcluirLogico => @"
            UPDATE PapelPermissao 
            SET DataExclusao = @DataExclusao
            WHERE Id = @Id AND DataExclusao IS NULL";

        public static string ExcluirLogicoPorPapelEPermissao => @"
            UPDATE PapelPermissao 
            SET DataExclusao = @DataExclusao
            WHERE PapelId = @PapelId 
                AND PermissaoId = @PermissaoId 
                AND DataExclusao IS NULL";

        public static string VerificarExistenciaAssociacao => @"
            SELECT COUNT(1) 
            FROM PapelPermissao 
            WHERE PapelId = @PapelId 
                AND PermissaoId = @PermissaoId 
                AND DataExclusao IS NULL";

        public static string VerificarExistencia => @"
            SELECT COUNT(1) 
            FROM PapelPermissao 
            WHERE Id = @Id 
                AND DataExclusao IS NULL";
    }

}
