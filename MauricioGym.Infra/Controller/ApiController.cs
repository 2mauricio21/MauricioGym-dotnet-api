using MauricioGym.Infra.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace MauricioGym.Infra.Controller
{
    [Authorize]
    public class ApiController : ControllerBase, IActionFilter
    {
        private readonly ServiceProvider serviceProvider;

        public ApiController()
        {
            serviceProvider = new ServiceCollection()
                .ConfigureServicesInfra()
                .BuildServiceProvider();

        }

        private IEnumerable<Claim> Claims => ((ClaimsIdentity)User.Identity!).Claims;

        public int IdUsuario
        {
            get
            {
                var idUsuario = Claims.FirstOrDefault(x => x.Type.Contains("IdUsuario"))?.Value;
                if (string.IsNullOrWhiteSpace(idUsuario))
                {
                    idUsuario = HttpContext.Request.Headers["IdUsuario"].ToString();
                    if (!string.IsNullOrWhiteSpace(idUsuario))
                        return int.Parse(idUsuario);
                }
                else
                    return int.Parse(idUsuario);

                return 0;
            }
        }

        public int IdAcademia
        {
            get
            {
                var idAcademia = Claims.FirstOrDefault(x => x.Type.Contains("IdAcademia"))?.Value;
                if (string.IsNullOrWhiteSpace(idAcademia))
                {
                    idAcademia = HttpContext.Request.Headers["IdAcademia"].ToString();
                    if (!string.IsNullOrWhiteSpace(idAcademia))
                        return int.Parse(idAcademia);
                }
                else
                    return int.Parse(idAcademia);

                return 0;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public FileContentResult FileContentResult(byte[] fileContents, string contentType = "application/octet-stream")
        {
            HttpContext.Response.ContentType = contentType;
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return new FileContentResult(fileContents, contentType);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Implementação básica - pode ser expandida conforme necessário
            // Para controle de acesso, limites, etc.
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Implementação básica - pode ser expandida conforme necessário
            // Para auditoria, logs, etc.
        }

        protected ActionResult ProcessarResultado<T>(IResultadoValidacao<T> resultado)
        {
            if (resultado.OcorreuErro)
            {
                return BadRequest(new { erro = resultado.MensagemErro });
            }

            return Ok(resultado.Retorno);
        }

        protected ActionResult ProcessarResultado(IResultadoValidacao resultado)
        {
            if (resultado.OcorreuErro)
            {
                return BadRequest(new { erro = resultado.MensagemErro });
            }

            return Ok();
        }
    }
}
