using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Validators
{
    public class MensalidadeValidator : ValidatorService
    {
        public IResultadoValidacao IncluirMensalidade(MensalidadeEntity mensalidade)
        {
            if (mensalidade == null)
                return new ResultadoValidacao("Mensalidade não informada.");

            if (mensalidade.IdAluno <= 0)
                return new ResultadoValidacao("Aluno não informado.");

            if (mensalidade.IdPlano <= 0)
                return new ResultadoValidacao("Plano não informado.");

            if (mensalidade.Valor <= 0)
                return new ResultadoValidacao("Valor inválido.");

            if (mensalidade.DataVencimento == DateTime.MinValue)
                return new ResultadoValidacao("Data de vencimento não informada.");

            if (string.IsNullOrEmpty(mensalidade.Status))
                return new ResultadoValidacao("Status não informado.");

            if (mensalidade.DataPagamento.HasValue)
            {
                if (mensalidade.DataPagamento > DateTime.Now)
                    return new ResultadoValidacao("Data de pagamento não pode ser maior que a data atual.");
            }

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarMensalidade(MensalidadeEntity mensalidade)
        {
            if (mensalidade == null)
                return new ResultadoValidacao("Mensalidade não informada.");

            if (mensalidade.Id <= 0)
                return new ResultadoValidacao("Id da mensalidade não informado.");

            return IncluirMensalidade(mensalidade);
        }

        public IResultadoValidacao ExcluirMensalidade(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("Id da mensalidade não informado.");

            return new ResultadoValidacao();
        }
    }
} 