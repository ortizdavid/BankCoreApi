using System.Net;
using BankCoreApi.Models.Reports;
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
        public async Task<IActionResult> GeAllAccounts([FromQuery] DateRangeFilter dateRange, string format="")
        {
            try
            {
                var accounts = await _repository.GetAllAsync(dateRange.StartDate, dateRange.EndDate);
                if (!accounts.Any())
                {
                    return NotFound("No records found for this range.");
                }
                return HandleFormatResponse(accounts, format, "All_Accounts");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("active-accounts")]
        public async Task<IActionResult> GetActiveAccounts([FromQuery] DateRangeFilter dateRange, string format="")
        {
            try
            {
                var accounts = await _repository.GetByAccountStatusAsync("Active", dateRange.StartDate, dateRange.EndDate);
                if (!accounts.Any())
                {
                    return NotFound("No records found for this range.");
                }
                return HandleFormatResponse(accounts, format, "Active_Accounts");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        
        [HttpGet("accounts-by-status")]
        public async Task<IActionResult> GetAccountsByStatus([FromQuery] string status, 
            [FromQuery] DateRangeFilter dateRange, string format="")
        {
            try
            {
                var accounts = await _repository.GetByAccountStatusAsync(status, dateRange.StartDate, dateRange.EndDate);
                if (!accounts.Any())
                {
                    return NotFound("No records found for this range.");
                }
                return HandleFormatResponse(accounts, format, "Accounts_By_Status");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("accounts-by-type")]
        public async Task<IActionResult> GetAccountsByType([FromQuery] string type, 
            [FromQuery] DateRangeFilter dateRange, string format="")
        {
            try
            { 
                var accounts = await _repository.GetByAccountTypeAsync(type, dateRange.StartDate, dateRange.EndDate);
                if (!accounts.Any())
                {
                    return NotFound("No records found for this range.");
                }
                return HandleFormatResponse(accounts, format, "Accounts_By_Type");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("accounts-by-customer")]
        public async Task<IActionResult> GetAccountsByCustomer([FromQuery] int customerId, 
            [FromQuery] DateRangeFilter dateRange, string format="")
        {
            try
            {
                var accounts = await _repository.GetByCustomerAsync(customerId, dateRange.StartDate, dateRange.EndDate);
                if (!accounts.Any())
                {
                    return NotFound("No records found for this range.");
                }
                return HandleFormatResponse(accounts, format, "Accounts_By_Customer");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


        private IActionResult HandleFormatResponse(IEnumerable<AccountReport> accounts, string format, string fileName)
        {
            if (string.IsNullOrEmpty(format) || format.ToLower() == "json")
            {
                return Ok(accounts);
            }
            else if (format.ToLower() == "excel")
            {
                var excelData = AccountsFormat.GenerateExcel(accounts);
                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName+"_Report.xlsx");
            }
            else if (format.ToLower() == "pdf")
            {
                var pdfData = AccountsFormat.GeneratePDF(accounts);
                return File(pdfData, "application/pdf", fileName+"_report.pdf");
            }
            else if (format.ToLower() == "csv")
            {
                var csvData = AccountsFormat.GenerateCSV(accounts);
                return File(csvData, "text/csv", fileName+"_Report.csv");
            }
            else
            {
                return BadRequest("Unsupported format requested. Supported formats: json, excel, pdf, csv.");
            }
        }

    }
}