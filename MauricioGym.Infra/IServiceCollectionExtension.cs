using MauricioGym.Infra.Config;
using MauricioGym.Infra.Databases.SqlServer;
using MauricioGym.Infra.Interfaces;
using MauricioGym.Infra.Repositories;
using MauricioGym.Infra.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace MauricioGym.Infra
{
    public static class IServiceCollectionExtension
    {        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            // Configuration
            AppConfiguration.CreateInstance(configuration);
            configuration = AppConfiguration.GetConfiguration();            // SQL Server
            services.Configure<SqlServerConnectionOptions>(options => 
            {
                options.DefaultConnection = configuration["ConnectionStrings:DefaultConnection"] ?? 
                                           AppConfig.SqlServerConnectionString;
            });
            services.AddScoped<SqlServerDbContext>();

            // Services
            services.AddTransient<IAuditoriaService, AuditoriaService>();
        }        public static IServiceCollection ConfigureServicesInfra(this IServiceCollection services, IConfiguration? configuration = null)
        {
            // Se configuration for null, cria uma configuração simples para testes
            if (configuration == null)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                configuration = builder.Build();
            }

            // Services
            ConfigureServices(services, configuration);

            // Repositories
            services.AddTransient<ITransactionSqlServerRepository, TransactionSqlServerRepository>();

            return services;
        }
    }
}
