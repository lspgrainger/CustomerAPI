namespace Api.Customer.Service
{
    public interface ICustomerService
    {
        Domain.Customer GetCustomer(int customerId);
    }
}