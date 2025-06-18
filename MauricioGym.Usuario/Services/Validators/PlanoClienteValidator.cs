using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Services.Validators
{
    public class PlanoClienteValidator : ValidatorService
    {
        public IResultadoValidacao IncluirPlanoCliente(PlanoClienteEntity planoCliente)
        {
            if (planoCliente == null)
                return new ResultadoValidacao("Plano do cliente não informado.");

            if (planoCliente.ClienteId <= 0)
                return new ResultadoValidacao("Cliente não informado.");

            if (planoCliente.PlanoId <= 0)
                return new ResultadoValidacao("Plano não informado.");

            if (planoCliente.Valor <= 0)
                return new ResultadoValidacao("Valor deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarPlanoCliente(PlanoClienteEntity planoCliente)
        {
            if (planoCliente == null)
                return new ResultadoValidacao("Plano do cliente não informado.");

            if (planoCliente.Id <= 0)
                return new ResultadoValidacao("ID do plano do cliente é inválido.");

            if (planoCliente.ClienteId <= 0)
                return new ResultadoValidacao("Cliente não informado.");

            if (planoCliente.PlanoId <= 0)
                return new ResultadoValidacao("Plano não informado.");

            if (planoCliente.Valor <= 0)
                return new ResultadoValidacao("Valor deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarPlanoCliente(int idPlanoCliente)
        {
            if (idPlanoCliente <= 0)
                return new ResultadoValidacao("O ID do plano do cliente deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ExcluirPlanoCliente(int idPlanoCliente)
        {
            if (idPlanoCliente <= 0)
                return new ResultadoValidacao("O ID do plano do cliente deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarPlanoClientePorCliente(int clienteId)
        {
            if (clienteId <= 0)
                return new ResultadoValidacao("O ID do cliente deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarPlanoAtivo(int clienteId)
        {
            if (clienteId <= 0)
                return new ResultadoValidacao("O ID do cliente deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao RenovarPlano(int idPlanoCliente)
        {
            if (idPlanoCliente <= 0)
                return new ResultadoValidacao("O ID do plano do cliente deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao CancelarPlano(int idPlanoCliente)
        {
            if (idPlanoCliente <= 0)
                return new ResultadoValidacao("O ID do plano do cliente deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarPlanosVencendo()
        {
            return new ResultadoValidacao();
        }
    }
} 