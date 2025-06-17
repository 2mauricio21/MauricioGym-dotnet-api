using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Validators
{
    public class CaixaValidator : ValidatorService
    {
        public IResultadoValidacao CriarCaixa(CaixaEntity caixa)
        {
            if (caixa == null)
                return new ResultadoValidacao("A caixa não pode ser nula.");

            if (string.IsNullOrWhiteSpace(caixa.Descricao))
                return new ResultadoValidacao("A descrição é obrigatória.");

            if (caixa.Valor <= 0)
                return new ResultadoValidacao("O valor deve ser maior que zero.");

            if (caixa.DataMovimento == DateTime.MinValue)
                return new ResultadoValidacao("A data de movimento é obrigatória.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AtualizarCaixa(CaixaEntity caixa)
        {
            if (caixa == null)
                return new ResultadoValidacao("A caixa não pode ser nula.");

            if (caixa.Id <= 0)
                return new ResultadoValidacao("O ID da caixa é obrigatório.");

            if (string.IsNullOrWhiteSpace(caixa.Descricao))
                return new ResultadoValidacao("A descrição é obrigatória.");

            if (caixa.Valor <= 0)
                return new ResultadoValidacao("O valor deve ser maior que zero.");

            if (caixa.DataMovimento == DateTime.MinValue)
                return new ResultadoValidacao("A data de movimento é obrigatória.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao RemoverCaixa(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("Informe o ID da caixa.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ObterCaixaPorId(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("Informe o ID da caixa.");

            return new ResultadoValidacao();
        }
    }
}
