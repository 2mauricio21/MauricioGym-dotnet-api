using Microsoft.AspNetCore.Mvc;

namespace MauricioGym.Usuario.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Status = "Healthy",
                Service = "Usuario API",
                Timestamp = DateTime.UtcNow,
                Version = "1.0.0"
            });
        }
    }
}