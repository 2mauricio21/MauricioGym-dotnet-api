namespace MauricioGym.Repositories.SqlServer.Queries
{
    public static class CheckInSqlServerQuery
    {
        public static string InserirCheckIn => "INSERT INTO CheckIn (PessoaId, DataHora) VALUES (@PessoaId, @DataHora); SELECT CAST(SCOPE_IDENTITY() as int);";
        public static string ObterPorId => "SELECT * FROM CheckIn WHERE Id = @Id";
        public static string ListarPorPessoa => "SELECT * FROM CheckIn WHERE PessoaId = @PessoaId";
        public static string ContarPorPessoaMes => "SELECT COUNT(*) FROM CheckIn WHERE PessoaId = @PessoaId AND YEAR(DataHora) = @Ano AND MONTH(DataHora) = @Mes";
    }
}
