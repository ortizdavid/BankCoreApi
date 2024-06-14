using BankCoreApi.Repositories.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerStatisticsController : ControllerBase
    {
        private readonly CustomerStatisticsRepository _repository;

        public CustomerStatisticsController(CustomerStatisticsRepository repository)
        {
            _repository = repository;
        }

    
        [HttpGet("count-by-type")]
        public async Task<IActionResult> CountCustomersByType([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }

        [HttpGet("count-by-age")]
        public async Task<IActionResult> CountCustomersByAge([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }

        [HttpGet("count-by-gender")]
        public async Task<IActionResult> CountCustomersByGender([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }


        [HttpGet("total-by-type")]
        public async Task<IActionResult> TotalBalanceByType([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }


        [HttpGet("total-by-status")]
        public async Task<IActionResult> TotalBalanceByStatus([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }

    }
}