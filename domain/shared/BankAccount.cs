using System;
using System.Collections.Generic;
using BankSystem.Domain.Entities;

public class BankAccount
{

    public int Id { get; protected set; }
    public int Number { get; protected set; }
    public string Holder { get; protected set; }


    public decimal Balance { get; protected set; }

    public AccountType Type { get; protected set; }
    public AccountStatus Status { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public int ClientId { get; protected set; }
    public List<Transaction> Transactions { get; protected set; }


    public BankAccount(int id, int number, decimal balance, AccountType type,
                       string holder, DateTime createdAt, AccountStatus status,
                       int clientId, List<Transaction> transactions)
    {
        this.Id = id;
        this.Number = number;
        this.Balance = balance;
        this.Type = type;
        this.Holder = holder;
        this.CreatedAt = createdAt;
        this.Status = status;
        this.ClientId = clientId;
        this.Transactions = transactions ?? new List<Transaction>();


    }


    public BankAccount(int id, int number, decimal balance, AccountType type, string holder, DateTime createdAt, AccountStatus status, int clientId)
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




    public BankAccount(AccountInputDto dto, int Number, int clientId)
    {

        this.Number = Number;
        this.Balance = dto.Saldo;
        this.Holder = dto.Titular;
        this.ClientId = clientId;
        this.Balance = dto.Saldo;
        this.CreatedAt = DateTime.Now;
        this.Status = AccountStatus.Active;

    }





    public void Deposit(decimal amount)
    {
        if (amount <= 0 || Status != AccountStatus.Active)
        {

            throw new ArgumentException("Invalid deposit amount or account status.");
        }
        Balance += amount;
    }

    // Método Virtual: Saque (Pode ser sobrescrito por classes derivadas)
    public virtual void Withdraw(decimal amount)
    {
        if (Status != AccountStatus.Active || amount <= 0)
        {

            throw new InvalidOperationException("Invalid withdrawal operation.");
        }

        if (amount > Balance)
        {
            throw new InvalidOperationException("Insufficient balance.");
        }

        Balance -= amount;
    }

    public void ChangeStatus(AccountStatus newStatus)
    {
        if (newStatus == AccountStatus.Closed && Balance != 0)
        {
            throw new InvalidOperationException("A conta só pode ser fechada se o saldo for zero.");
        }
        this.Status = newStatus;
    }
}