using MauricioGym.Infra.Interfaces;
using System.Text.Json;

namespace MauricioGym.Infra.Services
{
    public class AuditoriaService : IAuditoriaService
    {
        public async Task RegistrarAuditoriaAsync<T>(string acao, T entidade, int usuarioId, int? idRegistro = null)
        {
            // TODO: Implementar lógica de auditoria
            // Por enquanto, apenas um placeholder
            var dadosJson = JsonSerializer.Serialize(entidade);
            
            // Aqui você pode implementar a lógica para salvar no banco
            // ou em um sistema de logs
            await Task.CompletedTask;
        }
    }
}
