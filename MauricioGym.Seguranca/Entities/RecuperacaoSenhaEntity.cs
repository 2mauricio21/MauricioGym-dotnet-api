using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Seguranca.Entities
{
    /// <summary>
    /// Entidade para gerenciar solicitações de recuperação de senha
    /// </summary>
    public class RecuperacaoSenhaEntity : IEntity
    {
        public int IdRecuperacao { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime DataSolicitacao { get; set; } = DateTime.Now;
        public DateTime DataExpiracao { get; set; }
        public bool Utilizado { get; set; } = false;
        public DateTime? DataUtilizacao { get; set; }
        public string? EnderecoIP { get; set; }
        public string? UserAgent { get; set; }
        public bool Ativo { get; set; } = true;
        
        // Relacionamento com usuário
        public int IdUsuario { get; set; }
    }
}