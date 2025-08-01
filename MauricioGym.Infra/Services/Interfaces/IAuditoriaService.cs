using System.Threading.Tasks;

namespace MauricioGym.Infra.Services.Interfaces
{
    public interface IAuditoriaService
    {
        Task RegistrarAuditoriaAsync(int idUsuario, string descricao);
    }
}