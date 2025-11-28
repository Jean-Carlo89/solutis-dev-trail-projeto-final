

// using System;
// using System.Collections.Generic;
// using BankSystem.Domain.Entities;


// public class Account
// {

//     public int Id { get; private set; }
//     public int Number { get; private set; }
//     public string Holder { get; private set; }
//     public decimal Balance { get; private set; }
//     public AccountType Type { get; private set; }
//     public AccountStatus Status { get; private set; }
//     public DateTime CreatedAt { get; private set; }
//     public int ClientId { get; private set; }

//     public List<Transaction> Transactions { get; private set; }




//     public BankAccount(int id, int number, decimal balance, AccountType type, string holder, DateTime createdAt, AccountStatus status, int clientId, List<Transaction> transactions)
//     {

//         this.Id = id;
//         this.Number = number;
//         this.Balance = balance;
//         this.Type = type;
//         this.Holder = holder;
//         this.CreatedAt = createdAt;
//         this.Status = status;
//         this.ClientId = clientId;
//         this.Balance = Balance;
//         this.CreatedAt = createdAt != default(DateTime) ? createdAt : DateTime.Now;
//         this.Status = AccountStatus.Active;
//         this.Transactions = transactions;

//     }

//     public BankAccount(int id, int number, decimal balance, AccountType type, string holder, DateTime createdAt, AccountStatus status, int clientId)
//     {

//         this.Id = id;
//         this.Number = number;
//         this.Balance = balance;
//         this.Type = type;
//         this.Holder = holder;
//         this.CreatedAt = createdAt;
//         this.Status = status;
//         this.ClientId = clientId;
//         this.Balance = Balance;
//         this.CreatedAt = createdAt != default(DateTime) ? createdAt : DateTime.Now;
//         this.Status = AccountStatus.Active;


//     }



//     public BankAccount(AccountInputDto dto, int Number, int clientId)
//     {

//         this.Number = Number;
//         this.Balance = dto.Saldo;
//         this.Type = dto.Tipo;
//         this.Holder = dto.Titular;
//         this.ClientId = clientId;
//         this.Balance = dto.Saldo;
//         this.CreatedAt = DateTime.Now;
//         this.Status = AccountStatus.Active;

//     }


//     public void Deposit(decimal amount)
//     {
//         if (amount <= 0)
//         {
//             throw new ArgumentException("O valor do depósito deve ser positivo.");
//         }
//         if (Status != AccountStatus.Active)
//         {
//             throw new InvalidOperationException("Não é possível depositar em conta inativa.");
//         }

//         Balance += amount;
//     }

//     public void Withdraw(decimal amount)
//     {
//         if (Status != AccountStatus.Active)
//         {
//             throw new InvalidOperationException("Não é possível sacar de uma conta inativa.");
//         }

//         if (amount <= 0)
//         {
//             throw new ArgumentException("O valor do saque deve ser positivo.");
//         }

//         if (amount > Balance)
//         {

//             throw new InvalidOperationException("Saldo insuficiente para realizar o saque.");
//         }

//         Balance -= amount;
//     }

//     public void ChangeStatus(AccountStatus newStatus)
//     {
//         if (newStatus == AccountStatus.Closed && Balance != 0)
//         {
//             throw new InvalidOperationException("A conta só pode ser fechada se o saldo for zero.");
//         }
//         this.Status = newStatus;
//     }
// }