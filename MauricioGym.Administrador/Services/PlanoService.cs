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
    public class PlanoService : ServiceBase<PlanoValidator>, IPlanoService
    {
        #region [ Campos ]

        private readonly IPlanoSqlServerRepository _planoRepository;
        private readonly ILogger<PlanoService> _logger;

        #endregion

        #region [ Construtor ]

        public PlanoService(
            IPlanoSqlServerRepository planoRepository,
            ILogger<PlanoService> logger)
        {
            _planoRepository = planoRepository;
            _logger = logger;
        }

        #endregion

        #region [ Métodos Públicos ]

        public async Task<IResultadoValidacao<PlanoEntity?>> ObterPorIdAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<PlanoEntity?>(validacao);

                var plano = await _planoRepository.ObterPorIdAsync(id);
                return new ResultadoValidacao<PlanoEntity?>(plano);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter plano por ID: {Id}", id);
                return new ResultadoValidacao<PlanoEntity?>(ex, $"Erro ao obter plano com ID {id}");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PlanoEntity>>> ListarAsync()
        {
            try
            {
                var planos = await _planoRepository.ListarAsync();
                return new ResultadoValidacao<IEnumerable<PlanoEntity>>(planos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar planos");
                return new ResultadoValidacao<IEnumerable<PlanoEntity>>(ex, "Erro ao listar planos");
            }
        }

        public async Task<IResultadoValidacao<int>> CriarAsync(PlanoEntity plano)
        {
            try
            {
                var validacao = Validator.CriarPlano(plano);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);

                // Definir propriedades padrão
                plano.DataCriacao = DateTime.Now;
                plano.Ativo = true;

                var id = await _planoRepository.CriarAsync(plano);
                return new ResultadoValidacao<int>(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar plano: {Nome}", plano?.Nome);
                return new ResultadoValidacao<int>(ex, "Erro ao criar plano");
            }
        }

        public async Task<IResultadoValidacao<bool>> AtualizarAsync(PlanoEntity plano)
        {
            try
            {
                var validacao = Validator.AtualizarPlano(plano);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var sucesso = await _planoRepository.AtualizarAsync(plano);
                return new ResultadoValidacao<bool>(sucesso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar plano: {Id}", plano?.Id);
                return new ResultadoValidacao<bool>(ex, "Erro ao atualizar plano");
            }
        }

        public async Task<IResultadoValidacao<bool>> RemoverLogicamenteAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var sucesso = await _planoRepository.RemoverLogicamenteAsync(id);
                return new ResultadoValidacao<bool>(sucesso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover plano logicamente: {Id}", id);
                return new ResultadoValidacao<bool>(ex, "Erro ao remover plano");
            }
        }

        #endregion
    }
}
