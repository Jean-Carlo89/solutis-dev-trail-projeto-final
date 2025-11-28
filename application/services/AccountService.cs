using BankSystem.API.model;
using BankSystem.API.Repositories;
using BankSystem.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

public class AccountService : IAccountService
{

    private readonly IAccountRepository accountRepository;
    private readonly ITransactionRepository _transactionRepository;
    public AccountService(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
    {

        this.accountRepository = accountRepository;
        this._transactionRepository = transactionRepository;
    }

    private BankAccount createAccountByType(AccountInputDto accountDto, int randomNumber, int clientId)
    {
        if (accountDto.Tipo == AccountType.Corrente)
        {
            return new CheckingAccount(accountDto, randomNumber,
                                       clientId);
        }
        else if (accountDto.Tipo == AccountType.Poupança)
        {
            return new SavingsAccount(accountDto, randomNumber,
                                       clientId);
        }
        else
        {
            throw new ArgumentException("Tipo de conta inválido.");
        }
    }

    public async Task AddNewAccountAsync(AccountInputDto accountDto)
    {

        Random random = new Random();
        int maxExclusive = 10000000;
        int randomNumber = random.Next(0, maxExclusive);



        BankAccount newAccount = createAccountByType(accountDto, randomNumber, accountDto.ClientId);

        BankAccountModel accountModel = BankAccountModelMapper.ToModel(newAccount);

        await this.accountRepository.AddNewAccountAsync(accountModel);
        await this.accountRepository.SaveDatabaseChangesAsync();
    }

    public async Task<bool> checkIfAccountExistsByNumberAsync(int accountNumber)
    {
        BankAccountModel account = await this.accountRepository.GetAccountByNumberAsync(accountNumber);
        if (account == null)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> checkIfClientExistsByIdAsync(int clientId)
    {
        return await this.accountRepository.checkIfClientExistsByIdAsync(clientId);
    }




    private async Task<BankAccountModel> PrivateGetAccountByNumberAsync(int accountNumber)
    {
        var accountModel = await this.accountRepository.GetAccountByNumberAsync(accountNumber);
        return accountModel;

    }



    public async Task<AccountOutputDto> GetAccountByNumberAsync(int accountNumber)
    {
        var accountModel = await this.PrivateGetAccountByNumberAsync(accountNumber);

        if (accountModel == null)
        {
            return null;

        }
        BankAccount accountEntity = BankAccountModelMapper.ToEntity(accountModel);


        AccountOutputDto account = BankAccountModelMapper.ToOutputDto(accountEntity);



        return account;
    }

    public async Task withdrawFromAccountAsync(int accountNumber, decimal amount)
    {
        BankAccountModel account = await this.accountRepository.GetAccountByNumberAsync(accountNumber);


        BankAccount accountEntity = BankAccountModelMapper.ToEntity(account);


        accountEntity.Withdraw(amount);


        account.Balance = accountEntity.Balance;

        var transaction = this.createTransaction(TransactionType.Withdrawal, amount, account.Id);

        await _transactionRepository.AddTransactionAsync(transaction);

        await this.accountRepository.SaveDatabaseChangesAsync();
    }

    public async Task depositInAccountAsync(int accountNumber, decimal amount)
    {

        BankAccountModel account = await this.accountRepository.GetAccountByNumberAsync(accountNumber);


        BankAccount accountEntity = BankAccountModelMapper.ToEntity(account);


        accountEntity.Deposit(amount);


        account.Balance = accountEntity.Balance;

        var transaction = this.createTransaction(TransactionType.Deposit, amount, account.Id);

        await _transactionRepository.AddTransactionAsync(transaction);

        await this.accountRepository.SaveDatabaseChangesAsync();
    }

    private TransactionModel createTransaction(TransactionType type, decimal amount, int sourceAccountId)
    {
        return new TransactionModel
        {
            Type = type,
            Amount = amount,
            CreatedAt = DateTime.UtcNow,
            SourceAccountId = sourceAccountId,
            DestinationAccountId = sourceAccountId
        };
    }

    public async Task deleteAccountAsync(int accountNumber)
    {

        //*****  Atualizar métodod de delete para fazer direto pelo Id da conta


        BankAccountModel account = await this.accountRepository.GetAccountByNumberAsync(accountNumber);

        await this.accountRepository.DeleteAccountAsync(account);
        await this.accountRepository.SaveDatabaseChangesAsync();

    }

    public async Task<List<BankAccount>> GetAllAccountsAsync()
    {
        List<BankAccountModel> accounts = await this.accountRepository.GetAllAccountsAsync();

        return BankAccountModelMapper.ToEntityList(accounts);
    }

    public async Task<AccountOutputWithTransactionsDto> GetAccountWithTransactionsByNumberAsync(int accountNumber)
    {
        var accountModel = await this.accountRepository.GetAccountByNumberWithTransactionsAsync(accountNumber);
        if (accountModel == null)
        {
            return null;
        }


        var account = BankAccountModelMapper.ToEntityWithTransactions(accountModel);


        AccountOutputWithTransactionsDto accountDto = BankAccountModelMapper.ToOutputWithTransactionsDto(account);

        return accountDto;
    }

    // public async Task TransferBetweenAccountsAsync(int sourceAccountNumber, int destinationAccountNumber, decimal amount)
    // {
    //     BankAccountModel sourceAccountModel = await this.repository.GetAccountByNumberAsync(sourceAccountNumber);
    //     BankAccountModel destinationAccountModel = await this.repository.GetAccountByNumberAsync(destinationAccountNumber);

    //     BankAccount sourceAccountEntity = BankAccountModelMapper.ToEntity(sourceAccountModel);
    //     BankAccount destinationAccountEntity = BankAccountModelMapper.ToEntity(destinationAccountModel);

    //     sourceAccountEntity.Withdraw(amount);
    //     destinationAccountEntity.Deposit(amount);

    //     sourceAccountModel.Balance = sourceAccountEntity.Balance;
    //     destinationAccountModel.Balance = destinationAccountEntity.Balance;

    //     await this.repository.SaveDatabaseChangesAsync();
    // }


}

