using BankCoreApi.Helpers;
using BankCoreApi.Repositories.Reports;
using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    public class TransactionsReportController : ControllerBase
    {
        private readonly TransactionsReportRepository _repository;
        public TransactionsReportController(TransactionsReportRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("all-transactions")]
        public async Task<IActionResult> GetAllTransactions([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var transactions = await _repository.GetTransactions(startDate, endDate);
            if (!transactions.Any()) 
            {
                return NotFound("No records found!");
            }
            return Ok(transactions);
        }

        [HttpGet("transactions-by-date")]
        public async Task<IActionResult> GetAllTransactionsByDate([FromQuery] DateTime date)
        {
            var transactions = await _repository.GetTransactionsByDate(date);
            if (!transactions.Any()) 
            {
                return NotFound("No records found!");
            }
            return Ok(transactions);
        }

        [HttpGet("account-transactions")]
        public async Task<IActionResult> GetAllAccountTransactions([FromQuery] int accountId, 
            [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var transactions = await _repository.GetAccountTransactions(accountId, startDate, endDate);
            if (!transactions.Any()) 
            {
                return NotFound("No records found!");
            }
            return Ok(transactions);
        }
        [HttpGet("account-transactions-by-date")]
        public async Task<IActionResult> GetAllAccountTransactionsByDate([FromQuery] int accountId, [FromQuery] DateTime date)
        {
            var transactions = await _repository.GetAccountTransactionsByDate(accountId, date);
            if (!transactions.Any()) 
            {
                return NotFound("No records found!");
            }
            return Ok(transactions);
        }
    }
}