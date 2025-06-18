namespace MauricioGym.Infra.Databases.SQLServer.Errors
{
    internal class FieldSizeSqlErrorInfo
    {
        public string Table { get; set; }
        public string Column { get; set; }
        public int TruncatedValueLength { get; set; }
    }

    internal class DuplicateSqlErrorInfo
    {
        public string Table { get; set; }

        public string DuplicateValue { get; set; }

    }
}
