using BankSystem.API.model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BankSystem.API.Repositories
{
    public interface IClientRepository
    {
        Task<ClientModel> GetClientByIdAsync(int numero);

        Task<ClientModel> GetClientByEmailAsync(string email);

        Task<ClientModel> GetClientByCpfAsync(string cpf);
        //Task<bool> ClientExistsAsync(int clientId);
        Task AddNewClientAsync(ClientModel account);
        // Task UpdateAsync(ClientModel account);
        //  Task DeleteAsync(ClientModel account);
        Task SaveDatabaseChangesAsync();
    }
}