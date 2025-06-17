using FluentValidation;
using MauricioGym.Entities;

namespace MauricioGym.Services.Validators
{
    public class PessoaValidator : AbstractValidator<PessoaEntity>
    {
        public PessoaValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome é obrigatório");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email inválido");
            RuleFor(x => x.DataNascimento).LessThan(System.DateTime.Now).WithMessage("Data de nascimento inválida");
        }
    }
}
