using MauricioGym.Seguranca.Repositories.SqlServer;
using MauricioGym.Seguranca.Repositories.SqlServer.Interfaces;
using MauricioGym.Seguranca.Services;
using MauricioGym.Seguranca.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace MauricioGym.Seguranca
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection ConfigureServicesSeguranca(
            this IServiceCollection services,
            IConfiguration configuration = null)
        {
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            // Services
            services.AddTransient<IAutenticacaoService, AutenticacaoService>();
            services.AddTransient<IRecuperacaoSenhaService, RecuperacaoSenhaService>();
            services.AddTransient<IHashService, HashService>();

            // Repositories
            services.AddTransient<IAutenticacaoSqlServerRepository, AutenticacaoSqlServerRepository>();

            return services;
        }
    }
}