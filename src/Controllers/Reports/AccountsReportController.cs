using BankCoreApi.Repositories.Reports;
using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsReportController : ControllerBase
    {
        private readonly AccountsReportRepository _repository;

        public AccountsReportController(AccountsReportRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("all-accounts")]
        public async Task<IActionResult> GeAllAccounts([FromQuery] DateTime startDate,
             [FromQuery] DateTime endDate, string format="")
        {
            var accounts = await _repository.GetAllAsync(startDate, endDate);
            if (!accounts.Any())
            {
                return NotFound("No records found for this range");
            }
            return Ok(accounts);
        }


        [HttpGet("active-accounts")]
        public async Task<IActionResult> GetActiveAccounts([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, string format="")
        {
            var accounts = await _repository.GetByAccountStatusAsync("Active", startDate, endDate);
            if (!accounts.Any())
            {
                return NotFound("No records found for this range");
            }
            return Ok(accounts);
        }

        
        [HttpGet("accounts-by-status")]
        public async Task<IActionResult> GetAccountsByStatus([FromQuery] string status, 
            [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, string format="")
        {
             var accounts = await _repository.GetByAccountStatusAsync(status, startDate, endDate);
            if (!accounts.Any())
            {
                return NotFound("No records found for this range");
            }
            return Ok(accounts);
        }


        [HttpGet("accounts-by-type")]
        public async Task<IActionResult> GetAccountsByType([FromQuery] string type, 
            [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, string format="")
        {
            var accounts = await _repository.GetByAccountTypeAsync(type, startDate, endDate);
            if (!accounts.Any())
            {
                return NotFound("No records found for this range");
            }
            return Ok(accounts);
        }


        [HttpGet("accounts-by-customer")]
        public async Task<IActionResult> GetAccountsByCustomer([FromQuery] int customerId, 
            [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, string format="")
        {
            var accounts = await _repository.GetByCustomerAsync(customerId, startDate, endDate);
            if (!accounts.Any())
            {
                return NotFound("No records found for this range");
            }
            return Ok(accounts);
        }

    }
}