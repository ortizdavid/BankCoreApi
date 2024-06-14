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
        public async Task<IActionResult> GeAllAccounts([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }


        [HttpGet("active-accounts")]
        public async Task<IActionResult> GetActiveAccounts([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }


        [HttpGet("accounts-by-type")]
        public async Task<IActionResult> GetAccountsByType([FromQuery] int typeId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }


        [HttpGet("accounts-by-status")]
        public async Task<IActionResult> GetAccountsByStatus([FromQuery] int statusId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }

    }
}