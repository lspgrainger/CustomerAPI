using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Api.Customer.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public CustomerRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateCustomer(Domain.Customer customer)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<int>(
                    @"
                    INSERT INTO Customer(Forename, Surname, EmailAddress, Password)
                    VALUES(@Forename, @Surname, @EmailAddress, @Password);

                    SELECT last_insert_rowid()",
                    new
                    {
                        customer.Forename,
                        customer.Surname,
                        customer.EmailAddress,
                        customer.Password
                    });

                return result.FirstOrDefault();
            }
        }

        public async Task UpdateCustomer(Domain.Customer customer)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
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
            using (var connection = _dbConnectionFactory.CreateConnection())
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