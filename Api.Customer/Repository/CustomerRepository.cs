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