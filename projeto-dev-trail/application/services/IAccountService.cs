using System.Threading.Tasks;

public interface IAccountService
{

    Task<AccountOutputDto> GetAccountByNumberAsync(int accountNumber);

    Task<AccountOutputWithTransactionsDto> GetAccountWithTransactionsByNumberAsync(int accountNumber);

    Task AddNewAccountAsync(AccountInputDto account);

    Task<List<BankAccount>> GetAllAccountsAsync();

    Task<bool> checkIfClientExistsByIdAsync(int clientId);

    Task depositInAccountAsync(int accountNumber, decimal amount);
    Task withdrawFromAccountAsync(int accountNumber, decimal amount);

    Task<bool> checkIfAccountExistsByNumberAsync(int accountNumber);

    Task deleteAccountAsync(int accountNumber);
    // Task TransferBetweenAccountsAsync(int sourceAccountNumber, int destinationAccountNumber, decimal amount);
}