using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Api.Customer.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateCustomer(Domain.Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
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
            using (var connection = new SqlConnection(_connectionString))
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
            using (var connection = new SqlConnection(_connectionString))
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