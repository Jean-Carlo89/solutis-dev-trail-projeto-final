using BankSystem.Domain.Entities;

public class SavingsAccount : BankAccount
{

    public SavingsAccount(int id, int number, decimal balance, AccountType type,
                          string holder, DateTime createdAt, AccountStatus status,
                          int clientId, List<Transaction> transactions)
        : base(id, number, balance, type, holder, createdAt, status, clientId, transactions)
    {
        this.Type = AccountType.Poupança;

    }

    public SavingsAccount(AccountInputDto dto, int Number, int clientId) : base(dto, Number, clientId)
    {
        this.Type = AccountType.Poupança;

    }

    public SavingsAccount(int id, int number, decimal balance, AccountType type, string holder, DateTime createdAt, AccountStatus status, int clientId) : base(id, number, balance, type, holder, createdAt, status, clientId)
    {

        this.Id = id;
        this.Number = number;
        this.Balance = balance;
        this.Type = type;
        this.Holder = holder;
        this.CreatedAt = createdAt;
        this.Status = status;
        this.ClientId = clientId;
        this.Balance = Balance;
        this.CreatedAt = createdAt != default(DateTime) ? createdAt : DateTime.Now;
        this.Status = AccountStatus.Active;


    }




    public void ApplyInterest(decimal rate)
    {
        if (Status == AccountStatus.Active && Balance > 0)
        {
            decimal interest = Balance * rate;
            Balance += interest;
        }
    }


    public override void Withdraw(decimal amount)
    {
        if (Status != AccountStatus.Active || amount <= 0)
        {
            throw new InvalidOperationException("Invalid withdrawal operation.");
        }


        if (amount > Balance)
        {
            throw new InvalidOperationException("Insufficient balance. Savings accounts do not allow overdraft.");
        }

        this.Balance -= amount;
    }
}