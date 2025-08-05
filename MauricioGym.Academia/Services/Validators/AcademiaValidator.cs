using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Academia.Entities;

namespace MauricioGym.Academia.Services.Validators
{
    public class AcademiaValidator : ValidatorService
    {
        public IResultadoValidacao IncluirAcademiaAsync(AcademiaEntity academia)
        {
            if (academia == null)
                return new ResultadoValidacao("A academia não pode ser nula.");

            if (string.IsNullOrWhiteSpace(academia.Nome))
                return new ResultadoValidacao("O nome da academia é obrigatório.");

            if (string.IsNullOrWhiteSpace(academia.CNPJ))
                return new ResultadoValidacao("O CNPJ da academia é obrigatório.");

            if (string.IsNullOrWhiteSpace(academia.Telefone))
                return new ResultadoValidacao("O telefone da academia é obrigatório.");

            if (string.IsNullOrWhiteSpace(academia.Email))
                return new ResultadoValidacao("O email da academia é obrigatório.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarAcademiaAsync(AcademiaEntity academia)
        {
            if (academia == null)
                return new ResultadoValidacao("A academia não pode ser nula.");

            if (academia.IdAcademia <= 0)
                return new ResultadoValidacao("O ID da academia deve ser maior que zero.");

            if (string.IsNullOrWhiteSpace(academia.Nome))
                return new ResultadoValidacao("O nome da academia é obrigatório.");

            if (string.IsNullOrWhiteSpace(academia.CNPJ))
                return new ResultadoValidacao("O CNPJ da academia é obrigatório.");

            if (string.IsNullOrWhiteSpace(academia.Telefone))
                return new ResultadoValidacao("O telefone da academia é obrigatório.");

            if (string.IsNullOrWhiteSpace(academia.Email))
                return new ResultadoValidacao("O email da academia é obrigatório.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarAcademiaAsync(int idAcademia)
        {
            if (idAcademia <= 0)
                return new ResultadoValidacao("O ID da academia deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ExcluirAcademiaAsync(int idAcademia)
        {
            if (idAcademia <= 0)
                return new ResultadoValidacao("O ID da academia deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarAcademiaPorCNPJAsync(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return new ResultadoValidacao("O CNPJ da academia é obrigatório.");

            return new ResultadoValidacao();
        }
    }
}