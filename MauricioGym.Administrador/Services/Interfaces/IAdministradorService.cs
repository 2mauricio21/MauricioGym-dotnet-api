using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Services.Validators;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Interfaces
{
    public interface IAdministradorService : IService<AdministradorValidator>
    {
        Task<IResultadoValidacao<AdministradorEntity>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<AdministradorEntity>> ObterPorEmailAsync(string email);
        Task<IResultadoValidacao<AdministradorEntity>> ObterPorCpfAsync(string cpf);
        Task<IResultadoValidacao<IEnumerable<AdministradorEntity>>> ObterTodosAsync();
        Task<IResultadoValidacao<IEnumerable<AdministradorEntity>>> ObterAtivosAsync();
        Task<IResultadoValidacao<int>> IncluirAdministradorAsync(AdministradorEntity administrador, int idUsuario);
        Task<IResultadoValidacao<bool>> AlterarAdministradorAsync(AdministradorEntity administrador, int idUsuario);
        Task<IResultadoValidacao> ExcluirAdministradorAsync(int id, int idUsuario);
        Task<IResultadoValidacao<bool>> ExisteAsync(int id);
        Task<IResultadoValidacao<bool>> ExistePorEmailAsync(string email, int? idExcluir);
        Task<IResultadoValidacao<bool>> ExistePorCpfAsync(string cpf, int? idExcluir);
    }
}
