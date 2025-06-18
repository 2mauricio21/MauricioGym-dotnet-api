using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Validators
{
    public class AdministradorValidator : ValidatorService
    {
        public IResultadoValidacao ValidarId(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("O ID do administrador deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return new ResultadoValidacao("O e-mail do administrador é obrigatório.");

            if (!Common.IsValidEmail(email))
                return new ResultadoValidacao("O e-mail informado é inválido.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return new ResultadoValidacao("O CPF do administrador é obrigatório.");

            if (!Common.ValidarCpf(cpf))
                return new ResultadoValidacao("O CPF informado é inválido.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarAdministrador(AdministradorEntity administrador)
        {
            if (administrador == null)
                return new ResultadoValidacao("Administrador não informado.");

            if (string.IsNullOrWhiteSpace(administrador.Nome))
                return new ResultadoValidacao("O nome do administrador é obrigatório.");

            if (administrador.Nome.Length < 3)
                return new ResultadoValidacao("O nome do administrador deve ter pelo menos 3 caracteres.");

            var validacaoEmail = ValidarEmail(administrador.Email);
            if (validacaoEmail.OcorreuErro)
                return validacaoEmail;

            var validacaoCpf = ValidarCpf(administrador.Cpf);
            if (validacaoCpf.OcorreuErro)
                return validacaoCpf;

            if (string.IsNullOrWhiteSpace(administrador.Telefone))
                return new ResultadoValidacao("O telefone do administrador é obrigatório.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao IncluirAdministrador(AdministradorEntity administrador)
        {
            var validacao = ValidarAdministrador(administrador);
            if (validacao.OcorreuErro)
                return validacao;

            if (string.IsNullOrWhiteSpace(administrador.Senha))
                return new ResultadoValidacao("A senha do administrador é obrigatória.");

            if (administrador.Senha.Length < 6)
                return new ResultadoValidacao("A senha deve ter pelo menos 6 caracteres.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarAdministrador(AdministradorEntity administrador)
        {
            if (administrador == null)
                return new ResultadoValidacao("Administrador não informado.");

            if (administrador.Cpf != null && !Common.ValidarCpf(administrador.Cpf))
                return new ResultadoValidacao("O CPF informado é inválido.");

            if (administrador.Email != null && !Common.IsValidEmail(administrador.Email))
                return new ResultadoValidacao("O e-mail informado é inválido.");

            return new ResultadoValidacao();
        }
    }
}
