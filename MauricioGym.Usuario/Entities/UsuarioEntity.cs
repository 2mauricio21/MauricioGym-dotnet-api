using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Usuario.Entities
{
    public class UsuarioEntity : IEntity
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        // Senha foi movida para MauricioGym.Seguranca.Entities.AutenticacaoEntity
        public string CPF { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public string Endereco { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime? DataUltimoLogin { get; set; }
    }
}