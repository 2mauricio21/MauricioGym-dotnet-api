using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Seguranca.Entities;
using MauricioGym.Seguranca.Services.Validators;
using System.Threading.Tasks;

namespace MauricioGym.Seguranca.Services.Interfaces
{
    public interface IAutenticacaoService : IService<AutenticacaoValidator>
    {
        Task<IResultadoValidacao<string>> LoginAsync(string email, string senha);
        Task<IResultadoValidacao<bool>> ValidarTokenAsync(string token);
        Task<IResultadoValidacao<string>> RenovarTokenAsync(string token);
        Task<IResultadoValidacao> LogoutAsync(string token);
        Task<IResultadoValidacao> AlterarSenhaAsync(int idUsuario, string senhaAtual, string novaSenha);
    }
}