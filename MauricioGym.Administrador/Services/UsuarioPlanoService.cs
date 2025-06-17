using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Services.Interfaces;
using MauricioGym.Administrador.Services.Validators;
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace MauricioGym.Administrador.Services
{
    public class UsuarioPlanoService : ServiceBase<UsuarioPlanoValidator>, IUsuarioPlanoService
    {
        #region [ Campos ]

        private readonly IUsuarioPlanoSqlServerRepository _usuarioPlanoRepository;
        private readonly ILogger<UsuarioPlanoService> _logger;

        #endregion

        #region [ Construtor ]

        public UsuarioPlanoService(
            IUsuarioPlanoSqlServerRepository usuarioPlanoRepository,
            ILogger<UsuarioPlanoService> logger)
        {
            _usuarioPlanoRepository = usuarioPlanoRepository;
            _logger = logger;
        }

        #endregion

        #region [ Métodos Públicos ]

        public async Task<IResultadoValidacao<UsuarioPlanoEntity?>> ObterPorIdAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<UsuarioPlanoEntity?>(validacao);

                var usuarioPlano = await _usuarioPlanoRepository.ObterPorIdAsync(id);
                return new ResultadoValidacao<UsuarioPlanoEntity?>(usuarioPlano);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter usuário-plano por ID: {Id}", id);
                return new ResultadoValidacao<UsuarioPlanoEntity?>(ex, $"Erro ao obter usuário-plano com ID {id}");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>> ListarPorUsuarioAsync(int usuarioId)
        {
            try
            {
                var validacao = Validator.ValidarUsuarioId(usuarioId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>(validacao);

                var usuarioPlanos = await _usuarioPlanoRepository.ListarPorUsuarioAsync(usuarioId);
                return new ResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>(usuarioPlanos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar usuário-planos por usuário: {UsuarioId}", usuarioId);
                return new ResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>(ex, $"Erro ao listar usuário-planos do usuário {usuarioId}");
            }
        }

        public async Task<IResultadoValidacao<int>> CriarAsync(UsuarioPlanoEntity usuarioPlano)
        {
            try
            {
                var validacao = Validator.CriarUsuarioPlano(usuarioPlano);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);

                // Definir propriedades padrão
                usuarioPlano.DataCriacao = DateTime.Now;
                usuarioPlano.Ativo = true;

                var id = await _usuarioPlanoRepository.CriarAsync(usuarioPlano);
                return new ResultadoValidacao<int>(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar usuário-plano: UsuarioId={UsuarioId}, PlanoId={PlanoId}", 
                    usuarioPlano?.UsuarioId, usuarioPlano?.PlanoId);
                return new ResultadoValidacao<int>(ex, "Erro ao criar usuário-plano");
            }
        }

        public async Task<IResultadoValidacao<bool>> AtualizarAsync(UsuarioPlanoEntity usuarioPlano)
        {
            try
            {
                var validacao = Validator.AtualizarUsuarioPlano(usuarioPlano);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var sucesso = await _usuarioPlanoRepository.AtualizarAsync(usuarioPlano);
                return new ResultadoValidacao<bool>(sucesso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar usuário-plano: {Id}", usuarioPlano?.Id);
                return new ResultadoValidacao<bool>(ex, "Erro ao atualizar usuário-plano");
            }
        }

        public async Task<IResultadoValidacao<bool>> RemoverLogicamenteAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var sucesso = await _usuarioPlanoRepository.RemoverLogicamenteAsync(id);
                return new ResultadoValidacao<bool>(sucesso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover usuário-plano logicamente: {Id}", id);
                return new ResultadoValidacao<bool>(ex, "Erro ao remover usuário-plano");
            }
        }

        #endregion
    }
}
