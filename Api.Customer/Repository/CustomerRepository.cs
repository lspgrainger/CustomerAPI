namespace Api.Customer.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public Domain.Customer GetCustomer(int customerId)
        {
            var customer = new Domain.Customer
            {
                CustomerId = 1, Forename = "Liam", Surname = "Grainger", Password = "Password1234"
            };

            return customer;
        }
    }
}