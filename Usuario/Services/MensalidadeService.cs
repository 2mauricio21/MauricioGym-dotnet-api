using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.Interfaces;
using MauricioGym.Usuario.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace MauricioGym.Usuario.Services
{
    public class MensalidadeService : IMensalidadeService
    {
        private readonly IMensalidadeSqlServerRepository _mensalidadeRepository;
        private readonly ILogger<MensalidadeService> _logger;

        public MensalidadeService(
            IMensalidadeSqlServerRepository mensalidadeRepository,
            ILogger<MensalidadeService> logger)
        {
            _mensalidadeRepository = mensalidadeRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<MensalidadeEntity>> ObterTodosAsync()
        {
            try
            {
                return await _mensalidadeRepository.ObterTodosAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todas as mensalidades");
                throw;
            }
        }

        public async Task<MensalidadeEntity?> ObterPorIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return null;

                return await _mensalidadeRepository.ObterPorIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter mensalidade por ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<MensalidadeEntity>> ObterPorUsuarioAsync(int usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                    return Enumerable.Empty<MensalidadeEntity>();

                return await _mensalidadeRepository.ObterPorUsuarioAsync(usuarioId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter mensalidades por usuário: {UsuarioId}", usuarioId);
                throw;
            }
        }

        public async Task<IEnumerable<MensalidadeEntity>> ObterPendentesAsync()
        {
            try
            {
                return await _mensalidadeRepository.ObterPendentesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter mensalidades pendentes");
                throw;
            }
        }

        public async Task<IEnumerable<MensalidadeEntity>> ObterVencendasAsync(int dias = 7)
        {
            try
            {
                if (dias < 0)
                    dias = 7;

                return await _mensalidadeRepository.ObterVencendasAsync(dias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter mensalidades vencendas em {Dias} dias", dias);
                throw;
            }
        }

        public async Task<bool> EstaEmDiaAsync(int usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                    return false;

                return await _mensalidadeRepository.EstaEmDiaAsync(usuarioId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar se usuário está em dia: {UsuarioId}", usuarioId);
                throw;
            }
        }

        public async Task<bool> RemoverAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("ID inválido", nameof(id));

                if (!await _mensalidadeRepository.ExisteAsync(id))
                    throw new InvalidOperationException("Mensalidade não encontrada");

                return await _mensalidadeRepository.RemoverAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover mensalidade: {Id}", id);
                throw;
            }
        }        public async Task<bool> ExisteAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return false;

                return await _mensalidadeRepository.ExisteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar existência da mensalidade: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<MensalidadeEntity>> ListarPorUsuarioAsync(int usuarioId)
        {
            return await ObterPorUsuarioAsync(usuarioId);
        }

        public async Task<MensalidadeEntity?> ObterMensalidadeAtualAsync(int usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                    return null;

                return await _mensalidadeRepository.ObterMensalidadeAtualAsync(usuarioId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter mensalidade atual do usuário: {UsuarioId}", usuarioId);
                throw;
            }
        }

        public async Task<int> RegistrarPagamentoMensalidadeAsync(int usuarioId, int planoId, int meses, decimal valorBase, DateTime dataInicio)
        {
            try
            {
                if (usuarioId <= 0)
                    throw new ArgumentException("ID do usuário inválido", nameof(usuarioId));

                if (planoId <= 0)
                    throw new ArgumentException("ID do plano inválido", nameof(planoId));

                if (meses <= 0)
                    throw new ArgumentException("Quantidade de meses deve ser maior que zero", nameof(meses));

                if (valorBase <= 0)
                    throw new ArgumentException("Valor base deve ser maior que zero", nameof(valorBase));                var valorComDesconto = CalcularValorComDesconto(meses, valorBase);

                // Para a nova estrutura, vamos usar um UsuarioPlanoId fictício por enquanto
                // Isso deveria vir de um parâmetro do método
                var mesAtual = DateTime.Now.Month;
                var anoAtual = DateTime.Now.Year;

                var mensalidade = new MensalidadeEntity
                {
                    UsuarioPlanoId = 1, // TODO: Isso deveria vir como parâmetro
                    MesReferencia = mesAtual,
                    AnoReferencia = anoAtual,
                    DataVencimento = dataInicio.AddMonths(meses),
                    DataPagamento = DateTime.Now,
                    Valor = valorComDesconto,
                    Status = "Paga",
                    Ativo = true,
                    DataCriacao = DateTime.Now
                };

                return await _mensalidadeRepository.CriarAsync(mensalidade);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar pagamento de mensalidade: {UsuarioId}", usuarioId);
                throw;
            }
        }

        public async Task<bool> VerificarMensalidadeEmDiaAsync(int usuarioId)
        {
            return await EstaEmDiaAsync(usuarioId);
        }

        public decimal CalcularValorComDesconto(int meses, decimal valorBase)
        {
            var valorTotal = valorBase * meses;
            
            // Aplicar desconto baseado na quantidade de meses
            decimal percentualDesconto = meses switch
            {
                >= 12 => 0.15m, // 15% para 12 meses ou mais
                >= 6 => 0.10m,  // 10% para 6 meses ou mais
                >= 3 => 0.05m,  // 5% para 3 meses ou mais
                _ => 0m         // Sem desconto para menos de 3 meses
            };

            return valorTotal * (1 - percentualDesconto);
        }
    }
}
