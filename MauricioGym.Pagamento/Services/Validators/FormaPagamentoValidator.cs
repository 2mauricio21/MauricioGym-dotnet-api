using System;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Pagamento.Entities;

namespace MauricioGym.Pagamento.Services.Validators
{
    public class FormaPagamentoValidator : ValidatorService
    {
        public IResultadoValidacao IncluirFormaPagamentoAsync(FormaPagamentoEntity formaPagamento)
        {
            if (formaPagamento == null)
                return new ResultadoValidacao("A forma de pagamento não pode ser nula.");

            if (string.IsNullOrWhiteSpace(formaPagamento.Nome))
                return new ResultadoValidacao("O nome da forma de pagamento é obrigatório.");

            if (formaPagamento.IdAcademia <= 0)
                return new ResultadoValidacao("O ID da academia deve ser maior que zero.");

            if (formaPagamento.AceitaParcelamento && formaPagamento.MaximoParcelas <= 0)
                return new ResultadoValidacao("O máximo de parcelas deve ser maior que zero quando aceitar parcelamento.");

            if (formaPagamento.TaxaProcessamento < 0)
                return new ResultadoValidacao("A taxa de processamento não pode ser negativa.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarFormaPagamentoAsync(FormaPagamentoEntity formaPagamento)
        {
            if (formaPagamento == null)
                return new ResultadoValidacao("Forma de pagamento não pode ser nula.");

            if (formaPagamento.IdFormaPagamento <= 0)
                return new ResultadoValidacao("ID da forma de pagamento deve ser maior que zero.");

            if (formaPagamento.IdAcademia <= 0)
                return new ResultadoValidacao("ID da academia deve ser maior que zero.");

            if (string.IsNullOrWhiteSpace(formaPagamento.Nome))
                return new ResultadoValidacao("Nome da forma de pagamento é obrigatório.");

            if (formaPagamento.TaxaProcessamento < 0)
                return new ResultadoValidacao("Taxa de processamento deve ser maior ou igual a zero.");

            if (formaPagamento.AceitaParcelamento && formaPagamento.MaximoParcelas <= 0)
                return new ResultadoValidacao("Número máximo de parcelas deve ser maior que zero quando aceita parcelamento.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarFormaPagamentoAsync(int idFormaPagamento)
        {
            if (idFormaPagamento <= 0)
                return new ResultadoValidacao("ID da forma de pagamento deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ExcluirFormaPagamentoAsync(int idFormaPagamento)
        {
            if (idFormaPagamento <= 0)
                return new ResultadoValidacao("ID da forma de pagamento deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ListarFormasPagamentoPorAcademiaAsync(int idAcademia)
        {
            if (idAcademia <= 0)
                return new ResultadoValidacao("ID da academia deve ser maior que zero.");

            return new ResultadoValidacao();
        }
    }
}