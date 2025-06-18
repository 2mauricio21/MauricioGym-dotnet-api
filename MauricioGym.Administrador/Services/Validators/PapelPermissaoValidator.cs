using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Validators
{
    public class PapelPermissaoValidator : ValidatorService
    {
        public IResultadoValidacao ObterPorId(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("ID deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ObterPorPapelPermissao(int papelId, int permissaoId)
        {
            if (papelId <= 0)
                return new ResultadoValidacao("ID do papel deve ser maior que zero.");

            if (permissaoId <= 0)
                return new ResultadoValidacao("ID da permissão deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ListarPorPapel(int papelId)
        {
            if (papelId <= 0)
                return new ResultadoValidacao("ID do papel deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ListarPorPermissao(int permissaoId)
        {
            if (permissaoId <= 0)
                return new ResultadoValidacao("ID da permissão deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao IncluirPapelPermissao(PapelPermissaoEntity papelPermissao)
        {
            if (papelPermissao == null)
                return new ResultadoValidacao("Associação papel-permissão não pode ser nula.");

            if (papelPermissao.IdPapel <= 0)
                return new ResultadoValidacao("ID do papel deve ser maior que zero.");

            if (papelPermissao.IdPermissao <= 0)
                return new ResultadoValidacao("ID da permissão deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao RemoverPapelPermissao(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("ID deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao RemoverPorPapelPermissao(int papelId, int permissaoId)
        {
            if (papelId <= 0)
                return new ResultadoValidacao("ID do papel deve ser maior que zero.");

            if (permissaoId <= 0)
                return new ResultadoValidacao("ID da permissão deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AtribuirPermissaoAoPapel(int papelId, int permissaoId)
        {
            if (papelId <= 0)
                return new ResultadoValidacao("ID do papel deve ser maior que zero.");

            if (permissaoId <= 0)
                return new ResultadoValidacao("ID da permissão deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao RemoverPermissaoDoPapel(int papelId, int permissaoId)
        {
            if (papelId <= 0)
                return new ResultadoValidacao("ID do papel deve ser maior que zero.");

            if (permissaoId <= 0)
                return new ResultadoValidacao("ID da permissão deve ser maior que zero.");

            return new ResultadoValidacao();
        }
    }
} 