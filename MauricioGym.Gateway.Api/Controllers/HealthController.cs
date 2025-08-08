using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace MauricioGym.Gateway.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HealthController> _logger;

        public HealthController(HttpClient httpClient, ILogger<HealthController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetHealth()
        {
            var healthStatus = new
            {
                Gateway = "Healthy",
                Timestamp = DateTime.UtcNow,
                Services = await CheckServicesHealth()
            };

            return Ok(healthStatus);
        }

        [HttpGet("services")]
        public async Task<IActionResult> GetServicesHealth()
        {
            var services = await CheckServicesHealth();
            return Ok(services);
        }

        private async Task<Dictionary<string, object>> CheckServicesHealth()
        {
            var services = new Dictionary<string, object>();
            var apiEndpoints = new Dictionary<string, string>
            {
                { "Usuario", "http://localhost:5001/health" },
                { "Academia", "http://localhost:5002/health" },
                { "Acesso", "http://localhost:5003/health" },
                { "Pagamento", "http://localhost:5004/health" },
                { "Plano", "http://localhost:5005/health" }
            };

            foreach (var endpoint in apiEndpoints)
            {
                try
                {
                    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
                    var response = await _httpClient.GetAsync(endpoint.Value, cts.Token);
                    
                    services[endpoint.Key] = new
                    {
                        Status = response.IsSuccessStatusCode ? "Healthy" : "Unhealthy",
                        StatusCode = (int)response.StatusCode,
                        ResponseTime = DateTime.UtcNow,
                        Url = endpoint.Value
                    };
                }
                catch (Exception ex)
                {
                    services[endpoint.Key] = new
                    {
                        Status = "Unhealthy",
                        Error = ex.Message,
                        Url = endpoint.Value,
                        ResponseTime = DateTime.UtcNow
                    };
                }
            }

            return services;
        }
    }
}