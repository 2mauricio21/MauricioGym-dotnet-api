using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Services.Interfaces;

namespace MauricioGym.Administrador.Services
{
    public class UsuarioPlanoService : IUsuarioPlanoService
    {
        private readonly IUsuarioPlanoSqlServerRepository _usuarioPlanoRepository;

        public UsuarioPlanoService(IUsuarioPlanoSqlServerRepository usuarioPlanoRepository)
        {
            _usuarioPlanoRepository = usuarioPlanoRepository;
        }

        public Task<UsuarioPlanoEntity> ObterPorIdAsync(int id) => _usuarioPlanoRepository.ObterPorIdAsync(id);

        public Task<IEnumerable<UsuarioPlanoEntity>> ListarPorUsuarioAsync(int usuarioId) => _usuarioPlanoRepository.ListarPorUsuarioAsync(usuarioId);

        public Task<int> CriarAsync(UsuarioPlanoEntity usuarioPlano) => _usuarioPlanoRepository.CriarAsync(usuarioPlano);

        public Task<bool> AtualizarAsync(UsuarioPlanoEntity usuarioPlano) => _usuarioPlanoRepository.AtualizarAsync(usuarioPlano);

        public Task<bool> RemoverLogicamenteAsync(int id) => _usuarioPlanoRepository.RemoverLogicamenteAsync(id);
    }
}
