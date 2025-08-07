using System;
using MauricioGym.Infra.Entities;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Infra.Services.Validators;

namespace MauricioGym.Infra.Services.Validators
{
    public class AuditoriaValidator : ValidatorService
    {
        public IResultadoValidacao ValidarIncluir(AuditoriaEntity auditoria)
        {
            var entityValidation = EntityIsNull(auditoria);
            if (entityValidation.OcorreuErro)
            {
                return entityValidation;
            }

            if (auditoria.IdUsuario <= 0)
            {
                return new ResultadoValidacao("ID do usuário deve ser maior que zero.");
            }

            if (string.IsNullOrWhiteSpace(auditoria.Descricao))
            {
                return new ResultadoValidacao("Descrição da auditoria é obrigatória.");
            }
            
            if (auditoria.Descricao.Length > 500)
            {
                return new ResultadoValidacao("Descrição da auditoria não pode exceder 500 caracteres.");
            }

            if (auditoria.Data == default(DateTime))
            {
                return new ResultadoValidacao("Data da auditoria é obrigatória.");
            }

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarConsultar(int idAuditoria)
        {
            var idValidation = ValidarId(idAuditoria);
            if (idValidation.OcorreuErro)
            {
                return idValidation;
            }

            return new ResultadoValidacao();
        }
    }
}