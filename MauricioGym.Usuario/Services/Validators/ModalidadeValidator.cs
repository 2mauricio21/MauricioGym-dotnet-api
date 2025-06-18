using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Usuario.Entities;

namespace MauricioGym.Usuario.Services.Validators
{
    public class ModalidadeValidator : ValidatorService
    {
        public IResultadoValidacao IncluirModalidade(ModalidadeEntity modalidade)
        {
            if (modalidade == null)
                return new ResultadoValidacao("Modalidade não informada.");

            if (string.IsNullOrEmpty(modalidade.Descricao))
                return new ResultadoValidacao("Descrição não informada.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarModalidade(ModalidadeEntity modalidade)
        {
            if (modalidade == null)
                return new ResultadoValidacao("Modalidade não informada.");

            if (modalidade.Id <= 0)
                return new ResultadoValidacao("ID da modalidade é inválido.");

            if (string.IsNullOrEmpty(modalidade.Descricao))
                return new ResultadoValidacao("Descrição não informada.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarModalidade(int idModalidade)
        {
            if (idModalidade <= 0)
                return new ResultadoValidacao("O ID da modalidade deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ExcluirModalidade(int idModalidade)
        {
            if (idModalidade <= 0)
                return new ResultadoValidacao("O ID da modalidade deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarModalidadePorNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                return new ResultadoValidacao("Nome não informado.");

            return new ResultadoValidacao();
        }

        //ValidarNome
        public IResultadoValidacao ValidarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                return new ResultadoValidacao("Nome não informado.");

            return new ResultadoValidacao();
        }
    }
}