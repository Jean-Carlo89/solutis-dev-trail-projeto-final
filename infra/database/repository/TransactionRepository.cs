using BankSystem.API.model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BankSystem.API.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankContext _context;

        public TransactionRepository(BankContext context)
        {
            _context = context;
        }

        public async Task AddTransactionAsync(TransactionModel transaction)
        {
            await _context.Transactions.AddAsync(transaction);
        }

        public async Task<List<TransactionModel>> GetTransactionsByAccountIdAsync(int accountId)
        {


            return await _context.Transactions
                .Where(t => t.SourceAccountId == accountId)
                .Include(t => t.SourceAccount)
                .Include(t => t.DestinationAccount)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

    }
}