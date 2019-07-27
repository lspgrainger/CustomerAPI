using System.Threading.Tasks;

namespace Api.Customer.Service
{
    public interface ICustomerService
    {
        Task<int> CreateCustomer(Domain.Customer customer);

        Task UpdateCustomer(Domain.Customer customer);

        Task<Domain.Customer> GetCustomer(int customerId);

        Task<Domain.Customer> GetCustomerWithValidatedPassword(int customerId, string password);
    }
}