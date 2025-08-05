using MauricioGym.Plano.Repositories.SqlServer;
using MauricioGym.Plano.Repositories.SqlServer.Interfaces;
using MauricioGym.Plano.Services;
using MauricioGym.Plano.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace MauricioGym.Plano
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection ConfigureServicesCadastroPlano(
            this IServiceCollection services,
            IConfiguration configuration = null)
        {
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            // Services
            services.AddTransient<IPlanoService, PlanoService>();
            services.AddTransient<IUsuarioPlanoService, UsuarioPlanoService>();

            // Repositories
            services.AddTransient<IPlanoSqlServerRepository, PlanoSqlServerRepository>();
            services.AddTransient<IUsuarioPlanoSqlServerRepository, UsuarioPlanoSqlServerRepository>();

            return services;
        }
    }
}