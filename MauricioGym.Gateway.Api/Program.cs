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

// Add HttpClient for Health checks
builder.Services.AddHttpClient<MauricioGym.Gateway.Api.Controllers.HealthController>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(10);
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
        policy.SetIsOriginAllowed(origin => true)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
              .WithExposedHeaders("*");
    });
    
    options.AddPolicy("Development", policy =>
    {
        policy.WithOrigins(
                "http://localhost:8000",
                "https://localhost:8000",
                "https://localhost:8001",
                "http://127.0.0.1:8000",
                "https://127.0.0.1:8000",
                "https://127.0.0.1:8001",
                "http://192.168.1.1:8000",
                "https://192.168.1.1:8001",
                "http://10.0.0.1:8000",
                "https://10.0.0.1:8001"
              )
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
              .WithExposedHeaders("Content-Type", "Content-Length", "Date", "Server", "Authorization", "Access-Control-Allow-Origin");
    });
    
    options.AddPolicy("SwaggerUI", policy =>
    {
        policy.SetIsOriginAllowed(origin => 
                origin.StartsWith("http://localhost") || 
                origin.StartsWith("https://localhost") ||
                origin.StartsWith("http://127.0.0.1") ||
                origin.StartsWith("https://127.0.0.1") ||
                origin.StartsWith("http://192.168.") ||
                origin.StartsWith("https://192.168.") ||
                origin.StartsWith("http://10.") ||
                origin.StartsWith("https://10.") ||
                origin.StartsWith("http://172.") ||
                origin.StartsWith("https://172.")
              )
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
              .WithExposedHeaders("*");
    });
});

// Configure URLs - Both HTTP and HTTPS for flexibility
if (builder.Environment.IsDevelopment())
{
    // Support both HTTP and HTTPS for development
    builder.WebHost.UseUrls("http://*:8000", "https://*:8001");
    
    // Configure Kestrel for both protocols
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(8000); // HTTP - Accept from any IP
        options.ListenAnyIP(8001, listenOptions =>
        {
            listenOptions.UseHttps(); // HTTPS with development certificate
        });
    });
}
else
{
    builder.WebHost.UseUrls("http://*:8000", "https://*:8001");
}

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
        
        // Custom configurations for external browser compatibility
        c.ConfigObject.AdditionalItems["showExtensions"] = true;
        c.ConfigObject.AdditionalItems["showCommonExtensions"] = true;
        c.ConfigObject.AdditionalItems["supportedSubmitMethods"] = new[] { "get", "post", "put", "delete", "patch" };
        c.ConfigObject.AdditionalItems["tryItOutEnabled"] = true;
        
        // Custom styling and scripts for external browsers - Support both HTTP and HTTPS
        c.HeadContent = @"
            <style>
                .swagger-ui .topbar { background-color: #1976d2; }
                .swagger-ui .topbar .download-url-wrapper { display: none; }
                .swagger-ui .info .title { color: #1976d2; }
                .swagger-ui .scheme-container { background: #f8f9fa; padding: 10px; border-radius: 4px; }
                .swagger-ui .response .response-col_status { min-width: 100px; }
                .swagger-ui .response .response-col_links { min-width: 100px; }
            </style>
            <script>
                // Fix for external browsers - Support both HTTP and HTTPS with fallback
                window.addEventListener('DOMContentLoaded', function() {
                    console.log('Swagger UI loaded for external browser - HTTP/HTTPS mode');
                    
                    // Override fetch to handle CORS properly and fallback to HTTP if HTTPS fails
                    const originalFetch = window.fetch;
                    window.fetch = function(url, options = {}) {
                        options.mode = 'cors';
                        options.credentials = 'include';
                        
                        console.log('Making request to:', url, 'with options:', options);
                        
                        return originalFetch(url, options).catch(error => {
                            console.warn('Request failed, trying HTTP fallback:', error);
                            // If HTTPS fails, try HTTP as fallback
                            if (url.startsWith('https://')) {
                                const httpUrl = url.replace('https://', 'http://').replace(':8001', ':8000');
                                console.log('Falling back to HTTP:', httpUrl);
                                return originalFetch(httpUrl, options);
                            }
                            throw error;
                        });
                    };
                    
                    // Override XMLHttpRequest as fallback with HTTP/HTTPS support
                    const originalXHR = window.XMLHttpRequest;
                    window.XMLHttpRequest = function() {
                        const xhr = new originalXHR();
                        const originalOpen = xhr.open;
                        const originalSend = xhr.send;
                        
                        xhr.open = function(method, url, ...args) {
                            this._originalUrl = url;
                            return originalOpen.call(this, method, url, ...args);
                        };
                        
                        xhr.send = function(data) {
                            const originalOnError = this.onerror;
                            this.onerror = function(e) {
                                console.warn('XHR failed, trying HTTP fallback');
                                if (this._originalUrl && this._originalUrl.startsWith('https://')) {
                                    const httpUrl = this._originalUrl.replace('https://', 'http://').replace(':8001', ':8000');
                                    console.log('XHR: Falling back to HTTP:', httpUrl);
                                    const newXhr = new originalXHR();
                                    newXhr.open(this._method || 'GET', httpUrl, true);
                                    newXhr.send(data);
                                    return;
                                }
                                if (originalOnError) originalOnError.call(this, e);
                            };
                            return originalSend.call(this, data);
                        };
                        
                        return xhr;
                    };
                });
            </script>
        ";
    });
}

