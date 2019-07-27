using Api.Customer.Repository;

namespace Api.Customer.Service
{
    public class CustomerService:ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public Domain.Customer GetCustomer(int customerId)
        {
            var customer = _customerRepository.GetCustomer(customerId);
            return customer;
        }
    }
}