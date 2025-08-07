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
    public class FormaPagamentoService : ServiceBase<FormaPagamentoValidator>, IFormaPagamentoService
    {
        private readonly IFormaPagamentoSqlServerRepository formaPagamentoSqlServerRepository;
        private readonly IAuditoriaService auditoriaService;

        public FormaPagamentoService(
            IFormaPagamentoSqlServerRepository formaPagamentoSqlServerRepository,
            IAuditoriaService auditoriaService
            )
        {
            this.formaPagamentoSqlServerRepository = formaPagamentoSqlServerRepository;
            this.auditoriaService = auditoriaService;
        }

        public async Task<IResultadoValidacao<FormaPagamentoEntity>> IncluirFormaPagamentoAsync(FormaPagamentoEntity formaPagamento, int idUsuario)
        {
            try
            {
                var validacao = Validator.IncluirFormaPagamentoAsync(formaPagamento);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<FormaPagamentoEntity>(validacao.MensagemErro);

                formaPagamento.Ativo = true;

                var result = await formaPagamentoSqlServerRepository.IncluirAsync(formaPagamento);
                
                var incluirAuditoria = await auditoriaService.IncluirAuditoriaAsync(idUsuario,"A Forma de Pagamento foi cadastrada.");
                
                return new ResultadoValidacao<FormaPagamentoEntity>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<FormaPagamentoEntity>(ex, "[FormaPagamentoService]-Ocorreu erro ao tentar incluir a forma de pagamento.");
            }
        }

        public async Task<IResultadoValidacao<FormaPagamentoEntity>> ConsultarFormaPagamentoAsync(int idFormaPagamento)
        {
            try
            {
                var validacao = Validator.ConsultarFormaPagamentoAsync(idFormaPagamento);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<FormaPagamentoEntity>(validacao);

                var result = await formaPagamentoSqlServerRepository.ObterPorIdAsync(idFormaPagamento);
                return new ResultadoValidacao<FormaPagamentoEntity>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<FormaPagamentoEntity>(ex, "[FormaPagamentoService]-Ocorreu erro ao tentar consultar a forma de pagamento.");
            }
        }

        public async Task<IResultadoValidacao<FormaPagamentoEntity>> ConsultarFormaPagamentoPorNomeAsync(string nome, int idAcademia)
        {
            try
            {
                var result = await formaPagamentoSqlServerRepository.ObterPorNomeAsync(nome, idAcademia);
                return new ResultadoValidacao<FormaPagamentoEntity>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<FormaPagamentoEntity>(ex, "[FormaPagamentoService]-Ocorreu erro ao tentar consultar a forma de pagamento por nome.");
            }
        }

        public async Task<IResultadoValidacao> AlterarFormaPagamentoAsync(FormaPagamentoEntity formaPagamento, int idUsuario)
        {
            try
            {
                var validacao = Validator.AlterarFormaPagamentoAsync(formaPagamento);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao(validacao.MensagemErro);

                await formaPagamentoSqlServerRepository.AtualizarAsync(formaPagamento);
                
                var incluirAuditoria = await auditoriaService.IncluirAuditoriaAsync(idUsuario, "A Forma de Pagamento foi alterada.");
                
                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[FormaPagamentoService]-Ocorreu erro ao tentar alterar a forma de pagamento.");
            }
        }

        public async Task<IResultadoValidacao> ExcluirFormaPagamentoAsync(int idFormaPagamento, int idUsuario)
        {
            try
            {
                var validacao = Validator.ExcluirFormaPagamentoAsync(idFormaPagamento);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao(validacao.MensagemErro);

                var formaPagamento = await formaPagamentoSqlServerRepository.ObterPorIdAsync(idFormaPagamento);
                if (formaPagamento == null)
                    return new ResultadoValidacao($"Forma de pagamento com ID {idFormaPagamento} não encontrada.");

                await formaPagamentoSqlServerRepository.ExcluirAsync(idFormaPagamento);

                var incluirAuditoria = await auditoriaService.IncluirAuditoriaAsync(idUsuario, "A Forma de Pagamento foi excluída.");

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[FormaPagamentoService]-Ocorreu erro ao tentar excluir a forma de pagamento.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<FormaPagamentoEntity>>> ListarFormasPagamentoAsync()
        {
            try
            {
                var result = await formaPagamentoSqlServerRepository.ListarTodosAsync();
                return new ResultadoValidacao<IEnumerable<FormaPagamentoEntity>>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<FormaPagamentoEntity>>(ex, "[FormaPagamentoService]-Ocorreu erro ao tentar listar as formas de pagamento.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<FormaPagamentoEntity>>> ListarFormasPagamentoPorAcademiaAsync(int idAcademia)
        {
            try
            {
                var validacao = Validator.ListarFormasPagamentoPorAcademiaAsync(idAcademia);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<FormaPagamentoEntity>>(validacao.MensagemErro);

                var result = await formaPagamentoSqlServerRepository.ListarPorAcademiaAsync(idAcademia);
                return new ResultadoValidacao<IEnumerable<FormaPagamentoEntity>>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<FormaPagamentoEntity>>(ex, "[FormaPagamentoService]-Ocorreu erro ao tentar listar as formas de pagamento por academia.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<FormaPagamentoEntity>>> ListarFormasPagamentoAtivasAsync()
        {
            try
            {
                var result = await formaPagamentoSqlServerRepository.ListarAtivosAsync();
                return new ResultadoValidacao<IEnumerable<FormaPagamentoEntity>>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<FormaPagamentoEntity>>(ex, "[FormaPagamentoService]-Ocorreu erro ao tentar listar as formas de pagamento ativas.");
            }
        }


    }
}