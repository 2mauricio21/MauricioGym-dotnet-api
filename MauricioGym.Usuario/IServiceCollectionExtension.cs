using MauricioGym.Infra;
using MauricioGym.Usuario.Repositories.SqlServer;
using MauricioGym.Usuario.Repositories.SqlServer.Interfaces;
using MauricioGym.Usuario.Services;
using MauricioGym.Usuario.Services.Interfaces;
using MauricioGym.Usuario.Services.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MauricioGym.Usuario
{
    public static class IServiceCollectionExtension
    {
        private static void ConfigureServices(IServiceCollection services)
        {
            // Services
            services.AddTransient<ICheckInCompletoService, CheckInCompletoService>();
            services.AddTransient<ICheckInService, CheckInService>();
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IModalidadeService, ModalidadeService>();
            services.AddTransient<IPagamentoService, PagamentoService>();
            services.AddTransient<IPlanoClienteService, PlanoClienteService>();
            services.AddTransient<IPlanoService, PlanoService>();
            services.AddTransient<IVinculoClienteAcademiaService, VinculoClienteAcademiaService>();
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            // Repositories
            services.AddTransient<ICheckInSqlServerRepository, CheckInSqlServerRepository>();
            services.AddTransient<IClienteSqlServerRepository, ClienteSqlServerRepository>();
            services.AddTransient<IModalidadeSqlServerRepository, ModalidadeSqlServerRepository>();
            services.AddTransient<IPagamentoSqlServerRepository, PagamentoSqlServerRepository>();
            services.AddTransient<IPlanoClienteSqlServerRepository, PlanoClienteSqlServerRepository>();
            services.AddTransient<IPlanoSqlServerRepository, PlanoSqlServerRepository>();
            services.AddTransient<IVinculoClienteAcademiaSqlServerRepository, VinculoClienteAcademiaSqlServerRepository>();
        }

        private static void ConfigureValidators(IServiceCollection services)
        {
            // Validators
            services.AddTransient<CheckInValidator>();
            services.AddTransient<ClienteValidator>();
            services.AddTransient<ModalidadeValidator>();
            services.AddTransient<PagamentoValidator>();
            services.AddTransient<PlanoClienteValidator>();
            services.AddTransient<PlanoValidator>();
            services.AddTransient<VinculoClienteAcademiaValidator>();
        }

        public static IServiceCollection ConfigureServicesUsuario(this IServiceCollection services, IConfiguration configuration = null)
        {
            // Configurar a infraestrutura
            services.ConfigureServicesInfra(configuration);

            // Services
            ConfigureServices(services);

            // Repositories
            ConfigureRepositories(services);

            // Validators
            ConfigureValidators(services);

            return services;
        }
    }
}