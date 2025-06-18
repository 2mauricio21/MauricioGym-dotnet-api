using MauricioGym.Infra.Databases.SQLServer.Errors;
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
                if (excecao as SqlException != null)
                {
                    if (excecao.Message.Contains("data would be truncated in table"))
                    {
                        var sqlError = SqlErrorParser.ParseErrorMessageFieldSize(excecao.Message);
                        if (sqlError != null)
                            mensagemErro = $"O campo {sqlError.Column} do(a) {sqlError.Table.Replace("MauricioGym.dbo.", string.Empty)} não pode exceder {sqlError.TruncatedValueLength} caracteres.";
                    }

                    if (excecao.Message.Contains("Cannot insert duplicate key"))
                    {
                        var sqlError = SqlErrorParser.ParseErrorMessageDuplicate(excecao.Message);
                        if (sqlError != null)
                            mensagemErro = $"O valor {sqlError.DuplicateValue} já está existe.";
                    }
                }

                OcorreuErro = true;
                Excecao = excecao;
                MensagemErro = mensagemErro;

                // Registrar Erro no AppInsights
                AppInsigthsLogger.LogException(excecao, mensagemErro);
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

            if (validacao.Excecao as SqlException != null)
            {
                if (validacao.Excecao.Message.Contains("data would be truncated in table"))
                {
                    var sqlError = SqlErrorParser.ParseErrorMessageFieldSize(validacao.Excecao.Message);
                    if (sqlError != null)
                        validacao.MensagemErro = $"O campo {sqlError.Column} do(a) {sqlError.Table.Replace("MauricioGym.dbo.", string.Empty)} não pode exceder {sqlError.TruncatedValueLength} caracteres.";
                }

                if (validacao.Excecao.Message.Contains("Cannot insert duplicate key"))
                {
                    var sqlError = SqlErrorParser.ParseErrorMessageDuplicate(validacao.Excecao.Message);
                    if (sqlError != null)
                        validacao.MensagemErro = $"O valor {sqlError.DuplicateValue} já existe.";
                }
            }

            Validacoes ??= new List<IResultadoValidacao> { validacao };

            if (validacao.Excecao != null)
            {
                // Registrar Erro no AppInsights
                AppInsigthsLogger.LogException(validacao.Excecao, validacao.MensagemErro);
            }
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

        public IList<IResultadoValidacao> Validacoes { get; set; }

        public T Retorno { get; set; }

        public string MensagemErro
        {
            get
            {
                if (Validacoes == null || Validacoes.Count == 0)
                    return string.Empty;

                return Validacoes.FirstOrDefault().MensagemErro;
            }
        }



        public IResultadoValidacao Erro => Validacoes.FirstOrDefault();

        public bool LimiteExcedido { get; set; }
    }
}