app.UseCors("AllowAll");

// Add middleware to handle CORS and security headers for external browsers
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    
    // Add security headers for all requests
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
    context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    
    // Special handling for Swagger UI to work with external browsers
    if (context.Request.Path.StartsWithSegments("/swagger"))
    {
        // Allow embedding in same origin for Swagger UI
        context.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
        
        // Add Content Security Policy for Swagger UI - Support both HTTP and HTTPS
        context.Response.Headers["Content-Security-Policy"] = 
            "default-src 'self' 'unsafe-inline' 'unsafe-eval'; " +
            "script-src 'self' 'unsafe-inline' 'unsafe-eval'; " +
            "style-src 'self' 'unsafe-inline'; " +
            "img-src 'self' data:; " +
            "font-src 'self' data:; " +
            "connect-src 'self' http://*:* https://*:* http://localhost:* https://localhost:* http://127.0.0.1:* https://127.0.0.1:* http://192.168.*:* https://192.168.*:* http://10.*:* https://10.*:* http://172.*:* https://172.*:*;";
    }
    
    // Handle preflight requests for CORS - Enhanced for Chrome/Firefox
    if (context.Request.Method == "OPTIONS")
    {
        var origin = context.Request.Headers.Origin.ToString();
        context.Response.Headers["Access-Control-Allow-Origin"] = string.IsNullOrEmpty(origin) ? "*" : origin;
        context.Response.Headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS, PATCH";
        context.Response.Headers["Access-Control-Allow-Headers"] = "Content-Type, Authorization, X-Requested-With, Accept, Origin, Access-Control-Request-Method, Access-Control-Request-Headers";
        context.Response.Headers["Access-Control-Allow-Credentials"] = "true";
        context.Response.Headers["Access-Control-Max-Age"] = "86400";
        context.Response.Headers["Vary"] = "Origin";
        context.Response.StatusCode = 200;
        return;
    }
    
    // Add CORS headers for all requests - Chrome/Firefox compatibility
    var requestOrigin = context.Request.Headers.Origin.ToString();
    if (!string.IsNullOrEmpty(requestOrigin))
    {
        context.Response.Headers["Access-Control-Allow-Origin"] = requestOrigin;
        context.Response.Headers["Access-Control-Allow-Credentials"] = "true";
        context.Response.Headers["Vary"] = "Origin";
    }
    
    // Log request with origin
    logger.LogInformation("Request: {Method} {Path} from {Origin} - {UserAgent}", 
        context.Request.Method, 
        context.Request.Path,
        context.Request.Headers.Origin.ToString(),
        context.Request.Headers.UserAgent.ToString());
    
    // Capture response
    var originalBodyStream = context.Response.Body;
    using var responseBody = new MemoryStream();
    context.Response.Body = responseBody;
    
    await next();
    
    // Log response with CORS headers
    logger.LogInformation("Response: {StatusCode} {ContentType} CORS: {AccessControlAllowOrigin}", 
        context.Response.StatusCode,
        context.Response.ContentType,
        context.Response.Headers.AccessControlAllowOrigin.ToString());
    
    // Copy response back
    responseBody.Seek(0, SeekOrigin.Begin);
    await responseBody.CopyToAsync(originalBodyStream);
});

app.UseAuthentication();
app.UseAuthorization();

// Map reverse proxy routes
app.MapReverseProxy();

// Map controllers
app.MapControllers();

app.Run();