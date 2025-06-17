using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Services.Interfaces;

namespace MauricioGym.Administrador.Services
{
    public class PlanoService : IPlanoService
    {
        private readonly IPlanoSqlServerRepository _planoRepository;

        public PlanoService(IPlanoSqlServerRepository planoRepository)
        {
            _planoRepository = planoRepository;
        }

        public Task<PlanoEntity> ObterPorIdAsync(int id) => _planoRepository.ObterPorIdAsync(id);

        public Task<IEnumerable<PlanoEntity>> ListarAsync() => _planoRepository.ListarAsync();

        public Task<int> CriarAsync(PlanoEntity plano) => _planoRepository.CriarAsync(plano);

        public Task<bool> AtualizarAsync(PlanoEntity plano) => _planoRepository.AtualizarAsync(plano);

        public Task<bool> RemoverLogicamenteAsync(int id) => _planoRepository.RemoverLogicamenteAsync(id);
    }
}
