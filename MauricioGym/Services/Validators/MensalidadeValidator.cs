using FluentValidation;
using MauricioGym.Entities;

namespace MauricioGym.Services.Validators
{
    public class MensalidadeValidator : AbstractValidator<MensalidadeEntity>
    {
        public MensalidadeValidator()
        {
            RuleFor(x => x.PessoaId).GreaterThan(0).WithMessage("PessoaId obrigatório");
            RuleFor(x => x.PlanoId).GreaterThan(0).WithMessage("PlanoId obrigatório");
            RuleFor(x => x.ValorPago).GreaterThan(0).WithMessage("Valor deve ser maior que zero");
            RuleFor(x => x.PeriodoFim).GreaterThan(x => x.PeriodoInicio).WithMessage("Período final deve ser maior que o inicial");
        }
    }
}
