using System.Threading.Tasks;

namespace Api.Customer.Service
{
    public interface ICustomerService
    {
        //        Domain.Customer GetCustomer(int customerId);
        Task<Domain.Customer> GetCustomer(int customerId);
    }
}