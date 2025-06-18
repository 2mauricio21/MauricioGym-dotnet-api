using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Services.Validators
{
    public class VinculoClienteAcademiaValidator : ValidatorService
    {
        public IResultadoValidacao IncluirVinculoClienteAcademia(VinculoClienteAcademiaEntity vinculo)
        {
            if (vinculo == null)
                return new ResultadoValidacao("Vínculo não informado.");

            if (vinculo.ClienteId <= 0)
                return new ResultadoValidacao("Cliente não informado.");

            if (vinculo.AcademiaId <= 0)
                return new ResultadoValidacao("Academia não informada.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarVinculoClienteAcademia(VinculoClienteAcademiaEntity vinculo)
        {
            if (vinculo == null)
                return new ResultadoValidacao("Vínculo não informado.");

            if (vinculo.Id <= 0)
                return new ResultadoValidacao("ID do vínculo é inválido.");

            if (vinculo.ClienteId <= 0)
                return new ResultadoValidacao("Cliente não informado.");

            if (vinculo.AcademiaId <= 0)
                return new ResultadoValidacao("Academia não informada.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarVinculoClienteAcademia(int idVinculo)
        {
            if (idVinculo <= 0)
                return new ResultadoValidacao("O ID do vínculo deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ExcluirVinculoClienteAcademia(int idVinculo)
        {
            if (idVinculo <= 0)
                return new ResultadoValidacao("O ID do vínculo deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarVinculosPorAcademia(int academiaId)
        {
            if (academiaId <= 0)
                return new ResultadoValidacao("O ID da academia deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarVinculosPorCliente(int clienteId)
        {
            if (clienteId <= 0)
                return new ResultadoValidacao("O ID do cliente deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao DesativarVinculo(int idVinculo)
        {
            if (idVinculo <= 0)
                return new ResultadoValidacao("O ID do vínculo deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao VerificarVinculoAtivoPorCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return new ResultadoValidacao("CPF não informado.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ContarClientesAtivos(int academiaId)
        {
            if (academiaId <= 0)
                return new ResultadoValidacao("O ID da academia deve ser maior que zero.");

            return new ResultadoValidacao();
        }
    }
} 