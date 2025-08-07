using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using MauricioGym.Gateway.Api.Services;
using System.Text;

namespace MauricioGym.Gateway.Api.Controllers;

[ApiController]
[Route("swagger")]
public class SwaggerController : ControllerBase
{
    private readonly SwaggerAggregationService _swaggerService;
    private readonly ILogger<SwaggerController> _logger;

    public SwaggerController(SwaggerAggregationService swaggerService, ILogger<SwaggerController> logger)
    {
        _swaggerService = swaggerService;
        _logger = logger;
    }

    [HttpGet("v1/swagger.json")]
    public async Task<IActionResult> GetAggregatedSwagger()
    {
        try
        {
            var document = await _swaggerService.GetAggregatedSwaggerAsync();
            
            var stringBuilder = new StringBuilder();
            var writer = new OpenApiJsonWriter(new StringWriter(stringBuilder));
            document.SerializeAsV3(writer);
            
            var json = stringBuilder.ToString();
            return Content(json, "application/json");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating aggregated swagger");
            return StatusCode(500, new { error = "Failed to generate swagger documentation" });
        }
    }
}