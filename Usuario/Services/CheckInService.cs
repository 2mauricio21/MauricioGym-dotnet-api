using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.Interfaces;
using MauricioGym.Usuario.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace MauricioGym.Usuario.Services
{
    public class CheckInService : ICheckInService
    {
        private readonly ICheckInSqlServerRepository _checkInRepository;
        private readonly IMensalidadeService _mensalidadeService;
        private readonly ILogger<CheckInService> _logger;

        public CheckInService(
            ICheckInSqlServerRepository checkInRepository,
            IMensalidadeService mensalidadeService,
            ILogger<CheckInService> logger)
        {
            _checkInRepository = checkInRepository;
            _mensalidadeService = mensalidadeService;
            _logger = logger;
        }

        public async Task<IEnumerable<CheckInEntity>> ObterTodosAsync()
        {
            try
            {
                return await _checkInRepository.ObterTodosAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todos os check-ins");
                throw;
            }
        }

        public async Task<CheckInEntity?> ObterPorIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return null;

                return await _checkInRepository.ObterPorIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter check-in por ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<CheckInEntity>> ObterPorUsuarioAsync(int usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                    return Enumerable.Empty<CheckInEntity>();

                return await _checkInRepository.ObterPorUsuarioAsync(usuarioId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter check-ins por usuário: {UsuarioId}", usuarioId);
                throw;
            }
        }

        public async Task<IEnumerable<CheckInEntity>> ObterPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                if (dataInicio > dataFim)
                    throw new ArgumentException("Data início deve ser menor que data fim");

                return await _checkInRepository.ObterPorPeriodoAsync(dataInicio, dataFim);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter check-ins por período: {DataInicio} - {DataFim}", dataInicio, dataFim);
                throw;
            }
        }        public async Task<int> RealizarCheckInAsync(int usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                    throw new ArgumentException("ID do usuário inválido", nameof(usuarioId));

                if (!await PodeRealizarCheckInAsync(usuarioId))
                    throw new InvalidOperationException("Usuário não pode fazer check-in. Verifique se as mensalidades estão em dia");

                var checkIn = new CheckInEntity
                {
                    UsuarioId = usuarioId,
                    DataHora = DateTime.Now,
                    Removido = false
                };

                return await _checkInRepository.CriarAsync(checkIn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao realizar check-in para usuário: {UsuarioId}", usuarioId);
                throw;
            }
        }

        public async Task<bool> RemoverAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("ID inválido", nameof(id));

                if (!await _checkInRepository.ExisteAsync(id))
                    throw new InvalidOperationException("Check-in não encontrado");

                return await _checkInRepository.RemoverAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover check-in: {Id}", id);
                throw;
            }
        }        public async Task<bool> ExisteAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return false;

                return await _checkInRepository.ExisteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar existência do check-in: {Id}", id);
                throw;
            }
        }        public async Task<int> CriarAsync(CheckInEntity checkIn)
        {
            try
            {
                if (checkIn == null)
                    throw new ArgumentNullException(nameof(checkIn));

                if (checkIn.UsuarioId <= 0)
                    throw new ArgumentException("ID do usuário inválido", nameof(checkIn.UsuarioId));

                if (!await PodeRealizarCheckInAsync(checkIn.UsuarioId))
                    throw new InvalidOperationException("Usuário não pode fazer check-in. Verifique se as mensalidades estão em dia");

                return await _checkInRepository.CriarAsync(checkIn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar check-in para usuário: {UsuarioId}", checkIn?.UsuarioId);
                throw;
            }
        }

        public async Task<IEnumerable<CheckInEntity>> ListarPorUsuarioAsync(int usuarioId)
        {
            return await ObterPorUsuarioAsync(usuarioId);
        }

        public async Task<int> ContarCheckInsPorUsuarioMesAsync(int usuarioId, int ano, int mes)
        {
            try
            {
                if (usuarioId <= 0)
                    throw new ArgumentException("ID do usuário inválido", nameof(usuarioId));

                if (ano < 2000 || ano > DateTime.Now.Year + 1)
                    throw new ArgumentException("Ano inválido", nameof(ano));

                if (mes < 1 || mes > 12)
                    throw new ArgumentException("Mês inválido", nameof(mes));

                return await _checkInRepository.ContarCheckInsPorUsuarioMesAsync(usuarioId, ano, mes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao contar check-ins por mês: {UsuarioId}, {Ano}, {Mes}", usuarioId, ano, mes);
                throw;
            }
        }        public async Task<bool> PodeRealizarCheckInAsync(int usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                    return false;

                return await _mensalidadeService.VerificarMensalidadeEmDiaAsync(usuarioId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar se pode realizar check-in: {UsuarioId}", usuarioId);
                throw;
            }
        }
    }
}
