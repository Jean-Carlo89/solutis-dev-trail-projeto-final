using BankSystem.Domain.Entities;

namespace BankSystem.API.model;

public class TransactionModel
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public string Description { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public int? SourceAccountId { get; set; }
    public BankAccountModel SourceAccount { get; set; }


    public int? DestinationAccountId { get; set; }
    public BankAccountModel DestinationAccount { get; set; }
}