using BankCoreApi.Repositories.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers;

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
    public async Task<IActionResult> CountTransactionsByType()
    {
        var statistics = await _repository.CountByTypeAsync();
        if (!statistics.Any())
        {
            return NotFound("No record for statistics found");
        }
        return Ok(statistics);
    }

    [HttpGet("count-by-status")]
    public async Task<IActionResult> CountTransactionsByStatus()
    {
        var statistics = await _repository.CountByStatusAsync();
        if (!statistics.Any())
        {
            return NotFound("No record for statistics found");
        }
        return Ok(statistics);
    }

    [HttpGet("total-amount-by-type")]
    public async Task<IActionResult> TotalAmountByType()
    {
        var statistics = await _repository.TotalAmountByType();
        if (!statistics.Any())
        {
            return NotFound("No record for statistics found");
        }
        return Ok(statistics);
    }

    [HttpGet("total-amount-by-status")]
    public async Task<IActionResult> TotalAmountByStatus()
    {
        var statistics = await _repository.TotalAmountByStatus();
        if (!statistics.Any())
        {
            return NotFound("No record for statistics found");
        }
        return Ok(statistics);
    }
}