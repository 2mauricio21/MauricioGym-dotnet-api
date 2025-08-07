using Microsoft.Extensions.Configuration;

namespace MauricioGym.Infra.Config
{
    public static class AppConfig
    {
        public static bool ModoDebug => true;

        public static string SECURITY_KEY_JWT => "5CEE71AD-00B9-41B4-BF0C-97F2B6E80709";

        public static string ISSUER_JWT => "mauriciogym.com.br";

        public static string AUDIENCE_JWT => "mauriciogym.app";

        public static string SqlServerConnectionString => GetConfiguration()["ConnectionStrings:DefaultConnection"] ?? 
            "Server=(localdb)\\mssqllocaldb;Database=MauricioGym;Trusted_Connection=True;";

        private static IConfiguration GetConfiguration()
        {
            try
            {
                return AppConfiguration.GetConfiguration();
            }
            catch
            {
                // Fallback se a configuração não estiver inicializada
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build();
            }
        }
    }
}
