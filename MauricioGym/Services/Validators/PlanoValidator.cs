using FluentValidation;
using MauricioGym.Entities;

namespace MauricioGym.Services.Validators
{
    public class PlanoValidator : AbstractValidator<PlanoEntity>
    {
        public PlanoValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome é obrigatório");
            RuleFor(x => x.Valor).GreaterThan(0).WithMessage("Valor deve ser maior que zero");
            RuleFor(x => x.DuracaoMeses).GreaterThan(0).WithMessage("Duração deve ser maior que zero");
        }
    }
}
