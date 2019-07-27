using System.Linq;
using System.Threading.Tasks;
using Core.Sql;
using Dapper;

namespace Api.Customer.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public CustomerRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> CreateCustomer(Domain.Customer customer)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync<int>(
                    @"
                    INSERT INTO Customer(Forename, Surname, EmailAddress, Password)
                    VALUES(@Forename, @Surname, @EmailAddress, @Password)

                    SELECT SCOPE_IDENTITY() AS NewCustomerId;",
                    new
                    {
                        customer.Forename,
                        customer.Surname,
                        customer.EmailAddress,
                        customer.Password
                    }).ConfigureAwait(false);

                return result;
            }
        }

        public async Task UpdateCustomer(Domain.Customer customer)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var results = await connection.ExecuteAsync(@"
                    UPDATE Customer
                    SET Forename = @Forename, Surname = @Surname, EmailAddress = @EmailAddress, Password=@Password
                    WHERE CustomerId = @CustomerId;
                   ",
                    new
                    {
                        customer.CustomerId,
                        customer.Forename,
                        customer.Surname,
                        customer.EmailAddress,
                        customer.Password
                    });
            }
        }

        public async Task<Domain.Customer> GetCustomer(int customerId)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var results = await connection.QueryAsync<Domain.Customer>(@"
                    SELECT CustomerID, Forename, Surname, EmailAddress, Password
                    FROM Customer
                    WHERE CustomerID = @customerId
                   ", new
                {
                    customerId
                });

                var customer = results.FirstOrDefault();

                return customer;
            }
        }
    }
}