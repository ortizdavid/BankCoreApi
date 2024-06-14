using BankCoreApi.Repositories.Reports;
using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersReportController : ControllerBase
    {
        private readonly CustomersReportRepository _repository;

        public CustomersReportController(CustomersReportRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("all-customers")]
        public async Task<IActionResult> GetAllCustomers([FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, [FromQuery] string format)
        {
            return Ok();
        }


        [HttpGet("active-customers")]
        public async Task<IActionResult> GetActiveCustomers([FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, [FromQuery] string format)
        {
            return Ok();
        }


        [HttpGet("customers-by-status")]
        public async Task<IActionResult> GetCustomersByStatus([FromQuery] int statusId, [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, [FromQuery] string format)
        {
            return Ok();
        }


        [HttpGet("customers-by-type")]
        public async Task<IActionResult> GetCustomersByType([FromQuery] int typeId, [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, [FromQuery] string format)
        {
            return Ok();
        }


        [HttpGet("customers-by-gender")]
        public async Task<IActionResult> GetCustomersByGender([FromQuery] string gender, [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, [FromQuery] string format)
        {
            return Ok();
        }

    }
}