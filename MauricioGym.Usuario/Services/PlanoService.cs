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

        public async Task<IResultadoValidacao<IEnumerable<PlanoEntity>>> ObterTodosAsync()
        {
            try
            {
                var planos = await _planoRepository.ObterTodosAsync();
                return new ResultadoValidacao<IEnumerable<PlanoEntity>>(planos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todos os planos");
                return new ResultadoValidacao<IEnumerable<PlanoEntity>>(ex, "Erro ao obter todos os planos");
            }
        }

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

        public async Task<IResultadoValidacao<bool>> ExisteAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _planoRepository.ExisteAsync(id);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar existência do plano: {Id}", id);
                return new ResultadoValidacao<bool>(ex, "Erro ao verificar existência do plano");
            }
        }

        #endregion
    }
}
