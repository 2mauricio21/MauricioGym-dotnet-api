using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.Interfaces;
using MauricioGym.Usuario.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace MauricioGym.Usuario.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioSqlServerRepository _usuarioRepository;
        private readonly IMensalidadeService _mensalidadeService;
        private readonly ILogger<UsuarioService> _logger;

        public UsuarioService(
            IUsuarioSqlServerRepository usuarioRepository,
            IMensalidadeService mensalidadeService,
            ILogger<UsuarioService> logger)
        {
            _usuarioRepository = usuarioRepository;
            _mensalidadeService = mensalidadeService;
            _logger = logger;
        }

        public async Task<IEnumerable<UsuarioEntity>> ObterTodosAsync()
        {
            try
            {
                return await _usuarioRepository.ObterTodosAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todos os usuários");
                throw;
            }
        }

        public async Task<UsuarioEntity?> ObterPorIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return null;

                return await _usuarioRepository.ObterPorIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter usuário por ID: {Id}", id);
                throw;
            }
        }

        public async Task<UsuarioEntity?> ObterPorEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return null;

                return await _usuarioRepository.ObterPorEmailAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter usuário por email: {Email}", email);
                throw;
            }
        }

        public async Task<int> CriarAsync(UsuarioEntity usuario)
        {
            try
            {
                if (usuario == null)
                    throw new ArgumentNullException(nameof(usuario));

                if (string.IsNullOrWhiteSpace(usuario.Nome))
                    throw new ArgumentException("Nome é obrigatório", nameof(usuario));

                if (string.IsNullOrWhiteSpace(usuario.Email))
                    throw new ArgumentException("Email é obrigatório", nameof(usuario));                if (await _usuarioRepository.ExisteEmailAsync(usuario.Email))
                    throw new InvalidOperationException("Já existe um usuário com este email");

                usuario.DataCriacao = DateTime.Now;
                usuario.Ativo = true;

                return await _usuarioRepository.CriarAsync(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar usuário: {Nome}", usuario?.Nome);
                throw;
            }
        }

        public async Task<bool> AtualizarAsync(UsuarioEntity usuario)
        {
            try
            {
                if (usuario == null)
                    throw new ArgumentNullException(nameof(usuario));

                if (usuario.Id <= 0)
                    throw new ArgumentException("ID inválido", nameof(usuario));

                if (string.IsNullOrWhiteSpace(usuario.Nome))
                    throw new ArgumentException("Nome é obrigatório", nameof(usuario));

                if (string.IsNullOrWhiteSpace(usuario.Email))
                    throw new ArgumentException("Email é obrigatório", nameof(usuario));

                if (!await _usuarioRepository.ExisteAsync(usuario.Id))
                    throw new InvalidOperationException("Usuário não encontrado");

                if (await _usuarioRepository.ExisteEmailAsync(usuario.Email, usuario.Id))
                    throw new InvalidOperationException("Já existe outro usuário com este email");

                return await _usuarioRepository.AtualizarAsync(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar usuário: {Id}", usuario?.Id);
                throw;
            }
        }

        public async Task<bool> RemoverAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("ID inválido", nameof(id));

                if (!await _usuarioRepository.ExisteAsync(id))
                    throw new InvalidOperationException("Usuário não encontrado");

                return await _usuarioRepository.RemoverAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover usuário: {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return false;

                return await _usuarioRepository.ExisteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar existência do usuário: {Id}", id);
                throw;
            }
        }

        public async Task<bool> PodeFazerCheckInAsync(int usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                    return false;

                if (!await ExisteAsync(usuarioId))
                    return false;

                return await _mensalidadeService.EstaEmDiaAsync(usuarioId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar se usuário pode fazer check-in: {UsuarioId}", usuarioId);
                throw;
            }
        }
    }
}
