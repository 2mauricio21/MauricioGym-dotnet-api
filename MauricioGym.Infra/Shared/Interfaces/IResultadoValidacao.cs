namespace MauricioGym.Infra.Shared.Interfaces
{
    public interface IResultadoValidacao<T>
    {
        bool OcorreuErro { get; }

        T Retorno { get; set; }

        IList<IResultadoValidacao>? Validacoes { get; set; }

        IResultadoValidacao? Erro { get; }

        string MensagemErro { get; }

        public bool LimiteExcedido { get; set; }
    }

    public interface IResultadoValidacao
    {
        bool OcorreuErro { get; set; }

        string MensagemErro { get; set; }

        Exception? Excecao { get; set; }

        public bool LimiteExcedido { get; set; }
    }
}
