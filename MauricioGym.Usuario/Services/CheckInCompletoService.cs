using MauricioGym.Infra.Services;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.SqlServer.Interfaces;
using MauricioGym.Usuario.Services.Interfaces;
using MauricioGym.Usuario.Services.Validators;

namespace MauricioGym.Usuario.Services
{
    public class CheckInCompletoService : ServiceBase<CheckInValidator>, ICheckInCompletoService
    {
        private readonly ICheckInSqlServerRepository _checkInRepository;
        private readonly IClienteSqlServerRepository _clienteRepository;
        private readonly IPlanoClienteSqlServerRepository _planoClienteRepository;

        public CheckInCompletoService(
            ICheckInSqlServerRepository checkInRepository,
            IClienteSqlServerRepository clienteRepository,
            IPlanoClienteSqlServerRepository planoClienteRepository)
        {
            _checkInRepository = checkInRepository;
            _clienteRepository = clienteRepository;
            _planoClienteRepository = planoClienteRepository;
        }

        public async Task<IEnumerable<CheckInEntity>> ObterTodosAsync()
        {
            return await _checkInRepository.ObterTodosAsync();
        }

        public async Task<CheckInEntity?> ObterPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id deve ser maior que zero", nameof(id));

            return await _checkInRepository.ObterPorIdAsync(id);
        }

        public async Task<IEnumerable<CheckInEntity>> ObterPorClienteIdAsync(int clienteId)
        {
            if (clienteId <= 0)
                throw new ArgumentException("ClienteId deve ser maior que zero", nameof(clienteId));

            return await _checkInRepository.ObterPorClienteIdAsync(clienteId);
        }

        public async Task<IEnumerable<CheckInEntity>> ObterPorAcademiaIdAsync(int academiaId)
        {
            if (academiaId <= 0)
                throw new ArgumentException("AcademiaId deve ser maior que zero", nameof(academiaId));

            return await _checkInRepository.ObterPorAcademiaIdAsync(academiaId);
        }

        public async Task<IEnumerable<CheckInEntity>> ObterPorPeriodoAsync(DateTime dataInicio, DateTime dataFim, int? academiaId = null)
        {
            if (dataInicio > dataFim)
                throw new ArgumentException("Data de início deve ser anterior à data de fim");

            return await _checkInRepository.ObterPorPeriodoAsync(dataInicio, dataFim, academiaId);
        }

        public async Task<int> CriarAsync(CheckInEntity checkIn)
        {
            await ValidarCheckInAsync(checkIn.ClienteId, checkIn.AcademiaId, checkIn.PlanoClienteId);

            // Verificar se já fez check-in hoje
            if (await ClienteJaFezCheckInHojeAsync(checkIn.ClienteId, checkIn.AcademiaId))
                throw new InvalidOperationException("Cliente já fez check-in hoje");

            checkIn.DataCriacao = DateTime.Now;
            checkIn.Ativo = true;

            return await _checkInRepository.CriarAsync(checkIn);
        }

        public async Task<bool> AtualizarAsync(CheckInEntity checkIn)
        {
            ValidarCheckIn(checkIn);

            if (checkIn.Id <= 0)
                throw new ArgumentException("Id deve ser maior que zero", nameof(checkIn.Id));

            var checkInExistente = await _checkInRepository.ObterPorIdAsync(checkIn.Id);
            if (checkInExistente == null)
                throw new InvalidOperationException("Check-in não encontrado");

            checkIn.DataAlteracao = DateTime.Now;
            return await _checkInRepository.AtualizarAsync(checkIn);
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id deve ser maior que zero", nameof(id));

            var checkInExistente = await _checkInRepository.ObterPorIdAsync(id);
            if (checkInExistente == null)
                throw new InvalidOperationException("Check-in não encontrado");

            return await _checkInRepository.ExcluirAsync(id);
        }

        public async Task<bool> ExisteAsync(int id)
        {
            if (id <= 0)
                return false;

            return await _checkInRepository.ExisteAsync(id);
        }

        public async Task<bool> ClienteJaFezCheckInHojeAsync(int clienteId, int academiaId)
        {
            if (clienteId <= 0)
                throw new ArgumentException("ClienteId deve ser maior que zero", nameof(clienteId));

            if (academiaId <= 0)
                throw new ArgumentException("AcademiaId deve ser maior que zero", nameof(academiaId));

            return await _checkInRepository.ClienteJaFezCheckInHojeAsync(clienteId, academiaId);
        }

        public async Task<int> ContarCheckInsPorUsuarioMesAsync(int usuarioId, int ano, int mes)
        {
            if (usuarioId <= 0)
                throw new ArgumentException("UsuarioId deve ser maior que zero", nameof(usuarioId));

            return await _checkInRepository.ContarCheckInsPorUsuarioMesAsync(usuarioId, ano, mes);
        }

        private async Task ValidarCheckInAsync(int clienteId, int academiaId, int? planoClienteId)
        {
            if (clienteId <= 0)
                throw new ArgumentException("ClienteId deve ser maior que zero", nameof(clienteId));

            if (academiaId <= 0)
                throw new ArgumentException("AcademiaId deve ser maior que zero", nameof(academiaId));

            // Verificar se o cliente existe
            var clienteExiste = await _clienteRepository.ExisteAsync(clienteId);
            if (!clienteExiste)
                throw new InvalidOperationException("Cliente não encontrado");

            // Se foi informado um plano, verificar se existe
            if (planoClienteId.HasValue && planoClienteId.Value > 0)
            {
                var planoExiste = await _planoClienteRepository.ExisteAsync(planoClienteId.Value);
                if (!planoExiste)
                    throw new InvalidOperationException("Plano do cliente não encontrado");
            }
        }

        private static void ValidarCheckIn(CheckInEntity checkIn)
        {
            if (checkIn == null)
                throw new ArgumentNullException(nameof(checkIn));

            if (checkIn.ClienteId <= 0)
                throw new ArgumentException("ClienteId deve ser maior que zero", nameof(checkIn.ClienteId));

            if (checkIn.AcademiaId <= 0)
                throw new ArgumentException("AcademiaId deve ser maior que zero", nameof(checkIn.AcademiaId));
        }
    }
}
