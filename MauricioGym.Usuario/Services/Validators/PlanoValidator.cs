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
    }
}
