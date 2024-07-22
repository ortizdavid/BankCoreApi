using BankCoreApi.Exceptions;
using BankCoreApi.Helpers;
using BankCoreApi.Models.Customers;
using BankCoreApi.Repositories.Core;
using BankCoreApi.Services.Core;
using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CustomersController> _logger;
        private readonly CustomerService _service;

        public CustomersController(IConfiguration configuration, 
            ILogger<CustomersController> logger,
            CustomerService service)
        {
            _configuration = configuration;
            _logger = logger;
            _service = service;
        }


        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            try
            {
                await _service.CreateCustomer(request);   
                var message = $"Customer '{request.CustomerName}' Created.";
                _logger.LogInformation(message);
                return StatusCode(201, message);
            }
            catch (ApiException ex) 
            {
                _logger.LogError(ex.Message);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers([FromQuery] PaginationParam param)
        {
            try
            {
                var customers = await _service.GetAllCustomers(param);
                return Ok(customers);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var customer = await _service.GetCustomerById(id);
                return Ok(customer);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("by-uuid/{uniqueId}")]
        public async Task<IActionResult> GetCustomerByUniqueId(Guid uniqueId)
        {
            try
            {
                var customer = await _service.GetCustomerByUniqueId(uniqueId);
                return Ok(customer);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("change-status")]
        public async Task<IActionResult> ChangeCustomerStatus([FromBody] ChangeCustomerStatusRequest request)
        {
            try
            {
                await _service.ChangeCustomerStatus(request);
                var message = $"Customer Status changed  to '{nameof(request.NewStatus)}'";
                _logger.LogInformation(message);
                return Ok(message);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("change-type")]
        public async Task<IActionResult> ChangeCustomerType([FromBody] ChangeCustomerTypeRequest request)
        {
            try
            {
                await _service.ChangeCustomerType(request);
                var message = $"Customer Type changed  to '{nameof(request.NewType)}'";
                _logger.LogInformation(message);
                return Ok(message);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

    }
}