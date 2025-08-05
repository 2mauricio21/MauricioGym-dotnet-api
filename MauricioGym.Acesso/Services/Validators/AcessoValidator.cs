using MauricioGym.Acesso.Entities;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Acesso.Services.Validators
{
    public class AcessoValidator : ValidatorService
    {
        public IResultadoValidacao IncluirAcesso(AcessoEntity acesso)
        {
            if (acesso == null)
                return new ResultadoValidacao("O acesso não pode ser nulo");

            if (acesso.IdUsuario <= 0)
                return new ResultadoValidacao("O ID do usuário é obrigatório");

            if (acesso.IdAcademia <= 0)
                return new ResultadoValidacao("O ID da academia é obrigatório");

            if (string.IsNullOrWhiteSpace(acesso.TipoAcesso))
                return new ResultadoValidacao("O tipo de acesso é obrigatório");

            if (acesso.TipoAcesso.Length > 50)
                return new ResultadoValidacao("O tipo de acesso deve ter no máximo 50 caracteres");

            if (!string.IsNullOrEmpty(acesso.ObservacaoAcesso) && acesso.ObservacaoAcesso.Length > 500)
                return new ResultadoValidacao("A observação do acesso deve ter no máximo 500 caracteres");

            if (!string.IsNullOrEmpty(acesso.MotivoNegacao) && acesso.MotivoNegacao.Length > 500)
                return new ResultadoValidacao("O motivo da negação deve ter no máximo 500 caracteres");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarAcesso(AcessoEntity acesso)
        {
            if (acesso == null)
                return new ResultadoValidacao("O acesso não pode ser nulo");

            if (acesso.IdAcesso <= 0)
                return new ResultadoValidacao("O ID do acesso deve ser maior que zero");

            if (acesso.IdUsuario <= 0)
                return new ResultadoValidacao("O ID do usuário é obrigatório");

            if (acesso.IdAcademia <= 0)
                return new ResultadoValidacao("O ID da academia é obrigatório");

            if (string.IsNullOrWhiteSpace(acesso.TipoAcesso))
                return new ResultadoValidacao("O tipo de acesso é obrigatório");

            if (acesso.TipoAcesso.Length > 50)
                return new ResultadoValidacao("O tipo de acesso deve ter no máximo 50 caracteres");

            if (!string.IsNullOrEmpty(acesso.ObservacaoAcesso) && acesso.ObservacaoAcesso.Length > 500)
                return new ResultadoValidacao("A observação do acesso deve ter no máximo 500 caracteres");

            if (!string.IsNullOrEmpty(acesso.MotivoNegacao) && acesso.MotivoNegacao.Length > 500)
                return new ResultadoValidacao("O motivo da negação deve ter no máximo 500 caracteres");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarAcesso(int idAcesso)
        {
            if (idAcesso <= 0)
                return new ResultadoValidacao("O ID do acesso deve ser maior que zero");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarAcessosPorUsuario(int idUsuario)
        {
            if (idUsuario <= 0)
                return new ResultadoValidacao("O ID do usuário deve ser maior que zero");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarAcessosPorAcademia(int idAcademia)
        {
            if (idAcademia <= 0)
                return new ResultadoValidacao("O ID da academia deve ser maior que zero");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarAcessosAtivosPorAcademia(int idAcademia)
        {
            if (idAcademia <= 0)
                return new ResultadoValidacao("O ID da academia deve ser maior que zero");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao RegistrarSaida(int idAcesso)
        {
            if (idAcesso <= 0)
                return new ResultadoValidacao("O ID do acesso deve ser maior que zero");



            return new ResultadoValidacao();
        }

        public IResultadoValidacao ListarAcessosAsync()
        {
            return new ResultadoValidacao();
        }
    }
}