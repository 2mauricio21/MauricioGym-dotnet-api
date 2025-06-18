using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Usuario.Entities
{
    public class PagamentoEntity : IEntity
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int AcademiaId { get; set; }
        public int PlanoClienteId { get; set; }
        public int? CheckInId { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal Valor { get; set; }
        public string FormaPagamento { get; set; } = string.Empty; // Dinheiro, Cartão, PIX, etc.
        public string Status { get; set; } = "Pendente"; // Confirmado, Pendente, Cancelado
        public string? Observacoes { get; set; }
        public int? NumeroParcelas { get; set; }
        public int? ParcelaAtual { get; set; }
        public decimal? ValorParcela { get; set; }
        public DateTime? DataVencimento { get; set; }
        public string? CodigoTransacao { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataExclusao { get; set; }
        
        // Propriedades de navegação - não mapeadas diretamente no banco
        public ClienteEntity? Cliente { get; set; }
        public PlanoClienteEntity? PlanoCliente { get; set; }
        public CheckInEntity? CheckIn { get; set; }
    }
}