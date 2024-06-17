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
        public async Task<IActionResult> CountAccountsByType()
        {
            var statistics = await _repository.CountByTypeAsync();
            if (!statistics.Any())
            {
                return NotFound("No record for statistics found");
            }
            return Ok(statistics);
        }


        [HttpGet("count-by-status")]
        public async Task<IActionResult> CountAccountsByStatus()
        {
            var statistics = await _repository.CountByTypeAsync();
            if (!statistics.Any())
            {
                return NotFound("No record for statistics found");
            }
            return Ok(statistics);
        }

        
        [HttpGet("count-by-gender")]
        public async Task<IActionResult> CountAccountsByGender()
        {
            var statistics = await _repository.CountByGenderAsync();
            if (!statistics.Any())
            {
                return NotFound("No record for statistics found");
            }
            return Ok(statistics);
        }


        [HttpGet("count-by-age")]
        public async Task<IActionResult> CountAccountsByAge()
        {
            var statistics = await _repository.CountByAgeAsync();
            if (!statistics.Any())
            {
                return NotFound("No record for statistics found");
            }
            return Ok(statistics);
        }

    }
}