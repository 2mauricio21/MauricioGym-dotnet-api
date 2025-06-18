namespace MauricioGym.Infra.SQLServer
{
    public class SQLServerConnectionOptions
    {
        public bool IsDevelopment { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public string DatabaseName { get; set; }

        //public string ConnectionString
        //{
        //    get
        //    {
        //        return $"data source={Host};initial catalog={DatabaseName};persist security info=True;user id={User};password={Password};MultipleActiveResultSets=True";
        //    }
        //}

        public string ConnectionString
        {            
            get
            {
                if (IsDevelopment)
                {
                    return $"Server=(localdb)\\mssqllocaldb;Database={DatabaseName ?? "MauricioGymDB"};Trusted_Connection=True;MultipleActiveResultSets=True";
                }
                else
                {
                    return $"Data Source={Host};Initial Catalog={DatabaseName};Persist Security Info=True;User ID={User};Password={Password};MultipleActiveResultSets=True";
                }
            }
        }
    }
}
