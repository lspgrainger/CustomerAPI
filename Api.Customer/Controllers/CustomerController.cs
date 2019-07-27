using Api.Customer.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        [Route("customerId")]
        public ActionResult<string> Get(int customerId)
        {
            var customerService = new CustomerService();

            var customer = customerService.GetCustomer(customerId);

            return Ok(customer);
        }
    }
}