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
        public async Task<IActionResult> CountCustomersByType()
        {
            var statistics = await _repository.CountByTypeAsync();
            if (!statistics.Any())
            {
                return NotFound("No record for statistics found");
            }
            return Ok(statistics);
        }
        
        [HttpGet("count-by-status")]
        public async Task<IActionResult> CountCustomersByStatus()
        {
            var statistics = await _repository.CountByStatusAsync();
            if (!statistics.Any())
            {
                return NotFound("No record for statistics found");
            }
            return Ok(statistics);
        }

        [HttpGet("total-balance-by-type")]
        public async Task<IActionResult> TotalBalanceByType()
        {
            var statistics = await _repository.TotalBalanceByTypeAsync();
            if (!statistics.Any())
            {
                return NotFound("No record for statistics found");
            }
            return Ok(statistics);
        }

        [HttpGet("total-balance-by-status")]
        public async Task<IActionResult> TotalBalanceByStatus()
        {
            var statistics = await _repository.TotalBalanceByStatusAsync();
            if (!statistics.Any())
            {
                return NotFound("No record for statistics found");
            }
            return Ok(statistics);
        }
    }

}