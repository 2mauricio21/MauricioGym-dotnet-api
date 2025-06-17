using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Validators
{
    public class PlanoValidator : ValidatorService
    {
        public IResultadoValidacao CriarPlano(PlanoEntity plano)
        {
            if (plano == null)
                return new ResultadoValidacao("O plano não pode ser nulo.");

            if (string.IsNullOrWhiteSpace(plano.Nome))
                return new ResultadoValidacao("O nome é obrigatório.");

            if (plano.Nome.Length < 2)
                return new ResultadoValidacao("O nome deve ter pelo menos 2 caracteres.");

            if (plano.Valor <= 0)
                return new ResultadoValidacao("O valor deve ser maior que zero.");

            if (plano.DuracaoMeses <= 0)
                return new ResultadoValidacao("A duração em meses deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AtualizarPlano(PlanoEntity plano)
        {
            if (plano == null)
                return new ResultadoValidacao("O plano não pode ser nulo.");

            if (plano.Id <= 0)
                return new ResultadoValidacao("O ID do plano é obrigatório.");

            if (string.IsNullOrWhiteSpace(plano.Nome))
                return new ResultadoValidacao("O nome é obrigatório.");

            if (plano.Nome.Length < 2)
                return new ResultadoValidacao("O nome deve ter pelo menos 2 caracteres.");

            if (plano.Valor <= 0)
                return new ResultadoValidacao("O valor deve ser maior que zero.");

            if (plano.DuracaoMeses <= 0)
                return new ResultadoValidacao("A duração em meses deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao RemoverPlano(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("Informe o ID do plano.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ObterPlanoPorId(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("Informe o ID do plano.");

            return new ResultadoValidacao();
        }
    }
}
