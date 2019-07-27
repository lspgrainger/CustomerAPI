using System.Threading.Tasks;

namespace Api.Customer.Repository
{
    public interface ICustomerRepository
    {
        Task<int> CreateCustomer(Domain.Customer customer);

        Task UpdateCustomer(Domain.Customer customer);

        Task<Domain.Customer> GetCustomer(int customerId);
    }
}