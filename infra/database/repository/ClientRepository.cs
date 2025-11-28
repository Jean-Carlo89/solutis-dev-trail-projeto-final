using BankSystem.API.model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BankSystem.API.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly BankContext _context;


        public ClientRepository(BankContext context)
        {
            _context = context;
        }

        public async Task<ClientModel> GetClientByIdAsync(int Id)
        {

            return await _context.Clients
                                 .Include(c => c.Accounts)
                                 .FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<ClientModel> GetClientByCpfAsync(string cpf)
        {

            return await _context.Clients
                                 .Include(c => c.Accounts)
                                 .FirstOrDefaultAsync(c => c.Cpf == cpf);
        }

        public async Task<ClientModel> GetClientByEmailAsync(string email)
        {

            return await _context.Clients
                                 .Include(c => c.Accounts)
                                 .FirstOrDefaultAsync(c => c.Email == email);
        }





        public async Task AddNewClientAsync(ClientModel client)
        {
            await _context.Clients.AddAsync(client);
        }



        public async Task SaveDatabaseChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}