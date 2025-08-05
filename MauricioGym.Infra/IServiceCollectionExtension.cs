using MauricioGym.Infra.Repositories;
using MauricioGym.Infra.Repositories.SqlServer.Interfaces;
using MauricioGym.Infra.Repositories.SQLServer;
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using MauricioGym.Infra.SQLServer;

namespace MauricioGym.Infra
{
    public static class IServiceCollectionExtension
    {
        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            // Configuration
            AppConfiguration.CreateInstance(configuration);
            configuration = AppConfiguration.GetConfiguration();

            //SQLServerDbContext.Database = configuration["SqlServerDb:Transporte"];
            services.Configure<SQLServerConnectionOptions>(op => { configuration.GetSection("SqlServerDb").Bind(op); });
            // SQL Server
            services.AddScoped<SQLServerDbContext>();
        }

        public static IServiceCollection ConfigureServicesInfra(this IServiceCollection services, IConfiguration configuration = null)
        {
            // Services
            ConfigureServices(services, configuration);

            // Repositories
            services.AddTransient<ITransactionSqlServerRepository, TransactionSqlServerRepository>();
            services.AddTransient<IAuditoriaSqlServerRepository, AuditoriaSqlServerRepository>();
            
            // Services
            services.AddTransient<IAuditoriaService, AuditoriaService>();
            
            return services;
        }
    }
}
