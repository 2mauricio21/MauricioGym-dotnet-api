using Microsoft.Extensions.Configuration;

namespace MauricioGym.Infra
{
    public class AppConfiguration
    {
        private static AppConfiguration instance;

        public IConfiguration Configuration { get; }

        private AppConfiguration(IConfiguration configuration)
        {
            // Configuration
            if (configuration == null)
            {
                var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                Configuration = builder.Build();
            }
            else
                Configuration = configuration;
        }

        public static void CreateInstance(IConfiguration configuration)
        {
            if (instance == null)
                instance = new AppConfiguration(configuration);
        }

        public static AppConfiguration GetInstance()
        {
            if (instance == null)
                throw new Exception("MauricioGym.Infra.AppConfiguration não inicializado.");
            return instance;
        }

        public static IConfiguration GetConfiguration()
        {
            if (instance == null)
                throw new Exception("MauricioGym.Infra.AppConfiguration não inicializado.");
            return instance.Configuration;
        }
    }
}
