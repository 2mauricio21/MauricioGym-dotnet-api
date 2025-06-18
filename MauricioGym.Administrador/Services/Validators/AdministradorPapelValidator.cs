using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Validators
{
    public class AdministradorPapelValidator : ValidatorService
    {
        public IResultadoValidacao ValidarInclusao(AdministradorPapelEntity administradorPapel)
        {

            if (administradorPapel.PapelId <= 0)
                return new ResultadoValidacao("O ID do papel deve ser maior que zero.");

            if (administradorPapel.AdministradorId <= 0)
                return new ResultadoValidacao("O ID do administrador deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarExclusao(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("O ID do administrador deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarRemocao(int administradorId, int papelId)
        {
            if (papelId <= 0)
                return new ResultadoValidacao("O ID do papel deve ser maior que zero.");

            if (administradorId <= 0)
                return new ResultadoValidacao("O ID do administrador deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarVerificacao(int administradorId, int papelId)
        {
            if (papelId <= 0)
                return new ResultadoValidacao("O ID do papel deve ser maior que zero.");

            if (administradorId <= 0)
                return new ResultadoValidacao("O ID do administrador deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarExistencia(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("O ID do administrador deve ser maior que zero.");

            return new ResultadoValidacao();
        }
        // Métodos para compatibilidade com padrão Juris
        public IResultadoValidacao IncluirAdministradorPapel(AdministradorPapelEntity administradorPapel)
        {
            return ValidarInclusao(administradorPapel);
        }

        public IResultadoValidacao ExcluirAdministradorPapel(int id)
        {
            return ValidarExclusao(id);
        }

        public IResultadoValidacao RemoverPapelDoAdministrador(int administradorId, int papelId)
        {
            return ValidarRemocao(administradorId, papelId);
        }

        public IResultadoValidacao AdministradorPossuiPapel(int administradorId, int papelId)
        {
            return ValidarVerificacao(administradorId, papelId);
        }

        public IResultadoValidacao Existe(int id)
        {
            return ValidarExistencia(id);
        }

        public IResultadoValidacao ObterPorId(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("O ID do administrador deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ObterPorAdministradorEPapel(int administradorId, int papelId)
        {
            if (papelId <= 0)
                return new ResultadoValidacao("O ID do papel deve ser maior que zero.");

            if (administradorId <= 0)
                return new ResultadoValidacao("O ID do administrador deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ObterPorAdministradorId(int administradorId)
        {
            if (administradorId <= 0)
                return new ResultadoValidacao("O ID do administrador deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ObterPorPapelId(int papelId)
        {
            if (papelId <= 0)
                return new ResultadoValidacao("O ID do papel deve ser maior que zero.");
            return new ResultadoValidacao();
        }
    }
}
