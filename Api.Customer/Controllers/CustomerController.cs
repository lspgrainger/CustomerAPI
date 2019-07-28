using System.Threading.Tasks;
using Api.Customer.Repository;
using Api.Customer.Service;
using Microsoft.AspNetCore.Mvc;
using static Api.Customer.Controllers.CustomerControllerDto;

namespace Api.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<int> CreateCustomer([FromBody] RequestCreate customer)
        {
            var customerId = await _customerService.CreateCustomer(MapToDomain(customer));
            return customerId;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] RequestUpdate customer)
        {
            await _customerService.UpdateCustomer(MapToDomain(customer));
            return Ok();
        }

        [HttpGet]
        [Route("customerId")]
        public async Task<Domain.Customer> Get(int customerId)
        {
            var customer = await _customerService.GetCustomer(customerId);
            return customer;
        }

        [HttpPost]
        [Route("login")]
        public async Task<Domain.Customer> CustomerLogin([FromBody] RequestLogin customer)
        {
            var myCustomer = await _customerService.LoginCustomer(customer.CustomerId, customer.Password);
            return myCustomer;
        }
    }
}