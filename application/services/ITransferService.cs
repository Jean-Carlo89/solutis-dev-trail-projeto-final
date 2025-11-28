using System.Threading.Tasks;

namespace BankSystem.API.Services
{
    public interface ITransferService
    {
        Task ExecuteTransferAsync(int sourceAccountNumber, int destinationAccountNumber, decimal amount);
    }
}