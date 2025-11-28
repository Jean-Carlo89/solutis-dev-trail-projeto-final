using BankSystem.API.Dtos;
using BankSystem.API.model;
using System.Linq;



public static class ClientModelMapper
{

    public static ClientModel ToModel(Client entity)
    {
        if (entity == null)
        {
            return null;
        }

        return new ClientModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            DateOfBirth = entity.DateOfBirth,
            Cpf = entity.Cpf,
            Accounts = entity.Accounts?
                         .Select(BankAccountModelMapper.ToModel)
                         .ToList()


        };
    }


    public static Client ToEntity(ClientModel model)
    {
        if (model == null)
        {
            return null;
        }


        var entity = new Client(
            Id: model.Id,
            Name: model.Name,
            Email: model.Email,
            DateOfBirth: model.DateOfBirth



        );


        // entity.Accounts = model.Accounts.ForEach(account => { BankAccountModelMapper.ToEntity(account); });
        entity.Accounts = model.Accounts?
                               .Select(account => BankAccountModelMapper.ToEntity(account))
                               .ToList();

        return entity;
    }


    public static ClientOutputDto ToOutputDto(Client entity)
    {
        if (entity == null)
        {
            return null;
        }

        return new ClientOutputDto
        {
            Id = entity.Id,
            Nome = entity.Name,
            Email = entity.Email,
            DOB = entity.DateOfBirth,
            Contas = entity.Accounts?
                         .Select(BankAccountModelMapper.ToOutputDto)
                         .ToList()



        };
    }
}