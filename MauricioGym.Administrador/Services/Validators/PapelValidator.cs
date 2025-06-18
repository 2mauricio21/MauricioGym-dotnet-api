using MauricioGym.Administrador.Entities;
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services.Validators
{
    public class PapelValidator : ValidatorService
    {
        public IResultadoValidacao IncluirPapel(PapelEntity papel)
        {

            if (papel == null)
                return new ResultadoValidacao("O papel não pode ser nulo.");

            if (string.IsNullOrEmpty(papel.Nome))
                return new ResultadoValidacao("O nome do papel não pode ser vazio ou nulo.");

            if (papel.Nome.Length > 100)
                return new ResultadoValidacao("O nome do papel não pode ter mais de 100 caracteres.");

            if (papel.Descricao.Length > 500)
                return new ResultadoValidacao("A descrição do papel não pode ter mais de 500 caracteres.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao AlterarPapel(PapelEntity papel)
        {
            if (papel == null)
                return new ResultadoValidacao("O papel não pode ser nulo.");

            if (string.IsNullOrEmpty(papel.Nome))
                return new ResultadoValidacao("O nome do papel não pode ser vazio ou nulo.");

            if (papel.Nome.Length > 100)
                return new ResultadoValidacao("O nome do papel não pode ter mais de 100 caracteres.");

            if (papel.Descricao.Length > 500)
                return new ResultadoValidacao("A descrição do papel não pode ter mais de 500 caracteres.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ExcluirPapel(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("O ID do papel deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        //ExisteAsync
        public IResultadoValidacao ExistePapel(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("O ID do papel deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ObterPorId(int id)
        {
            if (id <= 0)
                return new ResultadoValidacao("O ID do papel deve ser maior que zero.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ObterPorNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                return new ResultadoValidacao("O nome do papel não pode ser vazio ou nulo.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ObterPorAdministradorId(int administradorId)
        {
            if (administradorId <= 0)
                return new ResultadoValidacao("O ID do administrador deve ser maior que zero.");

            return new ResultadoValidacao();
        }
    }
}
