using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("It works");
        }

    }
}