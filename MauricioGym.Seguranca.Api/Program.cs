using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.SQLServer;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Repositories.SqlServer.Interfaces;
using MauricioGym.Infra.Repositories.SqlServer;
using MauricioGym.Seguranca.Repositories.SqlServer.Interfaces;
using MauricioGym.Seguranca.Repositories.SqlServer;
using MauricioGym.Seguranca.Services.Interfaces;
using MauricioGym.Seguranca.Services;
using MauricioGym.Usuario.Repositories.SqlServer.Interfaces;
using MauricioGym.Usuario.Repositories.SqlServer;
using MauricioGym.Usuario.Services.Interfaces;
using MauricioGym.Usuario.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MauricioGym.Infra;

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
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Registro de dependências - Infra
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuditoriaSqlServerRepository, AuditoriaSqlServerRepository>();
builder.Services.AddScoped<IAuditoriaService, AuditoriaService>();
builder.Services.AddScoped<IHashService, HashService>();

// Registro de dependências - Segurança
builder.Services.AddScoped<IAutenticacaoSqlServerRepository, AutenticacaoSqlServerRepository>();
builder.Services.AddScoped<IRecuperacaoSenhaSqlServerRepository, RecuperacaoSenhaSqlServerRepository>();
builder.Services.AddScoped<IAutenticacaoService, AutenticacaoService>();
builder.Services.AddScoped<IRecuperacaoSenhaService, RecuperacaoSenhaService>();

// Registro de dependências - Usuário
builder.Services.AddScoped<IUsuarioSqlServerRepository, UsuarioSqlServerRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

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