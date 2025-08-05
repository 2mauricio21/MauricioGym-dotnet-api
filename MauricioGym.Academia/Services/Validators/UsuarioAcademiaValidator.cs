using MauricioGym.Academia.Entities;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Academia.Services.Validators
{
    public class UsuarioAcademiaValidator : ValidatorService
    {
        public IResultadoValidacao IncluirUsuarioAcademiaAsync(UsuarioAcademiaEntity usuarioAcademia)
        {
            if (usuarioAcademia == null)
                return new ResultadoValidacao("Os dados do vínculo usuário-academia são obrigatórios.");

            if (usuarioAcademia.IdUsuario <= 0)
                return new ResultadoValidacao("O ID do usuário deve ser maior que zero.");

            if (usuarioAcademia.IdAcademia <= 0)
                return new ResultadoValidacao("O ID da academia deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarUsuarioAcademiaAsync(UsuarioAcademiaEntity usuarioAcademia)
        {
            if (usuarioAcademia == null)
                return new ResultadoValidacao("Os dados do vínculo usuário-academia são obrigatórios.");

            if (usuarioAcademia.IdUsuarioAcademia <= 0)
                return new ResultadoValidacao("O ID do vínculo usuário-academia deve ser maior que zero.");

            if (usuarioAcademia.IdUsuario <= 0)
                return new ResultadoValidacao("O ID do usuário deve ser maior que zero.");

            if (usuarioAcademia.IdAcademia <= 0)
                return new ResultadoValidacao("O ID da academia deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarUsuarioAcademiaAsync(int idUsuarioAcademia)
        {
            if (idUsuarioAcademia <= 0)
                return new ResultadoValidacao("O ID do vínculo usuário-academia deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ExcluirUsuarioAcademiaAsync(int idUsuarioAcademia)
        {
            if (idUsuarioAcademia <= 0)
                return new ResultadoValidacao("O ID do vínculo usuário-academia deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ListarUsuarioAcademiaPorUsuarioAsync(int idUsuario)
        {
            if (idUsuario <= 0)
                return new ResultadoValidacao("O ID do usuário deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ListarUsuarioAcademiaPorAcademiaAsync(int idAcademia)
        {
            if (idAcademia <= 0)
                return new ResultadoValidacao("O ID da academia deve ser maior que zero.");

            return new ResultadoValidacao();
        }
    }
}