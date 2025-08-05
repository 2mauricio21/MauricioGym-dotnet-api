using MauricioGym.Pagamento.Repositories.SqlServer;
using MauricioGym.Pagamento.Repositories.SqlServer.Interfaces;
using MauricioGym.Pagamento.Services;
using MauricioGym.Pagamento.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace MauricioGym.Pagamento
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection ConfigureServicesCadastroPagamento(
            this IServiceCollection services,
            IConfiguration configuration = null)
        {
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            // Services
            services.AddTransient<IFormaPagamentoService, FormaPagamentoService>();
            services.AddTransient<IPagamentoService, PagamentoService>();

            // Repositories
            services.AddTransient<IFormaPagamentoSqlServerRepository, FormaPagamentoSqlServerRepository>();
            services.AddTransient<IPagamentoSqlServerRepository, PagamentoSqlServerRepository>();

            return services;
        }
    }
}