using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json;
using MauricioGym.Infra.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add HttpClient for Swagger aggregation
builder.Services.AddHttpClient<MauricioGym.Gateway.Api.Services.SwaggerAggregationService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
});

// Register Swagger aggregation service
builder.Services.AddScoped<MauricioGym.Gateway.Api.Services.SwaggerAggregationService>();

// Configure JWT Authentication using AppConfig
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

// Configure YARP Reverse Proxy
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// Configure Swagger with unified API documentation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MauricioGym - API Gateway Unificada",
        Version = "v1",
        Description = "Gateway unificado para todas as APIs do sistema MauricioGym"
    });

    // Include XML comments if available
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});



// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure URLs
builder.WebHost.UseUrls("http://localhost:8000");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(c =>
    {
        // Use the aggregated swagger endpoint
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MauricioGym - API Gateway Unificada v1");
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "MauricioGym - API Gateway Unificada";
        c.DefaultModelsExpandDepth(-1);
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
        c.EnableDeepLinking();
        c.EnableFilter();
        c.EnableValidator();
        c.DisplayRequestDuration();
        c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
        
        // Custom header
        c.HeadContent = @"
            <style>
                .swagger-ui .topbar { background-color: #1976d2; }
                .swagger-ui .topbar .download-url-wrapper { display: none; }
                .swagger-ui .info .title { color: #1976d2; }
                .swagger-ui .scheme-container { background: #f8f9fa; padding: 10px; border-radius: 4px; }
            </style>
        ";
    });
}

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

// Map reverse proxy routes
app.MapReverseProxy();

// Map controllers
app.MapControllers();

app.Run();