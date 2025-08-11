using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Seguranca.Entities
{
    /// <summary>
    /// Entidade para gerenciar dados de autenticação (mapeada da tabela Autenticacao)
    /// </summary>
    public class AutenticacaoEntity : IEntity
    {
        public int IdAutenticacao { get; set; }
        public int IdUsuario { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public int TentativasLogin { get; set; }
        public bool ContaBloqueada { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataUltimoLogin { get; set; }
        public DateTime? DataUltimaTentativa { get; set; }
        public DateTime? DataBloqueio { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? DataExpiracaoRefreshToken { get; set; }
        public bool Ativo { get; set; }
        public string? TokenRecuperacao { get; set; }
    }
}