using System.Threading.Tasks;
using BankSystem.API.Dtos;

public interface IClientService
{

    Task<ClientOutputDto> GetClientByIdAsync(int clientId);

    Task<ClientOutputDto> GetClientByCpfAsync(string cpf);

    Task<ClientOutputDto> AddNewClientAsync(ClientInputDto client);


}