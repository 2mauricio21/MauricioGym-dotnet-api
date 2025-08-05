using MauricioGym.Academia.Repositories.SqlServer;
using MauricioGym.Academia.Repositories.SqlServer.Interfaces;
using MauricioGym.Academia.Services;
using MauricioGym.Academia.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace MauricioGym.Academia
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection ConfigureServicesCadastroAcademia(
            this IServiceCollection services,
            IConfiguration configuration = null)
        {
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            // Services
            services.AddTransient<IAcademiaService, AcademiaService>();
            services.AddTransient<IUsuarioAcademiaService, UsuarioAcademiaService>();

            // Repositories
            services.AddTransient<IAcademiaSqlServerRepository, AcademiaSqlServerRepository>();
            services.AddTransient<IUsuarioAcademiaSqlServerRepository, UsuarioAcademiaSqlServerRepository>();

            return services;
        }
    }
}