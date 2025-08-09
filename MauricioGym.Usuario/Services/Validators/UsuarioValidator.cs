using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Usuario.Entities;

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

            if (string.IsNullOrWhiteSpace(usuario.CPF))
                return new ResultadoValidacao("O CPF é obrigatório.");

            if (!string.IsNullOrEmpty(usuario.Email) && !IsValidEmail(usuario.Email))
                return new ResultadoValidacao("Email inválido.");

            if (!string.IsNullOrEmpty(usuario.CPF) && !IsValidCPF(usuario.CPF))
                return new ResultadoValidacao("CPF inválido.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AtualizarUsuario(UsuarioEntity usuario)
        {
            if (usuario == null)
                return new ResultadoValidacao("O usuário não pode ser nulo.");

            if (usuario.IdUsuario <= 0)
                return new ResultadoValidacao("ID do usuário inválido.");

            return CriarUsuario(usuario);
        }

        public IResultadoValidacao ConsultarUsuario(int idUsuario)
        {
            return ValidarId(idUsuario);
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

        private bool IsValidCPF(string cpf)
        {
            // Remove caracteres especiais
            cpf = cpf.Replace(".", "").Replace("-", "");
            
            // Verifica se tem 11 dígitos
            if (cpf.Length != 11)
                return false;

            // Verifica se todos os dígitos são iguais
            bool todosIguais = true;
            for (int i = 1; i < 11; i++)
            {
                if (cpf[i] != cpf[0])
                {
                    todosIguais = false;
                    break;
                }
            }
            if (todosIguais)
                return false;

            // Calcula o primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * (10 - i);
            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            // Calcula o segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * (11 - i);
            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith(digito1.ToString() + digito2.ToString());
        }
    }
}