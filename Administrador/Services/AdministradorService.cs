using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Services.Interfaces;

namespace MauricioGym.Administrador.Services
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
