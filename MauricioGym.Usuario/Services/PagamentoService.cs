using MauricioGym.Infra.Services;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.SqlServer.Interfaces;
using MauricioGym.Usuario.Services.Interfaces;
using MauricioGym.Usuario.Services.Validators;
using Microsoft.Extensions.Logging;

namespace MauricioGym.Usuario.Services
{
    public class PagamentoService : ServiceBase<PagamentoValidator>, IPagamentoService
    {
        private readonly IPagamentoSqlServerRepository _pagamentoRepository;
        private readonly ILogger<PagamentoService> _logger;

        public PagamentoService(
            IPagamentoSqlServerRepository pagamentoRepository,
            ILogger<PagamentoService> logger)
        {
            _pagamentoRepository = pagamentoRepository;
            _logger = logger;
        }

        public async Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ObterTodosAsync()
        {
            try
            {
                var pagamentos = await _pagamentoRepository.ObterTodosAsync();
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(pagamentos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todos os pagamentos");
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(ex, "Erro ao obter todos os pagamentos");
            }
        }

        public async Task<IResultadoValidacao<PagamentoEntity?>> ObterPorIdAsync(int id)
        {
            try
            {
                var validacao = Validator.ConsultarPagamento(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<PagamentoEntity?>(validacao);

                var pagamento = await _pagamentoRepository.ObterPorIdAsync(id);
                return new ResultadoValidacao<PagamentoEntity?>(pagamento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter pagamento por ID: {Id}", id);
                return new ResultadoValidacao<PagamentoEntity?>(ex, "Erro ao obter pagamento por ID");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ObterPorClienteIdAsync(int clienteId)
        {
            try
            {
                var validacao = Validator.ConsultarPagamentosPorCliente(clienteId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(validacao);

                var pagamentos = await _pagamentoRepository.ObterPorClienteIdAsync(clienteId);
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(pagamentos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter pagamentos por cliente: {ClienteId}", clienteId);
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(ex, "Erro ao obter pagamentos por cliente");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ObterPorAcademiaIdAsync(int academiaId)
        {
            try
            {
                var validacao = Validator.ConsultarPagamentosPorAcademia(academiaId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(validacao);

                var pagamentos = await _pagamentoRepository.ObterPorAcademiaIdAsync(academiaId);
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(pagamentos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter pagamentos por academia: {AcademiaId}", academiaId);
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(ex, "Erro ao obter pagamentos por academia");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ObterPorPlanoClienteIdAsync(int planoClienteId)
        {
            try
            {
                var pagamentos = await _pagamentoRepository.ObterPorPlanoClienteIdAsync(planoClienteId);
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(pagamentos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter pagamentos por plano cliente: {PlanoClienteId}", planoClienteId);
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(ex, "Erro ao obter pagamentos por plano cliente");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ObterPorPeriodoAsync(DateTime dataInicio, DateTime dataFim, int? academiaId = null)
        {
            try
            {
                var validacao = Validator.ConsultarPagamentosPorPeriodo(dataInicio, dataFim);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(validacao);

                var pagamentos = await _pagamentoRepository.ObterPorPeriodoAsync(dataInicio, dataFim, academiaId);
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(pagamentos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter pagamentos por período: {DataInicio} a {DataFim}, Academia: {AcademiaId}",
                    dataInicio, dataFim, academiaId);
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(ex, "Erro ao obter pagamentos por período");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ObterPendentesAsync(int? academiaId = null)
        {
            try
            {
                var pagamentos = await _pagamentoRepository.ObterPendentesAsync(academiaId);
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(pagamentos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter pagamentos pendentes. Academia: {AcademiaId}", academiaId);
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(ex, "Erro ao obter pagamentos pendentes");
            }
        }

        public async Task<IResultadoValidacao<decimal>> ObterTotalRecebidoPorPeriodoAsync(DateTime dataInicio, DateTime dataFim, int? academiaId = null)
        {
            try
            {
                var validacao = Validator.ConsultarPagamentosPorPeriodo(dataInicio, dataFim);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<decimal>(validacao);

                var total = await _pagamentoRepository.ObterTotalRecebidoPorPeriodoAsync(dataInicio, dataFim, academiaId);
                return new ResultadoValidacao<decimal>(total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter total recebido por período: {DataInicio} a {DataFim}, Academia: {AcademiaId}",
                    dataInicio, dataFim, academiaId);
                return new ResultadoValidacao<decimal>(ex, "Erro ao obter total recebido por período");
            }
        }

        public async Task<IResultadoValidacao<int>> CriarAsync(PagamentoEntity pagamento)
        {
            try
            {
                var validacao = Validator.IncluirPagamento(pagamento);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);

                pagamento.DataCriacao = DateTime.Now;
                pagamento.Ativo = true;

                var id = await _pagamentoRepository.CriarAsync(pagamento);
                return new ResultadoValidacao<int>(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar pagamento: {@Pagamento}", pagamento);
                return new ResultadoValidacao<int>(ex, "Erro ao criar pagamento");
            }
        }

        public async Task<IResultadoValidacao<bool>> AtualizarAsync(PagamentoEntity pagamento)
        {
            try
            {
                var validacao = Validator.AlterarPagamento(pagamento);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _pagamentoRepository.ExisteAsync(pagamento.Id);
                if (!existe)
                    return new ResultadoValidacao<bool>("Pagamento não encontrado.");

                pagamento.DataAlteracao = DateTime.Now;
                var sucesso = await _pagamentoRepository.AtualizarAsync(pagamento);
                return new ResultadoValidacao<bool>(sucesso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar pagamento: {@Pagamento}", pagamento);
                return new ResultadoValidacao<bool>(ex, "Erro ao atualizar pagamento");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExcluirAsync(int id)
        {
            try
            {
                var validacao = Validator.ExcluirPagamento(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _pagamentoRepository.ExisteAsync(id);
                if (!existe)
                    return new ResultadoValidacao<bool>("Pagamento não encontrado.");

                var sucesso = await _pagamentoRepository.ExcluirAsync(id);
                return new ResultadoValidacao<bool>(sucesso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir pagamento: {Id}", id);
                return new ResultadoValidacao<bool>(ex, "Erro ao excluir pagamento");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteAsync(int id)
        {
            try
            {
                var validacao = Validator.ConsultarPagamento(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _pagamentoRepository.ExisteAsync(id);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar existência do pagamento: {Id}", id);
                return new ResultadoValidacao<bool>(ex, "Erro ao verificar existência do pagamento");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ObterPagamentosEmAtrasoPorUsuarioAsync(int usuarioId)
        {
            try
            {
                var validacao = Validator.ConsultarPagamentosEmAtrasoPorUsuario(usuarioId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(validacao);

                var pagamentos = await _pagamentoRepository.ObterPagamentosEmAtrasoPorUsuarioAsync(usuarioId);
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(pagamentos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter pagamentos em atraso por usuário: {UsuarioId}", usuarioId);
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(ex, "Erro ao obter pagamentos em atraso por usuário");
            }
        }
    }
}