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
                return new ResultadoValidacao("Plano n�o informado.");

            if (string.IsNullOrEmpty(plano.Nome))
                return new ResultadoValidacao("Nome do plano n�o informado.");

            if (plano.ValorMensal <= 0)
                return new ResultadoValidacao("Valor do plano deve ser maior que zero.");

            if (plano.DuracaoMeses <= 0)
                return new ResultadoValidacao("Dura��o do plano deve ser maior que zero meses.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarPlano(PlanoEntity plano)
        {
            if (plano == null)
                return new ResultadoValidacao("Plano n�o informado.");

            if (plano.Id <= 0)
                return new ResultadoValidacao("ID do plano � inv�lido.");

            if (string.IsNullOrEmpty(plano.Nome))
                return new ResultadoValidacao("Nome do plano n�o informado.");

            if (plano.ValorMensal <= 0)
                return new ResultadoValidacao("Valor do plano deve ser maior que zero.");

            if (plano.DuracaoMeses <= 0)
                return new ResultadoValidacao("Dura��o do plano deve ser maior que zero meses.");

            return new ResultadoValidacao();
        }
    }
}
