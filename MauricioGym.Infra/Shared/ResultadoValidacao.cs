using MauricioGym.Infra.Shared.Interfaces;
using Microsoft.Data.SqlClient;

namespace MauricioGym.Infra.Shared
{
    public class ResultadoValidacao : IResultadoValidacao
    {
        public bool OcorreuErro { get; set; }

        public string MensagemErro { get; set; } = string.Empty;

        public Exception? Excecao { get; set; }

        public bool LimiteExcedido { get; set; }

        public ResultadoValidacao()
        {
        }

        public ResultadoValidacao(string mensagemErro)
            : this(null, mensagemErro)
        {
        }

        public ResultadoValidacao(Exception? excecao, string mensagemErro)
        {
#if DEBUG
            if (excecao != null)
                mensagemErro += $":\n\n {excecao.Message} {excecao.StackTrace}";
#endif

            if (excecao != null || !string.IsNullOrWhiteSpace(mensagemErro))
            {
                if (excecao is SqlException)
                {
                    if (excecao.Message.Contains("data would be truncated in table"))
                    {
                        mensagemErro = "O tamanho do campo excede o limite permitido.";
                    }

                    if (excecao.Message.Contains("Cannot insert duplicate key"))
                    {
                        mensagemErro = "Este valor já existe no sistema.";
                    }
                }

                OcorreuErro = true;
                Excecao = excecao;
                MensagemErro = mensagemErro;
            }
        }
    }

    public class ResultadoValidacao<T> : IResultadoValidacao<T>
    {
        public ResultadoValidacao(IList<IResultadoValidacao> validacoes)
        {
            Validacoes = validacoes;
        }

        public ResultadoValidacao(IResultadoValidacao validacao)
        {
#if DEBUG
            if (validacao.Excecao != null)
                validacao.MensagemErro += $":\n\n {validacao.Excecao.Message} {validacao.Excecao.StackTrace}";
#endif

            if (validacao.Excecao is SqlException)
            {
                if (validacao.Excecao.Message.Contains("data would be truncated in table"))
                {
                    validacao.MensagemErro = "O tamanho do campo excede o limite permitido.";
                }

                if (validacao.Excecao.Message.Contains("Cannot insert duplicate key"))
                {
                    validacao.MensagemErro = "Este valor já existe no sistema.";
                }
            }

            Validacoes ??= new List<IResultadoValidacao> { validacao };
        }

        public ResultadoValidacao(string mensagemErro)
            : this(new ResultadoValidacao(mensagemErro))
        {
        }

        public ResultadoValidacao(Exception ex, string mensagemErro = "")
            : this(new ResultadoValidacao() { OcorreuErro = true, Excecao = ex, MensagemErro = mensagemErro })
        {
        }

        public ResultadoValidacao(T retorno)
            : this(new ResultadoValidacao() { OcorreuErro = false })
        {
            Retorno = retorno;
        }

        public bool OcorreuErro
        {
            get
            {
                if (Validacoes != null)
                {
                    return Validacoes.Count(x => x.OcorreuErro) > 0;
                }

                return false;
            }
        }

        public IList<IResultadoValidacao>? Validacoes { get; set; }

        public T Retorno { get; set; } = default(T)!;

        public string MensagemErro
        {
            get
            {
                if (Validacoes == null || Validacoes.Count == 0)
                    return string.Empty;

                return Validacoes.FirstOrDefault()?.MensagemErro ?? string.Empty;
            }
        }

        public IResultadoValidacao? Erro => Validacoes?.FirstOrDefault();

        public bool LimiteExcedido { get; set; }
    }
}
