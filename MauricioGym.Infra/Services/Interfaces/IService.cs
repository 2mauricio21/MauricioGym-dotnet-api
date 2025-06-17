namespace MauricioGym.Infra.Services.Interfaces
{
    public interface IService<TValidator>
    {
        TValidator Validator { get; }
    }
}
