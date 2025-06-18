using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Validators
{
    public class AvaliacaoFisicaValidator : ValidatorService
    {
        public IResultadoValidacao IncluirAvaliacaoFisica(AvaliacaoFisicaEntity avaliacaoFisica)
        {
            if (avaliacaoFisica == null)
                return new ResultadoValidacao("Avaliação física não informada.");

            if (avaliacaoFisica.IdAluno <= 0)
                return new ResultadoValidacao("Aluno não informado.");

            if (avaliacaoFisica.Peso <= 0)
                return new ResultadoValidacao("Peso inválido.");

            if (avaliacaoFisica.Altura <= 0)
                return new ResultadoValidacao("Altura inválida.");

            if (avaliacaoFisica.DataAvaliacao == DateTime.MinValue)
                return new ResultadoValidacao("Data da avaliação não informada.");

            if (avaliacaoFisica.DataAvaliacao > DateTime.Now)
                return new ResultadoValidacao("Data da avaliação não pode ser maior que a data atual.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarAvaliacaoFisica(AvaliacaoFisicaEntity avaliacaoFisica)
        {
            if (avaliacaoFisica == null)
                return new ResultadoValidacao("Avaliação física não informada.");

            if (avaliacaoFisica.Id <= 0)
                return new ResultadoValidacao("Id da avaliação física não informado.");

            return IncluirAvaliacaoFisica(avaliacaoFisica);
        }

        public IResultadoValidacao ExcluirAvaliacaoFisica(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("Id da avaliação física não informado.");

            return new ResultadoValidacao();
        }
    }
} 