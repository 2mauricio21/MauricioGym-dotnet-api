namespace MauricioGym.Infra.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AutorizacaoAttribute : Attribute
    {
        public string[] Permissoes { get; }

        public string Descricao { get; }

        public AutorizacaoAttribute(string descricao, params string[] permissoes)
        {
            Descricao = descricao;
            Permissoes = permissoes ?? Array.Empty<string>();
        }
    }
}
