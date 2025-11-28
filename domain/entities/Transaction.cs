using System;
using System.Collections.Generic;
using BankSystem.API.model;

namespace BankSystem.Domain.Entities
{


    public class Transaction
    {
        public int Id { get; private set; }
        public TransactionType Type { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime CreatedAt { get; private set; }


        public int? SourceAccountId { get; private set; }
        public int? DestinationAccountId { get; private set; }


        public Transaction(TransactionType type, decimal value, int sourceAccountId, int? destinationAccountId = null)
        {
            if (type == TransactionType.TransferOut && destinationAccountId == null)
            {
                throw new ArgumentException("TransferOut requires DestinationAccountId.");
            }
            if (type == TransactionType.TransferIn && sourceAccountId == null)
            {
                throw new ArgumentException("TransferIn requires SourceAccountId.");
            }

            this.Type = type;
            this.Amount = value;
            this.CreatedAt = DateTime.UtcNow;
            this.SourceAccountId = sourceAccountId;
            this.DestinationAccountId = destinationAccountId;
        }

        public Transaction(TransactionModel model)
        {


            this.Type = model.Type;
            this.Amount = model.Amount;
            this.Id = model.Id;
            this.CreatedAt = model.CreatedAt;
            this.SourceAccountId = model.SourceAccountId;
            this.DestinationAccountId = model.DestinationAccountId;
        }

    }
}