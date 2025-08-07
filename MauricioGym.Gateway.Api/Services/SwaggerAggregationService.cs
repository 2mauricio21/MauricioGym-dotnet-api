using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Writers;
using System.Text.Json;

namespace MauricioGym.Gateway.Api.Services;

public class SwaggerAggregationService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SwaggerAggregationService> _logger;
    private readonly IConfiguration _configuration;

    public SwaggerAggregationService(HttpClient httpClient, ILogger<SwaggerAggregationService> logger, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<OpenApiDocument> GetAggregatedSwaggerAsync()
    {
        var mainDocument = new OpenApiDocument
        {
            Info = new OpenApiInfo
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
            },
            Servers = new List<OpenApiServer>
            {
                new OpenApiServer
                {
                    Url = "http://localhost:8000",
                    Description = "Gateway de Desenvolvimento"
                }
            },
            Paths = new OpenApiPaths(),
            Components = new OpenApiComponents
            {
                SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>
                {
                    ["Bearer"] = new OpenApiSecurityScheme
                    {
                        Description = @"JWT Authorization header usando o esquema Bearer.
                      
Digite 'Bearer' [espaço] e então seu token na caixa de texto abaixo.
                      
Exemplo: 'Bearer 12345abcdef'",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT"
                    }
                }
            },
            SecurityRequirements = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
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
                }
            }
        };

        // APIs to aggregate
        var apis = new Dictionary<string, string>
        {
            { "Usuario", "http://localhost:5001" },
            { "Academia", "http://localhost:5002" },
            { "Acesso", "http://localhost:5003" },
            { "Pagamento", "http://localhost:5004" },
            { "Plano", "http://localhost:5005" }
        };

        foreach (var api in apis)
        {
            try
            {
                await AggregateApiEndpoints(mainDocument, api.Key, api.Value);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to aggregate API {ApiName} from {BaseUrl}", api.Key, api.Value);
            }
        }

        // Add Gateway endpoints
        AddGatewayEndpoints(mainDocument);

        return mainDocument;
    }

    private async Task AggregateApiEndpoints(OpenApiDocument mainDocument, string apiName, string baseUrl)
    {
        try
        {
            var swaggerUrl = $"{baseUrl}/swagger/v1/swagger.json";
            var response = await _httpClient.GetAsync(swaggerUrl);
            
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to fetch swagger from {SwaggerUrl}. Status: {StatusCode}", swaggerUrl, response.StatusCode);
                return;
            }

            var swaggerJson = await response.Content.ReadAsStringAsync();
            var reader = new OpenApiStringReader();
            var apiDocument = reader.Read(swaggerJson, out var diagnostic);

            if (apiDocument?.Paths != null)
            {
                foreach (var path in apiDocument.Paths)
                {
                    var newPath = path.Key;
                    
                    // Add tag to organize endpoints
                    foreach (var operation in path.Value.Operations)
                    {
                        var tag = GetTagForApi(apiName);
                        operation.Value.Tags = new List<OpenApiTag> { new OpenApiTag { Name = tag } };
                    }

                    mainDocument.Paths[newPath] = path.Value;
                }

                // Merge components (schemas, etc.)
                if (apiDocument.Components?.Schemas != null)
                {
                    foreach (var schema in apiDocument.Components.Schemas)
                    {
                        // Use original schema key to maintain references
                        if (!mainDocument.Components.Schemas.ContainsKey(schema.Key))
                        {
                            mainDocument.Components.Schemas[schema.Key] = schema.Value;
                        }
                    }
                }

                // Merge other components
                if (apiDocument.Components != null)
                {
                    // Merge SecuritySchemes
                    if (apiDocument.Components.SecuritySchemes != null)
                    {
                        foreach (var securityScheme in apiDocument.Components.SecuritySchemes)
                        {
                            if (!mainDocument.Components.SecuritySchemes.ContainsKey(securityScheme.Key))
                            {
                                mainDocument.Components.SecuritySchemes[securityScheme.Key] = securityScheme.Value;
                            }
                        }
                    }

                    // Merge Parameters
                    if (apiDocument.Components.Parameters != null)
                    {
                        mainDocument.Components.Parameters ??= new Dictionary<string, OpenApiParameter>();
                        foreach (var parameter in apiDocument.Components.Parameters)
                        {
                            if (!mainDocument.Components.Parameters.ContainsKey(parameter.Key))
                            {
                                mainDocument.Components.Parameters[parameter.Key] = parameter.Value;
                            }
                        }
                    }

                    // Merge Responses
                    if (apiDocument.Components.Responses != null)
                    {
                        mainDocument.Components.Responses ??= new Dictionary<string, OpenApiResponse>();
                        foreach (var apiResponse in apiDocument.Components.Responses)
                        {
                            if (!mainDocument.Components.Responses.ContainsKey(apiResponse.Key))
                            {
                                mainDocument.Components.Responses[apiResponse.Key] = apiResponse.Value;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error aggregating API {ApiName} from {BaseUrl}", apiName, baseUrl);
        }
    }

    private void AddGatewayEndpoints(OpenApiDocument mainDocument)
    {
        // Add Gateway info endpoint
        mainDocument.Paths["/api/gateway/info"] = new OpenApiPathItem
        {
            Operations = new Dictionary<OperationType, OpenApiOperation>
            {
                [OperationType.Get] = new OpenApiOperation
                {
                    Tags = new List<OpenApiTag> { new OpenApiTag { Name = "🌐 Gateway" } },
                    Summary = "Obtém informações sobre o Gateway e APIs disponíveis",
                    Responses = new OpenApiResponses
                    {
                        ["200"] = new OpenApiResponse
                        {
                            Description = "Informações do Gateway"
                        }
                    }
                }
            }
        };

        // Add Gateway health endpoint
        mainDocument.Paths["/api/gateway/health"] = new OpenApiPathItem
        {
            Operations = new Dictionary<OperationType, OpenApiOperation>
            {
                [OperationType.Get] = new OpenApiOperation
                {
                    Tags = new List<OpenApiTag> { new OpenApiTag { Name = "🌐 Gateway" } },
                    Summary = "Verifica o status de saúde do Gateway",
                    Responses = new OpenApiResponses
                    {
                        ["200"] = new OpenApiResponse
                        {
                            Description = "Status de saúde"
                        }
                    }
                }
            }
        };

        // Add Gateway protected endpoint
        mainDocument.Paths["/api/gateway/protected"] = new OpenApiPathItem
        {
            Operations = new Dictionary<OperationType, OpenApiOperation>
            {
                [OperationType.Get] = new OpenApiOperation
                {
                    Tags = new List<OpenApiTag> { new OpenApiTag { Name = "🌐 Gateway" } },
                    Summary = "Endpoint protegido para teste de autenticação",
                    Security = new List<OpenApiSecurityRequirement>
                    {
                        new OpenApiSecurityRequirement
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
                        }
                    },
                    Responses = new OpenApiResponses
                    {
                        ["200"] = new OpenApiResponse
                        {
                            Description = "Informações do usuário autenticado"
                        },
                        ["401"] = new OpenApiResponse
                        {
                            Description = "Não autorizado"
                        }
                    }
                }
            }
        };
    }

    private static string GetTagForApi(string apiName)
    {
        return apiName switch
        {
            "Usuario" => "👤 Usuários",
            "Academia" => "🏋️ Academias",
            "Acesso" => "🔐 Controle de Acesso",
            "Pagamento" => "💳 Pagamentos",
            "Plano" => "📋 Planos",
            _ => "🌐 Gateway"
        };
    }
}