using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Services.Validators
{
    public class PlanoValidator : ValidatorService
    {
        public IResultadoValidacao ObterPlanoPorId(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("Informe o ID do plano.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao VerificarExistencia(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("Informe o ID do plano.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                return new ResultadoValidacao("Informe o nome do plano.");

            return new ResultadoValidacao();
        }

        //incluirplano
        public IResultadoValidacao IncluirPlano(PlanoEntity plano)
        {
            if (plano == null)
                return new ResultadoValidacao("Plano não informado.");

            if (string.IsNullOrEmpty(plano.Nome))
                return new ResultadoValidacao("Nome do plano não informado.");

            if (plano.ValorMensal <= 0)
                return new ResultadoValidacao("Valor do plano deve ser maior que zero.");

            if (plano.DuracaoMeses <= 0)
                return new ResultadoValidacao("Duração do plano deve ser maior que zero meses.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarPlano(PlanoEntity plano)
        {
            if (plano == null)
                return new ResultadoValidacao("Plano não informado.");

            if (plano.Id <= 0)
                return new ResultadoValidacao("ID do plano é inválido.");

            if (string.IsNullOrEmpty(plano.Nome))
                return new ResultadoValidacao("Nome do plano não informado.");

            if (plano.ValorMensal <= 0)
                return new ResultadoValidacao("Valor do plano deve ser maior que zero.");

            if (plano.DuracaoMeses <= 0)
                return new ResultadoValidacao("Duração do plano deve ser maior que zero meses.");

            return new ResultadoValidacao();
        }
    }
}
