using MauricioGym.Repositories.SqlServer;
using MauricioGym.Repositories.SqlServer.Interfaces;
using MauricioGym.Services;
using MauricioGym.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MauricioGym
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AdicionarServicosMauricioGym(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IPessoaSqlServerRepository>(sp => new PessoaSqlServerRepository(connectionString));
            services.AddScoped<IPessoaService, PessoaService>();
            services.AddScoped<IPessoaPlanoSqlServerRepository>(sp => new PessoaPlanoSqlServerRepository(connectionString));
            services.AddScoped<IPessoaPlanoService, PessoaPlanoService>();
            services.AddScoped<IPermissaoManipulacaoUsuarioSqlServerRepository>(sp => new PermissaoManipulacaoUsuarioSqlServerRepository(connectionString));
            services.AddScoped<IPermissaoManipulacaoUsuarioService, PermissaoManipulacaoUsuarioService>();
            services.AddScoped<IPlanoSqlServerRepository>(sp => new PlanoSqlServerRepository(connectionString));
            services.AddScoped<IPlanoService, PlanoService>();
            services.AddScoped<IAdministradorSqlServerRepository>(sp => new AdministradorSqlServerRepository(connectionString));
            services.AddScoped<IAdministradorService, AdministradorService>();
            services.AddScoped<ICaixaSqlServerRepository>(sp => new CaixaSqlServerRepository(connectionString));
            services.AddScoped<ICaixaService, CaixaService>();
            services.AddScoped<ICheckInSqlServerRepository>(sp => new CheckInSqlServerRepository(connectionString));
            services.AddScoped<ICheckInService, CheckInService>();
            services.AddScoped<IMensalidadeSqlServerRepository>(sp => new MensalidadeSqlServerRepository(connectionString));
            services.AddScoped<IMensalidadeService, MensalidadeService>();
            // Adicione outros serviços e repositórios aqui
            return services;
        }
    }
}
