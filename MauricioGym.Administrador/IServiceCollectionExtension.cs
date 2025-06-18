using MauricioGym.Administrador.Repositories.SqlServer;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Services;
using MauricioGym.Administrador.Services.Interfaces;
using MauricioGym.Administrador.Services.Validators;
using MauricioGym.Infra;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MauricioGym.Administrador
{
    public static class IServiceCollectionExtension
    {
        private static void ConfigureServices(IServiceCollection services)
        {
            // Services
            services.AddTransient<IAcademiaService, AcademiaService>();
            services.AddTransient<IAdministradorPapelService, AdministradorPapelService>();
            services.AddTransient<IAdministradorService, AdministradorService>();
            services.AddTransient<IPapelPermissaoService, PapelPermissaoService>();
            services.AddTransient<IPapelService, PapelService>();
            services.AddTransient<IPermissaoService, PermissaoService>();
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            // Repositories
            services.AddTransient<IAcademiaSqlServerRepository, AcademiaSqlServerRepository>();
            services.AddTransient<IAdministradorPapelSqlServerRepository, AdministradorPapelSqlServerRepository>();
            services.AddTransient<IAdministradorSqlServerRepository, AdministradorSqlServerRepository>();
            services.AddTransient<IAlunoSqlServerRepository, AlunoSqlServerRepository>();
            services.AddTransient<IAvaliacaoFisicaSqlServerRepository, AvaliacaoFisicaSqlServerRepository>();
            services.AddTransient<IExercicioSqlServerRepository, ExercicioSqlServerRepository>();
            services.AddTransient<IFrequenciaSqlServerRepository, FrequenciaSqlServerRepository>();
            services.AddTransient<IMensalidadeSqlServerRepository, MensalidadeSqlServerRepository>();
            services.AddTransient<IPapelPermissaoSqlServerRepository, PapelPermissaoSqlServerRepository>();
            services.AddTransient<IPapelSqlServerRepository, PapelSqlServerRepository>();
            services.AddTransient<IPermissaoSqlServerRepository, PermissaoSqlServerRepository>();
            services.AddTransient<IPlanoSqlServerRepository, PlanoSqlServerRepository>();
            services.AddTransient<ITreinoExercicioSqlServerRepository, TreinoExercicioSqlServerRepository>();
            services.AddTransient<ITreinoSqlServerRepository, TreinoSqlServerRepository>();
            services.AddTransient<IUsuarioPapelSqlServerRepository, UsuarioPapelSqlServerRepository>();
        }

        private static void ConfigureValidators(IServiceCollection services)
        {
            // Validators
            services.AddTransient<AcademiaValidator>();
            services.AddTransient<AdministradorPapelValidator>();
            services.AddTransient<AdministradorValidator>();
            services.AddTransient<AlunoValidator>();
            services.AddTransient<AvaliacaoFisicaValidator>();
            services.AddTransient<MensalidadeValidator>();
            services.AddTransient<PapelPermissaoValidator>();
            services.AddTransient<PapelValidator>();
            services.AddTransient<PermissaoValidator>();
        }

        public static IServiceCollection ConfigureServicesAdministrador(this IServiceCollection services, IConfiguration configuration = null)
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