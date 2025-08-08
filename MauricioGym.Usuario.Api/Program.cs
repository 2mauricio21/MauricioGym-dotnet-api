using MauricioGym.Infra;
using MauricioGym.Usuario;
using MauricioGym.Infra.Config;
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins(
                "http://localhost:8000",
                "https://localhost:8001",
                "http://localhost:5001",
                "https://localhost:7001",
                "http://127.0.0.1:8000",
                "https://127.0.0.1:8001",
                "http://127.0.0.1:5001",
                "https://127.0.0.1:7001"
              )
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = AppConfig.ISSUER_JWT,
            ValidAudience = AppConfig.AUDIENCE_JWT,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.SECURITY_KEY_JWT))
        };
    });

// Configure custom services
builder.Services.ConfigureServicesInfra(builder.Configuration);
builder.Services.ConfigureServicesCadastroUsuario(builder.Configuration);

// Register JWT Service
builder.Services.AddScoped<IJwtService, JwtService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // Note: SwaggerUI is intentionally not configured to avoid auto-opening browser
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();