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
    public class MensalidadeService : ServiceBase<MensalidadeValidator>, IMensalidadeService
    {
        #region [ Campos ]

        private readonly IMensalidadeSqlServerRepository _mensalidadeRepository;
        private readonly ILogger<MensalidadeService> _logger;

        #endregion

        #region [ Construtor ]

        public MensalidadeService(
            IMensalidadeSqlServerRepository mensalidadeRepository,
            ILogger<MensalidadeService> logger)
        {
            _mensalidadeRepository = mensalidadeRepository;
            _logger = logger;
        }

        #endregion

        #region [ Métodos Públicos ]

        public async Task<IResultadoValidacao<IEnumerable<MensalidadeEntity>>> ObterTodosAsync()
        {
            try
            {
                var mensalidades = await _mensalidadeRepository.ObterTodosAsync();
                return new ResultadoValidacao<IEnumerable<MensalidadeEntity>>(mensalidades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todas as mensalidades");
                return new ResultadoValidacao<IEnumerable<MensalidadeEntity>>(ex, "Erro ao obter todas as mensalidades");
            }
        }

        public async Task<IResultadoValidacao<MensalidadeEntity?>> ObterPorIdAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<MensalidadeEntity?>(validacao);

                var mensalidade = await _mensalidadeRepository.ObterPorIdAsync(id);
                return new ResultadoValidacao<MensalidadeEntity?>(mensalidade);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter mensalidade por ID: {Id}", id);
                return new ResultadoValidacao<MensalidadeEntity?>(ex, $"Erro ao obter mensalidade com ID {id}");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<MensalidadeEntity>>> ObterPorUsuarioAsync(int usuarioId)
        {
            try
            {
                var validacao = Validator.ValidarUsuarioId(usuarioId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<MensalidadeEntity>>(validacao);

                var mensalidades = await _mensalidadeRepository.ObterPorUsuarioAsync(usuarioId);
                return new ResultadoValidacao<IEnumerable<MensalidadeEntity>>(mensalidades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter mensalidades por usuário: {UsuarioId}", usuarioId);
                return new ResultadoValidacao<IEnumerable<MensalidadeEntity>>(ex, $"Erro ao obter mensalidades do usuário {usuarioId}");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<MensalidadeEntity>>> ObterPendentesAsync()
        {
            try
            {
                var mensalidades = await _mensalidadeRepository.ObterPendentesAsync();
                return new ResultadoValidacao<IEnumerable<MensalidadeEntity>>(mensalidades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter mensalidades pendentes");
                return new ResultadoValidacao<IEnumerable<MensalidadeEntity>>(ex, "Erro ao obter mensalidades pendentes");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<MensalidadeEntity>>> ObterVencendasAsync(int dias = 7)
        {
            try
            {
                var validacao = Validator.ValidarDias(dias);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<MensalidadeEntity>>(validacao);

                var mensalidades = await _mensalidadeRepository.ObterVencendasAsync(dias);
                return new ResultadoValidacao<IEnumerable<MensalidadeEntity>>(mensalidades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter mensalidades vencendas em {Dias} dias", dias);
                return new ResultadoValidacao<IEnumerable<MensalidadeEntity>>(ex, $"Erro ao obter mensalidades vencendas em {dias} dias");
            }
        }

        public async Task<IResultadoValidacao<bool>> EstaEmDiaAsync(int usuarioId)
        {
            try
            {
                var validacao = Validator.ValidarUsuarioId(usuarioId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var estaEmDia = await _mensalidadeRepository.EstaEmDiaAsync(usuarioId);
                return new ResultadoValidacao<bool>(estaEmDia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar se usuário está em dia: {UsuarioId}", usuarioId);
                return new ResultadoValidacao<bool>(ex, "Erro ao verificar se usuário está em dia");
            }
        }

        public async Task<IResultadoValidacao<bool>> RemoverAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                // Verificar se existe
                var existe = await _mensalidadeRepository.ExisteAsync(id);
                if (!existe)
                    return new ResultadoValidacao<bool>("Mensalidade não encontrada.");

                var sucesso = await _mensalidadeRepository.RemoverAsync(id);
                return new ResultadoValidacao<bool>(sucesso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover mensalidade: {Id}", id);
                return new ResultadoValidacao<bool>(ex, "Erro ao remover mensalidade");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _mensalidadeRepository.ExisteAsync(id);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar existência da mensalidade: {Id}", id);
                return new ResultadoValidacao<bool>(ex, "Erro ao verificar existência da mensalidade");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<MensalidadeEntity>>> ListarPorUsuarioAsync(int usuarioId)
        {
            return await ObterPorUsuarioAsync(usuarioId);
        }

        public async Task<IResultadoValidacao<MensalidadeEntity?>> ObterMensalidadeAtualAsync(int usuarioId)
        {
            try
            {
                var validacao = Validator.ValidarUsuarioId(usuarioId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<MensalidadeEntity?>(validacao);

                var mensalidade = await _mensalidadeRepository.ObterMensalidadeAtualAsync(usuarioId);
                return new ResultadoValidacao<MensalidadeEntity?>(mensalidade);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter mensalidade atual do usuário: {UsuarioId}", usuarioId);
                return new ResultadoValidacao<MensalidadeEntity?>(ex, "Erro ao obter mensalidade atual do usuário");
            }
        }        public async Task<IResultadoValidacao<int>> RegistrarPagamentoMensalidadeAsync(int usuarioId, int planoId, int meses, decimal valorBase, DateTime dataInicio)
        {
            try
            {
                var valorComDesconto = CalcularValorComDesconto(meses, valorBase);
                var mesAtual = DateTime.Now.Month;
                var anoAtual = DateTime.Now.Year;
                
                var validacao = Validator.ValidarPagamentoMensalidade(dataInicio, "Paga", valorComDesconto, mesAtual, anoAtual);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);

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

                var id = await _mensalidadeRepository.CriarAsync(mensalidade);
                return new ResultadoValidacao<int>(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar pagamento de mensalidade: {UsuarioId}", usuarioId);
                return new ResultadoValidacao<int>(ex, "Erro ao registrar pagamento de mensalidade");
            }
        }

        public async Task<IResultadoValidacao<bool>> VerificarMensalidadeEmDiaAsync(int usuarioId)
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

        #endregion
    }
}
