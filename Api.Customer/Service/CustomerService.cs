using System.Threading.Tasks;
using Api.Customer.Domain;
using Api.Customer.Repository;

namespace Api.Customer.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<int> CreateCustomer(Domain.Customer customer)
        {
            HashCustomerPassword(customer);
            return _customerRepository.CreateCustomer(customer);
        }

        public Task UpdateCustomer(Domain.Customer customer)
        {
            HashCustomerPassword(customer);
            return _customerRepository.UpdateCustomer(customer);
        }

        public Task<Domain.Customer> GetCustomer(int customerId)
        {
            var customer = _customerRepository.GetCustomer(customerId);
            return customer;
        }

        private static void HashCustomerPassword(Domain.Customer customer)
        {
            var salt = Hashing.GetRandomSalt();
            var hashedPassword = Hashing.HashPassword(customer.Password, salt);
            customer.Password = hashedPassword;
        }

    }
}