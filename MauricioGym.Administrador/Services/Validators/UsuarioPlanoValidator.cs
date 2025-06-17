using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Validators
{
    public class UsuarioPlanoValidator : ValidatorService
    {        public IResultadoValidacao CriarUsuarioPlano(UsuarioPlanoEntity usuarioPlano)
        {
            if (usuarioPlano == null)
                return new ResultadoValidacao("O usuário plano não pode ser nulo.");

            if (usuarioPlano.UsuarioId <= 0)
                return new ResultadoValidacao("O ID do usuário é obrigatório.");

            if (usuarioPlano.PlanoId <= 0)
                return new ResultadoValidacao("O ID do plano é obrigatório.");

            if (usuarioPlano.DataInicio == DateTime.MinValue)
                return new ResultadoValidacao("A data de início é obrigatória.");

            if (usuarioPlano.DataFim.HasValue && usuarioPlano.DataFim <= usuarioPlano.DataInicio)
                return new ResultadoValidacao("A data de fim deve ser posterior à data de início.");

            return new ResultadoValidacao();
        }        public IResultadoValidacao AtualizarUsuarioPlano(UsuarioPlanoEntity usuarioPlano)
        {
            if (usuarioPlano == null)
                return new ResultadoValidacao("O usuário plano não pode ser nulo.");

            if (usuarioPlano.Id <= 0)
                return new ResultadoValidacao("O ID do usuário plano é obrigatório.");

            if (usuarioPlano.UsuarioId <= 0)
                return new ResultadoValidacao("O ID do usuário é obrigatório.");

            if (usuarioPlano.PlanoId <= 0)
                return new ResultadoValidacao("O ID do plano é obrigatório.");

            if (usuarioPlano.DataInicio == DateTime.MinValue)
                return new ResultadoValidacao("A data de início é obrigatória.");

            if (usuarioPlano.DataFim.HasValue && usuarioPlano.DataFim <= usuarioPlano.DataInicio)
                return new ResultadoValidacao("A data de fim deve ser posterior à data de início.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao RemoverUsuarioPlano(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("Informe o ID do usuário plano.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ObterUsuarioPlanoPorId(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("Informe o ID do usuário plano.");

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
