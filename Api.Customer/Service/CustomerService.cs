using Api.Customer.Repository;

namespace Api.Customer.Service
{
    public class CustomerService:ICustomerService
    {
        public Domain.Customer GetCustomer(int customerId)
        {
            var customerRepository = new CustomerRepository();
            var customer = customerRepository.GetCustomer(customerId);
            return customer;
        }
    }
}