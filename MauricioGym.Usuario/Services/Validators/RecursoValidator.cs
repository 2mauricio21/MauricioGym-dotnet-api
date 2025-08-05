using MauricioGym.Usuario.Entities;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Usuario.Services.Validators
{
    public class RecursoValidator : ValidatorService
    {
        public IResultadoValidacao IncluirRecursoAsync(RecursoEntity recurso)
        {
            if (recurso == null)
                return new ResultadoValidacao("O recurso não pode ser nulo");

            if (string.IsNullOrEmpty(recurso.Nome))
                return new ResultadoValidacao("O nome do recurso é obrigatório");

            if (string.IsNullOrEmpty(recurso.Codigo))
                return new ResultadoValidacao("O código do recurso é obrigatório");

            if (recurso.Codigo.Length > 50)
                return new ResultadoValidacao("O código do recurso deve ter no máximo 50 caracteres");

            if (recurso.Nome.Length > 100)
                return new ResultadoValidacao("O nome do recurso deve ter no máximo 100 caracteres");

            if (!string.IsNullOrEmpty(recurso.Descricao) && recurso.Descricao.Length > 500)
                return new ResultadoValidacao("A descrição do recurso deve ter no máximo 500 caracteres");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarRecursoAsync(RecursoEntity recurso)
        {
            if (recurso == null)
                return new ResultadoValidacao("O recurso não pode ser nulo");

            if (recurso.IdRecurso <= 0)
                return new ResultadoValidacao("O ID do recurso deve ser maior que zero");

            if (string.IsNullOrEmpty(recurso.Nome))
                return new ResultadoValidacao("O nome do recurso é obrigatório");

            if (string.IsNullOrEmpty(recurso.Codigo))
                return new ResultadoValidacao("O código do recurso é obrigatório");

            if (recurso.Codigo.Length > 50)
                return new ResultadoValidacao("O código do recurso deve ter no máximo 50 caracteres");

            if (recurso.Nome.Length > 100)
                return new ResultadoValidacao("O nome do recurso deve ter no máximo 100 caracteres");

            if (!string.IsNullOrEmpty(recurso.Descricao) && recurso.Descricao.Length > 500)
                return new ResultadoValidacao("A descrição do recurso deve ter no máximo 500 caracteres");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarRecursoAsync(int idRecurso)
        {
            if (idRecurso <= 0)
                return new ResultadoValidacao("O ID do recurso deve ser maior que zero");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ConsultarRecursoPorCodigoAsync(string codigo)
        {
            if (string.IsNullOrEmpty(codigo))
                return new ResultadoValidacao("O código do recurso é obrigatório");

            if (codigo.Length > 50)
                return new ResultadoValidacao("O código do recurso deve ter no máximo 50 caracteres");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ExcluirRecursoAsync(int idRecurso)
        {
            if (idRecurso <= 0)
                return new ResultadoValidacao("O ID do recurso deve ser maior que zero");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ListarRecursosAsync()
        {
            return new ResultadoValidacao();
        }

        public IResultadoValidacao ListarRecursosAtivosAsync()
        {
            return new ResultadoValidacao();
        }
    }
}