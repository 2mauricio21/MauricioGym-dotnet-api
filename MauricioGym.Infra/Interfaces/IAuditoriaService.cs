namespace MauricioGym.Infra.Interfaces
{
    public interface IAuditoriaService
    {
        Task RegistrarAuditoriaAsync<T>(string acao, T entidade, int usuarioId, int? idRegistro = null);
    }
}
