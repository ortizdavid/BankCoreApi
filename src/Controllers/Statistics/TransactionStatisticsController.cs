using BankCoreApi.Repositories.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionStatisticsController : ControllerBase
    {
        private readonly TransactionStatisticsRepository _repository;
        
        public TransactionStatisticsController(TransactionStatisticsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("count-by-type")]
        public async Task<IActionResult> CountTransactionsByType([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }

        [HttpGet("count-by-age")]
        public async Task<IActionResult> CountTransactionsByAge([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }

        [HttpGet("count-by-gender")]
        public async Task<IActionResult> CountTransactionsByGender([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }


        [HttpGet("total-by-type")]
        public async Task<IActionResult> TotalAmountByType([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }


        [HttpGet("total-by-status")]
        public async Task<IActionResult> TotalAmountByStatus([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }
    }
}