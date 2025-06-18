using MauricioGym.Infra.Entities.Interfaces;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Infra.Services.Validators
{
    public abstract class ValidatorService : IValidatorService
    {
        protected IResultadoValidacao EntityIsNull<T>(T entity) where T : IEntity
        {
            try
            {
                if (entity == null)
                    return new ResultadoValidacao($"A entidade {nameof(T)} não pode ser nula.");

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[ValidatorService] - Não foi possível Validar Entidade");
            }
        }

        public IResultadoValidacao ValidarId(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("O ID Inválido.");

            return new ResultadoValidacao();
        }
    }
}
