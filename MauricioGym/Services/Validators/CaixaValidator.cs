using FluentValidation;
using MauricioGym.Entities;

namespace MauricioGym.Services.Validators
{
    public class CaixaValidator : AbstractValidator<CaixaEntity>
    {
        public CaixaValidator()
        {
            RuleFor(x => x.QuantidadeAlunos).GreaterThanOrEqualTo(0).WithMessage("Quantidade de alunos deve ser zero ou maior");
            RuleFor(x => x.ValorTotal).GreaterThanOrEqualTo(0).WithMessage("Valor total deve ser zero ou maior");
        }
    }
}
