using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Validators
{
    public class AcademiaValidator : ValidatorService
    {
        public IResultadoValidacao ValidarId(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("ID deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarCnpj(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return new ResultadoValidacao("CNPJ é obrigatório.");

            if (cnpj.Length != 18)
                return new ResultadoValidacao("CNPJ deve ter 18 caracteres (incluindo pontos e traços).");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                return new ResultadoValidacao("CEP é obrigatório.");

            if (cep.Length != 9)
                return new ResultadoValidacao("CEP deve ter 9 caracteres (incluindo traço).");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao IncluirAcademia(AcademiaEntity academia)
        {
            if (academia == null)
                return new ResultadoValidacao("Academia não pode ser nula.");

            if (string.IsNullOrWhiteSpace(academia.Nome))
                return new ResultadoValidacao("Nome da academia é obrigatório.");

            var validacaoCnpj = ValidarCnpj(academia.Cnpj);
            if (validacaoCnpj.OcorreuErro)
                return validacaoCnpj;

            var validacaoCep = ValidarCep(academia.Cep);
            if (validacaoCep.OcorreuErro)
                return validacaoCep;

            if (string.IsNullOrWhiteSpace(academia.Email))
                return new ResultadoValidacao("E-mail da academia é obrigatório.");

            if (string.IsNullOrWhiteSpace(academia.Telefone))
                return new ResultadoValidacao("Telefone da academia é obrigatório.");

            if (string.IsNullOrWhiteSpace(academia.Endereco))
                return new ResultadoValidacao("Endereço da academia é obrigatório.");

            if (string.IsNullOrWhiteSpace(academia.Cidade))
                return new ResultadoValidacao("Cidade da academia é obrigatória.");

            if (string.IsNullOrWhiteSpace(academia.Estado))
                return new ResultadoValidacao("Estado da academia é obrigatório.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarAcademia(AcademiaEntity academia)
        {
            if (academia == null)
                return new ResultadoValidacao("Academia não pode ser nula.");

            if (academia.Id <= 0)
                return new ResultadoValidacao("ID da academia deve ser maior que zero.");

            return IncluirAcademia(academia);
        }

        public IResultadoValidacao ExcluirAcademia(int id)
        {
            return ValidarId(id);
        }

        public IResultadoValidacao ObterPorId(int id)
        {
            return ValidarId(id);
        }

        public IResultadoValidacao ObterPorCnpj(string cnpj)
        {
            return ValidarCnpj(cnpj);
        }

        public IResultadoValidacao ValidarLicencaAcademia(int idAcademia)
        {
            return ValidarId(idAcademia);
        }

        public IResultadoValidacao ExisteAcademia(int id)
        {
            return ValidarId(id);
        }

        public IResultadoValidacao ExisteCnpj(string cnpj)
        {
            return ValidarCnpj(cnpj);
        }
    }
} 