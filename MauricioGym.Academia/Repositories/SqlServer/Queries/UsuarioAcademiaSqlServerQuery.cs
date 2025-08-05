namespace MauricioGym.Academia.Repositories.SqlServer.Queries
{
    public static class UsuarioAcademiaSqlServerQuery
    {
        public static string IncluirUsuarioAcademia => @"INSERT INTO UsuarioAcademias 
                                                        (IdUsuario, IdAcademia, DataVinculo, Ativo)
                                                        OUTPUT INSERTED.IdUsuarioAcademia
                                                        VALUES 
                                                        (@IdUsuario, @IdAcademia, @DataVinculo, @Ativo)";

        public static string ConsultarUsuarioAcademia => @"SELECT IdUsuarioAcademia,
                                                                  IdUsuario,
                                                                  IdAcademia,
                                                                  DataVinculo,
                                                                  Ativo
                                                           FROM UsuarioAcademias
                                                           WHERE IdUsuarioAcademia = @IdUsuarioAcademia
                                                             AND Ativo = 1";

        public static string AlterarUsuarioAcademia => @"UPDATE UsuarioAcademias 
                                                        SET IdUsuario = @IdUsuario,
                                                            IdAcademia = @IdAcademia,
                                                            DataVinculo = @DataVinculo,
                                                            Ativo = @Ativo
                                                        WHERE IdUsuarioAcademia = @IdUsuarioAcademia";

        public static string ExcluirUsuarioAcademia => @"UPDATE UsuarioAcademias 
                                                        SET Ativo = 0
                                                        WHERE IdUsuarioAcademia = @IdUsuarioAcademia";

        public static string ListarUsuarioAcademia => @"SELECT IdUsuarioAcademia,
                                                              IdUsuario,
                                                              IdAcademia,
                                                              DataVinculo,
                                                              Ativo
                                                       FROM UsuarioAcademias
                                                       WHERE Ativo = 1
                                                       ORDER BY DataVinculo DESC";

        public static string ListarUsuarioAcademiaPorUsuario => @"SELECT IdUsuarioAcademia,
                                                                        IdUsuario,
                                                                        IdAcademia,
                                                                        DataVinculo,
                                                                        Ativo
                                                                 FROM UsuarioAcademias
                                                                 WHERE IdUsuario = @IdUsuario
                                                                   AND Ativo = 1
                                                                 ORDER BY DataVinculo DESC";

        public static string ListarUsuarioAcademiaPorAcademia => @"SELECT IdUsuarioAcademia,
                                                                         IdUsuario,
                                                                         IdAcademia,
                                                                         DataVinculo,
                                                                         Ativo
                                                                  FROM UsuarioAcademias
                                                                  WHERE IdAcademia = @IdAcademia
                                                                    AND Ativo = 1
                                                                  ORDER BY DataVinculo DESC";
    }
}