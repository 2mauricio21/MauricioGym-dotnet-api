using MauricioGym.Usuario.Entities;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Usuario.Services.Validators
{
    public class UsuarioValidator : ValidatorService
    {
        public IResultadoValidacao CriarUsuario(UsuarioEntity usuario)
        {
            if (usuario == null)
                return new ResultadoValidacao("O usuário não pode ser nulo.");

            if (string.IsNullOrWhiteSpace(usuario.Nome))
                return new ResultadoValidacao("O nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(usuario.Email))
                return new ResultadoValidacao("O email é obrigatório.");

            if (usuario.Email.Length > 100)
                return new ResultadoValidacao("O email não pode ter mais de 100 caracteres.");

            if (!IsValidEmail(usuario.Email))
                return new ResultadoValidacao("O email não possui um formato válido.");

            if (string.IsNullOrWhiteSpace(usuario.Senha))
                return new ResultadoValidacao("A senha é obrigatória.");

            if (usuario.Senha.Length < 6)
                return new ResultadoValidacao("A senha deve ter pelo menos 6 caracteres.");

            if (usuario.DataNascimento > DateTime.Today)
                return new ResultadoValidacao("A data de nascimento não pode ser futura.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AtualizarUsuario(UsuarioEntity usuario)
        {
            if (usuario == null)
                return new ResultadoValidacao("O usuário não pode ser nulo.");

            if (usuario.Id <= 0)
                return new ResultadoValidacao("O ID do usuário é obrigatório.");

            if (string.IsNullOrWhiteSpace(usuario.Nome))
                return new ResultadoValidacao("O nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(usuario.Email))
                return new ResultadoValidacao("O email é obrigatório.");

            if (usuario.Email.Length > 100)
                return new ResultadoValidacao("O email não pode ter mais de 100 caracteres.");

            if (!IsValidEmail(usuario.Email))
                return new ResultadoValidacao("O email não possui um formato válido.");

            if (usuario.DataNascimento > DateTime.Today)
                return new ResultadoValidacao("A data de nascimento não pode ser futura.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarId(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("O ID do usuário deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarEmail(string email, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(email))
                return new ResultadoValidacao("O email é obrigatório.");

            if (!IsValidEmail(email))
                return new ResultadoValidacao("O email não possui um formato válido.");

            return new ResultadoValidacao();
        }

        private bool IsValidEmail(string email)
        {
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
