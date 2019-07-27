using System.Threading.Tasks;

namespace Api.Customer.Repository
{
    public interface ICustomerRepository
    {
        Task<Domain.Customer> GetCustomer(int customerId);
    }
}