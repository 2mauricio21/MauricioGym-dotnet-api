using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Services.Validators
{
    public class CheckInValidator : ValidatorService
    {
        public IResultadoValidacao CriarCheckIn(CheckInEntity checkIn)
        {
            if (checkIn == null)
                return new ResultadoValidacao("O check-in não pode ser nulo.");

            if (checkIn.UsuarioId <= 0)
                return new ResultadoValidacao("O ID do usuário é obrigatório.");

            if (checkIn.DataHora == DateTime.MinValue)
                return new ResultadoValidacao("A data e hora são obrigatórias.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AtualizarCheckIn(CheckInEntity checkIn)
        {
            if (checkIn == null)
                return new ResultadoValidacao("O check-in não pode ser nulo.");

            if (checkIn.Id <= 0)
                return new ResultadoValidacao("O ID do check-in é obrigatório.");

            if (checkIn.UsuarioId <= 0)
                return new ResultadoValidacao("O ID do usuário é obrigatório.");

            if (checkIn.DataHora == DateTime.MinValue)
                return new ResultadoValidacao("A data e hora são obrigatórias.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao RemoverCheckIn(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("Informe o ID do check-in.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ObterCheckInPorId(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("Informe o ID do check-in.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ObterPorUsuario(int usuarioId)
        {
            if (usuarioId <= 0)
                return new ResultadoValidacao("Informe o ID do usuário.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            if (dataInicio == DateTime.MinValue)
                return new ResultadoValidacao("A data de início é obrigatória.");

            if (dataFim == DateTime.MinValue)
                return new ResultadoValidacao("A data de fim é obrigatória.");

            if (dataFim < dataInicio)
                return new ResultadoValidacao("A data de fim não pode ser menor que a data de início.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao RealizarCheckIn(int usuarioId)
        {
            if (usuarioId <= 0)
                return new ResultadoValidacao("Informe o ID do usuário.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ContarCheckInsPorUsuarioMes(int usuarioId, int ano, int mes)
        {
            if (usuarioId <= 0)
                return new ResultadoValidacao("Informe o ID do usuário.");

            if (ano < 2000 || ano > DateTime.Now.Year + 1)
                return new ResultadoValidacao("Ano inválido.");

            if (mes < 1 || mes > 12)
                return new ResultadoValidacao("Mês inválido.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarUsuarioId(int usuarioId)
        {
            if (usuarioId <= 0)
                return new ResultadoValidacao("O ID do usuário é obrigatório.");

            return new ResultadoValidacao();
        }

    }
}
