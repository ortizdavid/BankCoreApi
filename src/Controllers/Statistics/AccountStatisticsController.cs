using BankCoreApi.Repositories.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountStatisticsController : ControllerBase
    {
        private readonly AccountStatisticsRepository _repository;
        
        public AccountStatisticsController(AccountStatisticsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("count-by-type")]
        public async Task<IActionResult> CountAccountsByType([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }

        [HttpGet("count-by-status")]
        public async Task<IActionResult> CountAccountsByStatus([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }

        
        [HttpGet("count-by-gender")]
        public async Task<IActionResult> CountAccountsByGender([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }


        [HttpGet("count-by-age")]
        public async Task<IActionResult> CountAccountsByAge([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }

    }
}