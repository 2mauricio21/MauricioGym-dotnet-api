using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Validators;

namespace MauricioGym.Usuario.Services.Interfaces
{
    public interface IPlanoService : IService<PlanoValidator>
    {
        Task<IResultadoValidacao<PlanoEntity?>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<IEnumerable<PlanoEntity>>> ObterTodosAsync();
        Task<IResultadoValidacao<IEnumerable<PlanoEntity>>> ObterPorAcademiaIdAsync(int academiaId);
        Task<IResultadoValidacao<int>> IncluirPlanoAsync(PlanoEntity plano, int idUsuario);
        Task<IResultadoValidacao<bool>> AlterarPlanoAsync(PlanoEntity plano, int idUsuario);
        Task<IResultadoValidacao> ExcluirPlanoAsync(int id, int idUsuario);
        Task<IResultadoValidacao<bool>> ExisteAsync(int id);
        Task<IResultadoValidacao<bool>> ExisteNomeAsync(string nome, int? idExcluir = null);
    }
}
