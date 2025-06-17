using FluentValidation;
using MauricioGym.Entities;

namespace MauricioGym.Services.Validators
{
    public class CheckInValidator : AbstractValidator<CheckInEntity>
    {
        public CheckInValidator()
        {
            RuleFor(x => x.PessoaId).GreaterThan(0).WithMessage("PessoaId obrigatório");
        }
    }
}
