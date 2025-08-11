using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Repositories.SqlServer.Interfaces;
using MauricioGym.Infra.Repositories.SqlServer;
using MauricioGym.Usuario.Repositories.SqlServer.Interfaces;
using MauricioGym.Usuario.Repositories.SqlServer;
using MauricioGym.Usuario.Services.Interfaces;
using MauricioGym.Usuario.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MauricioGym.Infra;
using MauricioGym.Seguranca;
using MauricioGym.Usuario;

var builder = WebApplication.CreateBuilder(args);

// Inicializar AppConfiguration
AppConfiguration.CreateInstance(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do SQLServer
builder.Services.AddScoped<SQLServerDbContext>();

// Configuração do JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey não configurada");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "http://localhost:8000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Configuração de serviços por módulo
builder.Services.ConfigureServicesInfra(builder.Configuration);
builder.Services.ConfigureServicesSeguranca(builder.Configuration);
builder.Services.ConfigureServicesCadastroUsuario(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();