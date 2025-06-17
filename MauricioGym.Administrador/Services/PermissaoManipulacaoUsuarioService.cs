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
    public class PermissaoManipulacaoUsuarioService : ServiceBase<PermissaoManipulacaoUsuarioValidator>, IPermissaoManipulacaoUsuarioService
    {
        #region [ Campos ]

        private readonly IPermissaoManipulacaoUsuarioSqlServerRepository _permissaoRepository;
        private readonly ILogger<PermissaoManipulacaoUsuarioService> _logger;

        #endregion

        #region [ Construtor ]

        public PermissaoManipulacaoUsuarioService(
            IPermissaoManipulacaoUsuarioSqlServerRepository permissaoRepository,
            ILogger<PermissaoManipulacaoUsuarioService> logger)
        {
            _permissaoRepository = permissaoRepository;
            _logger = logger;
        }

        #endregion

        #region [ Métodos Públicos ]

        public async Task<IResultadoValidacao<PermissaoManipulacaoUsuarioEntity?>> ObterPorIdAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<PermissaoManipulacaoUsuarioEntity?>(validacao);

                var permissao = await _permissaoRepository.ObterPorIdAsync(id);
                return new ResultadoValidacao<PermissaoManipulacaoUsuarioEntity?>(permissao);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter permissão por ID: {Id}", id);
                return new ResultadoValidacao<PermissaoManipulacaoUsuarioEntity?>(ex, $"Erro ao obter permissão com ID {id}");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PermissaoManipulacaoUsuarioEntity>>> ListarPorUsuarioAsync(int usuarioId)
        {
            try
            {
                var validacao = Validator.ValidarUsuarioId(usuarioId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<PermissaoManipulacaoUsuarioEntity>>(validacao);

                var permissoes = await _permissaoRepository.ListarPorUsuarioAsync(usuarioId);
                return new ResultadoValidacao<IEnumerable<PermissaoManipulacaoUsuarioEntity>>(permissoes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar permissões por usuário: {UsuarioId}", usuarioId);
                return new ResultadoValidacao<IEnumerable<PermissaoManipulacaoUsuarioEntity>>(ex, $"Erro ao listar permissões do usuário {usuarioId}");
            }
        }

        public async Task<IResultadoValidacao<int>> CriarAsync(PermissaoManipulacaoUsuarioEntity permissao)
        {
            try
            {
                var validacao = Validator.CriarPermissao(permissao);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);

                // Definir propriedades padrão
                permissao.DataCriacao = DateTime.Now;
                permissao.Ativo = true;

                var id = await _permissaoRepository.CriarAsync(permissao);
                return new ResultadoValidacao<int>(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar permissão: UsuarioId={UsuarioId}", permissao?.UsuarioId);
                return new ResultadoValidacao<int>(ex, "Erro ao criar permissão");
            }
        }

        public async Task<IResultadoValidacao<bool>> AtualizarAsync(PermissaoManipulacaoUsuarioEntity permissao)
        {
            try
            {
                var validacao = Validator.AtualizarPermissao(permissao);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var sucesso = await _permissaoRepository.AtualizarAsync(permissao);
                return new ResultadoValidacao<bool>(sucesso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar permissão: {Id}", permissao?.Id);
                return new ResultadoValidacao<bool>(ex, "Erro ao atualizar permissão");
            }
        }

        public async Task<IResultadoValidacao<bool>> RemoverAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var sucesso = await _permissaoRepository.RemoverAsync(id);
                return new ResultadoValidacao<bool>(sucesso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover permissão: {Id}", id);
                return new ResultadoValidacao<bool>(ex, "Erro ao remover permissão");
            }
        }

        #endregion
    }
}
