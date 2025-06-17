using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Services.Validators
{
    public class MensalidadeValidator : ValidatorService
    {
        public IResultadoValidacao CriarMensalidade(MensalidadeEntity mensalidade)
        {
            if (mensalidade == null)
                return new ResultadoValidacao("A mensalidade não pode ser nula.");

            if (mensalidade.UsuarioId <= 0)
                return new ResultadoValidacao("O ID do usuário é obrigatório.");

            if (mensalidade.PlanoId <= 0)
                return new ResultadoValidacao("O ID do plano é obrigatório.");

            if (mensalidade.Valor <= 0)
                return new ResultadoValidacao("O valor deve ser maior que zero.");

            if (mensalidade.DataVencimento == DateTime.MinValue)
                return new ResultadoValidacao("A data de vencimento é obrigatória.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AtualizarMensalidade(MensalidadeEntity mensalidade)
        {
            if (mensalidade == null)
                return new ResultadoValidacao("A mensalidade não pode ser nula.");

            if (mensalidade.Id <= 0)
                return new ResultadoValidacao("O ID da mensalidade é obrigatório.");

            if (mensalidade.UsuarioId <= 0)
                return new ResultadoValidacao("O ID do usuário é obrigatório.");

            if (mensalidade.PlanoId <= 0)
                return new ResultadoValidacao("O ID do plano é obrigatório.");

            if (mensalidade.Valor <= 0)
                return new ResultadoValidacao("O valor deve ser maior que zero.");

            if (mensalidade.DataVencimento == DateTime.MinValue)
                return new ResultadoValidacao("A data de vencimento é obrigatória.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao RemoverMensalidade(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("Informe o ID da mensalidade.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ObterMensalidadePorId(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("Informe o ID da mensalidade.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ObterPorUsuario(int usuarioId)
        {
            if (usuarioId <= 0)
                return new ResultadoValidacao("Informe o ID do usuário.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao VerificarUsuarioEmDia(int usuarioId)
        {
            if (usuarioId <= 0)
                return new ResultadoValidacao("Informe o ID do usuário.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao RegistrarPagamento(int usuarioId, int planoId, int meses, decimal valorBase, DateTime dataInicio)
        {
            if (usuarioId <= 0)
                return new ResultadoValidacao("Informe o ID do usuário.");

            if (planoId <= 0)
                return new ResultadoValidacao("Informe o ID do plano.");

            if (meses <= 0)
                return new ResultadoValidacao("O número de meses deve ser maior que zero.");

            if (valorBase <= 0)
                return new ResultadoValidacao("O valor base deve ser maior que zero.");

            if (dataInicio == DateTime.MinValue)
                return new ResultadoValidacao("A data de início é obrigatória.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ObterVencendas(int dias)
        {
            if (dias < 0)
                return new ResultadoValidacao("O número de dias não pode ser negativo.");

            return new ResultadoValidacao();
        }
    }
}
