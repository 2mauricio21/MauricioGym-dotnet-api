using System;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Services.Validators
{
    public class PagamentoValidator : ValidatorService
    {
        public IResultadoValidacao IncluirPagamento(PagamentoEntity pagamento)
        {
            if (pagamento == null)
                return new ResultadoValidacao("Pagamento não informado.");

            if (pagamento.ClienteId <= 0)
                return new ResultadoValidacao("Cliente não informado.");

            if (pagamento.AcademiaId <= 0)
                return new ResultadoValidacao("Academia não informada.");

            if (pagamento.PlanoClienteId <= 0)
                return new ResultadoValidacao("Plano do cliente não informado.");

            if (pagamento.Valor <= 0)
                return new ResultadoValidacao("Valor deve ser maior que zero.");

            if (string.IsNullOrEmpty(pagamento.FormaPagamento))
                return new ResultadoValidacao("Forma de pagamento não informada.");

            if (pagamento.NumeroParcelas.HasValue && pagamento.NumeroParcelas <= 0)
                return new ResultadoValidacao("Número de parcelas deve ser maior que zero.");

            if (pagamento.ParcelaAtual.HasValue && pagamento.ParcelaAtual <= 0)
                return new ResultadoValidacao("Parcela atual deve ser maior que zero.");

            if (pagamento.ParcelaAtual.HasValue && pagamento.NumeroParcelas.HasValue && 
                pagamento.ParcelaAtual > pagamento.NumeroParcelas)
                return new ResultadoValidacao("Parcela atual não pode ser maior que o número de parcelas.");

            if (pagamento.NumeroParcelas.HasValue && pagamento.NumeroParcelas > 1 && 
                (!pagamento.ValorParcela.HasValue || pagamento.ValorParcela <= 0))
                return new ResultadoValidacao("Valor da parcela deve ser informado para pagamentos parcelados.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarPagamento(PagamentoEntity pagamento)
        {
            if (pagamento == null)
                return new ResultadoValidacao("Pagamento não informado.");

            if (pagamento.Id <= 0)
                return new ResultadoValidacao("ID do pagamento é inválido.");

            if (pagamento.ClienteId <= 0)
                return new ResultadoValidacao("Cliente não informado.");

            if (pagamento.AcademiaId <= 0)
                return new ResultadoValidacao("Academia não informada.");

            if (pagamento.PlanoClienteId <= 0)
                return new ResultadoValidacao("Plano do cliente não informado.");

            if (pagamento.Valor <= 0)
                return new ResultadoValidacao("Valor deve ser maior que zero.");

            if (string.IsNullOrEmpty(pagamento.FormaPagamento))
                return new ResultadoValidacao("Forma de pagamento não informada.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarPagamento(int idPagamento)
        {
            if (idPagamento <= 0)
                return new ResultadoValidacao("O ID do pagamento deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ExcluirPagamento(int idPagamento)
        {
            if (idPagamento <= 0)
                return new ResultadoValidacao("O ID do pagamento deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarPagamentosPorCliente(int clienteId)
        {
            if (clienteId <= 0)
                return new ResultadoValidacao("O ID do cliente deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarPagamentosPorAcademia(int academiaId)
        {
            if (academiaId <= 0)
                return new ResultadoValidacao("O ID da academia deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarPagamentosPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            if (dataInicio >= dataFim)
                return new ResultadoValidacao("Data de início deve ser anterior à data de fim.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarPagamentosEmAtrasoPorUsuario(int usuarioId)
        {
            if (usuarioId <= 0)
                return new ResultadoValidacao("O ID do usuário deve ser maior que zero.");

            return new ResultadoValidacao();
        }
    }
}