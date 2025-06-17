using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.Interfaces;
using MauricioGym.Usuario.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace MauricioGym.Usuario.Services
{
    public class PlanoService : IPlanoService
    {
        private readonly IPlanoSqlServerRepository _planoRepository;
        private readonly ILogger<PlanoService> _logger;

        public PlanoService(
            IPlanoSqlServerRepository planoRepository,
            ILogger<PlanoService> logger)
        {
            _planoRepository = planoRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<PlanoEntity>> ObterTodosAsync()
        {
            try
            {
                return await _planoRepository.ObterTodosAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todos os planos");
                throw;
            }
        }

        public async Task<PlanoEntity?> ObterPorIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return null;

                return await _planoRepository.ObterPorIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter plano por ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return false;

                return await _planoRepository.ExisteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar existência do plano: {Id}", id);
                throw;
            }
        }
    }
}
