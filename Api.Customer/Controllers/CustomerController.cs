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
            return Ok(new {customerId});
        }
    }
}