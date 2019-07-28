using System.Threading.Tasks;
using Api.Customer.Controllers;
using Api.Customer.Repository;
using Api.Customer.Service;
using NUnit.Framework;

namespace Api.Customer.UnitTests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        [SetUp]
        public void Initialise()
        {
            var customerDbConnectionFactory = new CustomerSQLiteDatabase(@"Data Source=.\customerDb.sqlite;Version=3;");
            var customerRepository = new CustomerRepository(customerDbConnectionFactory);
            _customerService = new CustomerService(customerRepository);
        }

        private ICustomerService _customerService;

        [Test]
        public async Task GivenCustomerDetailsCheckCustomerIsCreated()
        {
            var customerController = new CustomerController(_customerService);

            var createCustomerDto = new CustomerControllerDto.RequestCreate
            {
                Forename = "John",
                Surname = "Doe",
                EmailAddress = "test@testemail.com",
                Password = "$ecur3P@££4"
            };

            var createdCustomerId = await customerController.CreateCustomer(createCustomerDto);

            Assert.IsTrue(createdCustomerId > 0);
        }

        [Test]
        public async Task GivenCreatedCustomerWhenGetByCustomerIdCheckReturnsDetails()
        {
            var customerController = new CustomerController(_customerService);

            var createCustomerDto = new CustomerControllerDto.RequestCreate
            {
                Forename = "John",
                Surname = "Doe",
                EmailAddress = "test@testemail.com",
                Password = "$ecur3P@££4"
            };

            var createdCustomerId = await customerController.CreateCustomer(createCustomerDto);

            Assert.IsTrue(createdCustomerId > 0);

            var customerResponse = await customerController.Get(createdCustomerId);

            Assert.IsNotNull(customerResponse);
            Assert.AreEqual(createCustomerDto.Forename, customerResponse.Forename);
            Assert.AreEqual(createCustomerDto.Surname, customerResponse.Surname);
            Assert.AreEqual(createCustomerDto.EmailAddress, customerResponse.EmailAddress);
        }

        [Test]
        public async Task GivenCreatedCustomerWhenCustomerLoginCheckReturnsDetails()
        {
            var customerController = new CustomerController(_customerService);

            var createCustomerDto = new CustomerControllerDto.RequestCreate
            {
                Forename = "John",
                Surname = "Doe",
                EmailAddress = "test@testemail.com",
                Password = "$ecur3P@££4"
            };

            var createdCustomerId = await customerController.CreateCustomer(createCustomerDto);

            Assert.IsTrue(createdCustomerId > 0);

            var loginCustomerDto = new CustomerControllerDto.RequestLogin
            {
                CustomerId= createdCustomerId,
                Password = "$ecur3P@££4"
            };
            var customerResponse = await customerController.CustomerLogin(loginCustomerDto);

            Assert.IsNotNull(customerResponse);
            Assert.AreEqual(createCustomerDto.Forename, customerResponse.Forename);
            Assert.AreEqual(createCustomerDto.Surname, customerResponse.Surname);
            Assert.AreEqual(createCustomerDto.EmailAddress, customerResponse.EmailAddress);
        }

        [Test]
        public async Task GivenCreatedCustomerAndAmendedCustomerWhenGetByCustomerIdCheckReturnsDetails()
        {
            var customerController = new CustomerController(_customerService);

            var createCustomerDto = new CustomerControllerDto.RequestCreate
            {
                Forename = "John",
                Surname = "Doe",
                EmailAddress = "test@testemail.com",
                Password = "$ecur3P@££4"
            };

            var createdCustomerId = await customerController.CreateCustomer(createCustomerDto);

            Assert.IsTrue(createdCustomerId > 0);

            var updateCustomerDto = new CustomerControllerDto.RequestUpdate
            {
                CustomerId = createdCustomerId,
                Forename = "John",
                Surname = "Doe",
                EmailAddress = "test@testemail.com",
                Password = "$ecur3P@££4"
            };
            await customerController.UpdateCustomer(updateCustomerDto);

            var customerResponse = await customerController.Get(createdCustomerId);

            Assert.IsNotNull(customerResponse);
            Assert.AreEqual(createCustomerDto.Forename, customerResponse.Forename);
            Assert.AreEqual(createCustomerDto.Surname, customerResponse.Surname);
            Assert.AreEqual(createCustomerDto.EmailAddress, customerResponse.EmailAddress);
        }

    }
}