using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Api.Customer.Repository
{
    public interface ICustomerRepository
    {
        Domain.Customer GetCustomer(int customerId);
    }
}
