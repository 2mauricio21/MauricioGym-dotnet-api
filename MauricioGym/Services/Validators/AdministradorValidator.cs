using FluentValidation;
using MauricioGym.Entities;

namespace MauricioGym.Services.Validators
{
    public class AdministradorValidator : AbstractValidator<AdministradorEntity>
    {
        public AdministradorValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome é obrigatório");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email inválido");
            RuleFor(x => x.SenhaHash).NotEmpty().WithMessage("Senha é obrigatória");
        }
    }
}
