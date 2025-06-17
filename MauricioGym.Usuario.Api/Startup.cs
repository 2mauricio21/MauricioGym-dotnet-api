using MauricioGym.Usuario.Services;
using MauricioGym.Usuario.Services.Interfaces;
using MauricioGym.Usuario.Repositories.SqlServer;
using MauricioGym.Usuario.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace MauricioGym.Usuario.Api
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
                    Title = "MauricioGym - API Usuario", 
                    Version = "v1",
                    Description = "API para usuários do MauricioGym (Check-in e Mensalidades)"
                });
            });            // Connection String
            var connectionString = Configuration.GetConnectionString("DefaultConnection") 
                ?? "Server=(localdb)\\mssqllocaldb;Database=MauricioGymDB;Trusted_Connection=true;TrustServerCertificate=true;";

            // Services
            services.AddScoped<ICheckInService, CheckInService>();
            services.AddScoped<IMensalidadeService, MensalidadeService>();

            // Repositories
            services.AddScoped<ICheckInSqlServerRepository>(provider => 
                new CheckInSqlServerRepository(connectionString));
            services.AddScoped<IMensalidadeSqlServerRepository>(provider => 
                new MensalidadeSqlServerRepository(connectionString));

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MauricioGym - API Usuario v1");
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
