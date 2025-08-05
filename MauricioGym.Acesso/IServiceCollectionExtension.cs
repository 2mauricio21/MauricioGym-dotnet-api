using MauricioGym.Acesso.Repositories.SqlServer;
using MauricioGym.Acesso.Repositories.SqlServer.Interfaces;
using MauricioGym.Acesso.Services;
using MauricioGym.Acesso.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace MauricioGym.Acesso
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection ConfigureServicesCadastroAcesso(
            this IServiceCollection services,
            IConfiguration configuration = null)
        {
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            // Services
            services.AddTransient<IAcessoService, AcessoService>();
            services.AddTransient<IBloqueioAcessoService, BloqueioAcessoService>();

            // Repositories
            services.AddTransient<IAcessoSqlServerRepository, AcessoSqlServerRepository>();
            services.AddTransient<IBloqueioAcessoSqlServerRepository, BloqueioAcessoSqlServerRepository>();

            return services;
        }
    }
}