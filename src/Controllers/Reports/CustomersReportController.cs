using BankCoreApi.Models.Accounts;
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
            [FromQuery] DateTime endDate, string format="")
        {
            var customers = await _repository.GetAllAsync(startDate, endDate);
            if (!customers.Any())
            {
                return NotFound("No records found for this range");
            }
            return Ok(customers);
        }


        [HttpGet("active-customers")]
        public async Task<IActionResult> GetActiveCustomers([FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, string format="")
        {

            var customers = await _repository.GetCustomerByStatusAsync("Active", startDate, endDate);
            if (!customers.Any())
            {
                return NotFound("No records found for this range");
            }
            return Ok(customers);
        }


        [HttpGet("customers-by-status")]
        public async Task<IActionResult> GetCustomersByStatus([FromQuery] string status, [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, string format="")
        {
            var customers = await _repository.GetCustomerByStatusAsync(status, startDate, endDate);
            if (!customers.Any())
            {
                return NotFound("No records found for this range");
            }
            return Ok(customers);
        }


        [HttpGet("customers-by-type")]
        public async Task<IActionResult> GetCustomersByType([FromQuery] string type, [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, string format="")
        {
            var customers = await _repository.GetCustomerByTypeAsync(type, startDate, endDate);
            if (!customers.Any())
            {
                return NotFound("No records found for this range");
            }
            return Ok(customers);
        }


        [HttpGet("customers-by-gender")]
        public async Task<IActionResult> GetCustomersByGender([FromQuery] string gender, [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, string format="")
        {
            var customers = await _repository.GetCustomerByGenderAsync(gender, startDate, endDate);
            if (!customers.Any())
            {
                return NotFound("No records found for this range");
            }
            return Ok(customers);
        }

    }
}