using BankCoreApi.Helpers;
using BankCoreApi.Models.Reports;
using BankCoreApi.Repositories.Accounts;
using BankCoreApi.Repositories.Reports;
using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsReportController : ControllerBase
    {
        private readonly TransactionsReportRepository _repository;
        private readonly AccountRepository _accountRepository;

        public TransactionsReportController(TransactionsReportRepository repository, AccountRepository accountRepository)
        {
            _repository = repository;
            _accountRepository = accountRepository;
        }


        [HttpGet("all-transactions")]
        public async Task<IActionResult> GetAllTransactions([FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, string format)
        {
            try
            {
                var transactions = await _repository.GetTransactions(startDate, endDate);
                if (!transactions.Any())
                {
                    return NotFound("No records found for this range.");
                }
                return HandleFormatResponse(transactions, format, "all_transactions");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("transactions-by-date")]
        public async Task<IActionResult> GetTransactionsByDate([FromQuery] DateTime date, string format)
        {
            try
            {
                var transactions = await _repository.GetTransactionsByDate(date);
                if (!transactions.Any())
                {
                    return NotFound("No records found for this date.");
                }
                return HandleFormatResponse(transactions, format, "transaction_by_date");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("transactions-by-type")]
        public async Task<IActionResult> GetTransactionsByType(int typeId, [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, string format)
        {
            try
            {
                var transactions = await _repository.GetTransactionsByType(typeId, startDate, endDate);
                if (!transactions.Any())
                {
                    return NotFound("No records found for this date.");
                }
                return HandleFormatResponse(transactions, format, "transaction_by_type");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("transactions-by-status")]
        public async Task<IActionResult> GetTransactionsByStatus(int statusId, [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, string format)
        {
            try
            {
                var transactions = await _repository.GetTransactionsByStatus(statusId, startDate, endDate);
                if (!transactions.Any())
                {
                    return NotFound("No records found for this date.");
                }
                return HandleFormatResponse(transactions, format, "transaction_by_status");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("account-transactions")]
        public async Task<IActionResult> GetAccountTransactions([FromQuery] int accountId, 
            [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, string format)
        {
            try
            {
                if (!await _accountRepository.ExistsAsync(accountId))
                {
                    return NotFound("Invalid account");
                }
                var transactions = await _repository.GetAccountTransactions(accountId, startDate, endDate);
                if (!transactions.Any())
                {
                    return NotFound("No records found for this account and date range.");
                }
                return HandleFormatResponse(transactions, format, "account_transactions");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("account-transactions-by-date")]
        public async Task<IActionResult> GetAllAccountTransactionsByDate([FromQuery] int accountId, 
            [FromQuery] DateTime date, string format)
        {
            try
            {
                if (!await _accountRepository.ExistsAsync(accountId))
                {
                    return NotFound("Invalid account");
                }
                var transactions = await _repository.GetAccountTransactionsByDate(accountId, date);
                if (!transactions.Any())
                {
                    return NotFound("No records found for this account and date.");
                }
                return HandleFormatResponse(transactions, format, "account_transactions_by_date");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("account-transactions-by-type")]
        public async Task<IActionResult> GetAccountTransactionsByType([FromQuery] int accountId, [FromQuery] int typeId, 
            [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, string format)
        {
            try
            {
                if (!await _accountRepository.ExistsAsync(accountId))
                {
                    return NotFound("Invalid account");
                }
                var transactions = await _repository.GetAccountTransactionsByType(accountId, typeId, startDate, endDate);
                if (!transactions.Any())
                {
                    return NotFound("No records found for this account and date range.");
                }
                return HandleFormatResponse(transactions, format, "account_transactions_by_type");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("account-transactions-by-status")]
        public async Task<IActionResult> GetAccountTransactionsByStatus([FromQuery] int accountId, [FromQuery] int statusId, 
            [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, string format)
        {
            try
            {
                if (!await _accountRepository.ExistsAsync(accountId))
                {
                    return NotFound("Invalid account");
                }
                var transactions = await _repository.GetAccountTransactionsByStatus(accountId, statusId, startDate, endDate);
                if (!transactions.Any())
                {
                    return NotFound("No records found for this account and date range.");
                }
                return HandleFormatResponse(transactions, format, "account_transactions_by_status");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("account-transactions-by-customer")]
        public async Task<IActionResult> GetAccountTransactionsByCustomer([FromQuery] int customerId, [FromQuery] int accountId, 
            [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, string format)
        {
            try
            {
                if (!await _accountRepository.ExistsAsync(accountId))
                {
                    return NotFound("Invalid account");
                }
                if (!await _accountRepository.ExistsAsync(accountId))
                {
                    return NotFound("Invalid account");
                }
                var transactions = await _repository.GetAccountTransactionsByCustomer(customerId, accountId, startDate, endDate);
                if (!transactions.Any())
                {
                    return NotFound("No records found for this account and date.");
                }
                return HandleFormatResponse(transactions, format, "account_transactions_by_customer");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        private IActionResult HandleFormatResponse(IEnumerable<TransactionReport> transactions, string format, string fileName)
        {
            if (string.IsNullOrEmpty(format) || format.ToLower() == "json")
            {
                return Ok(transactions);
            }
            else if (format.ToLower() == "excel")
            {
                var excelData = TransactionsFormat.GenerateExcel(transactions);
                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName+"_report.xlsx");
            }
            else if (format.ToLower() == "pdf")
            {
                var pdfData = TransactionsFormat.GeneratePDF(transactions);
                return File(pdfData, "application/pdf", fileName+"_report.pdf");
            }
            else if (format.ToLower() == "csv")
            {
                var csvData = TransactionsFormat.GenerateCSV(transactions);
                return File(csvData, "text/csv", fileName+"_report.csv");
            }
            else
            {
                return BadRequest("Unsupported format requested. Supported formats: json, excel, pdf, csv.");
            }
        }

    }
}