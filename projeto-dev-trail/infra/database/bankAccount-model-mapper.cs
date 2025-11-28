using BankSystem.API.Mappers;
using BankSystem.API.model;
public static class BankAccountModelMapper
{


    public static BankAccountModel ToModel(BankAccount entity)
    {
        if (entity == null)
        {
            return null;
        }


        return new BankAccountModel
        {

            Id = entity.Id,
            Number = entity.Number,
            Holder = entity.Holder,
            Balance = entity.Balance,
            CreatedAt = entity.CreatedAt,
            Type = entity.Type,
            Status = entity.Status,
            ClientId = entity.ClientId,
            Transactions = TransactionMapper.ToModelList(entity.Transactions?.ToList()),

        };
    }

    public static List<BankAccountModel> ToModel(List<BankAccount> entities)
    {
        if (entities == null)
        {
            return null;
        }

        return entities.Select(ToModel).ToList();
    }


    public static BankAccount ToEntity(BankAccountModel model)
    {
        if (model == null)
        {
            return null;
        }



        switch (model.Type)
        {
            case AccountType.Corrente:
                return new CheckingAccount(
                    id: model.Id,
                    number: model.Number,
                    balance: model.Balance,
                    type: model.Type,
                    holder: model.Holder,
                    createdAt: model.CreatedAt,
                    status: model.Status,
                    clientId: model.ClientId,
                    transactions: TransactionMapper.ToEntityList(model.Transactions?.ToList())
                );

            case AccountType.Poupança:
                return new SavingsAccount(
                    id: model.Id,
                    number: model.Number,
                    balance: model.Balance,
                    type: model.Type,
                    holder: model.Holder,
                    createdAt: model.CreatedAt,
                    status: model.Status,
                    clientId: model.ClientId,
                transactions: TransactionMapper.ToEntityList(model.Transactions?.ToList())
                );

            default:

                throw new NotSupportedException($"Account type '{model.Type}' is not supported for mapping.");
        }


        // return new BankAccount(
        //     id: model.Id,
        //     number: model.Number,
        //     balance: model.Balance,
        //     type: model.Type,
        //     holder: model.Holder,
        //     createdAt: model.CreatedAt,
        //     status: model.Status,
        //     clientId: model.ClientId
        // // transactions: TransactionMapper.ToEntityList(model.Transactions.ToList())
        // );


    }


    public static BankAccount ToEntityWithTransactions(BankAccountModel model)
    {
        if (model == null)
        {
            return null;
        }


        var mappedAccount = ToEntity(model);
        // var mappedAccount = new BankAccount(
        //             id: model.Id,
        //             number: model.Number,
        //             balance: model.Balance,
        //             type: model.Type,
        //             holder: model.Holder,
        //             createdAt: model.CreatedAt,
        //             status: model.Status,
        //             clientId: model.ClientId,
        //             transactions: TransactionMapper.ToEntityList(model.Transactions.ToList())
        //         );
        return mappedAccount;


    }

    public static List<BankAccount> ToEntityList(List<BankAccountModel> models)
    {

        if (models == null)
        {
            return null;
        }

        var mappedList = models

                .Select(model => ToEntity(model))
                .ToList();

        return mappedList;



    }

    public static AccountOutputDto ToOutputDto(BankAccount entity)
    {
        if (entity == null)
        {
            return null;
        }

        return new AccountOutputDto
        {

            Numero = entity.Number,
            Saldo = entity.Balance,
            Titular = entity.Holder,


            Tipo = entity.Type.ToString()
        };
    }



    public static List<AccountOutputDto> ToOutputDto(List<BankAccount> entities)
    {
        if (entities == null)
        {
            return null;
        }


        return entities.Select(ToOutputDto).ToList();
    }

    public static AccountOutputWithTransactionsDto ToOutputWithTransactionsDto(BankAccount entity)
    {

        if (entity == null)
        {
            return null;
        }

        return new AccountOutputWithTransactionsDto
        {
            DetalhesDaConta = ToOutputDto(entity),
            Transacoes = TransactionMapper.ToOutputDtoList(entity.Transactions?.ToList())
        };
    }
}