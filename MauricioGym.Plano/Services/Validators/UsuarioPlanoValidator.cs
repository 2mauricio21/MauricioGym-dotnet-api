using System.Threading.Tasks;
using MauricioGym.Plano.Entities;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Infra.Services.Validators;

namespace MauricioGym.Plano.Services.Validators
{
    public class UsuarioPlanoValidator : ValidatorService
    {
        public async Task<IResultadoValidacao> IncluirUsuarioPlanoAsync(UsuarioPlanoEntity usuarioPlano)
        {
            if (usuarioPlano == null)
                return new ResultadoValidacao("[UsuarioPlanoValidator]-Entidade usuário-plano não pode ser nula.");

            if (usuarioPlano.IdUsuario <= 0)
                return new ResultadoValidacao("[UsuarioPlanoValidator]-ID do usuário é obrigatório.");

            if (usuarioPlano.IdPlano <= 0)
                return new ResultadoValidacao("[UsuarioPlanoValidator]-ID do plano é obrigatório.");

            if (usuarioPlano.DataInicio == default)
                return new ResultadoValidacao("[UsuarioPlanoValidator]-Data de início é obrigatória.");

            if (usuarioPlano.DataInicio > usuarioPlano.DataFim)
                return new ResultadoValidacao("[UsuarioPlanoValidator]-Data de início deve ser anterior à data de fim.");

            return new ResultadoValidacao();
        }

        public async Task<IResultadoValidacao> AlterarUsuarioPlanoAsync(UsuarioPlanoEntity usuarioPlano)
        {
            if (usuarioPlano == null)
                return new ResultadoValidacao("[UsuarioPlanoValidator]-Entidade usuário-plano não pode ser nula.");

            if (usuarioPlano.IdUsuarioPlano <= 0)
                return new ResultadoValidacao("[UsuarioPlanoValidator]-ID do usuário plano é obrigatório.");

            if (usuarioPlano.IdUsuario <= 0)
                return new ResultadoValidacao("[UsuarioPlanoValidator]-ID do usuário é obrigatório.");

            if (usuarioPlano.IdPlano <= 0)
                return new ResultadoValidacao("[UsuarioPlanoValidator]-ID do plano é obrigatório.");

            if (usuarioPlano.DataInicio == default)
                return new ResultadoValidacao("[UsuarioPlanoValidator]-Data de início é obrigatória.");

            if (usuarioPlano.DataInicio > usuarioPlano.DataFim)
                return new ResultadoValidacao("[UsuarioPlanoValidator]-Data de início deve ser anterior à data de fim.");

            return new ResultadoValidacao();
        }

        public async Task<IResultadoValidacao> CancelarUsuarioPlanoAsync(int idUsuarioPlano)
        {
            if (idUsuarioPlano <= 0)
                return new ResultadoValidacao("[UsuarioPlanoValidator]-ID do usuário plano é obrigatório.");

            return new ResultadoValidacao();
        }
    }
}