using BankSystem.API.model;
using System.Threading.Tasks;

namespace BankSystem.API.Repositories
{
    public interface ITransactionRepository
    {
        Task AddTransactionAsync(TransactionModel transaction);


        Task<List<TransactionModel>> GetTransactionsByAccountIdAsync(int accountId);


    }
}