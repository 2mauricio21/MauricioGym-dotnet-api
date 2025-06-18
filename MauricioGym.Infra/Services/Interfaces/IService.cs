using MauricioGym.Infra.Services.Validators;

namespace MauricioGym.Infra.Services.Interfaces
{
    public interface IService<out TValidator> : IDisposable where TValidator : IValidatorService
    {
        TValidator Validator { get; }
    }
}
