using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Validators
{
    public class AlunoValidator : ValidatorService
    {
        public IResultadoValidacao IncluirAluno(AlunoEntity aluno)
        {
            if (aluno == null)
                return new ResultadoValidacao("Aluno não informado.");

            if (string.IsNullOrEmpty(aluno.Nome))
                return new ResultadoValidacao("Nome não informado.");

            if (string.IsNullOrEmpty(aluno.Email))
                return new ResultadoValidacao("Email não informado.");

            if (string.IsNullOrEmpty(aluno.Cpf))
                return new ResultadoValidacao("CPF não informado.");

            if (string.IsNullOrEmpty(aluno.Telefone))
                return new ResultadoValidacao("Telefone não informado.");

            if (aluno.DataNascimento == DateTime.MinValue)
                return new ResultadoValidacao("Data de nascimento não informada.");

            if (aluno.DataNascimento > DateTime.Today)
                return new ResultadoValidacao("Data de nascimento não pode ser maior que a data atual.");

            if (aluno.IdPlano <= 0)
                return new ResultadoValidacao("Plano não informado.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarAluno(AlunoEntity aluno)
        {
            if (aluno == null)
                return new ResultadoValidacao("Aluno não informado.");

            if (aluno.Id <= 0)
                return new ResultadoValidacao("ID do aluno é inválido.");

            if (string.IsNullOrEmpty(aluno.Nome))
                return new ResultadoValidacao("Nome não informado.");

            if (string.IsNullOrEmpty(aluno.Email))
                return new ResultadoValidacao("Email não informado.");

            if (string.IsNullOrEmpty(aluno.Cpf))
                return new ResultadoValidacao("CPF não informado.");

            if (string.IsNullOrEmpty(aluno.Telefone))
                return new ResultadoValidacao("Telefone não informado.");

            if (aluno.DataNascimento == DateTime.MinValue)
                return new ResultadoValidacao("Data de nascimento não informada.");

            if (aluno.DataNascimento > DateTime.Today)
                return new ResultadoValidacao("Data de nascimento não pode ser maior que a data atual.");

            if (aluno.IdPlano <= 0)
                return new ResultadoValidacao("Plano não informado.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarAluno(int idAluno)
        {
            if (idAluno <= 0)
                return new ResultadoValidacao("O ID do aluno deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ExcluirAluno(int idAluno)
        {
            if (idAluno <= 0)
                return new ResultadoValidacao("O ID do aluno deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarAlunoPorCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return new ResultadoValidacao("CPF não informado.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarAlunoPorEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return new ResultadoValidacao("Email não informado.");

            return new ResultadoValidacao();
        }
    }
} 