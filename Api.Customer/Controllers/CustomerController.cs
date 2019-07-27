using System.Threading.Tasks;
using Api.Customer.Repository;
using Api.Customer.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService, ICustomerRepository customerRepository)
        {
            _customerService = customerService;
            _customerRepository = customerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Domain.Customer customer)
        {
            var customerId = await _customerService.CreateCustomer(customer);
            return Ok(new { customerId });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] Domain.Customer customer)
        {
            await _customerService.UpdateCustomer(customer);
            return Ok();
        }

        [HttpGet]
        [Route("customerId")]
        public async Task<ActionResult<string>> Get(int customerId)
        {
            var customer = await _customerService.GetCustomer(customerId);
            return Ok(customer);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> CustomerLogin([FromBody] LoginCredentials customer)
        {
            var myCustomer = await _customerService.GetCustomerWithValidatedPassword(customer.CustomerId, customer.Password);
            return Ok(myCustomer);
        }

        public class LoginCredentials
        {
            public int CustomerId { get; set; }
            public string Password { get; set; }
        }
    }
}