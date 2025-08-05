using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MauricioGym.Infra.Services
{
  public abstract class ServiceBase<TValidator> : IDisposable, IService<TValidator> where TValidator : IValidatorService, new()
  {
    #region [ Propriedades ]
    public virtual TValidator Validator => new TValidator();
    private readonly ServiceProvider serviceProvider;

    #endregion

    #region [ Construtores ]
    public ServiceBase()
    {
      serviceProvider = new ServiceCollection()
          .ConfigureServicesInfra()
          .BuildServiceProvider();
    }
    #endregion

    #region [ Métodos Públicos ]

    public void Dispose()
    {
    }

  #endregion
  }
}
