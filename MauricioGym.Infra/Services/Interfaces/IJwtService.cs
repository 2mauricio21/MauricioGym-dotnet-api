using MauricioGym.Infra.Entities;
using System.Security.Claims;

namespace MauricioGym.Infra.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Usuario usuario);
        ClaimsPrincipal? ValidateToken(string token);
        DateTime GetTokenExpiration();
    }
}