using BankCoreApi.Models.Accounts;
using BankCoreApi.Models.Reports;
using BankCoreApi.Repositories.Reports;
using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersReportController : ControllerBase
    {
        private readonly CustomersReportRepository _repository;

        public CustomersReportController(CustomersReportRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("all-customers")]
        public async Task<IActionResult> GetAllCustomers([FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, string format="")
        {
            try
            {
                var customers = await _repository.GetAllAsync(startDate, endDate);
                if (!customers.Any())
                {
                    return NotFound("No records found for this range.");
                }
                return HandleFormatResponse(customers, format, "All_Customers");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("active-customers")]
        public async Task<IActionResult> GetActiveCustomers([FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, string format="")
        {
            try
            {
                var customers = await _repository.GetCustomerByStatusAsync("Active", startDate, endDate);
                if (!customers.Any())
                {
                    return NotFound("No records found for this range.");
                }
                return HandleFormatResponse(customers, format, "Active_Customers");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("customers-by-status")]
        public async Task<IActionResult> GetCustomersByStatus([FromQuery] string status, [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, string format="")
        {
            try
            {
                var customers = await _repository.GetCustomerByStatusAsync(status, startDate, endDate);
                if (!customers.Any())
                {
                    return NotFound("No records found for this range.");
                }
                return HandleFormatResponse(customers, format, "Customers_By_Status");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("customers-by-type")]
        public async Task<IActionResult> GetCustomersByType([FromQuery] string type, [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, string format="")
        {
            try
            {
                var customers = await _repository.GetCustomerByTypeAsync(type, startDate, endDate);
                if (!customers.Any())
                {
                    return NotFound("No records found for this range.");
                }
                return HandleFormatResponse(customers, format, "Customers_By_Type");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("customers-by-gender")]
        public async Task<IActionResult> GetCustomersByGender([FromQuery] string gender, [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, string format="")
        {
            try
            {
                var customers = await _repository.GetCustomerByGenderAsync(gender, startDate, endDate);
                if (!customers.Any())
                {
                    return NotFound("No records found for this range.");
                }
                return HandleFormatResponse(customers, format, "Customers_By_Gender");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private IActionResult HandleFormatResponse(IEnumerable<CustomerReport> customers, string format, string fileName)
        {
            if (string.IsNullOrEmpty(format) || format.ToLower() == "json")
            {
                return Ok(customers);
            }
            else if (format.ToLower() == "excel")
            {
                var excelData = CustomersFormat.GenerateExcel(customers);
                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName+"_Report.xlsx");
            }
            else if (format.ToLower() == "pdf")
            {
                var pdfData = CustomersFormat.GeneratePDF(customers);
                return File(pdfData, "application/pdf", fileName+"_Report.pdf");
            }
            else if (format.ToLower() == "csv")
            {
                var csvData = CustomersFormat.GenerateCSV(customers);
                return File(csvData, "text/csv", fileName+"_Report.csv");
            }
            else
            {
                return BadRequest("Unsupported format requested. Supported formats: json, excel, pdf, csv.");
            }
        }

    }
}