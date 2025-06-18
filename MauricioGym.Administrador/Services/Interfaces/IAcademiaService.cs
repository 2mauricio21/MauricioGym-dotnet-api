using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Validators;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Interfaces
{
    public interface IAcademiaService : IService<AcademiaValidator>
    {
        Task<IResultadoValidacao<AcademiaEntity?>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<AcademiaEntity?>> ObterPorCnpjAsync(string cnpj);
        Task<IResultadoValidacao<IEnumerable<AcademiaEntity>>> ObterTodosAsync();
        Task<IResultadoValidacao<IEnumerable<AcademiaEntity>>> ObterAtivosAsync();
        Task<IResultadoValidacao<int>> IncluirAcademiaAsync(AcademiaEntity academia, int idUsuario);
        Task<IResultadoValidacao<bool>> AlterarAcademiaAsync(AcademiaEntity academia, int idUsuario);
        Task<IResultadoValidacao> ExcluirAcademiaAsync(int id, int idUsuario);
        Task<IResultadoValidacao<bool>> ValidarLicencaAcademiaAsync(int idAcademia);
        Task<IResultadoValidacao<bool>> ExisteAsync(int id);
        Task<IResultadoValidacao<bool>> ExisteCnpjAsync(string cnpj, int? idExcluir = null);
    }
}
