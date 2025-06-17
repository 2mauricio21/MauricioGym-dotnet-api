using MauricioGym.Infra.Entities.Interfaces;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Infra.Services.Validators
{
    public class ValidatorService : IValidatorService
    {
        public IResultadoValidacao EntityIsNull<T>(T entity) where T : IEntity
        {
            if (entity == null)
            {
                return new ResultadoValidacao("A entidade não pode ser nula.");
            }

            if (entity.Id <= 0)
            {
                return new ResultadoValidacao("O ID da entidade deve ser maior que zero.");
            }

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarId(int id)
        {
            if (id <= 0)
            {
                return new ResultadoValidacao("O ID é obrigatório e deve ser maior que zero.");
            }

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarId(int? id, string nomeEntidade = "registro")
        {
            if (!id.HasValue || id <= 0)
            {
                return new ResultadoValidacao($"O ID do {nomeEntidade} é obrigatório e deve ser maior que zero.");
            }

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarTextoObrigatorio(string texto, string nomeCampo)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return new ResultadoValidacao($"O campo {nomeCampo} é obrigatório.");
            }

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarTamanhoTexto(string texto, string nomeCampo, int tamanhoMaximo)
        {
            if (!string.IsNullOrWhiteSpace(texto) && texto.Length > tamanhoMaximo)
            {
                return new ResultadoValidacao($"O campo {nomeCampo} não pode exceder {tamanhoMaximo} caracteres.");
            }

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return new ResultadoValidacao("O email é obrigatório.");
            }

            if (!email.Contains("@") || email.Length < 5)
            {
                return new ResultadoValidacao("O email deve ter um formato válido.");
            }

            return new ResultadoValidacao();
        }        public IResultadoValidacao ValidarUsuarioId(int usuarioId)
        {
            if (usuarioId <= 0)
            {
                return new ResultadoValidacao("O ID do usuário é obrigatório e deve ser maior que zero.");
            }

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarPlanoId(int planoId)
        {
            if (planoId <= 0)
            {
                return new ResultadoValidacao("O ID do plano é obrigatório e deve ser maior que zero.");
            }

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarAdministradorId(int administradorId)
        {
            if (administradorId <= 0)
            {
                return new ResultadoValidacao("O ID do administrador é obrigatório e deve ser maior que zero.");
            }

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarValor(decimal valor)
        {
            if (valor <= 0)
            {
                return new ResultadoValidacao("O valor deve ser maior que zero.");
            }

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarData(DateTime data, string nomeCampo)
        {
            if (data == DateTime.MinValue)
            {
                return new ResultadoValidacao($"A {nomeCampo} é obrigatória.");
            }

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            if (dataInicio > dataFim)
            {
                return new ResultadoValidacao("A data de início não pode ser maior que a data de fim.");
            }

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarDias(int dias)
        {
            if (dias <= 0)
            {
                return new ResultadoValidacao("O número de dias deve ser maior que zero.");
            }

            return new ResultadoValidacao();
        }        public IResultadoValidacao ValidarPagamentoMensalidade(DateTime? dataPagamento, string status)
        {
            if (dataPagamento.HasValue && string.IsNullOrWhiteSpace(status))
            {
                return new ResultadoValidacao("O status é obrigatório quando há data de pagamento.");
            }

            if (dataPagamento.HasValue && status != "Paga")
            {
                return new ResultadoValidacao("Mensalidades pagas devem ter status 'Paga'.");
            }

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarPagamentoMensalidade(DateTime? dataPagamento, string status, decimal valor, int mes, int ano)
        {
            var resultadoBasico = ValidarPagamentoMensalidade(dataPagamento, status);
            if (resultadoBasico.OcorreuErro)
                return resultadoBasico;

            if (valor <= 0)
                return new ResultadoValidacao("O valor deve ser maior que zero.");

            if (mes < 1 || mes > 12)
                return new ResultadoValidacao("O mês deve ser entre 1 e 12.");

            if (ano < DateTime.Now.Year - 10 || ano > DateTime.Now.Year + 1)
                return new ResultadoValidacao("O ano deve estar dentro de um período válido.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarDataConcessao(DateTime dataConcessao)
        {
            if (dataConcessao == DateTime.MinValue)
            {
                return new ResultadoValidacao("A data de concessão é obrigatória.");
            }

            if (dataConcessao > DateTime.Now)
            {
                return new ResultadoValidacao("A data de concessão não pode ser futura.");
            }

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarTipoPermissao(string tipoPermissao)
        {
            if (string.IsNullOrWhiteSpace(tipoPermissao))
            {
                return new ResultadoValidacao("O tipo de permissão é obrigatório.");
            }

            var tiposValidos = new[] { "Cadastrar", "Editar", "Excluir" };
            if (!tiposValidos.Contains(tipoPermissao))
            {
                return new ResultadoValidacao($"Tipo de permissão inválido. Valores válidos: {string.Join(", ", tiposValidos)}");
            }

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarUsuarioMesAno(int usuarioId, int mes, int ano)
        {
            var validacaoUsuario = ValidarUsuarioId(usuarioId);
            if (validacaoUsuario.OcorreuErro)
                return validacaoUsuario;

            if (mes < 1 || mes > 12)
                return new ResultadoValidacao("O mês deve ser entre 1 e 12.");

            if (ano < DateTime.Now.Year - 10 || ano > DateTime.Now.Year + 1)
                return new ResultadoValidacao("O ano deve estar dentro de um período válido.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                return new ResultadoValidacao("A descrição é obrigatória.");

            if (descricao.Length > 500)
                return new ResultadoValidacao("A descrição não pode exceder 500 caracteres.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ValidarDataMovimento(DateTime dataMovimento)
        {
            if (dataMovimento == DateTime.MinValue)
                return new ResultadoValidacao("A data de movimento é obrigatória.");

            if (dataMovimento > DateTime.Now)
                return new ResultadoValidacao("A data de movimento não pode ser futura.");

            return new ResultadoValidacao();
        }

        public IResultadoValidacao ObterUsuarioPorId(int id)
        {
            return ValidarId(id, "usuário");
        }

        public IResultadoValidacao ObterUsuarioPorEmail(string email)
        {
            return ValidarEmail(email);
        }

        public IResultadoValidacao RemoverUsuario(int id)
        {
            return ValidarId(id, "usuário");
        }
    }
}
