using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Validators;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Interfaces
{
    public interface IPapelService : IService<PapelValidator>
    {
        Task<IResultadoValidacao<PapelEntity>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<PapelEntity>> ObterPorNomeAsync(string nome);
        Task<IResultadoValidacao<IEnumerable<PapelEntity>>> ObterTodosAsync();
        Task<IResultadoValidacao<IEnumerable<PapelEntity>>> ObterPorAdministradorIdAsync(int administradorId);
        Task<IResultadoValidacao<PapelEntity>> IncluirPapelAsync(PapelEntity papel, int idUsuario);
        Task<IResultadoValidacao<PapelEntity>> AlterarPapelAsync(PapelEntity papel, int idUsuario);
        Task<IResultadoValidacao> ExcluirPapelAsync(int id, int idUsuario);
        Task<IResultadoValidacao<bool>> ExisteAsync(int id);
        Task<IResultadoValidacao<bool>> ExisteNomeAsync(string nome, int? idExcluir = null);
    }
}
