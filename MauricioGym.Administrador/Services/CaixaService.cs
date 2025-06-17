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
    public class CaixaService : ServiceBase<CaixaValidator>, ICaixaService
    {
        #region [ Campos ]

        private readonly ICaixaSqlServerRepository _caixaRepository;
        private readonly ILogger<CaixaService> _logger;

        #endregion

        #region [ Construtor ]

        public CaixaService(
            ICaixaSqlServerRepository caixaRepository,
            ILogger<CaixaService> logger)
        {
            _caixaRepository = caixaRepository;
            _logger = logger;
        }

        #endregion

        #region [ Métodos Públicos ]

        public async Task<IResultadoValidacao<CaixaEntity>> ObterPorIdAsync(int id)
        {
            try
            {
                var validacao = Validator.ObterCaixaPorId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<CaixaEntity>(validacao);

                var caixa = await _caixaRepository.ObterPorIdAsync(id);
                if (caixa == null)
                    return new ResultadoValidacao<CaixaEntity>("Caixa não encontrada");

                return new ResultadoValidacao<CaixaEntity>(caixa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter caixa por ID: {Id}", id);
                return new ResultadoValidacao<CaixaEntity>(ex, "Erro ao obter caixa");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<CaixaEntity>>> ListarAsync()
        {
            try
            {
                var caixas = await _caixaRepository.ListarAsync();
                return new ResultadoValidacao<IEnumerable<CaixaEntity>>(caixas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar caixas");
                return new ResultadoValidacao<IEnumerable<CaixaEntity>>(ex, "Erro ao listar caixas");
            }
        }

        public async Task<IResultadoValidacao<int>> CriarAsync(CaixaEntity caixa)
        {
            try
            {
                var validacao = Validator.CriarCaixa(caixa);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);

                var id = await _caixaRepository.CriarAsync(caixa);
                return new ResultadoValidacao<int>(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar caixa");
                return new ResultadoValidacao<int>(ex, "Erro ao criar caixa");
            }
        }

        public async Task<IResultadoValidacao<bool>> AtualizarAsync(CaixaEntity caixa)
        {
            try
            {
                var validacao = Validator.AtualizarCaixa(caixa);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var sucesso = await _caixaRepository.AtualizarAsync(caixa);
                return new ResultadoValidacao<bool>(sucesso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar caixa: {Id}", caixa?.Id);
                return new ResultadoValidacao<bool>(ex, "Erro ao atualizar caixa");
            }
        }

        public async Task<IResultadoValidacao<bool>> RemoverAsync(int id)
        {
            try
            {
                var validacao = Validator.RemoverCaixa(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var sucesso = await _caixaRepository.RemoverAsync(id);
                return new ResultadoValidacao<bool>(sucesso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover caixa: {Id}", id);
                return new ResultadoValidacao<bool>(ex, "Erro ao remover caixa");
            }
        }

        #endregion
    }
}
