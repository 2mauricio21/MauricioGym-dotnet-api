using MauricioGym.Infra.Entities;
using MauricioGym.Infra.Entities.Interfaces;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Infra.Services.Validators
{
  public class AuditoriaValidator : ValidatorService
  {
    public IResultadoValidacao IncluirAuditoria(AuditoriaEntity auditoria)
    {
      if (auditoria == null)
        return new ResultadoValidacao("A auditoria não pode ser nula.");

      if (string.IsNullOrEmpty(auditoria.Descricao))
        return new ResultadoValidacao("A descrição da auditoria é obrigatória.");

      return new ResultadoValidacao();
    }

    // COnsultarAuditoriaPorId
    public IResultadoValidacao ConsultarAuditoriaPorId(AuditoriaEntity auditoria)
    {
      if (auditoria == null)
        return new ResultadoValidacao("A auditoria não pode ser nula.");

      if (auditoria.IdAuditoria == 0)
        return new ResultadoValidacao("O id da auditoria é obrigatório.");

      return new ResultadoValidacao();
    }
  }

}
