using MauricioGym.Infra.Services;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.SqlServer.Interfaces;
using MauricioGym.Usuario.Services.Interfaces;
using MauricioGym.Usuario.Services.Validators;

namespace MauricioGym.Usuario.Services
{
    public class VinculoClienteAcademiaService : ServiceBase<VinculoClienteAcademiaValidator>, IVinculoClienteAcademiaService
    {
        private readonly IVinculoClienteAcademiaSqlServerRepository _vinculoRepository;
        private readonly IClienteSqlServerRepository _clienteRepository;

        public VinculoClienteAcademiaService(
            IVinculoClienteAcademiaSqlServerRepository vinculoRepository,
            IClienteSqlServerRepository clienteRepository)
        {
            _vinculoRepository = vinculoRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<VinculoClienteAcademiaEntity?> ObterPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id deve ser maior que zero", nameof(id));

            return await _vinculoRepository.ObterPorIdAsync(id);
        }

        public async Task<VinculoClienteAcademiaEntity?> ObterPorClienteEAcademiaAsync(int clienteId, int academiaId)
        {
            if (clienteId <= 0)
                throw new ArgumentException("ClienteId deve ser maior que zero", nameof(clienteId));

            if (academiaId <= 0)
                throw new ArgumentException("AcademiaId deve ser maior que zero", nameof(academiaId));

            return await _vinculoRepository.ObterPorClienteEAcademiaAsync(clienteId, academiaId);
        }

        public async Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterPorClienteIdAsync(int clienteId)
        {
            if (clienteId <= 0)
                throw new ArgumentException("ClienteId deve ser maior que zero", nameof(clienteId));

            return await _vinculoRepository.ObterPorClienteIdAsync(clienteId);
        }

        public async Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterPorAcademiaIdAsync(int academiaId)
        {
            if (academiaId <= 0)
                throw new ArgumentException("AcademiaId deve ser maior que zero", nameof(academiaId));

            return await _vinculoRepository.ObterPorAcademiaIdAsync(academiaId);
        }

        public async Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterAtivosPorClienteIdAsync(int clienteId)
        {
            if (clienteId <= 0)
                throw new ArgumentException("ClienteId deve ser maior que zero", nameof(clienteId));

            return await _vinculoRepository.ObterAtivosPorClienteIdAsync(clienteId);
        }

        public async Task<IEnumerable<VinculoClienteAcademiaEntity>> ObterAtivosPorAcademiaIdAsync(int academiaId)
        {
            if (academiaId <= 0)
                throw new ArgumentException("AcademiaId deve ser maior que zero", nameof(academiaId));

            return await _vinculoRepository.ObterAtivosPorAcademiaIdAsync(academiaId);
        }

        public async Task<int> CriarAsync(VinculoClienteAcademiaEntity vinculo)
        {
            await ValidarVinculoAsync(vinculo.ClienteId, vinculo.AcademiaId);

            // Verificar se já existe vínculo ativo
            var vinculoExistente = await _vinculoRepository.ExisteVinculoAtivoAsync(vinculo.ClienteId, vinculo.AcademiaId);
            if (vinculoExistente)
                throw new InvalidOperationException("Cliente já possui vínculo ativo com esta academia");

            vinculo.Ativo = true;
            vinculo.DataVinculo = DateTime.UtcNow;
            vinculo.DataInclusao = DateTime.UtcNow;

            return await _vinculoRepository.CriarAsync(vinculo);
        }

        public async Task<bool> AtualizarAsync(VinculoClienteAcademiaEntity vinculo)
        {
            ValidarVinculo(vinculo);

            if (vinculo.Id <= 0)
                throw new ArgumentException("Id deve ser maior que zero", nameof(vinculo.Id));

            var vinculoExistente = await _vinculoRepository.ObterPorIdAsync(vinculo.Id);
            if (vinculoExistente == null)
                throw new InvalidOperationException("Vínculo não encontrado");

            vinculo.DataAlteracao = DateTime.UtcNow;
            return await _vinculoRepository.AtualizarAsync(vinculo);
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id deve ser maior que zero", nameof(id));

            var vinculoExistente = await _vinculoRepository.ObterPorIdAsync(id);
            if (vinculoExistente == null)
                throw new InvalidOperationException("Vínculo não encontrado");

            await _vinculoRepository.ExcluirAsync(id);
            return true;


        }

        public async Task<bool> ExisteAsync(int id)
        {
            if (id <= 0)
                return false;

            return await _vinculoRepository.ExisteAsync(id);
        }

        public async Task<bool> ExisteVinculoAsync(int clienteId, int academiaId)
        {
            if (clienteId <= 0 || academiaId <= 0)
                return false;

            return await _vinculoRepository.ExisteVinculoAsync(clienteId, academiaId);
        }

        public async Task<bool> ExisteVinculoAtivoAsync(int clienteId, int academiaId)
        {
            if (clienteId <= 0 || academiaId <= 0)
                return false;

            return await _vinculoRepository.ExisteVinculoAtivoAsync(clienteId, academiaId);
        }

        private async Task ValidarVinculoAsync(int clienteId, int academiaId)
        {
            if (clienteId <= 0)
                throw new ArgumentException("ClienteId deve ser maior que zero", nameof(clienteId));

            if (academiaId <= 0)
                throw new ArgumentException("AcademiaId deve ser maior que zero", nameof(academiaId));

            // Verificar se o cliente existe
            var clienteExiste = await _clienteRepository.ExisteAsync(clienteId);
            if (!clienteExiste)
                throw new InvalidOperationException("Cliente não encontrado");
        }

        private static void ValidarVinculo(VinculoClienteAcademiaEntity vinculo)
        {
            if (vinculo == null)
                throw new ArgumentNullException(nameof(vinculo));

            if (vinculo.ClienteId <= 0)
                throw new ArgumentException("ClienteId deve ser maior que zero", nameof(vinculo.ClienteId));

            if (vinculo.AcademiaId <= 0)
                throw new ArgumentException("AcademiaId deve ser maior que zero", nameof(vinculo.AcademiaId));
        }
    }
}
