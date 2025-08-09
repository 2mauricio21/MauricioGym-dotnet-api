using MauricioGym.Infra.Repositories;
using MauricioGym.Infra.Repositories.SqlServer.Interfaces;
using MauricioGym.Infra.Repositories.SQLServer;
using MauricioGym.Infra.Repositories.SqlServer;
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
            services.AddTransient<IHashService, HashService>();
            
            return services;
        }

        /// <summary>
        /// Configura CORS de forma centralizada para todas as APIs do sistema
        /// </summary>
        /// <param name="services">Collection de serviços</param>
        /// <returns>IServiceCollection configurado</returns>
        public static IServiceCollection ConfigureCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.WithOrigins(
                            // Frontend Angular
                            "http://localhost:4200",
                            "https://localhost:4200",
                            "http://127.0.0.1:4200",
                            "https://127.0.0.1:4200",
                            // Gateway API
                            "http://localhost:8000",
                            "https://localhost:8001",
                            "http://127.0.0.1:8000",
                            "https://127.0.0.1:8001",
                            // APIs individuais (para desenvolvimento)
                            "http://localhost:5001", "https://localhost:7001", // Usuario API
                            "http://localhost:5002", "https://localhost:7002", // Academia API
                            "http://localhost:5003", "https://localhost:7003", // Acesso API
                            "http://localhost:5004", "https://localhost:7004", // Pagamento API
                            "http://localhost:5005", "https://localhost:7005", // Plano API
                            "http://127.0.0.1:5001", "https://127.0.0.1:7001",
                            "http://127.0.0.1:5002", "https://127.0.0.1:7002",
                            "http://127.0.0.1:5003", "https://127.0.0.1:7003",
                            "http://127.0.0.1:5004", "https://127.0.0.1:7004",
                            "http://127.0.0.1:5005", "https://127.0.0.1:7005"
                          )
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            return services;
        }
    }
}
