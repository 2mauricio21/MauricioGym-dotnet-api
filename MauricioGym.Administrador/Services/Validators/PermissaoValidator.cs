using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Validators
{
    public class PermissaoValidator : ValidatorService
    {
        public IResultadoValidacao ValidarId(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("ID deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao Validar(PermissaoEntity permissao)
        {
            if (permissao == null)
                return new ResultadoValidacao("Permissão não pode ser nula.");

            if (string.IsNullOrWhiteSpace(permissao.Nome))
                return new ResultadoValidacao("Nome da permissão é obrigatório.");

            if (string.IsNullOrWhiteSpace(permissao.Descricao))
                return new ResultadoValidacao("Descrição da permissão é obrigatória.");

            if (string.IsNullOrWhiteSpace(permissao.Recurso))
                return new ResultadoValidacao("Recurso da permissão é obrigatório.");

            if (string.IsNullOrWhiteSpace(permissao.Acao))
                return new ResultadoValidacao("Ação da permissão é obrigatória.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarAtualizacao(PermissaoEntity permissao)
        {
            if (permissao == null)
                return new ResultadoValidacao("Permissão não pode ser nula.");

            if (permissao.Id <= 0)
                return new ResultadoValidacao("ID da permissão deve ser maior que zero.");

            return Validar(permissao);
        }

        public IResultadoValidacao ValidarTipo(string tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo))
                return new ResultadoValidacao("Tipo da permissão é obrigatório.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao IncluirPermissao(PermissaoEntity permissao)
        {
            if (permissao == null)
                return new ResultadoValidacao("Permissão não pode ser nula.");

            if (string.IsNullOrWhiteSpace(permissao.Nome))
                return new ResultadoValidacao("Nome da permissão é obrigatório.");

            if (permissao.Nome.Length > 100)
                return new ResultadoValidacao("Nome da permissão deve ter no máximo 100 caracteres.");

            if (permissao.Recurso == null)
                return new ResultadoValidacao("Recurso da permissão é obrigatório.");

            if (string.IsNullOrWhiteSpace(permissao.Recurso))
                return new ResultadoValidacao("Recurso da permissão é obrigatório.");

            if (permissao.Recurso.Length > 100)
                return new ResultadoValidacao("Recurso da permissão deve ter no máximo 100 caracteres.");

            if (string.IsNullOrWhiteSpace(permissao.Acao))
                return new ResultadoValidacao("Ação da permissão é obrigatória.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarPermissao(PermissaoEntity permissao)
        {
            var validacaoId = ValidarId(permissao?.Id ?? 0);
            if (validacaoId.OcorreuErro)
                return validacaoId;

            return IncluirPermissao(permissao);
        }

        public IResultadoValidacao ExcluirPermissao(int id)
        {
            return ValidarId(id);
        }

        public IResultadoValidacao ObterPorId(int id)
        {
            return ValidarId(id);
        }

        public IResultadoValidacao ObterPorNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return new ResultadoValidacao("Nome da permissão é obrigatório.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ObterPorPapelId(int papelId)
        {
            return ValidarId(papelId);
        }

        public IResultadoValidacao ObterPorRecurso(string recurso)
        {
            if (string.IsNullOrWhiteSpace(recurso))
                return new ResultadoValidacao("Nome da permissão é obrigatório.");

            return new ResultadoValidacao();
        }
    }
}
