using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Services.Validators;

namespace MauricioGym.Usuario.Services.Interfaces
{
    public interface IPlanoClienteService : IService<PlanoClienteValidator>
    {
        Task<IResultadoValidacao<PlanoClienteEntity?>> ObterPorIdAsync(int id);
        Task<IResultadoValidacao<IEnumerable<PlanoClienteEntity>>> ObterPorClienteIdAsync(int clienteId);
        Task<IResultadoValidacao<IEnumerable<PlanoClienteEntity>>> ObterPorPlanoIdAsync(int planoId);
        Task<IResultadoValidacao<IEnumerable<PlanoClienteEntity>>> ObterAtivosPorClienteIdAsync(int clienteId);
        Task<IResultadoValidacao<IEnumerable<PlanoClienteEntity>>> ObterVencidosPorClienteIdAsync(int clienteId);
        Task<IResultadoValidacao<IEnumerable<PlanoClienteEntity>>> ObterVencendoAsync(int diasAntecedencia = 7);
        Task<IResultadoValidacao<int>> IncluirPlanoClienteAsync(PlanoClienteEntity planoCliente, int idUsuario);
        Task<IResultadoValidacao<bool>> AlterarPlanoClienteAsync(PlanoClienteEntity planoCliente, int idUsuario);
        Task<IResultadoValidacao> ExcluirPlanoClienteAsync(int id, int idUsuario);
        Task<IResultadoValidacao<bool>> ExisteAsync(int id);
        Task<IResultadoValidacao<bool>> ClientePossuiPlanoAtivoAsync(int clienteId, int planoId);
        Task<IResultadoValidacao<IEnumerable<PlanoClienteEntity>>> ObterPlanosDoClienteAsync(string cpfCliente, int idAcademia);
        Task<IResultadoValidacao<PlanoClienteEntity?>> ObterPlanoAtivoAsync(string cpfCliente, int idAcademia);
        Task<IResultadoValidacao<int>> ContratarPlanoAsync(string cpfCliente, int idPlano, int idAcademia, int meses, decimal valorPago, int idUsuario);
        Task<IResultadoValidacao<bool>> RenovarPlanoAsync(string cpfCliente, int idAcademia, int mesesAdicionais, decimal valorPago, int idUsuario);
        Task<IResultadoValidacao<bool>> CancelarPlanoAsync(int idPlanoCliente, string motivo, int idUsuario);
        Task<IResultadoValidacao<bool>> ClientePodeEfetuarCheckInAsync(string cpfCliente, int idAcademia);
        Task<IResultadoValidacao<IEnumerable<PlanoClienteEntity>>> ObterPlanosVencendoAsync(int idAcademia, int dias = 7);
    }
}
