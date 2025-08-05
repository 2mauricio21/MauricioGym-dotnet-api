using MauricioGym.Usuario.Repositories.SqlServer;
using MauricioGym.Usuario.Repositories.SqlServer.Interfaces;
using MauricioGym.Usuario.Services;
using MauricioGym.Usuario.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace MauricioGym.Usuario
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection ConfigureServicesCadastroUsuario(
            this IServiceCollection services,
            IConfiguration configuration = null)
        {
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            // Services
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IRecursoService, RecursoService>();

            // Repositories
            services.AddTransient<IUsuarioSqlServerRepository, UsuarioSqlServerRepository>();
            services.AddTransient<IRecursoSqlServerRepository, RecursoSqlServerRepository>();

            return services;
        }
    }
}