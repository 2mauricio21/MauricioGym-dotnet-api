using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Validators;

namespace MauricioGym.Usuario.Services.Interfaces
{
    public interface IClienteService : IService<ClienteValidator>
    {
        Task<IResultadoValidacao<ClienteEntity?>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<ClienteEntity?>> ObterPorCpfAsync(string cpf);
        Task<IResultadoValidacao<ClienteEntity?>> ObterPorEmailAsync(string email);
        Task<IResultadoValidacao<IEnumerable<ClienteEntity>>> ObterTodosAsync();
        Task<IResultadoValidacao<int>> IncluirClienteAsync(ClienteEntity cliente, int idUsuario);
        Task<IResultadoValidacao<bool>> AlterarClienteAsync(ClienteEntity cliente, int idUsuario);
        Task<IResultadoValidacao> ExcluirClienteAsync(int id, int idUsuario);
        Task<IResultadoValidacao<bool>> ExisteAsync(int id);
        Task<IResultadoValidacao<bool>> ExisteCpfAsync(string cpf, int? idExcluir = null);
        Task<IResultadoValidacao<bool>> ExisteEmailAsync(string email, int? idExcluir = null);
    }
}
