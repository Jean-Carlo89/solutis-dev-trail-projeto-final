using BankSystem.API.model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BankSystem.API.Repositories
{
    public interface IAccountRepository
    {
        Task<BankAccountModel> GetAccountByNumberAsync(int numero);
        Task<List<BankAccountModel>> GetAllAccountsAsync();
        Task<bool> checkIfClientExistsByIdAsync(int clientId);
        Task AddNewAccountAsync(BankAccountModel account);
        Task UpdateAccountAsync(BankAccountModel account);
        Task DeleteAccountAsync(BankAccountModel account);
        Task SaveDatabaseChangesAsync();

        Task<BankAccountModel> GetAccountByNumberWithTransactionsAsync(int accountNumber);
    }
}