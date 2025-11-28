using BankSystem.API.Dtos;
using BankSystem.API.Repositories;

public class ClientService : IClientService
{

    private readonly IClientRepository repository;

    public ClientService(IClientRepository repository)
    {
        this.repository = repository;
    }

    public async Task<ClientOutputDto> GetClientByIdAsync(int clientId)
    {
        var clientModel = await this.repository.GetClientByIdAsync(clientId);

        if (clientModel == null)
        {
            return null;
        }

        var clientEntity = ClientModelMapper.ToEntity(clientModel);

        var clientDto = ClientModelMapper.ToOutputDto(clientEntity);

        return clientDto;
    }

    public async Task<ClientOutputDto> GetClientByCpfAsync(string cpf)
    {
        var clientModel = await this.repository.GetClientByCpfAsync(cpf);

        if (clientModel == null)
        {
            return null;
        }

        var clientEntity = ClientModelMapper.ToEntity(clientModel);

        var clientDto = ClientModelMapper.ToOutputDto(clientEntity);

        return clientDto;
    }

    private async Task<List<string>> CanCreateAccount(ClientInputDto client)
    {

        List<string> validationErrors = new List<string>();


        var existingClientByCpf = await this.repository.GetClientByCpfAsync(client.Cpf);
        if (existingClientByCpf != null)
        {

            validationErrors.Add($"Já existe um cliente com o CPF '{client.Cpf}'.");
        }


        var existingClientByEmail = await this.repository.GetClientByEmailAsync(client.email);
        if (existingClientByEmail != null)
        {

            validationErrors.Add($"Já existe um cliente com o Email '{client.email}'.");
        }



        if (validationErrors.Count > 0)
        {

            string combinedError = "Falha no cadastro:\n" + string.Join("\n", validationErrors);


            throw new ArgumentException(combinedError);
        }

        return validationErrors;
    }

    public async Task<ClientOutputDto> AddNewClientAsync(ClientInputDto client)
    {

        var validationErrors = await this.CanCreateAccount(client);

        if (validationErrors.Count > 0)
        {

            string combinedError = "Falha no cadastro:\n" + string.Join("\n", validationErrors);


            throw new ArgumentException(combinedError);
        }



        Client newClient = new Client(client.Nome, client.email, client.Cpf, null);
        ClientModel newClientModel = ClientModelMapper.ToModel(newClient);
        var clientDto = ClientModelMapper.ToOutputDto(newClient);
        await this.repository.AddNewClientAsync(newClientModel);
        await this.repository.SaveDatabaseChangesAsync();
        return clientDto;
    }


}