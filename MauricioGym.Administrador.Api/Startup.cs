using MauricioGym.Administrador.Services;
using MauricioGym.Administrador.Services.Interfaces;
using MauricioGym.Administrador.Repositories.SqlServer;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Infra;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace MauricioGym.Administrador.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "MauricioGym - API Administrador", 
                    Version = "v1",
                    Description = "API para gerenciamento administrativo do MauricioGym"
                });
            });

            // Configurar a infraestrutura
            services.ConfigureServicesInfra(Configuration);

            // Connection String
            var connectionString = Configuration.GetConnectionString("DefaultConnection") 
                ?? "Server=(localdb)\\mssqllocaldb;Database=MauricioGymDB;Trusted_Connection=true;TrustServerCertificate=true;";

            // Services
            services.AddScoped<IAdministradorService, AdministradorService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IPlanoService, PlanoService>();
            services.AddScoped<ICaixaService, CaixaService>();
            services.AddScoped<IPermissaoManipulacaoUsuarioService, PermissaoManipulacaoUsuarioService>();
            services.AddScoped<IUsuarioPlanoService, UsuarioPlanoService>();

            // Repositories
            services.AddScoped<IAdministradorSqlServerRepository>(provider => 
                new AdministradorSqlServerRepository(connectionString));
            services.AddScoped<IUsuarioSqlServerRepository>(provider =>
                new UsuarioSqlServerRepository(connectionString));
            services.AddScoped<IPlanoSqlServerRepository>(provider => 
                new PlanoSqlServerRepository(connectionString));
            services.AddScoped<ICaixaSqlServerRepository>(provider => 
                new CaixaSqlServerRepository(connectionString));
            services.AddScoped<IPermissaoManipulacaoUsuarioSqlServerRepository>(provider => 
                new PermissaoManipulacaoUsuarioSqlServerRepository(connectionString));
            services.AddScoped<IUsuarioPlanoSqlServerRepository>(provider => 
                new UsuarioPlanoSqlServerRepository(connectionString));

            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Sempre habilitar Swagger para desenvolvimento e testes
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MauricioGym - API Administrador v1");
                c.RoutePrefix = "swagger";
            });

            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
