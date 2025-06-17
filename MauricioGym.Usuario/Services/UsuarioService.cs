using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.Interfaces;
using MauricioGym.Usuario.Services.Interfaces;
using MauricioGym.Usuario.Services.Validators;
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace MauricioGym.Usuario.Services
{
    public class UsuarioService : ServiceBase<UsuarioValidator>, IUsuarioService
    {
        #region [ Campos ]

        private readonly IUsuarioSqlServerRepository _usuarioRepository;
        private readonly IMensalidadeService _mensalidadeService;
        private readonly ILogger<UsuarioService> _logger;

        #endregion

        #region [ Construtor ]

        public UsuarioService(
            IUsuarioSqlServerRepository usuarioRepository,
            IMensalidadeService mensalidadeService,
            ILogger<UsuarioService> logger)
        {
            _usuarioRepository = usuarioRepository;
            _mensalidadeService = mensalidadeService;
            _logger = logger;
        }

        #endregion

        #region [ Métodos Públicos ]

        public async Task<IResultadoValidacao<IEnumerable<UsuarioEntity>>> ObterTodosAsync()
        {
            try
            {
                var usuarios = await _usuarioRepository.ObterTodosAsync();
                return new ResultadoValidacao<IEnumerable<UsuarioEntity>>(usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todos os usuários");
                return new ResultadoValidacao<IEnumerable<UsuarioEntity>>(ex, "Erro ao obter todos os usuários");
            }
        }

        public async Task<IResultadoValidacao<UsuarioEntity?>> ObterPorIdAsync(int id)
        {
            try
            {
                var validacao = Validator.ObterUsuarioPorId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<UsuarioEntity?>(validacao);

                var usuario = await _usuarioRepository.ObterPorIdAsync(id);
                return new ResultadoValidacao<UsuarioEntity?>(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter usuário por ID: {Id}", id);
                return new ResultadoValidacao<UsuarioEntity?>(ex, $"Erro ao obter usuário com ID {id}");
            }
        }

        public async Task<IResultadoValidacao<UsuarioEntity?>> ObterPorEmailAsync(string email)
        {
            try
            {
                var validacao = Validator.ObterUsuarioPorEmail(email);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<UsuarioEntity?>(validacao);

                var usuario = await _usuarioRepository.ObterPorEmailAsync(email);
                return new ResultadoValidacao<UsuarioEntity?>(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter usuário por email: {Email}", email);
                return new ResultadoValidacao<UsuarioEntity?>(ex, $"Erro ao obter usuário com email {email}");
            }
        }

        public async Task<IResultadoValidacao<int>> CriarAsync(UsuarioEntity usuario)
        {
            try
            {
                var validacao = Validator.CriarUsuario(usuario);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);

                // Verificar se o email já existe
                var usuarioExistente = await _usuarioRepository.ExisteEmailAsync(usuario.Email);
                if (usuarioExistente)
                    return new ResultadoValidacao<int>("Já existe um usuário com este email.");

                // Definir propriedades padrão
                usuario.DataCriacao = DateTime.Now;
                usuario.Ativo = true;

                var id = await _usuarioRepository.CriarAsync(usuario);
                return new ResultadoValidacao<int>(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar usuário: {Email}", usuario?.Email);
                return new ResultadoValidacao<int>(ex, "Erro ao criar usuário");
            }
        }

        public async Task<IResultadoValidacao<bool>> AtualizarAsync(UsuarioEntity usuario)
        {
            try
            {
                var validacao = Validator.AtualizarUsuario(usuario);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                // Verificar se o usuário existe
                var usuarioExiste = await _usuarioRepository.ExisteAsync(usuario.Id);
                if (!usuarioExiste)
                    return new ResultadoValidacao<bool>("Usuário não encontrado.");

                // Verificar se o email já existe para outro usuário
                var emailExiste = await _usuarioRepository.ExisteEmailAsync(usuario.Email, usuario.Id);
                if (emailExiste)
                    return new ResultadoValidacao<bool>("Já existe outro usuário com este email.");

                var sucesso = await _usuarioRepository.AtualizarAsync(usuario);
                return new ResultadoValidacao<bool>(sucesso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar usuário: {Id}", usuario?.Id);
                return new ResultadoValidacao<bool>(ex, "Erro ao atualizar usuário");
            }
        }

        public async Task<IResultadoValidacao<bool>> RemoverAsync(int id)
        {
            try
            {
                var validacao = Validator.RemoverUsuario(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                // Verificar se o usuário existe
                var usuarioExiste = await _usuarioRepository.ExisteAsync(id);
                if (!usuarioExiste)
                    return new ResultadoValidacao<bool>("Usuário não encontrado.");

                var sucesso = await _usuarioRepository.RemoverAsync(id);
                return new ResultadoValidacao<bool>(sucesso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover usuário: {Id}", id);
                return new ResultadoValidacao<bool>(ex, "Erro ao remover usuário");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteAsync(int id)
        {
            try
            {
                var validacao = Validator.ObterUsuarioPorId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _usuarioRepository.ExisteAsync(id);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar existência do usuário: {Id}", id);
                return new ResultadoValidacao<bool>(ex, "Erro ao verificar existência do usuário");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteEmailAsync(string email, int? excludeId = null)
        {
            try
            {
                var validacao = Validator.ObterUsuarioPorEmail(email);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _usuarioRepository.ExisteEmailAsync(email, excludeId);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar existência do email: {Email}", email);
                return new ResultadoValidacao<bool>(ex, "Erro ao verificar existência do email");
            }
        }

        public async Task<IResultadoValidacao<bool>> PodeFazerCheckInAsync(int usuarioId)
        {
            try
            {
                var validacao = Validator.ObterUsuarioPorId(usuarioId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                // Verificar se o usuário existe
                var usuarioExiste = await _usuarioRepository.ExisteAsync(usuarioId);
                if (!usuarioExiste)
                    return new ResultadoValidacao<bool>("Usuário não encontrado.");                // Verificar se está em dia com a mensalidade
                var resultadoMensalidade = await _mensalidadeService.EstaEmDiaAsync(usuarioId);
                if (resultadoMensalidade.OcorreuErro)
                    return new ResultadoValidacao<bool>(resultadoMensalidade.Erro);

                return new ResultadoValidacao<bool>(resultadoMensalidade.Retorno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar se usuário pode fazer check-in: {UsuarioId}", usuarioId);
                return new ResultadoValidacao<bool>(ex, "Erro ao verificar se usuário pode fazer check-in");
            }
        }

        #endregion
    }
}
