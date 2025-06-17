using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Validators
{
    public class AdministradorValidator : ValidatorService
    {
        public IResultadoValidacao CriarAdministrador(AdministradorEntity administrador)
        {
            if (administrador == null)
                return new ResultadoValidacao("O administrador não pode ser nulo.");

            if (string.IsNullOrWhiteSpace(administrador.Nome))
                return new ResultadoValidacao("O nome é obrigatório.");

            if (administrador.Nome.Length < 2)
                return new ResultadoValidacao("O nome deve ter pelo menos 2 caracteres.");

            if (string.IsNullOrWhiteSpace(administrador.Email))
                return new ResultadoValidacao("O email é obrigatório.");

            if (!IsValidEmail(administrador.Email))
                return new ResultadoValidacao("O email informado é inválido.");

            if (string.IsNullOrWhiteSpace(administrador.Senha))
                return new ResultadoValidacao("A senha é obrigatória.");

            if (administrador.Senha.Length < 6)
                return new ResultadoValidacao("A senha deve ter pelo menos 6 caracteres.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AtualizarAdministrador(AdministradorEntity administrador)
        {
            if (administrador == null)
                return new ResultadoValidacao("O administrador não pode ser nulo.");

            if (administrador.Id <= 0)
                return new ResultadoValidacao("O ID do administrador é obrigatório.");

            if (string.IsNullOrWhiteSpace(administrador.Nome))
                return new ResultadoValidacao("O nome é obrigatório.");

            if (administrador.Nome.Length < 2)
                return new ResultadoValidacao("O nome deve ter pelo menos 2 caracteres.");

            if (string.IsNullOrWhiteSpace(administrador.Email))
                return new ResultadoValidacao("O email é obrigatório.");

            if (!IsValidEmail(administrador.Email))
                return new ResultadoValidacao("O email informado é inválido.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao RemoverAdministrador(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("Informe o ID do administrador.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ObterAdministradorPorId(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("Informe o ID do administrador.");

            return new ResultadoValidacao();
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
