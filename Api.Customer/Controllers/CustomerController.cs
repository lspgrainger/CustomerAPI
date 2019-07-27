using Api.Customer.Repository;
using Api.Customer.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerService customerService, ICustomerRepository customerRepository)
        {
            _customerService = customerService;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        [Route("customerId")]
        public ActionResult<string> Get(int customerId)
        {
            var customer = _customerService.GetCustomer(customerId);

            return Ok(customer.Result);
        }
    }
}