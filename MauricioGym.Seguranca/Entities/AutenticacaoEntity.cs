using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Seguranca.Entities
{
    /// <summary>
    /// Entidade para gerenciar dados de autenticação
    /// </summary>
    public class AutenticacaoEntity : IEntity
    {
        public int IdAutenticacao { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string? TokenRecuperacao { get; set; }
        public DateTime? DataExpiracaoToken { get; set; }
        public int TentativasLogin { get; set; } = 0;
        public DateTime? DataUltimaTentativa { get; set; }
        public bool ContaBloqueada { get; set; } = false;
        public DateTime? DataBloqueio { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataUltimoLogin { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? DataExpiracaoRefreshToken { get; set; }
        public bool Ativo { get; set; } = true;
        
        // Relacionamento com usuário
        public int IdUsuario { get; set; }
    }
}