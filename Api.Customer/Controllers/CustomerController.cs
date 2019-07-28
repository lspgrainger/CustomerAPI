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
        public async Task<IActionResult> CreateCustomer([FromBody] RequestCreate customer)
        {
            var customerId = await _customerService.CreateCustomer(MapToDomain(customer));
            return Ok(new { customerId });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] RequestUpdate customer)
        {
            await _customerService.UpdateCustomer(MapToDomain(customer));
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
        public async Task<ActionResult<string>> CustomerLogin([FromBody] RequestLogin customer)
        {
            var myCustomer = await _customerService.LoginCustomer(customer.CustomerId, customer.Password);
            return Ok(myCustomer);
        }
    }
}