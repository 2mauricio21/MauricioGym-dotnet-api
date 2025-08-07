using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? "MauricioGym_SecretKey_2024_SuperSecure_Key_For_JWT_Authentication";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "MauricioGym";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "MauricioGym";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
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
        Description = @"Gateway unificado para todas as APIs do sistema MauricioGym.
        
**Domínios Disponíveis:**
- **Usuários** (/api/usuario): Gerenciamento de usuários, autenticação e perfis
- **Academias** (/api/academia, /api/usuarioacademia): Gerenciamento de academias e associações
- **Controle de Acesso** (/api/acesso, /api/bloqueio): Controle de entrada e bloqueios
- **Pagamentos** (/api/pagamento, /api/formapagamento): Gestão financeira e formas de pagamento
- **Planos** (/api/plano, /api/usuarioplano): Gerenciamento de planos e assinaturas
        
**Autenticação:** Utilize JWT Bearer Token para acessar endpoints protegidos.",
        Contact = new OpenApiContact
        {
            Name = "MauricioGym Team",
            Email = "contato@mauriciogym.com"
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Configure JWT Authentication in Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header usando o esquema Bearer.
                      
Digite 'Bearer' [espaço] e então seu token na caixa de texto abaixo.
                      
Exemplo: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // Add tags for better organization
    c.TagActionsBy(api => new[] { GetTagFromPath(api.RelativePath) });
    c.DocInclusionPredicate((name, api) => true);
    
    // Include XML comments if available
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Helper method to organize endpoints by domain
static string GetTagFromPath(string? path)
{
    if (string.IsNullOrEmpty(path)) return "Gateway";
    
    if (path.Contains("/api/usuario")) return "👤 Usuários";
    if (path.Contains("/api/academia") || path.Contains("/api/usuarioacademia")) return "🏋️ Academias";
    if (path.Contains("/api/acesso") || path.Contains("/api/bloqueio")) return "🔐 Controle de Acesso";
    if (path.Contains("/api/pagamento") || path.Contains("/api/formapagamento")) return "💳 Pagamentos";
    if (path.Contains("/api/plano") || path.Contains("/api/usuarioplano")) return "📋 Planos";
    
    return "🌐 Gateway";
}

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
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "swagger/{documentName}/swagger.json";
    });
    
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MauricioGym Gateway v1");
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "MauricioGym - API Gateway Unificada";
        c.DefaultModelsExpandDepth(-1);
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
        c.EnableDeepLinking();
        c.EnableFilter();
        c.EnableValidator();
        c.DisplayRequestDuration();
        c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
        
        // Custom CSS for better appearance
        c.InjectStylesheet("/swagger-ui/custom.css");
        
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