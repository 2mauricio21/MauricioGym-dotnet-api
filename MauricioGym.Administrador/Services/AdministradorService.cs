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
    public class AdministradorService : ServiceBase<AdministradorValidator>, IAdministradorService
    {
        #region [ Campos ]

        private readonly IAdministradorSqlServerRepository _administradorRepository;
        private readonly ILogger<AdministradorService> _logger;

        #endregion

        #region [ Construtor ]

        public AdministradorService(
            IAdministradorSqlServerRepository administradorRepository,
            ILogger<AdministradorService> logger)
        {
            _administradorRepository = administradorRepository;
            _logger = logger;
        }

        #endregion

        #region [ Métodos Públicos ]

        public async Task<IResultadoValidacao<AdministradorEntity>> ObterPorIdAsync(int id)
        {
            try
            {
                var validacao = Validator.ObterAdministradorPorId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<AdministradorEntity>(validacao);

                var administrador = await _administradorRepository.ObterPorIdAsync(id);
                if (administrador == null)
                    return new ResultadoValidacao<AdministradorEntity>("Administrador não encontrado");

                return new ResultadoValidacao<AdministradorEntity>(administrador);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter administrador por ID: {Id}", id);
                return new ResultadoValidacao<AdministradorEntity>(ex, "Erro ao obter administrador");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<AdministradorEntity>>> ListarAsync()
        {
            try
            {
                var administradores = await _administradorRepository.ListarAsync();
                return new ResultadoValidacao<IEnumerable<AdministradorEntity>>(administradores);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar administradores");
                return new ResultadoValidacao<IEnumerable<AdministradorEntity>>(ex, "Erro ao listar administradores");
            }
        }

        public async Task<IResultadoValidacao<int>> CriarAsync(AdministradorEntity administrador)
        {
            try
            {
                var validacao = Validator.CriarAdministrador(administrador);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);

                var id = await _administradorRepository.CriarAsync(administrador);
                return new ResultadoValidacao<int>(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar administrador: {Email}", administrador?.Email);
                return new ResultadoValidacao<int>(ex, "Erro ao criar administrador");
            }
        }

        public async Task<IResultadoValidacao<bool>> AtualizarAsync(AdministradorEntity administrador)
        {
            try
            {
                var validacao = Validator.AtualizarAdministrador(administrador);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var sucesso = await _administradorRepository.AtualizarAsync(administrador);
                return new ResultadoValidacao<bool>(sucesso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar administrador: {Id}", administrador?.Id);
                return new ResultadoValidacao<bool>(ex, "Erro ao atualizar administrador");
            }
        }

        public async Task<IResultadoValidacao<bool>> RemoverLogicamenteAsync(int id)
        {
            try
            {
                var validacao = Validator.RemoverAdministrador(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var sucesso = await _administradorRepository.RemoverLogicamenteAsync(id);
                return new ResultadoValidacao<bool>(sucesso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover administrador: {Id}", id);
                return new ResultadoValidacao<bool>(ex, "Erro ao remover administrador");
            }
        }

        #endregion
    }
}
