using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Services.Interfaces;

namespace MauricioGym.Administrador.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioSqlServerRepository _usuarioRepository;

        public UsuarioService(IUsuarioSqlServerRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Task<UsuarioEntity> ObterPorIdAsync(int id) => _usuarioRepository.ObterPorIdAsync(id);

        public Task<IEnumerable<UsuarioEntity>> ListarAsync() => _usuarioRepository.ListarAsync();

        public Task<int> CriarAsync(UsuarioEntity usuario) => _usuarioRepository.CriarAsync(usuario);

        public Task<bool> AtualizarAsync(UsuarioEntity usuario) => _usuarioRepository.AtualizarAsync(usuario);

        public Task<bool> RemoverLogicamenteAsync(int id) => _usuarioRepository.RemoverLogicamenteAsync(id);
    }
}
