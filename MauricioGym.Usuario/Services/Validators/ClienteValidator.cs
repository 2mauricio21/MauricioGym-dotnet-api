using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Services.Validators
{
    public class ClienteValidator : ValidatorService
    {
        public IResultadoValidacao IncluirCliente(ClienteEntity cliente)
        {
            if (cliente == null)
                return new ResultadoValidacao("Cliente não informado.");

            if (string.IsNullOrEmpty(cliente.Nome))
                return new ResultadoValidacao("Nome não informado.");

            if (string.IsNullOrEmpty(cliente.Email))
                return new ResultadoValidacao("Email não informado.");

            if (string.IsNullOrEmpty(cliente.Cpf))
                return new ResultadoValidacao("CPF não informado.");

            if (string.IsNullOrEmpty(cliente.Telefone))
                return new ResultadoValidacao("Telefone não informado.");

            if (cliente.DataNascimento == DateTime.MinValue)
                return new ResultadoValidacao("Data de nascimento não informada.");

            if (cliente.DataNascimento > DateTime.Today)
                return new ResultadoValidacao("Data de nascimento não pode ser maior que a data atual.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarCliente(ClienteEntity cliente)
        {
            if (cliente == null)
                return new ResultadoValidacao("Cliente não informado.");

            if (cliente.Id <= 0)
                return new ResultadoValidacao("ID do cliente é inválido.");

            if (string.IsNullOrEmpty(cliente.Nome))
                return new ResultadoValidacao("Nome não informado.");

            if (string.IsNullOrEmpty(cliente.Email))
                return new ResultadoValidacao("Email não informado.");

            if (string.IsNullOrEmpty(cliente.Cpf))
                return new ResultadoValidacao("CPF não informado.");

            if (string.IsNullOrEmpty(cliente.Telefone))
                return new ResultadoValidacao("Telefone não informado.");

            if (cliente.DataNascimento == DateTime.MinValue)
                return new ResultadoValidacao("Data de nascimento não informada.");

            if (cliente.DataNascimento > DateTime.Today)
                return new ResultadoValidacao("Data de nascimento não pode ser maior que a data atual.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarCliente(int idCliente)
        {
            if (idCliente <= 0)
                return new ResultadoValidacao("O ID do cliente deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ExcluirCliente(int idCliente)
        {
            if (idCliente <= 0)
                return new ResultadoValidacao("O ID do cliente deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarClientePorCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return new ResultadoValidacao("CPF não informado.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarClientePorEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return new ResultadoValidacao("Email não informado.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return new ResultadoValidacao("CPF não informado.");

            if (!Common.ValidarCpf(cpf))
                return new ResultadoValidacao("CPF inválido.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return new ResultadoValidacao("Email não informado.");

            if (!Common.IsValidEmail(email))
                return new ResultadoValidacao("Email inválido.");

            return new ResultadoValidacao();
        }
    }
}