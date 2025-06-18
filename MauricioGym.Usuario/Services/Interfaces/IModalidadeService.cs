using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Validators;

namespace MauricioGym.Usuario.Services.Interfaces
{
    public interface IModalidadeService : IService<ModalidadeValidator>
    {
        Task<IResultadoValidacao<ModalidadeEntity?>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<ModalidadeEntity?>> ObterPorNomeAsync(string nome);
        Task<IResultadoValidacao<IEnumerable<ModalidadeEntity>>> ObterTodosAsync();
        Task<IResultadoValidacao<IEnumerable<ModalidadeEntity>>> ObterPorAcademiaIdAsync(int academiaId);
        Task<IResultadoValidacao<int>> IncluirModalidadeAsync(ModalidadeEntity modalidade, int idUsuario);
        Task<IResultadoValidacao<bool>> AlterarModalidadeAsync(ModalidadeEntity modalidade, int idUsuario);
        Task<IResultadoValidacao> ExcluirModalidadeAsync(int id, int idUsuario);
        Task<IResultadoValidacao<bool>> ExisteAsync(int id);
    }
}
