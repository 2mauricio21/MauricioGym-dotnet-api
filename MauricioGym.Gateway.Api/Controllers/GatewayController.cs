using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MauricioGym.Infra.Controller;

namespace MauricioGym.Gateway.Api.Controllers;

/// <summary>
/// Controller principal do Gateway para informações e status das APIs
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class GatewayController : ApiController
{
    /// <summary>
    /// Obtém informações sobre o Gateway e APIs disponíveis
    /// </summary>
    /// <returns>Informações do Gateway</returns>
    [HttpGet("info")]
    public IActionResult GetGatewayInfo()
    {
        var info = new
        {
            Gateway = "MauricioGym API Gateway",
            Version = "1.0.0",
            Description = "Gateway unificado para todas as APIs do sistema MauricioGym",
            AvailableApis = new[]
            {
                new { Name = "Usuario API", BaseUrl = "/api/usuario", Description = "Gestão de usuários e autenticação" },
                new { Name = "Academia API", BaseUrl = "/api/academia", Description = "Gestão de academias" },
                new { Name = "Usuario Academia API", BaseUrl = "/api/usuarioacademia", Description = "Associação usuário-academia" },
                new { Name = "Acesso API", BaseUrl = "/api/acesso", Description = "Controle de acesso" },
                new { Name = "Bloqueio API", BaseUrl = "/api/bloqueio", Description = "Gestão de bloqueios" },
                new { Name = "Pagamento API", BaseUrl = "/api/pagamento", Description = "Gestão de pagamentos" },
                new { Name = "Forma Pagamento API", BaseUrl = "/api/formapagamento", Description = "Formas de pagamento" },
                new { Name = "Plano API", BaseUrl = "/api/plano", Description = "Gestão de planos" },
                new { Name = "Usuario Plano API", BaseUrl = "/api/usuarioplano", Description = "Assinaturas de planos" }
            },
            Timestamp = DateTime.UtcNow
        };

        return Ok(info);
    }

    /// <summary>
    /// Verifica o status de saúde do Gateway
    /// </summary>
    /// <returns>Status de saúde</returns>
    [HttpGet("health")]
    public IActionResult GetHealth()
    {
        var health = new
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            Uptime = Environment.TickCount64,
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"
        };

        return Ok(health);
    }

    /// <summary>
    /// Endpoint protegido para teste de autenticação
    /// </summary>
    /// <returns>Informações do usuário autenticado</returns>
    [HttpGet("protected")]
    [Authorize]
    public IActionResult GetProtectedInfo()
    {
        var userInfo = new
        {
            Message = "Acesso autorizado ao Gateway",
            User = User.Identity?.Name ?? "Unknown",
            Claims = User.Claims.Select(c => new { c.Type, c.Value }),
            Timestamp = DateTime.UtcNow
        };

        return Ok(userInfo);
    }
}