using MauricioGym.Acesso.Entities;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Acesso.Services.Validators
{
    public class BloqueioAcessoValidator : ValidatorService
    {
        public IResultadoValidacao IncluirBloqueioAcessoAsync(BloqueioAcessoEntity bloqueioAcesso)
        {
            if (bloqueioAcesso == null)
                return new ResultadoValidacao("O bloqueio de acesso não pode ser nulo");

            if (bloqueioAcesso.IdUsuario <= 0)
                return new ResultadoValidacao("O ID do usuário é obrigatório");

            if (bloqueioAcesso.IdAcademia <= 0)
                return new ResultadoValidacao("O ID da academia é obrigatório");

            if (string.IsNullOrWhiteSpace(bloqueioAcesso.MotivoBloqueio))
                return new ResultadoValidacao("O motivo do bloqueio é obrigatório");

            if (bloqueioAcesso.MotivoBloqueio.Length > 500)
                return new ResultadoValidacao("O motivo do bloqueio deve ter no máximo 500 caracteres");

            if (bloqueioAcesso.DataInicioBloqueio > DateTime.Now)
                return new ResultadoValidacao("A data de início não pode ser futura");

            if (bloqueioAcesso.DataFimBloqueio.HasValue && bloqueioAcesso.DataFimBloqueio < bloqueioAcesso.DataInicioBloqueio)
                return new ResultadoValidacao("A data de fim deve ser posterior à data de início");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarBloqueioAcessoAsync(BloqueioAcessoEntity bloqueioAcesso)
        {
            if (bloqueioAcesso == null)
                return new ResultadoValidacao("O bloqueio de acesso não pode ser nulo");

            if (bloqueioAcesso.IdBloqueioAcesso <= 0)
                return new ResultadoValidacao("O ID do bloqueio deve ser maior que zero");

            if (bloqueioAcesso.IdUsuario <= 0)
                return new ResultadoValidacao("O ID do usuário é obrigatório");

            if (bloqueioAcesso.IdAcademia <= 0)
                return new ResultadoValidacao("O ID da academia é obrigatório");

            if (string.IsNullOrWhiteSpace(bloqueioAcesso.MotivoBloqueio))
                return new ResultadoValidacao("O motivo do bloqueio é obrigatório");

            if (bloqueioAcesso.MotivoBloqueio.Length > 500)
                return new ResultadoValidacao("O motivo do bloqueio deve ter no máximo 500 caracteres");

            if (bloqueioAcesso.DataInicioBloqueio > DateTime.Now)
                return new ResultadoValidacao("A data de início não pode ser futura");

            if (bloqueioAcesso.DataFimBloqueio.HasValue && bloqueioAcesso.DataFimBloqueio < bloqueioAcesso.DataInicioBloqueio)
                return new ResultadoValidacao("A data de fim deve ser posterior à data de início");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarBloqueioAcessoAsync(int idBloqueioAcesso)
        {
            if (idBloqueioAcesso <= 0)
                return new ResultadoValidacao("O ID do bloqueio deve ser maior que zero");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarBloqueiosPorUsuarioAsync(int idUsuario)
        {
            if (idUsuario <= 0)
                return new ResultadoValidacao("O ID do usuário deve ser maior que zero");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarBloqueiosPorUsuarioAcademiaAsync(int idUsuario, int idAcademia)
        {
            if (idUsuario <= 0)
                return new ResultadoValidacao("O ID do usuário deve ser maior que zero");

            if (idAcademia <= 0)
                return new ResultadoValidacao("O ID da academia deve ser maior que zero");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarBloqueioAtivoPorUsuarioAcademiaAsync(int idUsuario, int idAcademia)
        {
            if (idUsuario <= 0)
                return new ResultadoValidacao("O ID do usuário deve ser maior que zero");

            if (idAcademia <= 0)
                return new ResultadoValidacao("O ID da academia deve ser maior que zero");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao CancelarBloqueioAcessoAsync(int idBloqueioAcesso)
        {
            if (idBloqueioAcesso <= 0)
                return new ResultadoValidacao("O ID do bloqueio deve ser maior que zero");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ListarBloqueiosAcessoAsync()
        {
            return new ResultadoValidacao();
        }

        public IResultadoValidacao ListarBloqueiosAtivosAsync()
        {
            return new ResultadoValidacao();
        }
    }
}