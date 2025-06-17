using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Validators
{
    public class PermissaoManipulacaoUsuarioValidator : ValidatorService
    {
        public IResultadoValidacao CriarPermissao(PermissaoManipulacaoUsuarioEntity permissao)
        {
            if (permissao == null)
                return new ResultadoValidacao("A permissão não pode ser nula.");

            if (permissao.UsuarioId <= 0)
                return new ResultadoValidacao("O ID do usuário é obrigatório.");

            if (string.IsNullOrWhiteSpace(permissao.TipoPermissao))
                return new ResultadoValidacao("O tipo de permissão é obrigatório.");

            if (permissao.DataConcessao == DateTime.MinValue)
                return new ResultadoValidacao("A data de concessão é obrigatória.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AtualizarPermissao(PermissaoManipulacaoUsuarioEntity permissao)
        {
            if (permissao == null)
                return new ResultadoValidacao("A permissão não pode ser nula.");

            if (permissao.Id <= 0)
                return new ResultadoValidacao("O ID da permissão é obrigatório.");

            if (permissao.UsuarioId <= 0)
                return new ResultadoValidacao("O ID do usuário é obrigatório.");

            if (string.IsNullOrWhiteSpace(permissao.TipoPermissao))
                return new ResultadoValidacao("O tipo de permissão é obrigatório.");

            if (permissao.DataConcessao == DateTime.MinValue)
                return new ResultadoValidacao("A data de concessão é obrigatória.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao RemoverPermissao(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("Informe o ID da permissão.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ObterPermissaoPorId(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("Informe o ID da permissão.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ListarPorUsuario(int usuarioId)
        {
            if (usuarioId <= 0)
                return new ResultadoValidacao("Informe o ID do usuário.");

            return new ResultadoValidacao();
        }
    }
}
