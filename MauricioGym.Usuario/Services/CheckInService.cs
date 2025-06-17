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
    public class CheckInService : ServiceBase<CheckInValidator>, ICheckInService
    {
        #region [ Campos ]

        private readonly ICheckInSqlServerRepository _checkInRepository;
        private readonly IMensalidadeService _mensalidadeService;
        private readonly ILogger<CheckInService> _logger;

        #endregion

        #region [ Construtor ]

        public CheckInService(
            ICheckInSqlServerRepository checkInRepository,
            IMensalidadeService mensalidadeService,
            ILogger<CheckInService> logger)
        {
            _checkInRepository = checkInRepository;
            _mensalidadeService = mensalidadeService;
            _logger = logger;
        }

        #endregion

        #region [ Métodos Públicos ]

        public async Task<IResultadoValidacao<IEnumerable<CheckInEntity>>> ObterTodosAsync()
        {
            try
            {
                var checkIns = await _checkInRepository.ObterTodosAsync();
                return new ResultadoValidacao<IEnumerable<CheckInEntity>>(checkIns);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todos os check-ins");
                return new ResultadoValidacao<IEnumerable<CheckInEntity>>(ex, "Erro ao obter todos os check-ins");
            }
        }

        public async Task<IResultadoValidacao<CheckInEntity?>> ObterPorIdAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<CheckInEntity?>(validacao);

                var checkIn = await _checkInRepository.ObterPorIdAsync(id);
                return new ResultadoValidacao<CheckInEntity?>(checkIn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter check-in por ID: {Id}", id);
                return new ResultadoValidacao<CheckInEntity?>(ex, $"Erro ao obter check-in com ID {id}");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<CheckInEntity>>> ObterPorUsuarioAsync(int usuarioId)
        {
            try
            {
                var validacao = Validator.ValidarUsuarioId(usuarioId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<CheckInEntity>>(validacao);

                var checkIns = await _checkInRepository.ObterPorUsuarioAsync(usuarioId);
                return new ResultadoValidacao<IEnumerable<CheckInEntity>>(checkIns);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter check-ins por usuário: {UsuarioId}", usuarioId);
                return new ResultadoValidacao<IEnumerable<CheckInEntity>>(ex, $"Erro ao obter check-ins do usuário {usuarioId}");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<CheckInEntity>>> ObterPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var validacao = Validator.ValidarPeriodo(dataInicio, dataFim);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<CheckInEntity>>(validacao);

                var checkIns = await _checkInRepository.ObterPorPeriodoAsync(dataInicio, dataFim);
                return new ResultadoValidacao<IEnumerable<CheckInEntity>>(checkIns);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter check-ins por período: {DataInicio} - {DataFim}", dataInicio, dataFim);
                return new ResultadoValidacao<IEnumerable<CheckInEntity>>(ex, "Erro ao obter check-ins por período");
            }
        }

        public async Task<IResultadoValidacao<int>> RealizarCheckInAsync(int usuarioId)
        {
            try
            {
                var validacao = Validator.ValidarUsuarioId(usuarioId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);                var podeRealizarResult = await PodeRealizarCheckInAsync(usuarioId);
                if (podeRealizarResult.OcorreuErro)
                    return new ResultadoValidacao<int>(podeRealizarResult.Erro);

                if (!podeRealizarResult.Retorno)
                    return new ResultadoValidacao<int>("Usuário não pode fazer check-in. Verifique se as mensalidades estão em dia.");

                var checkIn = new CheckInEntity
                {
                    UsuarioId = usuarioId,
                    DataHora = DateTime.Now,
                    Ativo = true,
                    DataCriacao = DateTime.Now
                };

                var id = await _checkInRepository.CriarAsync(checkIn);
                return new ResultadoValidacao<int>(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao realizar check-in para usuário: {UsuarioId}", usuarioId);
                return new ResultadoValidacao<int>(ex, "Erro ao realizar check-in");
            }
        }

        public async Task<IResultadoValidacao<bool>> RemoverAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _checkInRepository.ExisteAsync(id);
                if (!existe)
                    return new ResultadoValidacao<bool>("Check-in não encontrado.");

                var sucesso = await _checkInRepository.RemoverAsync(id);
                return new ResultadoValidacao<bool>(sucesso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover check-in: {Id}", id);
                return new ResultadoValidacao<bool>(ex, "Erro ao remover check-in");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _checkInRepository.ExisteAsync(id);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar existência do check-in: {Id}", id);
                return new ResultadoValidacao<bool>(ex, "Erro ao verificar existência do check-in");
            }
        }

        public async Task<IResultadoValidacao<int>> CriarAsync(CheckInEntity checkIn)
        {
            try
            {
                var validacao = Validator.CriarCheckIn(checkIn);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);                var podeRealizarResult = await PodeRealizarCheckInAsync(checkIn.UsuarioId);
                if (podeRealizarResult.OcorreuErro)
                    return new ResultadoValidacao<int>(podeRealizarResult.Erro);

                if (!podeRealizarResult.Retorno)
                    return new ResultadoValidacao<int>("Usuário não pode fazer check-in. Verifique se as mensalidades estão em dia.");

                // Definir propriedades padrão
                checkIn.DataCriacao = DateTime.Now;
                checkIn.Ativo = true;

                var id = await _checkInRepository.CriarAsync(checkIn);
                return new ResultadoValidacao<int>(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar check-in para usuário: {UsuarioId}", checkIn?.UsuarioId);
                return new ResultadoValidacao<int>(ex, "Erro ao criar check-in");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<CheckInEntity>>> ListarPorUsuarioAsync(int usuarioId)
        {
            return await ObterPorUsuarioAsync(usuarioId);
        }

        public async Task<IResultadoValidacao<int>> ContarCheckInsPorUsuarioMesAsync(int usuarioId, int ano, int mes)
        {
            try
            {
                var validacao = Validator.ValidarUsuarioMesAno(usuarioId, mes, ano);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);

                var contador = await _checkInRepository.ContarCheckInsPorUsuarioMesAsync(usuarioId, ano, mes);
                return new ResultadoValidacao<int>(contador);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao contar check-ins por mês: {UsuarioId}, {Ano}, {Mes}", usuarioId, ano, mes);
                return new ResultadoValidacao<int>(ex, "Erro ao contar check-ins por mês");
            }
        }

        public async Task<IResultadoValidacao<bool>> PodeRealizarCheckInAsync(int usuarioId)
        {
            try
            {
                var validacao = Validator.ValidarUsuarioId(usuarioId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                return await _mensalidadeService.VerificarMensalidadeEmDiaAsync(usuarioId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar se pode realizar check-in: {UsuarioId}", usuarioId);
                return new ResultadoValidacao<bool>(ex, "Erro ao verificar se pode realizar check-in");
            }
        }

        #endregion
    }
}
