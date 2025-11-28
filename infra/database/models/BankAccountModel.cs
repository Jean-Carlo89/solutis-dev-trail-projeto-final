using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace BankSystem.API.model;

public class BankAccountModel


{


    public BankAccountModel() { }

    public BankAccountModel(int Number, decimal Balance, AccountType Type, string Holder)
    {
        this.Number = Number;
        this.Balance = Balance;
        // this.Type = Enum.Parse<AccountType>(Type);
        this.Type = Type;
        this.Holder = Holder;
        this.CreatedAt = DateTime.Now;
        this.Status = AccountStatus.Active;
        Transactions = new List<TransactionModel>();
    }

    [Key]
    public int Id { get; set; }


    [Key]
    [Required]
    [MaxLength(20)]
    public int Number { get; set; }


    public string Holder { get; set; } = "";


    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance { get; set; }


    [Required]
    public DateTime CreatedAt { get; set; }


    [Required]
    public AccountType Type { get; set; }


    [Required]
    public AccountStatus Status { get; set; }


    public int ClientId { get; set; }
    public ClientModel Client { get; set; }

    public ICollection<TransactionModel> Transactions { get; set; }

    public ICollection<TransactionModel> DestinationTransactions { get; set; }




}