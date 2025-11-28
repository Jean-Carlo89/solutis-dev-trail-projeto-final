using System.ComponentModel.DataAnnotations;
using BankSystem.Domain.Entities;

public class TransactionOutputDto
{

    public TransactionType TipoDeTransacao { get; set; }
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }

    public int IdContaOriginaria { get; set; }
    public int IdContaDestino { get; set; }
}