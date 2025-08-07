using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Pagamento.Entities;
using MauricioGym.Pagamento.Repositories.SqlServer.Interfaces;
using MauricioGym.Pagamento.Services.Interfaces;
using MauricioGym.Pagamento.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Infra.Entities;

namespace MauricioGym.Pagamento.Services
{
    public class PagamentoService : ServiceBase<PagamentoValidator>, IPagamentoService
    {
        private readonly IPagamentoSqlServerRepository pagamentoSqlServerRepository;
        private readonly IAuditoriaService auditoriaService;

        public PagamentoService(
            IPagamentoSqlServerRepository pagamentoSqlServerRepository,
            IAuditoriaService auditoriaService
            )
        {
            this.pagamentoSqlServerRepository = pagamentoSqlServerRepository;
            this.auditoriaService = auditoriaService;
        }

        public async Task<IResultadoValidacao<PagamentoEntity>> IncluirPagamentoAsync(PagamentoEntity pagamento, int idUsuario)
        {
            try
            {
                var validacao = Validator.IncluirPagamentoAsync(pagamento);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<PagamentoEntity>(validacao);
                
                pagamento.DataPagamento = DateTime.Now;
                pagamento.StatusPagamento = "Pendente";
                pagamento.TransacaoId = GerarTransacaoId();
                pagamento.Ativo = true;
                pagamento.DataCadastro = DateTime.Now;

                var result = await pagamentoSqlServerRepository.IncluirAsync(pagamento);
                
                await auditoriaService.IncluirAuditoriaAsync(idUsuario, "O Pagamento foi cadastrado.", pagamento);
                
                return new ResultadoValidacao<PagamentoEntity>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<PagamentoEntity>(ex, "[PagamentoService]-Ocorreu erro ao tentar incluir o pagamento.");
            }
        }

        public async Task<IResultadoValidacao<PagamentoEntity>> ConsultarPagamentoAsync(int idPagamento)
        {
            try
            {
                var validacao = Validator.ConsultarPagamento(idPagamento);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<PagamentoEntity>(validacao);

                var result = await pagamentoSqlServerRepository.ObterPorIdAsync(idPagamento);
                return new ResultadoValidacao<PagamentoEntity>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<PagamentoEntity>(ex, "[PagamentoService]-Ocorreu erro ao tentar consultar o pagamento.");
            }
        }

        public async Task<IResultadoValidacao<PagamentoEntity>> ConsultarPagamentoPorTransacaoAsync(string transacaoId)
        {
            try
            {
                var validacao = Validator.ConsultarPagamentoPorTransacao(transacaoId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<PagamentoEntity>(validacao);

                var pagamento = await pagamentoSqlServerRepository.ObterPorTransacaoAsync(transacaoId);
                return new ResultadoValidacao<PagamentoEntity>(pagamento);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<PagamentoEntity>(ex, "[PagamentoService]-Ocorreu erro ao tentar consultar o pagamento por transação.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ConsultarPagamentosPorUsuarioAsync(int idUsuario)
        {
            try
            {
                var validacao = Validator.ListarPorUsuario(idUsuario);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(validacao);

                var pagamentos = await pagamentoSqlServerRepository.ListarPorUsuarioAsync(idUsuario);
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(pagamentos);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(ex, "[PagamentoService]-Ocorreu erro ao tentar consultar pagamentos por usuário.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ConsultarPagamentosPorUsuarioPlanoAsync(int idUsuarioPlano)
        {
            try
            {
                var validacao = Validator.ListarPorUsuarioPlano(idUsuarioPlano, 0); // Need to adjust this based on actual requirements
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(validacao);

                var pagamentos = await pagamentoSqlServerRepository.ListarPorUsuarioPlanoAsync(idUsuarioPlano);
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(pagamentos);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(ex, "[PagamentoService]-Ocorreu erro ao tentar consultar pagamentos por plano do usuário.");
            }
        }

        public async Task<IResultadoValidacao> AlterarPagamentoAsync(PagamentoEntity pagamento, int idUsuario)
        {
            try
            {
                var validacao = Validator.AlterarPagamento(pagamento);
                if (validacao.OcorreuErro)
                    return validacao;

                await pagamentoSqlServerRepository.AtualizarAsync(pagamento);
                
                await auditoriaService.IncluirAuditoriaAsync(idUsuario, "O Pagamento foi alterado.", pagamento);
                
                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[PagamentoService]-Ocorreu erro ao tentar alterar o pagamento.");
            }
        }

        public async Task<IResultadoValidacao> CancelarPagamentoAsync(int idPagamento, int idUsuario)
        {
            try
            {
                var validacao = Validator.CancelarPagamento(idPagamento);
                if (validacao.OcorreuErro)
                    return validacao;

                var pagamento = await pagamentoSqlServerRepository.ObterPorIdAsync(idPagamento);
                if (pagamento == null)
                    return new ResultadoValidacao($"Pagamento com ID {idPagamento} não encontrado.");

                pagamento.StatusPagamento = "Cancelado";
                await pagamentoSqlServerRepository.AtualizarAsync(pagamento);
                
                await auditoriaService.IncluirAuditoriaAsync(idUsuario, "O Pagamento foi cancelado.", pagamento);
                
                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[PagamentoService]-Ocorreu erro ao tentar cancelar o pagamento.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ListarPagamentosAsync()
        {
            try
            {
                var result = await pagamentoSqlServerRepository.ListarTodosAsync();
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(ex, "[PagamentoService]-Ocorreu erro ao tentar listar os pagamentos.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ListarPagamentosPendentesAsync()
        {
            try
            {
                var result = await pagamentoSqlServerRepository.ListarPendentesAsync();
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(ex, "[PagamentoService]-Ocorreu erro ao tentar listar os pagamentos pendentes.");
            }
        }



        public async Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ListarPorUsuarioAsync(int idUsuario)
        {
            try
            {
                var validacao = Validator.ListarPorUsuario(idUsuario);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(validacao);

                var result = await pagamentoSqlServerRepository.ListarPorUsuarioAsync(idUsuario);
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(ex, "[PagamentoService]-Ocorreu erro ao tentar listar os pagamentos por usuário.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PagamentoEntity>>> ListarPorUsuarioPlanoAsync(int idUsuarioPlano)
        {
            try
            {
                var validacao = Validator.ListarPorUsuarioPlano(idUsuarioPlano, 0); // Adjust validation as needed
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(validacao);

                var result = await pagamentoSqlServerRepository.ListarPorUsuarioPlanoAsync(idUsuarioPlano);
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PagamentoEntity>>(ex, "[PagamentoService]-Ocorreu erro ao tentar listar os pagamentos por usuário e plano.");
            }
        }

        private string GerarTransacaoId()
        {
            return Guid.NewGuid().ToString("N").ToUpper();
        }
    }
}