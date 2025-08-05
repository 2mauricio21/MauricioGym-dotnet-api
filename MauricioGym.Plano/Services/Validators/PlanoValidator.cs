using System.Threading.Tasks;
using MauricioGym.Plano.Entities;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Infra.Services.Validators;

namespace MauricioGym.Plano.Services.Validators
{
    public class PlanoValidator : ValidatorService
    {
        public async Task<IResultadoValidacao> IncluirPlanoAsync(PlanoEntity plano)
        {
            if (plano == null)
                return new ResultadoValidacao("Plano não pode ser nulo");

            if (string.IsNullOrWhiteSpace(plano.Nome))
                return new ResultadoValidacao("Nome do plano é obrigatório");

            if (plano.Valor <= 0)
                return new ResultadoValidacao("Valor do plano deve ser maior que zero");

            if (plano.DuracaoEmDias <= 0)
                return new ResultadoValidacao("Duração do plano deve ser maior que zero");

            return new ResultadoValidacao();
        }

        public async Task<IResultadoValidacao> AlterarPlanoAsync(PlanoEntity plano)
        {
            if (plano == null)
                return new ResultadoValidacao("Plano não pode ser nulo");

            if (plano.IdPlano <= 0)
                return new ResultadoValidacao("ID do plano é obrigatório");

            if (string.IsNullOrWhiteSpace(plano.Nome))
                return new ResultadoValidacao("Nome do plano é obrigatório");

            if (plano.Valor <= 0)
                return new ResultadoValidacao("Valor do plano deve ser maior que zero");

            if (plano.DuracaoEmDias <= 0)
                return new ResultadoValidacao("Duração do plano deve ser maior que zero");

            return new ResultadoValidacao();
        }

        public async Task<IResultadoValidacao> ExcluirPlanoAsync(int idPlano)
        {
            if (idPlano <= 0)
                return new ResultadoValidacao("ID do plano é obrigatório");

            return new ResultadoValidacao();
        }
    }
}