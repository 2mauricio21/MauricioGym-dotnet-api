using System;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Pagamento.Entities;

namespace MauricioGym.Pagamento.Services.Validators
{
    public class PagamentoValidator : ValidatorService
    {
        public IResultadoValidacao IncluirPagamentoAsync(PagamentoEntity pagamento)
        {
            if (pagamento == null)
                return new ResultadoValidacao("Pagamento não pode ser nulo.");

            if (pagamento.IdUsuario <= 0)
                return new ResultadoValidacao("ID do usuário deve ser maior que zero.");

            if (pagamento.Valor <= 0)
                return new ResultadoValidacao("Valor do pagamento deve ser maior que zero.");

            if (pagamento.DataVencimento == default)
                return new ResultadoValidacao("Data de vencimento é obrigatória.");

            if (string.IsNullOrWhiteSpace(pagamento.FormaPagamento))
                return new ResultadoValidacao("Forma de pagamento é obrigatória.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarPagamento(PagamentoEntity pagamento)
        {
            if (pagamento == null)
                return new ResultadoValidacao("O pagamento não pode ser nulo.");

            if (pagamento.IdPagamento <= 0)
                return new ResultadoValidacao("O ID do pagamento deve ser maior que zero.");

            if (pagamento.IdUsuario <= 0)
                return new ResultadoValidacao("O ID do usuário deve ser maior que zero.");

            if (pagamento.IdUsuarioPlano <= 0)
                return new ResultadoValidacao("O ID do plano do usuário deve ser maior que zero.");

            if (pagamento.Valor <= 0)
                return new ResultadoValidacao("O valor do pagamento deve ser maior que zero.");

            if (pagamento.DataVencimento == default)
                return new ResultadoValidacao("A data de vencimento é obrigatória.");

            if (string.IsNullOrWhiteSpace(pagamento.FormaPagamento))
                return new ResultadoValidacao("A forma de pagamento é obrigatória.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarPagamento(int idPagamento)
        {
            if (idPagamento <= 0)
                return new ResultadoValidacao("ID do pagamento deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarPagamentoPorTransacao(string transacaoId)
        {
            if (string.IsNullOrWhiteSpace(transacaoId))
                return new ResultadoValidacao("ID da transação é obrigatório.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ListarPorUsuario(int idUsuario)
        {
            if (idUsuario <= 0)
                return new ResultadoValidacao("ID do usuário deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ListarPorUsuarioPlano(int idUsuario, int idPlano)
        {
            if (idUsuario <= 0)
                return new ResultadoValidacao("ID do usuário deve ser maior que zero.");

            if (idPlano <= 0)
                return new ResultadoValidacao("ID do plano deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao CancelarPagamento(int idPagamento)
        {
            if (idPagamento <= 0)
                return new ResultadoValidacao("ID do pagamento deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ListarPagamentos()
        {
            return new ResultadoValidacao();
        }

        public IResultadoValidacao ListarPagamentosPendentes()
        {
            return new ResultadoValidacao();
        }
    }
}