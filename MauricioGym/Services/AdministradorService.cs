using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Entities;
using MauricioGym.Repositories.SqlServer.Interfaces;
using MauricioGym.Services.Interfaces;

namespace MauricioGym.Services
{
    public class AdministradorService : IAdministradorService
    {
        private readonly IAdministradorSqlServerRepository _administradorRepository;

        public AdministradorService(IAdministradorSqlServerRepository administradorRepository)
        {
            _administradorRepository = administradorRepository;
        }

        public Task<AdministradorEntity> ObterPorIdAsync(int id) => _administradorRepository.ObterPorIdAsync(id);

        public Task<IEnumerable<AdministradorEntity>> ListarAsync() => _administradorRepository.ListarAsync();

        public Task<int> CriarAsync(AdministradorEntity administrador) => _administradorRepository.CriarAsync(administrador);

        public Task<bool> AtualizarAsync(AdministradorEntity administrador) => _administradorRepository.AtualizarAsync(administrador);

        public Task<bool> RemoverLogicamenteAsync(int id) => _administradorRepository.RemoverLogicamenteAsync(id);
    }
}
